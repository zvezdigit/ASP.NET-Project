using JoinMyCarTrip.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static JoinMyCarTrip.Data.DataConstants;

namespace JoinMyCarTrip.Data
{
    internal static class DatabaseSeeder
    {
        public static void SeedDatabase(this ModelBuilder modelBuilder)
        {
            modelBuilder.SeedTripTypes();

            modelBuilder.SeedSuperAdmin();
        }

        private static void SeedSuperAdmin(this ModelBuilder modelBuilder)
        {
            const string roleId = "c8c2b9c6-17b9-4cc3-b3d6-d2006471dc82";
            const string adminId = "670b3e98-faec-4026-994a-cd3a4a231bf4";

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = AdminRole,
                NormalizedName = AdminRole.ToUpper(),
                Id = roleId,
                ConcurrencyStamp = "1"
            });

            modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = adminId,
                Email = "pesho@abv.com",
                EmailConfirmed = false,
                FullName = "Super Pesho",
                UserName = "pesho@abv.bg",
                NormalizedUserName = "PESHO@ABV.BG",
                PasswordHash = "AQAAAAEAACcQAAAAEKSVbyMRRKapKw7uTWWqVsNkuegEau2em6hA5EWnPzoTn6uFGWWhktxPAr08m6k3xw=="
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId,
                UserId = adminId
            });
        }

        private static void SeedTripTypes(this ModelBuilder modelBuilder)
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
        }
    }
}
