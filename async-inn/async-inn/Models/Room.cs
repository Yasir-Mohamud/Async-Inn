using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace async_inn.Models
{
    public class Room
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public Layout Layout { get; set; }

        // Navigation prop
        public List<RoomAmenity> RoomAmenities { get; set; }
        public List<HotelRoom> HotelRooms { get; set; }

    }
    
    public enum Layout
    {
        studio = 1,
        oneBedroom,
        twoBedroom
    }
        
}
