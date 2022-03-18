using JoinMyCarTrip.Application.Interfaces;
using JoinMyCarTrip.Application.Models.Trips;
using JoinMyCarTrip.Data.Common;
using JoinMyCarTrip.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace JoinMyCarTrip.Application.Services
{
    public class CarTripService : ICarTripService
    {
        private readonly IRepository repository;

        public CarTripService(IRepository _repository)
        {
            this.repository = _repository;
        }
        public async Task CreateTrip(CreateTripViewModel model, string userId)
        {

            var trip = new Trip()
            {
                StartPoint = model.StartPoint,
                EndPoint = model.EndPoint,
                Seats = model.Seats,
                TripTypeId = model.TripTypeId,
                TripOrganizerId = userId,
                DepartureTime = model.DepartureTime,
                CarId = model.CarId
            };

            await repository.AddAsync(trip);
            await repository.SaveChangesAsync();

            //var user = trip.TripOrganizer;

            //user.UserTrips.Add(new UserTrip()
            //{
            //    TripId = trip.Id,
            //    Trip = trip,
            //    User = user,
            //    UserId = user.Id
            //});


            //repository.SaveChanges();
        }

        public IEnumerable<TripTypeViewModel> GetAllTripTypes()
        {
            return repository.All<TripType>().Select(x => new TripTypeViewModel
            {
                Id = x.Id,
                Type = x.Type
            });
        }

        public TripCarsListViewModel GetAllTripCars(string userId)
        {
            return repository.All<ApplicationUser>()
                .Include(x => x.Cars)
                .Where(r => r.Id == userId)
                .Select(user => new TripCarsListViewModel
                {
                    MyCars = user.Cars.Select(car => new TripCarViewModel
                    {
                        CarId = car.Id,
                        Model = car.Model
                    }).ToList()
                }).FirstOrDefault();
        }

        public TripDetailsViewModel GetTripDetails(string tripId)
        {
            return repository.All<Trip>()
               .Where(t => t.Id == tripId)
               .Select(t => new TripDetailsViewModel()
               {
                   TripOrganizer = t.TripOrganizer.FullName,
                   TripOrganizerId = t.TripOrganizerId,
                   StartPoint = t.StartPoint,
                   EndPoint = t.EndPoint,
                   DepartureTime = t.DepartureTime,
                   Seats = t.Seats,
                   TripType = t.TripType.Type,
                   CarModel = t.Car.Model,
                   CarImageUrl=t.Car.ImageUrl,
                   Smoking=t.Car.Smoking,
                   LuggageAllowed=t.Car.LuggageAllowed,
                   PetsAllowed=t.Car.PetsAllowed,
                   AirConditioner=t.Car.IsWithAirConditioner
                
               }).FirstOrDefault();
        }

        //public IEnumerable<TripViewModel> GetAllTrips()
        //{
        //    //var trips = repository.GetAllTrips();

        //    //var viewModels = trips.Select(t =>
        //    //{
        //    //    return new TripViewModel
        //    //    {
        //    //                // ...
        //    //            };
        //    //}).ToList();

        //    return viewModels;
        //}
    }

}
