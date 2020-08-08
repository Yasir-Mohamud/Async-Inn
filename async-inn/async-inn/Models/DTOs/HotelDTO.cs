using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace async_inn.Models.DTOs
{
    public class HotelDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Phone { get; set; }

        // Navigation Property
        public List<HotelRoomDTO> HotelRooms { get; set; }
    }
}
