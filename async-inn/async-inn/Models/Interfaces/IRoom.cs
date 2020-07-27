using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace async_inn.Models.Interfaces
{
    public interface IRoom
    {
        // create
        Task<Room> Create(Room room);

        //Get all
        Task<List<Room>> GetRooms();

        //Gets indiviually by id
        Task<Room> GetRoom(int id);

        //Updates
        Task<Room> Update(Room room);

        //DELETES
        Task Delete(int id);

        /// <summary>
        /// Adds an amenity to a room
        /// </summary>
        /// <param name="roomId"> identifier for room</param>
        /// <param name="amenityId"> identifier for amenity</param>
        /// <returns> Task of completion  </returns>
        Task AddAmenity(int roomId, int amenityId);


        /// <summary>
        /// Removes an amenity to a room
        /// </summary>
        /// <param name="roomId"> identifier for room</param>
        /// <param name="amenityId"> identifier for amenity</param>
        /// <returns> Task of completion  </returns>
        Task RemoveAmenity(int roomId, int amenityId);
    }
}
