using JoinMyCarTrip.Data;
using JoinMyCarTrip.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class JoinMyCarTripDbContext : IdentityDbContext<ApplicationUser>
    {
        public JoinMyCarTripDbContext(DbContextOptions<JoinMyCarTripDbContext> options)
            : base(options)
        {
        }

        public DbSet<Pet> Pets { get; set; }

        public DbSet<Trip> Trips { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<UserTrip> UserTrips { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<TripType> TripTypes { get; set; }

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

            modelBuilder.Entity<Comment>()
                   .HasOne(c => c.TripOrganizer)
                   .WithMany(c => c.Comments)
                   .HasForeignKey(c => c.TripOrganizerId)
                   .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                   .HasOne(m => m.Author)
                   .WithMany(m => m.Messages)
                   .HasForeignKey(m => m.AuthorId)
                   .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Car>()
                  .HasOne(c => c.User)
                  .WithMany(c => c.Cars)
                  .HasForeignKey(c => c.UserId)
                  .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.SeedDatabase();

            base.OnModelCreating(modelBuilder);
        }
    }
}