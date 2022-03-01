using JoinMyCarTrip.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class JoinMyCarTripDbContext : IdentityDbContext
    {
        public JoinMyCarTripDbContext(DbContextOptions<JoinMyCarTripDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Pet> Pets { get; set; }

        public DbSet<Trip> Trips { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<TripType> TripTypes { get; set; }

        public DbSet<UserTrip> UserTrips { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTrip>(e =>
            {
                e.HasKey(ut => new { ut.UserId, ut.TripId });

                e
                    .HasOne(ut => ut.User)
                    .WithMany(ut => ut.UserTrips)
                    .HasForeignKey(op => op.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                e
                    .HasOne(ut => ut.Trip)
                    .WithMany(ut => ut.UserTrips)
                    .HasForeignKey(op => op.TripId)
                    .OnDelete(DeleteBehavior.Restrict);

            });

            base.OnModelCreating(modelBuilder);
        }
    }
}