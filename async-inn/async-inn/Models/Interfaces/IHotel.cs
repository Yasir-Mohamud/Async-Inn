using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace async_inn.Models.Interfaces
{
    public interface IHotel
    {
        // create
        Task<Hotel> Create(Hotel hotel);

        //Get all
       Task<List<Hotel>> GetHotels();

        //Gets indiviually by id
       Task<Hotel> GetHotel(int id);

        //Updates
        Task<Hotel> Update(Hotel hotel);

        //DELETES
        Task Delete(int id);
    }
}
