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
    public class HotelRoomRepository : IHotelRoom
    {
        private AsyncInnDbContext _context;
        private IRoom _room;

        public HotelRoomRepository(AsyncInnDbContext context, IRoom room)
        {
            _context = context;
            _room = room;
        }


        /// <summary>
        /// Creates a hotel room
        /// </summary>
        /// <param name="hotelRoom"> hotelroom object</param>
        /// <param name="hotelId">hotel identifier</param>
        /// <returns> Task completion </returns>
        public async Task<HotelRoomDTO> Create(HotelRoomDTO hotelRoom, int hotelId)
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
        public async Task<List<HotelRoomDTO>> GetHotelRooms(int hotelId)
        {
            List<HotelRoom> hotelRoom = await _context.HotelRooms.Where(x => x.HotelId == hotelId)
                                                                 .Include(x => x.room)
                                                                   .ToListAsync();
            var  dto = new List<HotelRoomDTO>();
            foreach (var item in hotelRoom)
            {
                dto.Add(await GetHotelRoom(item.HotelId, item.RoomNumber));
            }
            return dto;
        }


        /// <summary>
        /// Gets an individual  room
        /// </summary>
        /// <param name="roomNumber">identifies the number of the room </param>
        /// <param name="hotelId">hotel identifier</param>
        /// <returns>Hotel room with room and amenities</returns>
        public async Task<HotelRoomDTO> GetHotelRoom(int hotelId, int roomNumber)
        {
            HotelRoom hotelRoom = await _context.HotelRooms.Where(x => x.HotelId == hotelId && x.RoomNumber == roomNumber)
                                                     .Include(x => x.hotel)
                                                     .Include(x => x.room)
                                                     .ThenInclude(x => x.RoomAmenities )
                                                     .ThenInclude(x => x.amenity)
                                                     .FirstOrDefaultAsync();

            RoomDTO roomdto = await _room.GetRoom(hotelRoom.RoomId);
            HotelRoomDTO dto = new HotelRoomDTO()
            {
                HotelId = hotelRoom.HotelId,
                RoomNumber = hotelRoom.RoomNumber,
                Rate = hotelRoom.Rate,
                PetFriendly = hotelRoom.PetFriendly,
                RoomId = hotelRoom.RoomId,
                Room = roomdto
               
            };
       
            return dto;
        }

        /// <summary>
        /// Updates the hotel room
        /// </summary>
        /// <param name="hotelRoom"> the hotel room object </param>
        /// <returns> task completion </returns>
        public async Task Update(HotelRoomDTO hotelRoom)
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
            HotelRoom hotelRoom = await _context.HotelRooms.FindAsync(hotelId, roomNumber);
            _context.Entry(hotelRoom).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
