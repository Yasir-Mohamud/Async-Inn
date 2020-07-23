using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace async_inn.Models.Interfaces
{
    interface IAmenity
    {
        // create
        Task<Amenity> Create(Amenity amenity);

        //Get all
        Task<List<Amenity>> GetAmenities();

        //Gets indiviually by id
        Task<Amenity> GetAmenity(int id);

        //Updates
        Task<Amenity> Update(Amenity amenity);

        //DELETES
        Task Delete(int id);

    }
}
