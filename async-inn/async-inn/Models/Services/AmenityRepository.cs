using async_inn.Data;
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

        public AmenityRepository(AsyncInnDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates an amenity
        /// </summary>
        /// <param name="amenity">amenity object </param>
        /// <returns>task completion</returns>
        public async Task<Amenity> Create(Amenity amenity)
        {
            _context.Entry(amenity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
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
            Amenity amenity = await GetAmenity(id);
            // Deletes it in the DB
            _context.Entry(amenity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            // Saves Changes
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets all the amenities
        /// </summary>
        /// <returns> list of amenities </returns>
        public async Task<List<Amenity>> GetAmenities()
        {
            var amenity = await _context.Amenities.ToListAsync();
            return amenity;
        }

        /// <summary>
        /// Gets a single amenity by id
        /// </summary>
        /// <param name="id">amenity identifier</param>
        /// <returns>task completion</returns>
        public async Task<Amenity> GetAmenity(int id)
        {
            Amenity amenity = await _context.Amenities.FindAsync(id);
            var roomAmenities = await _context.RoomAmenities.Where(x => x.AmenityId == id)
                                                            .Include(x => x.room)
                                                            .ToListAsync();
            amenity.RoomAmenities = roomAmenities;
            return amenity;
        }

        /// <summary>
        /// updates amenity
        /// </summary>
        /// <param name="amenity"> amenity object</param>
        /// <returns>task completion </returns>
        public async Task<Amenity> Update(Amenity amenity)
        {
            _context.Entry(amenity).State = EntityState.Modified;
            // Save changes
            await _context.SaveChangesAsync();
            return amenity;
        }
    }
}
