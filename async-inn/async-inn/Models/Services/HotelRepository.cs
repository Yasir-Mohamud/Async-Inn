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
    public class HotelRepository : IHotel
    {
        private AsyncInnDbContext _context;
        private IHotelRoom _hotelroom;
      
        public HotelRepository(AsyncInnDbContext context, IHotelRoom hotelroom)
        {
            _context = context;
            _hotelroom = hotelroom;
        }

        /// <summary>
        /// Creates hotel 
        /// </summary>
        /// <param name="hotel"> hotel object </param>
        /// <returns> task completion </returns>
        public async Task<HotelDTO> Create(HotelDTO hoteldto)
        {
            // convert dto to entity
            Hotel hotel = new Hotel()
            {
                Name = hoteldto.Name,
                StreetAddress = hoteldto.StreetAddress,
                City = hoteldto.City,
                State = hoteldto.State,
                Phone = hoteldto.Phone
            };
            // adds to the database
            _context.Entry(hotel).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            // this saves it and then associates it with an id
            await _context.SaveChangesAsync();

            return hoteldto;
        }


        /// <summary>
        /// Deletes an hotel
        /// </summary>
        /// <param name="id">hotel identifier</param>
        /// <returns>task completion</returns>
        public async Task Delete(int id)
        {
            Hotel hotel = await _context.Hotels.FindAsync(id);
            _context.Entry(hotel).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            await _context.SaveChangesAsync();
        }


        /// <summary>
        /// gets a single hotel
        /// </summary>
        /// <param name="id">hotel identifier</param>
        /// <returns>task completion </returns>
        public async Task<HotelDTO> GetHotel(int id)
        {
            // looks into the db and finds the object with the same id 
            Hotel hotel = await _context.Hotels.Where(x => x.Id == id)
                                                .Include(x => x.HotelRooms)
                                                 .ThenInclude(x => x.room)
                                              .ThenInclude(x => x.RoomAmenities)
                                              .ThenInclude(x => x.amenity)
                                                .FirstOrDefaultAsync();

           List<HotelRoomDTO> hotelroom = await _hotelroom.GetHotelRooms(id);

            HotelDTO hoteldto = new HotelDTO()
            {
                Name = hotel.Name,
                StreetAddress = hotel.StreetAddress,
                City = hotel.City,
                State = hotel.State,
                Phone = hotel.Phone,
                HotelRooms = hotelroom
            };

        
            return hoteldto;
        }

        /// <summary>
        /// gets all the hotels
        /// </summary>
        /// <returns>task completion</returns>
        public async Task<List<HotelDTO>> GetHotels()
        {
            var hotels = await _context.Hotels.ToListAsync();

            List<HotelDTO> hoteldto = new List<HotelDTO>();

            foreach (var hotel in hotels)
            {
               

                hoteldto.Add(new HotelDTO()
                {
                    Id = hotel.Id,
                    Name = hotel.Name,
                    StreetAddress = hotel.StreetAddress,
                    City = hotel.City,
                    State = hotel.State,
                    Phone = hotel.Phone
                   
                });
            }

            return hoteldto;

        }

        /// <summary>
        /// updates the hotel
        /// </summary>
        /// <param name="hotel">hotel identifier</param>
        /// <returns>task completion</returns>
        public async Task<HotelDTO> Update(HotelDTO hoteldto)
        {
            Hotel hotel = new Hotel()
            {
                Id = hoteldto.Id,
                Name = hoteldto.Name,
                StreetAddress = hoteldto.StreetAddress,
                City = hoteldto.City,
                State = hoteldto.State,
                Phone = hoteldto.Phone
            };
            _context.Entry(hotel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hoteldto;

        }
    }
}
