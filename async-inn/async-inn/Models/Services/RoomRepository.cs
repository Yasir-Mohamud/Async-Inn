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
    public class RoomRepository : IRoom
    {
        private AsyncInnDbContext _context;
        private IAmenity _amenity;

       
        
        public RoomRepository(AsyncInnDbContext context, IAmenity amenity)
        {
            _context = context;
            _amenity = amenity;
        }

        /// <summary>
        /// Creates a room
        /// </summary>
        /// <param name="room"> room object</param>
        /// <returns> task completion </returns>
        public async Task<RoomDTO> Create(RoomDTO room)
        {
            // adds to the database
            _context.Entry(room).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            // saves it on the db and the associated with an id
            await _context.SaveChangesAsync();
            return room;
        }


        /// <summary>
        /// Deletes selected room
        /// </summary>
        /// <param name="id">room identifier</param>
        /// <returns> task completion </returns>
        public async Task Delete(int id)
        {
            Room room = await _context.Rooms.FindAsync(id);
            _context.Entry(room).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            await _context.SaveChangesAsync();
        }


        /// <summary>
        /// Gets a single room
        /// </summary>
        /// <param name="id"> room identifier</param>
        /// <returns> The room with all its amenities</returns>
        public async Task<RoomDTO> GetRoom(int id)
        {
            Room room = await _context.Rooms.FindAsync(id);
            // includes all of the amenities in that room.
            var roomAmenities = await _context.RoomAmenities.Where(x => x.RoomId == id)
                                                      .Include(x => x.amenity)
                                                     .ToListAsync();
            RoomDTO dto = new RoomDTO()
            {
                Id = room.Id,
                Name = room.Name,
                Layout = room.Layout.ToString()
                
             };
           
            dto.Amenities = new List<AmenityDTO>();
            /*  foreach (var item in roomAmenities)
              {
                  AmenityDTO amenitydto = new AmenityDTO()
                  {
                      Id = item.amenity.Id,
                      Name = item.amenity.Name
                  };
                  dto.Amenities.Add(amenitydto);
              }*/
            foreach (var item in roomAmenities)
            {
                dto.Amenities.Add(await _amenity.GetAmenity(item.amenity.Id));

            }

            return dto;

        }


        /// <summary>
        /// Gets all the rooms
        /// </summary>
        /// <returns> List of all rooms</returns>
        public async Task<List<RoomDTO>> GetRooms()
        {
            var rooms = await _context.Rooms.ToListAsync();
            List<RoomDTO> dtos = new List<RoomDTO>();
            foreach (var item in rooms)
            {
                dtos.Add(await GetRoom(item.Id));
            }

            return dtos;
        }


        /// <summary>
        /// Updates room
        /// </summary>
        /// <param name="room"> room object</param>
        /// <returns>the updated room object</returns>
        public async Task<RoomDTO> Update(RoomDTO room)
        {
            _context.Entry(room).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return room;
        }

        /// <summary>
        /// Adds an amenity to a room
        /// </summary>
        /// <param name="roomId"> room foreign key</param>
        /// <param name="amenityId">amenity foreign key</param>
        /// <returns> saves changes to db </returns>
        public async Task AddAmenityToRoom(int roomId, int amenityId)
        {
            RoomAmenity roomAmenity = new RoomAmenity()
            {
                RoomId = roomId,
                AmenityId = amenityId,
            };
            _context.Entry(roomAmenity).State = EntityState.Added;
                await _context.SaveChangesAsync();
        }


        /// <summary>
        /// Removes an amenity to a room
        /// </summary>
        /// <param name="roomId"> identifier for room</param>
        /// <param name="amenityId"> identifier for amenity</param>
        /// <returns> Task of completion  </returns>
        public async Task RemoveAmenityFromRoom(int roomId, int amenityId)
        {
            var result =  await _context.RoomAmenities.FirstOrDefaultAsync(x => x.RoomId == roomId && x.AmenityId == amenityId );
            _context.Entry(result).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

    }
}
