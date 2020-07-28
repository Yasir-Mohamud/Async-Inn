using async_inn.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace async_inn.Models
{
    public class HotelRoom 
    {
        public int HotelId { get; set; }
        public int RoomId { get; set; }
        public int RoomNumber { get; set; }
        public decimal Rate { get; set; }
        public bool PetFriendly { get; set; }

        /// navigation prop
        
        public Hotel hotel { get; set; }
        public Room room { get; set; }
       
    }
}
