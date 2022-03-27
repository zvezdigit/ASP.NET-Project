using JoinMyCarTrip.Application.Models.Trips;
using JoinMyCarTrip.Application.Services;
using JoinMyCarTrip.Data.Common;
using JoinMyCarTrip.Data.Entities;
using NSubstitute;
using Xunit;

namespace JoinMyCarTrip.Application.Test
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

            var service = new TripService(repository);

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
            var service = new TripService(repository);

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
            var service = new TripService(repository);

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
            var service = new TripService(repository);

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
            var service = new TripService(repository);


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
            var service = new TripService(repository);


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
            var service = new TripService(repository);


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
            var service = new TripService(repository);

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
            var service = new TripService(repository);

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
            var service = new TripService(repository);

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
            var service = new TripService(repository);

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
            var service = new TripService(repository);

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
            var service = new TripService(repository);

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
            var service = new TripService(repository);

            repository.All<Trip>().Returns(new[] { trip }.AsQueryable());

            //act and assert

            Assert.Throws<ArgumentException>(() => service.GetTripDetails(trip2Id));
        }

        [Fact]

        public void ReturnAllUsersCars()
        {
            var userId = "123";

            var user = new ApplicationUser
            {
                Id = userId,
                FullName = "user1",
                Email = "user@abv.bg"
            };

            var car1 = new Car
            {
                Id = "123",
                Model = "model"
            };

            user.Cars = new List<Car>
            {
             car1,
             new Car { Id ="456", Model ="model1"}
            };

            var repository = Substitute.For<IRepository>();
            var service = new TripService(repository);



            repository.All<ApplicationUser>().Returns(new[] { user }.AsQueryable());

            var cars = service.GetAllTripCars(userId);

            //assert
            Assert.Equal(2, cars.MyCars.Count);
            Assert.Equal(cars.MyCars.First().Model, car1.Model);


        }

        [Fact]

        public void WhenUserCarsAreNotFoundReturnZero()
        {

            var userId = "123";

            var user = new ApplicationUser
            {
                Id = userId,
                FullName = "user1",
                Email = "user@abv.bg"
            };

            user.Cars = new List<Car>();


            var repository = Substitute.For<IRepository>();
            var service = new TripService(repository);



            repository.All<ApplicationUser>().Returns(new[] { user }.AsQueryable());

            var cars = service.GetAllTripCars(userId);

            Assert.Equal(0, cars.MyCars.Count);
        }

        [Fact]
        public void WhenReturningAllUserCarsAndUserNotFound()
        {
            //arrange
            var userId = "123";

            var repository = Substitute.For<IRepository>();
            var service = new TripService(repository);

            repository.All<ApplicationUser>().Returns(new[]
            {
                 new ApplicationUser { Id = "1" },
                 new ApplicationUser { Id = "2" },
                 new ApplicationUser { Id = "3" },
            }.AsQueryable());



            //act
            var cars = service.GetAllTripCars(userId);

            //assert
            Assert.Null(cars);
        }

        [Fact]

        public void ReturnAllTripTypes()
        {
            //arrange
            var tripType1 = new TripType
            {
                Id = "123",
                Type = "type1"
            };

            var tripType2 = new TripType
            {
                Id = "456",
                Type = "type2"
            };

            var tipTypes = new List<TripType> { tripType1, tripType2 };

            var repository = Substitute.For<IRepository>();
            var service = new TripService(repository);

            repository.All<TripType>().Returns(new[] { tripType1, tripType2 }.AsQueryable());

            //act
            var types = service.GetAllTripTypes();

            //assert
            Assert.Equal(2, types.Count());
            Assert.Equal(types.First().Type, tripType1.Type);


        }

        [Fact]

        public async Task WhenCreatingATripANewTripIsCreated()
        {
            var tripTypeId = "1";
            var carId = "car1";
            var userId = "123";

            var tripModel = new CreateTripFormViewModel
            {
                StartPoint = "X",
                EndPoint = "Y",
                Seats = 4,
                DepartureTime = new DateTime(),
                TripTypeId = tripTypeId,
                CarId = carId,
            };

            var repository = Substitute.For<IRepository>();
            var service = new TripService(repository);

            await service.CreateTrip(tripModel, userId);

            await repository.Received(1).AddAsync(Arg.Is<Trip>(t => Matches(tripModel, t) && t.TripOrganizerId == userId));

            await repository.Received(1).AddAsync(Arg.Is<UserTrip>(t => Matches(tripModel, t.Trip) && t.UserId == userId));

            await repository.Received(1).SaveChangesAsync();
        }

        private bool Matches(CreateTripFormViewModel tripModel, Trip trip)
        {
            return trip.StartPoint == tripModel.StartPoint 
                && trip.EndPoint == tripModel.EndPoint 
                && trip.Seats == tripModel.Seats 
                && trip.DepartureTime == tripModel.DepartureTime
                && trip.TripTypeId == tripModel.TripTypeId
                && trip.CarId == tripModel.CarId;
        }

        [Fact]

        public async Task WhenCreatingATripAndAUserIsNotFound()
        {
            string userId = null;

            var repository = Substitute.For<IRepository>();
            var service = new TripService(repository);

            await Assert.ThrowsAsync<ArgumentException>(
                   async () => await service.CreateTrip(new CreateTripFormViewModel(), userId));
        }

        [Fact]

        public async Task WhenCreatingATripAndAModelIsNull()
        {
            string userId = "123";

            var repository = Substitute.For<IRepository>();
            var service = new TripService(repository);

            await Assert.ThrowsAsync<ArgumentNullException>(
                   async () => await service.CreateTrip(null, userId));
        }
    }
}
