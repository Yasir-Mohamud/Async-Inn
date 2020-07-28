using async_inn.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace async_inn.Data
{
    public class AsyncInnDbContext : DbContext
    {
        public AsyncInnDbContext(DbContextOptions<AsyncInnDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tells DB that HotelRoom has a combination composite key of HotelID and RoomNumber
            modelBuilder.Entity<HotelRoom>().HasKey(x => new { x.HotelId, x.RoomNumber });
            // Tells DB that RoomAmenity has a combination composite key of RoomID and AmenityId
            modelBuilder.Entity<RoomAmenity>().HasKey(x => new { x.RoomId, x.AmenityId });

            // SEEDING DATA INTO THE DATABASE
            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "Y.M Hotels",
                    StreetAddress = "5th and Oliver street",
                    City = "Los Angeles",
                    State = "California",
                    Phone = "123-456-789",
                },
                 new Hotel
                 {
                     Id = 2,
                     Name = "Y.M Luxury",
                     StreetAddress = "100th south ave",
                     City = "Seattle",
                     State = "Washington",
                     Phone = "222-456-789",
                 },
                    new Hotel
                    {
                        Id = 3,
                        Name = "Y.M Complex",
                        StreetAddress = "23rd Jump street",
                        City = "Portland",
                        State = "Oregon",
                        Phone = "123-654-789",
                    }
                );

            modelBuilder.Entity<Room>().HasData(
                new Room
                {
                    Id = 1,
                    Name = "Amaterasu",
                    Layout = Layout.studio,
                },
                 new Room
                 {
                     Id = 2,
                     Name = "Sweet Tea",
                     Layout = Layout.oneBedroom,
                 },
                  new Room
                  {
                      Id = 3,
                      Name = "Habibi",
                      Layout = Layout.twoBedroom,
                  }
                );


            modelBuilder.Entity<Amenity>().HasData(
                new Amenity
                {
                    Id = 1,
                    Name = "Fridge",
                },
                 new Amenity
                 {
                     Id = 2,
                     Name = "Water Fountain",
                 },
                  new Amenity
                  {
                      Id = 3,
                      Name = "Pool",
                  }
                );

            modelBuilder.Entity<HotelRoom>().HasData(
                new HotelRoom
                {
                    HotelId = 1,
                    RoomId = 1,
                    RoomNumber = 1,
                    Rate = 100m,
                    PetFriendly = true,

                },
                 new HotelRoom
                 {
                     HotelId = 2,
                     RoomId = 2,
                     RoomNumber = 2,
                     Rate = 10m,
                     PetFriendly = false,

                 },
                  new HotelRoom
                  {
                      HotelId = 3,
                      RoomId = 3,
                      RoomNumber = 1,
                      Rate = 200m,
                      PetFriendly = true,

                  }
                );
        }

        // to create an intial migration
        // add-migration intial
        // Install-Package Microsoft.EntityFrameworkCore.Tools
        // add-migration {migrationName}

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<RoomAmenity> RoomAmenities { get; set; }
        public DbSet<HotelRoom> HotelRooms { get; set; }
    
    }
}
