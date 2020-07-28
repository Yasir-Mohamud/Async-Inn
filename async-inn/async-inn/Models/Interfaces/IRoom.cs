using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace async_inn.Models.Interfaces
{
    public interface IRoom
    {
        /// <summary>
        /// Creates a room
        /// </summary>
        /// <param name="room"> room object</param>
        /// <returns> task completion </returns>
        Task<Room> Create(Room room);

        /// <summary>
        /// Gets all the rooms
        /// </summary>
        /// <returns> List of all rooms</returns>
        Task<List<Room>> GetRooms();

        /// <summary>
        /// Gets a single room
        /// </summary>
        /// <param name="id"> room identifier</param>
        /// <returns> The room with all its amenities</returns>
        Task<Room> GetRoom(int id);

        /// <summary>
        /// Updates room
        /// </summary>
        /// <param name="room"> room object</param>
        /// <returns>the updated room object</returns>
        Task<Room> Update(Room room);

        /// <summary>
        /// Deletes selected room
        /// </summary>
        /// <param name="id">room identifier</param>
        /// <returns> task completion </returns>
        Task Delete(int id);

        /// <summary>
        /// Adds an amenity to a room
        /// </summary>
        /// <param name="roomId"> identifier for room</param>
        /// <param name="amenityId"> identifier for amenity</param>
        /// <returns> Task of completion  </returns>
        Task AddAmenityToRoom(int roomId, int amenityId);


        /// <summary>
        /// Removes an amenity to a room
        /// </summary>
        /// <param name="roomId"> identifier for room</param>
        /// <param name="amenityId"> identifier for amenity</param>
        /// <returns> Task of completion  </returns>
        Task RemoveAmenityFromRoom(int roomId, int amenityId);
    }
}
