using System;
using System.Collections.Generic;
using System.Text;
using AptOnly.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AptOnly.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region CitiesSeed

            builder.Entity<City>().HasData(new City
            {
                CityId = 1,
                CityName = "Artane"
            });
            builder.Entity<City>().HasData(new City
            {
                CityId = 2,
                CityName = "Besiane"
            });
            builder.Entity<City>().HasData(new City
            {
                CityId = 3,
                CityName = "Burim"
            });
            builder.Entity<City>().HasData(new City
            {
                CityId = 4,
                CityName = "Dardane"
            });
            builder.Entity<City>().HasData(new City
            {
                CityId = 5,
                CityName = "Deçan"
            });
            builder.Entity<City>().HasData(new City
            {
                CityId = 6,
                CityName = "Dragash"
            });
            builder.Entity<City>().HasData(new City
            {
                CityId = 7,
                CityName = "Drenas"
            });
            builder.Entity<City>().HasData(new City
            {
                CityId = 8,
                CityName = "Ferizaj"
            });
            builder.Entity<City>().HasData(new City
            {
                CityId = 9,
                CityName = "Fushë Kosovë"
            });
            builder.Entity<City>().HasData(new City
            {
                CityId = 10,
                CityName = "Gjakovë"
            });
            builder.Entity<City>().HasData(new City
            {
                CityId = 11,
                CityName = "Gjilan"
            });
            builder.Entity<City>().HasData(new City
            {
                CityId = 12,
                CityName = "Kastriot"
            });
            builder.Entity<City>().HasData(new City
            {
                CityId = 13,
                CityName = "Kaçanik"
            });
            builder.Entity<City>().HasData(new City
            {
                CityId = 14,
                CityName = "Klinë"
            });
            builder.Entity<City>().HasData(new City
            {
                CityId = 15,
                CityName = "Leposaviq"
            });
            builder.Entity<City>().HasData(new City
            {
                CityId = 16,
                CityName = "Lipjan"
            });
            builder.Entity<City>().HasData(new City
            {
                CityId = 17,
                CityName = "Malishevë"
            });
            builder.Entity<City>().HasData(new City
            {
                CityId = 18,
                CityName = "Mitrovicë"
            });
            builder.Entity<City>().HasData(new City
            {
                CityId = 19,
                CityName = "Pejë"
            });
            builder.Entity<City>().HasData(new City
            {
                CityId = 20,
                CityName = "Prishtinë"
            });
            builder.Entity<City>().HasData(new City
            {
                CityId = 24,
                CityName = "Prizren"
            });
            builder.Entity<City>().HasData(new City
            {
                CityId = 25,
                CityName = "Rahovec"
            });
            builder.Entity<City>().HasData(new City
            {
                CityId = 26,
                CityName = "Skenderaj"
            });
            builder.Entity<City>().HasData(new City
            {
                CityId = 28,
                CityName = "Shtërpcë"
            });
            builder.Entity<City>().HasData(new City
            {
                CityId = 29,
                CityName = "Shtime"
            });
            builder.Entity<City>().HasData(new City
            {
                CityId = 30,
                CityName = "Therandë"
            });
            builder.Entity<City>().HasData(new City
            {
                CityId = 31,
                CityName = "Viti"
            });
            builder.Entity<City>().HasData(new City
            {
                CityId = 32,
                CityName = "Zubin Potok"
            });
            builder.Entity<City>().HasData(new City
            {
                CityId = 33,
                CityName = "Zveçan"
            });
            #endregion
        }   

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Status> Statuses { get; set; }
    }
}

