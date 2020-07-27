using async_inn.Data;
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
        public RoomRepository(AsyncInnDbContext context)
        {
            _context = context;
        }
        public async Task<Room> Create(Room room)
        {
            // adds to the database
            _context.Entry(room).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            // saves it on the db and the associated with an id
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task Delete(int id)
        {
            Room room = await GetRoom(id);
            _context.Entry(room).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            await _context.SaveChangesAsync();

        }

        public async Task<Room> GetRoom(int id)
        {
            Room room = await _context.Rooms.FindAsync(id);
            // includes all of the amenities in that room.
            var roomAmenities = await _context.RoomAmenities.Where(x => x.RoomId == id)
                                                      .Include(x => x.amenity)
                                                     .ToListAsync();
            room.RoomAmenities = roomAmenities;
            return room;

        }

        public async Task<List<Room>> GetRooms()
        {
            var room = await _context.Rooms.ToListAsync();
      
            return room;
        }



        public async Task<Room> Update(Room room)
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
        public async Task AddAmenity(int roomId, int amenityId)
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
        public async Task RemoveAmenity(int roomId, int amenityId)
        {
            var result =  await _context.RoomAmenities.FirstOrDefaultAsync(x => x.RoomId == roomId && x.AmenityId == amenityId );
            _context.Entry(result).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

    }
}
