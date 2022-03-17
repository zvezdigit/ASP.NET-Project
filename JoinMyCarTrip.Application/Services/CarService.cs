using JoinMyCarTrip.Application.Interfaces;
using JoinMyCarTrip.Application.Models.Cars;
using JoinMyCarTrip.Data.Common;
using JoinMyCarTrip.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinMyCarTrip.Application.Services
{
    public class CarService : ICarService
    {
        private readonly IRepository repository;

        public CarService(IRepository _repository)
        {
                repository = _repository;
        }


        public async Task AddCar(CarViewModel model, string userId)
        {
            var car = new Car
            {
                Model = model.BrandAndModel,
                Year = model.Year,
                ImageUrl = model.ImageUrl,
                LuggageAllowed = model.LuggageAllowed,
                Smoking = model.Smoking,
                PetsAllowed = model.PetsAllowed,
                IsWithAirConditioner = model.IsWithAirConditioner,
                UserId = userId
            };

            await repository.AddAsync(car);
            await repository.SaveChangesAsync();
        }
    }
}
