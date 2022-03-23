using JoinMyCarTrip.Application.Services;
using JoinMyCarTrip.Data.Common;
using JoinMyCarTrip.Data.Entities;
using NSubstitute;
using Xunit;

namespace JoinMyCartTrip.Application.Test
{
    public class TripServiceTest
    {
        [Fact]
        public async Task WhenAddingAUserToATripANewUserTripIsCreated()
        {
            // arrange
            var userId = "123";
            var tripId = "456";

            var repository = Substitute.For<IRepository>();
            var service = new CarTripService(repository);

            var trips = new List<Trip>
            {
                new Trip { Id = tripId, Seats = 4 },
                new Trip { Id = "754675467", Seats = 2 },
            }.AsQueryable();

            var users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = userId },
                new ApplicationUser { Id = "56745674567567" },
            }.AsQueryable();

            repository.All<Trip>().Returns(trips);
            repository.All<ApplicationUser>().Returns(users);

            // act
            await service.AddUserToTrip(tripId, userId);

            // assert
            await repository.Received(1).AddAsync(Arg.Is<UserTrip>(t => t.UserId == userId && t.Trip == trips.First()));
            await repository.Received(1).SaveChangesAsync();
        }

        [Fact]
        public async Task WhenAddingAUserToATripTheTripSeatsAreDecrementedByOne()
        {
            // arrange
            var userId = "123";
            var tripId = "456";

            var repository = Substitute.For<IRepository>();
            var service = new CarTripService(repository);

            var trips = new List<Trip>
            {
                new Trip { Id = tripId, Seats = 4 },
                new Trip { Id = "754675467", Seats = 2 },
            }.AsQueryable();

            var users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = userId },
                new ApplicationUser { Id = "56745674567567" },
            }.AsQueryable();

            repository.All<Trip>().Returns(trips);
            repository.All<ApplicationUser>().Returns(users);

            // act
            await service.AddUserToTrip(tripId, userId);

            // assert
            Assert.Equal(3, trips.First().Seats);
        }

        [Fact]
        public async Task WhenAddingAUserToATripAndTheTripDoesNotExistAnExceptionIsThrown()
        {
            // arrange
            var userId = "123";
            var tripId = "456";

            var repository = Substitute.For<IRepository>();
            var service = new CarTripService(repository);

            var trips = new List<Trip>
            {
                new Trip { Id = "754675467", Seats = 2 },
            }.AsQueryable();

            var users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = userId },
                new ApplicationUser { Id = "56745674567567" },
            }.AsQueryable();

            repository.All<Trip>().Returns(trips);
            repository.All<ApplicationUser>().Returns(users);

            // act + assert
            await Assert.ThrowsAsync<ArgumentException>(() => service.AddUserToTrip(tripId, userId));
        }

        [Fact]
        public async Task WhenAddingAUserToATripAndTheUserDoesNotExistAnExceptionIsThrown()
        {
            // ...
        }
    }
}
