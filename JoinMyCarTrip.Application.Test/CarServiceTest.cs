using JoinMyCarTrip.Application.Models.Cars;
using JoinMyCarTrip.Application.Services;
using JoinMyCarTrip.Data.Common;
using JoinMyCarTrip.Data.Entities;
using NSubstitute;
using Xunit;

namespace JoinMyCarTrip.Application.Test
{
    public class CarServiceTest
    {
        [Fact]

        public async Task WhenAddingACarANewCarIsAdded()
        {
            var userId = "123";

            var carModel = new AddCarFormViewModel
            {
                BrandAndModel = "carModel",
                Year = 2020,
                ImageUrl = "imageUrl",
                Smoking = true,
                IsWithAirConditioner = true,
                PetsAllowed = true,
                LuggageAllowed = false
            };

            var repository = Substitute.For<IRepository>();
            var service = new CarService(repository);

            await service.AddCar(carModel, userId);

            await repository.Received(1).AddAsync(Arg.Is<Car>(c => Matches(carModel, c) && c.UserId == userId));

            await repository.Received(1).SaveChangesAsync();



        }

        private bool Matches(AddCarFormViewModel carModel, Car car)
        {
            return car.Model == carModel.BrandAndModel
                && car.Year == carModel.Year
                && car.ImageUrl == car.ImageUrl
                && car.Smoking == carModel.Smoking
                && car.IsWithAirConditioner == carModel.IsWithAirConditioner
                && car.LuggageAllowed == carModel.LuggageAllowed
                && car.PetsAllowed == carModel.PetsAllowed;
        }

        [Fact]

        public async Task WhenAddingACarAndUserIsNotFound()
        {
            string userId = null;

            var repository = Substitute.For<IRepository>();
            var service = new CarService(repository);

            
            //act + assert
          await Assert.ThrowsAsync<ArgumentException>(
                    async () => await service.AddCar(new AddCarFormViewModel(), userId));

            var exception =  await Assert.ThrowsAsync<ArgumentException>(
                    async () => await service.AddCar(new AddCarFormViewModel(), userId));

            Assert.Equal("userId cannot be null or empty (Parameter 'userId')", exception.Message);
        }


        [Fact]

        public async Task WhenAddingACarAndAModelIsNull()
        {
            string userId = "123";

            var repository = Substitute.For<IRepository>();
            var service = new CarService(repository);


            //act + assert
            await Assert.ThrowsAsync<ArgumentNullException>(
                      async () => await service.AddCar(null, userId));
        }

        [Fact]
        public void ReturnAllUserCars()
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
                Id = "car123",
                Model = "model1",
                Year = 2010,
                Smoking = true,
                IsWithAirConditioner = false,
                LuggageAllowed = true,
                PetsAllowed = true
            };

            var car2 = new Car
            {
                Id = "car456",
                Model = "model2",
                Year = 2020,
                Smoking = true,
                IsWithAirConditioner = false,
                LuggageAllowed = true,
                PetsAllowed = true
            };

            var cars = new List<Car> { car1, car2 };

            user.Cars = cars;

            var repository = Substitute.For<IRepository>();
            var service = new CarService(repository);

            repository.All<ApplicationUser>().Returns(new[] { user }.AsQueryable());


            // act
            var myCars = service.GetAllCars(userId);

            //assert
            Assert.Equal(2, myCars.Cars.Count);
           
        }

        [Fact]

        public void WhenReturningAllUserCarsAndAUserIsNotFound()
        {
            var userId = "123";

            var repository = Substitute.For<IRepository>();
            var service = new CarService(repository);

            repository.All<ApplicationUser>().Returns(new[]
            {
                 new ApplicationUser { Id = "1" },
                 new ApplicationUser { Id = "2" },
                 new ApplicationUser { Id = "3" },
            }.AsQueryable());



            //act
            var myCars = service.GetAllCars(userId);

            //assert
            Assert.Null(myCars);
        }

        [Fact]
        public void WhenReturningAllUserCarsAndCarsNotFound()
        {
            var userId = "123";

            var user = new ApplicationUser
            {
                Id = userId,
                FullName = "user1",
                Email = "user@abv.bg"
            };

          
            var cars = new List<Car> ();

            user.Cars = cars;

            var repository = Substitute.For<IRepository>();
            var service = new CarService(repository);

            repository.All<ApplicationUser>().Returns(new[] { user }.AsQueryable());


            // act
            var myCars = service.GetAllCars(userId);

            //assert
            Assert.Equal(0, myCars.Cars.Count);
        }

    }
}
