using async_inn.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace async_inn.Models.Interfaces
{
    public interface IAmenity
    {
        
        /// <summary>
        /// Creates an amenity
        /// </summary>
        /// <param name="amenity">amenity object </param>
        /// <returns>task completion</returns>
        Task<Amenity> Create(Amenity amenity);

        /// <summary>
        /// Gets all the amenities
        /// </summary>
        /// <returns> list of amenities </returns>
        Task<List<Amenity>> GetAmenities();

        /// <summary>
        /// Gets a single amenity by id
        /// </summary>
        /// <param name="id">amenity identifier</param>
        /// <returns>task completion</returns>
        Task<Amenity> GetAmenity(int id);


        /// <summary>
        /// updates amenity
        /// </summary>
        /// <param name="amenity"> amenity object</param>
        /// <returns>task completion </returns>
        Task<Amenity> Update(Amenity amenity);

       
        /// <summary>
        /// deletes selected amenity 
        /// </summary>
        /// <param name="id">amenity identifier</param>
        /// <returns>task completion </returns>
        Task Delete(int id);

    }
}
