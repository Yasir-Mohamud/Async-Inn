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
        Task<Hotel> Create(Hotel hotel);

        /// <summary>
        /// gets all the hotels
        /// </summary>
        /// <returns>task completion</returns>
        Task<List<Hotel>> GetHotels();

        /// <summary>
        /// gets a single hotel
        /// </summary>
        /// <param name="id">hotel identifier</param>
        /// <returns>task completion </returns>
        Task<Hotel> GetHotel(int id);

        /// <summary>
        /// updates the hotel
        /// </summary>
        /// <param name="hotel">hotel identifier</param>
        /// <returns>task completion</returns>
        Task<Hotel> Update(Hotel hotel);

        /// <summary>
        /// Deletes an hotel
        /// </summary>
        /// <param name="id">hotel identifier</param>
        /// <returns>task completion</returns>
        Task Delete(int id);
    }
}
