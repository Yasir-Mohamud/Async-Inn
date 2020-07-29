using async_inn.Data;
using async_inn.Models.DTOs;
using async_inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace async_inn.Models.Services
{
    public class AmenityRepository : IAmenity
    {
        private AsyncInnDbContext _context;
        
        // bringin in db
        public AmenityRepository(AsyncInnDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates an amenity
        /// </summary>
        /// <param name="amenity">amenity object </param>
        /// <returns>task completion</returns>
        public async Task<AmenityDTO> Create(AmenityDTO amenity)
        {
            // convert amenityDTO to an entity

            Amenity entity = new Amenity()
            {
                Name = amenity.Name
            };

            _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            // Saves Changes
            await _context.SaveChangesAsync();
            return amenity;
        }

        /// <summary>
        /// deletes selected amenity 
        /// </summary>
        /// <param name="id">amenity identifier</param>
        /// <returns>task completion </returns>
        public async Task Delete(int id)
        {

            var amenity = await _context.Amenities.FindAsync(id);
            _context.Entry(amenity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

        }

        /// <summary>
        /// Gets all the amenities
        /// </summary>
        /// <returns> list of amenities </returns>
        public async Task<List<AmenityDTO>> GetAmenities()
        {
            var list = await _context.Amenities.ToListAsync();
            var amenities = new List<AmenityDTO>();
            foreach (var item in list)
            {
                amenities.Add(await GetAmenity(item.Id));
            }
            return amenities;
        }

        /// <summary>
        /// Gets a single amenity by id
        /// </summary>
        /// <param name="id">amenity identifier</param>
        /// <returns>task completion</returns>
        public async Task<AmenityDTO> GetAmenity(int id)
        {
            Amenity amenity = await _context.Amenities.FindAsync(id);
            /*      var roomAmenities = await _context.RoomAmenities.Where(x => x.AmenityId == id)
                                                                  .Include(x => x.room)
                                                                  .ToListAsync();*/
            AmenityDTO dto = new AmenityDTO()
            {
                Id = amenity.Id,
                Name = amenity.Name,

            };

            return dto;
        }

        /// <summary>
        /// updates amenity
        /// </summary>
        /// <param name="amenity"> amenity object</param>
        /// <returns>task completion </returns>
        public async Task<AmenityDTO> Update(AmenityDTO amenity)
        {
            // change amenityDTO to entity
            Amenity entity = new Amenity()
            {
                Name = amenity.Name
            };
            _context.Entry(entity).State = EntityState.Modified;
            // Save changes
            await _context.SaveChangesAsync();
            return amenity;
        }
    }
}
