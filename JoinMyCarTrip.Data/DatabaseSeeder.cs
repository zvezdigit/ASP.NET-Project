using JoinMyCarTrip.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinMyCarTrip.Data
{
    internal static class DatabaseSeeder
    {
        public static void SeedDatabase(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TripType>().HasData(new[]
            {
                new TripType
                {
                    Id = "e651e301-ab94-4a1c-8b57-bbb9b63904e2",
                    Type = "One-time"
                },
                new TripType
                {
                    Id = "97673d10-47a9-4e3e-b8ec-adcec8929f6e",
                    Type = "Daily"
                },
                new TripType
                {
                    Id = "5fa1d2f7-ebee-418d-9f61-028908b003c1",
                    Type = "Weekly"
                },
                new TripType
                {
                    Id = "ffe4071c-da82-411a-bedc-0d6124ceff85",
                    Type = "Monthly"
                },
            });

            // TODO: seed admin user
            // var rootAdmin = new ApplicationUser
            // {
            //     Id = "",
            //     FullName = "",
            //     UserName = "",
            // 
            // };
            // 
            // modelBuilder.Entity<ApplicationUser>().HasData(new[] {
            //     
            // });
        }
    }
}
