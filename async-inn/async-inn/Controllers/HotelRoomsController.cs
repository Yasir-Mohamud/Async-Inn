using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using async_inn.Data;
using async_inn.Models;
using async_inn.Models.Interfaces;
using async_inn.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace async_inn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelRoomsController : ControllerBase
    {
        private readonly IHotelRoom _hotelRoom;

        public HotelRoomsController(IHotelRoom hotelRoom)
        {
            _hotelRoom = hotelRoom;
        }

        // GET: api/Hotel/id/rooms
        [HttpGet, Route("/api/Hotels/{hotelId}/Rooms")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<HotelRoomDTO>>> GetHotelRooms(int hotelId)
        {
            return await _hotelRoom.GetHotelRooms(hotelId);
        }

        // GET: api/HotelRooms/5
        [HttpGet, Route("/api/Hotels/{hotelId}/Rooms/{roomNumber}")]
        [AllowAnonymous]
        public async Task<ActionResult<HotelRoomDTO>> GetHotelRoom( int hotelId , int roomNumber)
        {
            var hotelRoomdto = await _hotelRoom.GetHotelRoom(hotelId, roomNumber);

            if (hotelRoomdto == null)
            {
                return NotFound();
            }

            return hotelRoomdto;

        }

        // PUT: api/HotelRooms/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("/api/Hotels/{hotelId}/Rooms/{roomNumber}")]
        public async Task<IActionResult> PutHotelRoom(int hotelId, int roomNumber, HotelRoomDTO hotelRoomdto)
        {
            if(hotelId != hotelRoomdto.HotelId || roomNumber != hotelRoomdto.RoomNumber)
            {
                return BadRequest();
            }
            //update
            await _hotelRoom.Update(hotelRoomdto);
            return NoContent();
        }

        // POST: api/HotelRooms
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost, Route ("/api/Hotels/{hotelId}/Rooms")]
        public async Task<ActionResult<HotelRoomDTO>> PostHotelRoom(HotelRoomDTO hotelRoom, int hotelId)
        {
            await _hotelRoom.Create(hotelRoom, hotelId);

            return CreatedAtAction("GetHotelRoom", new { id = hotelRoom.HotelId }, hotelRoom);
        }

        // DELETE: api/HotelRooms/5
        [HttpDelete("/api/Hotels/{hotelId}/Rooms/{roomNumber}")]
        public async Task<ActionResult<HotelRoom>> DeleteHotelRoom(int hotelId, int roomNumber)
        {
            await _hotelRoom.Delete(hotelId, roomNumber);

            return NoContent();
        }

  
    }
}
