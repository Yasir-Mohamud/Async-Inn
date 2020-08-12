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

    // our constructor is bringing in a reference to our db
    public class RoomsController : ControllerBase
    {
        private readonly IRoom _room;

        public RoomsController(IRoom room)
        {
            _room = room;
        }

        // GET: api/Rooms
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<RoomDTO>>> GetRooms()
        {
            return await _room.GetRooms();
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<RoomDTO>> GetRoom(int id)
        {
            RoomDTO roomdto = await _room.GetRoom(id);
            return roomdto;
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, RoomDTO roomdto)
        {
            if (id != roomdto.Id)
            {
                return BadRequest();
            }
           var updatedRoom = await _room.Update(roomdto);

            return Ok(updatedRoom);
        }

        // POST: api/Rooms
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Room>> PostRoom(RoomDTO roomdto)
        {
            await _room.Create(roomdto);
            return CreatedAtAction("GetRoom", new { id = roomdto.Id }, roomdto);
        }

        //Post 
        [HttpPost]
        [Route("{roomId}/{amenityId}")]
        // Mode Binding
        public async Task<IActionResult> AddAmenityToRoom(int roomId, int amenityId)
        {
            await _room.AddAmenityToRoom(roomId, amenityId);
            return Ok();
        }

        [HttpDelete]
        [Route("{roomId}/{amenityId}")]
        public async Task<IActionResult> RemoveAmenityFromRome(int roomId, int amenityId)
        {
            await _room.RemoveAmenityFromRoom(roomId, amenityId);
            return Ok();
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Room>> DeleteRoom(int id)
        {
            await _room.Delete(id);
            return NoContent();

        }
    }
}
