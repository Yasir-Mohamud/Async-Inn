using async_inn.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace async_inn.Models.Interfaces
{
    public interface IHotel
    {

        /// <summary>
        /// Creates hotel 
        /// </summary>
        /// <param name="hotel"> hotel object </param>
        /// <returns> task completion </returns>
        Task<HotelDTO> Create(HotelDTO hoteldto);

        /// <summary>
        /// gets all the hotels
        /// </summary>
        /// <returns>task completion</returns>
        Task<List<HotelDTO>> GetHotels();

        /// <summary>
        /// gets a single hotel
        /// </summary>
        /// <param name="id">hotel identifier</param>
        /// <returns>task completion </returns>
        Task<HotelDTO> GetHotel(int id);

        /// <summary>
        /// updates the hotel
        /// </summary>
        /// <param name="hotel">hotel identifier</param>
        /// <returns>task completion</returns>
        Task<HotelDTO> Update(HotelDTO hoteldto);

        /// <summary>
        /// Deletes an hotel
        /// </summary>
        /// <param name="id">hotel identifier</param>
        /// <returns>task completion</returns>
        Task Delete(int id);
    }
}
