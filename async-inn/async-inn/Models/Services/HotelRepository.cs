﻿using async_inn.Data;
using async_inn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace async_inn.Models.Services
{
    public class HotelRepository : IHotel
    {
        private AsyncInnDbContext _context;

        public HotelRepository(AsyncInnDbContext context)
        {
            _context = context;
        }
        public async Task<Hotel> Create(Hotel hotel)
        {
            // adds to the database
            _context.Entry(hotel).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            // this saves it and then associates it with an id
            await _context.SaveChangesAsync();

            return hotel;
        }

        public async Task Delete(int id)
        {
            Hotel hotel = await GetHotel(id);
            _context.Entry(hotel).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<Hotel> GetHotel(int id)
        {
            // looks into the db and finds the object with the same id 
            Hotel hotel = await _context.Hotels.FindAsync(id);
        
            return hotel;
        }

        public async Task<List<Hotel>> GetHotels()
        {
           var hotel =  await _context.Hotels.ToListAsync();
            return hotel;
        }

        public async Task<Hotel> Update( Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hotel;

        }
    }
}
