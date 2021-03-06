﻿using async_inn.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace async_inn.Models.Interfaces
{
   public  interface IHotelRoom
    {
        /// <summary>
        /// Creates a hotel room
        /// </summary>
        /// <param name="hotelRoom"> hotel room object</param>
        /// <param name="hotelId">hotel identifier</param>
        /// <returns> Task completion </returns>
        Task<HotelRoomDTO> Create(HotelRoomDTO hotelRoom, int hotelId);

        /// <summary>
        /// gets all hotel rooms
        /// </summary>
        /// <param name="hotelId"> hotel identifier</param>
        /// <returns> list of hotel rooms</returns>
        Task<List<HotelRoomDTO>> GetHotelRooms(int hotelId);

        /// <summary>
        /// Gets an individual  room
        /// </summary>
        /// <param name="roomNumber">identifies the number of the room </param>
        /// <param name="hotelId">hotel identifier</param>
        /// <returns>Hotel room with room and amenities</returns>
        Task<HotelRoomDTO> GetHotelRoom(int hotelId , int roomNumber);

        /// <summary>
        /// Updates the hotel room
        /// </summary>
        /// <param name="hotelRoom"> the hotel room object </param>
        /// <returns> task completion </returns>
        Task<HotelRoomDTO> Update(HotelRoomDTO hotelRoom);

        /// <summary>
        /// Deletes selected room
        /// </summary>
        /// <param name="hotelId"> hotel identifier</param>
        /// <param name="roomNumber">Number of the room</param>
        /// <returns> task completion </returns>
        Task Delete(int HotelId , int RoomNumber);
    }
}
