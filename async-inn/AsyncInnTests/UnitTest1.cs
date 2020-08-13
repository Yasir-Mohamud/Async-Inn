using async_inn.Models;
using async_inn.Models.DTOs;
using async_inn.Models.Interfaces;
using async_inn.Models.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace AsyncInnTests
{
    public class UnitTest1 : DataBaseTest
    {
        private IAmenity BuildRepository()
        {
            return new AmenityRepository(_db);
        }


        [Fact]
        public async void CanCreateAAmenity()
        {
            AmenityDTO amenity = new AmenityDTO()
            {
                Name = "Chips"
            };

            var service = BuildRepository();

            var created = await service.Create(amenity);
            Assert.Equal("Chips" , created.Name);
        }


        [Fact]
        public async void CanGetAAmenity()
        {
            var service = BuildRepository();

            var result = await service.GetAmenity(1);
            Assert.Equal("Fridge", result.Name);

        }


        [Fact]
        public async void CanGetAllAAmenities()
        {
            var service = BuildRepository();

            List<AmenityDTO> amenity = await service.GetAmenities();

            Assert.Equal(3, amenity.Count);
        }


        [Fact]
        public async void CanUpdateAAmenity()
        {
            var service = BuildRepository();
            var amenity = new AmenityDTO
            {
                Id = 1,
                Name = "Fridge full"
            };

           
            AmenityDTO updated = await service.Update(amenity);

            Assert.Equal("Fridge full", updated.Name);

        }


        [Fact]
        public async void CanDeleteAAmenity()
        {
            var service = BuildRepository();
            List<AmenityDTO> amenity = await service.GetAmenities();
            Assert.Equal(3, amenity.Count);
            await service.Delete(1);
            List<AmenityDTO> amenity2 = await service.GetAmenities();
            Assert.Equal(2, amenity2.Count);

        }
    }
}
