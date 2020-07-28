using async_inn.Data;
using async_inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace async_inn.Models.Services
{
    public class HotelRoomRepository : IHotelRoom
    {
        private AsyncInnDbContext _context;
        public HotelRoomRepository(AsyncInnDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Creates a hotel room
        /// </summary>
        /// <param name="hotelRoom"> hotelroom object</param>
        /// <param name="hotelId">hotel identifier</param>
        /// <returns> Task completion </returns>
        public async Task<HotelRoom> Create(HotelRoom hotelRoom, int hotelId)
        {
            hotelRoom.HotelId = hotelId;
            // adds to the database
            _context.Entry(hotelRoom).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            // saves it on the db and the associated with an id
            await _context.SaveChangesAsync();
            return hotelRoom;
        }

        /// <summary>
        /// gets all hotel rooms
        /// </summary>
        /// <param name="hotelId"> hotel identifier</param>
        /// <returns> list of hotel rooms</returns>
        public async Task<List<HotelRoom>> GetHotelRooms(int hotelId)
        {
            List<HotelRoom> hotelRoom = await _context.HotelRooms.Where(x => x.HotelId == hotelId)
                                                                 .Include(x => x.room)
                                                                 .ToListAsync();
            return hotelRoom;
        }


        /// <summary>
        /// Gets an individual  room
        /// </summary>
        /// <param name="roomNumber">identifies the number of the room </param>
        /// <param name="hotelId">hotel identifier</param>
        /// <returns>Hotel room with room and amenities</returns>
        public async Task<HotelRoom> GetHotelRoom(int hotelId, int roomNumber)
        {
            var hotelRoom = await _context.HotelRooms.Where(x => x.HotelId == hotelId && x.RoomNumber == roomNumber)
                                                     .Include(x => x.hotel)
                                                     .Include(x => x.room)
                                                     .ThenInclude(x => x.RoomAmenities )
                                                     .ThenInclude(x => x.amenity)
                                                     .FirstOrDefaultAsync();

            return hotelRoom;
        }

        /// <summary>
        /// Updates the hotel room
        /// </summary>
        /// <param name="hotelRoom"> the hotel room object </param>
        /// <returns> task completion </returns>
        public async Task Update(HotelRoom hotelRoom)
        {
            _context.Entry(hotelRoom).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes selected room
        /// </summary>
        /// <param name="hotelId"> hotel identifier</param>
        /// <param name="roomNumber">Number of the room</param>
        /// <returns> task completion </returns>
        public async Task Delete(int hotelId, int roomNumber)
        {
            HotelRoom hotelRoom = await GetHotelRoom(hotelId, roomNumber);
            _context.Entry(hotelRoom).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
