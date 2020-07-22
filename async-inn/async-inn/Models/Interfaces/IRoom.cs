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


    }
}
