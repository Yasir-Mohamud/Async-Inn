using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace async_inn.Models
{
    public class Amenity
    {
        public int Id {get; set; }
        public string Name { get; set; }
        

        /// Navigation properties
        public Room room { get; set; }
        public List<RoomAmenity> RoomAmenities { get; set; }
    }
}
