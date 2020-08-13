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
using Microsoft.AspNetCore.Authorization;
using async_inn.Models.DTOs;

namespace async_inn.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    // our constructor is bringing in a reference to our db
    public class HotelsController : ControllerBase
    {
        private readonly IHotel _hotel;

        public HotelsController(IHotel hotel)
        {
            _hotel = hotel;
        }

        // GET: api/Hotels
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<HotelDTO>>> GetHotels()
        {
            return  await _hotel.GetHotels();
            
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<HotelDTO>> GetHotel(int id)
        {
            return await _hotel.GetHotel(id);
            

        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize(Policy = "MediumPrivileges")]
        public async Task<IActionResult> PutHotel(int id, HotelDTO hoteldto)
        {
            if (id != hoteldto.Id)
            {
                return BadRequest();
            }

            var updatedHotel = await _hotel.Update(hoteldto);
            return Ok(updatedHotel);
        }

        // POST: api/Hotels
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(Policy = "HighPrivileges")]
        public async Task<ActionResult<HotelDTO>> PostHotel(HotelDTO hoteldto)
        {
            await _hotel.Create(hoteldto);

            return CreatedAtAction("GetHotel", new { id = hoteldto.Id }, hoteldto);
        }

        // DELETE: api/Hotels/5
        [HttpDelete, Route("/api/Hotels/{hotelId}")]
        [Authorize(Policy = "HighPrivileges")]
        public async Task<ActionResult> DeleteHotel(int id)
        {
            await _hotel.Delete(id);
            return NoContent();      
        }
      
    }
}
