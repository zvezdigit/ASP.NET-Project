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

            var service = new CarTripService(repository);

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

            var trip1 = new Trip { Id = tripId, Seats = 4 };
            var trip2 = new Trip { Id = "754675467", Seats = 2 };

            var repository = Substitute.For<IRepository>();
            var service = new CarTripService(repository);

            var trips = new List<Trip> { trip1, trip2 }.AsQueryable();

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
            Assert.Equal(3, trip1.Seats);
            Assert.Equal(2, trip2.Seats);
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
            // arrange

            var userId = "123";
            var tripId = "456";

            var repository = Substitute.For<IRepository>();
            var service = new CarTripService(repository);

            var trips = new List<Trip>
            {
                new Trip { Id = tripId, Seats = 4},
                new Trip { Id = "754675467", Seats = 2 },
            }.AsQueryable();

            var users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = "56745674567567" }
            }.AsQueryable();

            repository.All<Trip>().Returns(trips);
            repository.All<ApplicationUser>().Returns(users);

            // act + assert
            await Assert.ThrowsAsync<ArgumentException>(() => service.AddUserToTrip(tripId, userId));

        }

        [Fact]

        public async Task WhenAddingAUserToATripAndTripOrganizerAndAUserAreTheSameAnExceptionIsThrown()
        {
            //arrange

            var userId = "123";
            var tripId = "456";
            var tripOrganizerId = "123";

            var repository = Substitute.For<IRepository>();
            var service = new CarTripService(repository);


            var trips = new List<Trip>
            {
                new Trip { Id = tripId, Seats = 4, TripOrganizerId=tripOrganizerId},
                new Trip { Id = "754675467", Seats = 2 },
            }.AsQueryable();

            var users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = userId },
                new ApplicationUser { Id = "56745674567567" }
            }.AsQueryable();


            repository.All<Trip>().Returns(trips);
            repository.All<ApplicationUser>().Returns(users);

            // act + assert
            await Assert.ThrowsAsync<ArgumentException>(() => service.AddUserToTrip(tripId, userId));
        }

        [Fact]

        public async Task WhenAddingAUserToATripAndAUserHasAlreadyJoinedTheTripAnExceptionIsThrown()
        {
            var userId = "123";
            var tripId = "456";

            var repository = Substitute.For<IRepository>();
            var service = new CarTripService(repository);


            var trips = new List<Trip>
            {
                new Trip
                {
                    Id = tripId,
                    Seats = 4,
                    UserTrips= new[] { new UserTrip{ UserId = userId, TripId = tripId } }
                },
                new Trip { Id = "754675467", Seats = 2 },
            }.AsQueryable();


            var users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = userId },
                new ApplicationUser { Id = "56745674567567" }
            }.AsQueryable();

            repository.All<Trip>().Returns(trips);
            repository.All<ApplicationUser>().Returns(users);

            // act + assert
            await Assert.ThrowsAsync<ArgumentException>(() => service.AddUserToTrip(tripId, userId));
        }

        [Fact]
        public async Task WhenAddingAUserToATripAndTripSeatsAreEqualToZeroAnExceptionIsThrown()
        {
            //arrange

            var userId = "123";
            var tripId = "456";


            var repository = Substitute.For<IRepository>();
            var service = new CarTripService(repository);


            var trips = new List<Trip>
            {
                new Trip { Id = tripId, Seats = 0},
                new Trip { Id = "754675467", Seats = 2 },
            }.AsQueryable();

            var users = new List<ApplicationUser>
            {
                new ApplicationUser { Id = userId },
                new ApplicationUser { Id = "56745674567567" }
            }.AsQueryable();


            repository.All<Trip>().Returns(trips);
            repository.All<ApplicationUser>().Returns(users);

            // act + assert
            await Assert.ThrowsAsync<ArgumentException>(() => service.AddUserToTrip(tripId, userId));
        }

        [Fact]

        public void ReturnAllUserTrips()
        {
            //arrange
            var userId = "123";
            var user = new ApplicationUser
            {
                Id = userId,
                UserName = "user123",
                PhoneNumber = "phone 1234",
                Email = "user123@u.bg"
            };

            var trip1 = new Trip
            {
                Id = "123",
                TripOrganizerId = userId,
                StartPoint = "X",
                EndPoint = "Y",

                UserTrips = new List<UserTrip>
                {
                    new UserTrip { User = user }, // me
                    new UserTrip { User = new ApplicationUser{ Id = "456", FullName = "user 456", Email = "user456@u.bg" } }, // other guy
                }
            };

            var trip2 = new Trip
            {

                UserTrips = new List<UserTrip>
                {
                    new UserTrip { User = user }, // me
                    new UserTrip { User = new ApplicationUser { Id = "456", FullName = "user 456", Email = "user456@u.bg" } }, // other guy
                    new UserTrip { User = new ApplicationUser { Id = "789", FullName = "user 789", Email = "user789@u.bg"} } // other guy
                }
            };

            user.UserTrips = new List<UserTrip>
            {
                new UserTrip { User = user, UserId = userId, Trip = trip1 },
                new UserTrip { User = user, UserId = userId, Trip = trip2 },
            };

            var repository = Substitute.For<IRepository>();
            var service = new CarTripService(repository);

            repository.All<ApplicationUser>().Returns(new[] { user }.AsQueryable());


            // act
            var myTrips = service.GetMyTrips(userId);

            //assert
            Assert.Equal(2, myTrips.Trips.Count);
            Assert.Equal(2, myTrips.Trips.First().Passengers.Count);
            Assert.Equal(3, myTrips.Trips.Skip(1).First().Passengers.Count);
        }



        [Fact]

        public void WhenReturningAllUserTripsAndUserNotFound()
        {
            //arrange
            var userId = "777";

            var repository = Substitute.For<IRepository>();
            var service = new CarTripService(repository);

            repository.All<ApplicationUser>().Returns(new[] 
            {
                 new ApplicationUser { Id = "1" },
                 new ApplicationUser { Id = "2" },
                 new ApplicationUser { Id = "3" },
            }.AsQueryable());


         
            //act
            var myTrips = service.GetMyTrips(userId);

            //assert
            Assert.Null(myTrips);
        }

        [Fact]

        public void WhenReturningAllUserTripsAndTripsNotFound()
        {

            //arrange
            var userId = "123";
            var user = new ApplicationUser
            {
                Id = userId,
                UserName = "user123",
                PhoneNumber = "phone 1234",
                Email = "user123@u.bg"
            };

            user.UserTrips = new List<UserTrip>
            {
            };

            var repository = Substitute.For<IRepository>();
            var service = new CarTripService(repository);

            repository.All<ApplicationUser>().Returns(new[] { user }.AsQueryable());


            // act
            var myTrips = service.GetMyTrips(userId);

            //assert
            Assert.Equal(0, myTrips.Trips.Count);

        }

        [Fact]

        public void ReturnAllTrips()
        {


            var trip1 = new Trip
            {
                Id = "123",
                TripOrganizerId = "123",
                StartPoint = "X",
                EndPoint = "Y",

                UserTrips = new List<UserTrip>
                {
                    new UserTrip { User = new ApplicationUser{ Id = "456", FullName = "user 456", Email = "user456@u.bg" } }, // other guy
                }
            };

            var trip2 = new Trip
            {

                UserTrips = new List<UserTrip>
                {
                    new UserTrip { User = new ApplicationUser { Id = "456", FullName = "user 456", Email = "user456@u.bg" } }, // other guy
                    new UserTrip { User = new ApplicationUser { Id = "789", FullName = "user 789", Email = "user789@u.bg"} } // other guy
                }
            };

            var repository = Substitute.For<IRepository>();
            var service = new CarTripService(repository);

            repository.All<Trip>().Returns(new[] { trip1, trip2 }.AsQueryable());

            //act

            var trips = service.GetAllTrips();

            //assert

            Assert.Equal(2, trips.Count());

        }

        [Fact]

        public void ReturnEmptyListWhenTripsAreZero()
        {
            var trips = new List<Trip>();


            var repository = Substitute.For<IRepository>();
            var service = new CarTripService(repository);

            repository.All<Trip>().Returns(trips.AsQueryable());

            var allTrips = service.GetAllTrips();

            //assert
            Assert.Empty(allTrips);
        }

        [Fact]

        public void ReturnTripDetails()
        {
            //arrange
            var tripId = "123";

            var car = new Car
            {
                Id="123",
                Model = "ford",
                IsWithAirConditioner = true,
                LuggageAllowed = true,
                PetsAllowed = false,
                Smoking = false
            };

            var tripOrganizer = new ApplicationUser
            {
                Id = "123",
                FullName = "üser 123",
                Email = "uzer123@u.bg"
            };

            var trip = new Trip
            {
                Id = tripId,
                TripOrganizerId = tripOrganizer.Id,
                TripOrganizer=tripOrganizer,
                StartPoint = "X",
                EndPoint = "Y",
                DepartureTime = new DateTime(),
                Seats = 4,
                TripType = new TripType
                {
                    Id = "1",
                    Type = "weekly"
                },
                Car = car
            };

            var repository = Substitute.For<IRepository>();
            var service = new CarTripService(repository);

            repository.All<Trip>().Returns(new[] { trip }.AsQueryable());


            //act
            var tripdetails = service.GetTripDetails(tripId);

            //assert

            Assert.Equal(tripdetails.TripId, trip.Id);
            Assert.Equal(tripdetails.TripOrganizerId, trip.TripOrganizerId);
            Assert.Equal(tripdetails.TripOrganizerId, trip.TripOrganizerId);
            Assert.Equal(tripdetails.StartPoint, trip.StartPoint);
            Assert.Equal(tripdetails.DepartureTime, trip.DepartureTime);
            Assert.Equal(tripdetails.Seats, trip.Seats);
        }

        [Fact]

        public void ReturningTripDetailsWhenATripIsNotFoundAnExceptionIsThrown()
        {
            //arrange
            var tripId = "123";
            var trip2Id = "145";

            var car = new Car
            {
                Id = "123",
                Model = "ford",
                IsWithAirConditioner = true,
                LuggageAllowed = true,
                PetsAllowed = false,
                Smoking = false
            };

            var tripOrganizer = new ApplicationUser
            {
                Id = "123",
                FullName = "üser 123",
                Email = "uzer123@u.bg"
            };

            var trip = new Trip
            {
                Id = tripId,
                TripOrganizerId = tripOrganizer.Id,
                TripOrganizer = tripOrganizer,
                StartPoint = "X",
                EndPoint = "Y",
                DepartureTime = new DateTime(),
                Seats = 4,
                TripType = new TripType
                {
                    Id = "1",
                    Type = "weekly"
                },
                Car = car
            };

            var repository = Substitute.For<IRepository>();
            var service = new CarTripService(repository);

            repository.All<Trip>().Returns(new[] { trip }.AsQueryable());


        

            //act and assert

            Assert.Throws<ArgumentException>(() => service.GetTripDetails(trip2Id));
        }
    }
}
