using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace async_inn.Models
{
    public class RoomAmenity
    {
        // composite key
        public int RoomId { get; set; }
        public int AmenityId { get; set; }

        // Navigation props
        public Room room { get; set; }
        public Amenity amenity { get; set; }
    }
}
