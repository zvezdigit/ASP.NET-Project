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
        public async Task CreateTrip(CreateTripFormViewModel model, string userId)
        {
            var trip = new Trip()
            {
                StartPoint = model.StartPoint,
                EndPoint = model.EndPoint,
                Seats = model.Seats,
                TripTypeId = model.TripTypeId,
                TripOrganizerId = userId,
                DepartureTime = model.DepartureTime.Value,
                CarId = model.CarId
            };

            await repository.AddAsync(trip);

            await repository.AddAsync(new UserTrip()
            {
                Trip = trip,
                UserId = userId
            });

            await repository.SaveChangesAsync();

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
            var trip = repository.All<Trip>()
                .FirstOrDefault(t => t.Id == tripId);

            if (trip == null)
            {
                throw new ArgumentException("Trip not found");
            }

            return repository.All<Trip>()
               .Where(t => t.Id == trip.Id)
               .Select(t => new TripDetailsViewModel()
               {
                   TripId = t.Id,
                   TripOrganizer = t.TripOrganizer.FullName,
                   TripOrganizerId = t.TripOrganizerId,
                   StartPoint = t.StartPoint,
                   EndPoint = t.EndPoint,
                   DepartureTime = t.DepartureTime,
                   Seats = t.Seats,
                   TripType = t.TripType.Type,
                   CarModel = t.Car.Model,
                   CarImageUrl = t.Car.ImageUrl,
                   Smoking = t.Car.Smoking,
                   LuggageAllowed = t.Car.LuggageAllowed,
                   PetsAllowed = t.Car.PetsAllowed,
                   AirConditioner = t.Car.IsWithAirConditioner,
                   Messages = t.Messages.Select(m=>new TripMessageListViewModel 
                   {
                    Author= m.Author.FullName,
                    Text = m.Text,
                    Date = m.Date
                   }).ToList()

               }).FirstOrDefault();
        }

        public IEnumerable<TripListViewModel> GetAllTrips()
        {
            return repository.All<Trip>()
                 .Include(x => x.UserTrips)
                 .Select(trip => new TripListViewModel
                 {
                     TripId = trip.Id,
                     TripOrganizerId = trip.TripOrganizerId,
                     StartPoint = trip.StartPoint,
                     EndPoint = trip.EndPoint,
                     DepartureTime = trip.DepartureTime,
                     Passengers = trip.UserTrips.Select(u => new UserTripViewModel
                     {
                         GravatarLink = Utils.Gravtar.GetUrl(u.User.Email),
                         UserId = u.UserId

                     }).ToList()
                 }).ToList();
        }

        public AllTripsViewModel GetMyTrips(string userId)
        {
            return repository.All<ApplicationUser>()
               .Include(x => x.UserTrips)
               .Where(r => r.Id == userId)
               .Select(user => new AllTripsViewModel
               {
                   Trips = user.UserTrips.Select(trip => new TripListViewModel
                   {
                       TripId = trip.TripId,
                       UserId=userId,
                       TripOrganizerId=trip.Trip.TripOrganizerId,
                       StartPoint = trip.Trip.StartPoint,
                       EndPoint = trip.Trip.EndPoint,
                       DepartureTime = trip.Trip.DepartureTime,
                       Passengers = trip.Trip.UserTrips.Select(u => new UserTripViewModel
                       {
                           GravatarLink = Utils.Gravtar.GetUrl(u.User.Email),
                           UserId = u.UserId
                       }).ToList()
                   }).ToList()

               }).FirstOrDefault();
        }

        public async Task AddUserToTrip(string tripId, string userId)
        {
            var user = repository.All<ApplicationUser>()
               .FirstOrDefault(u => u.Id == userId);

            var trip = repository.All<Trip>()
                .FirstOrDefault(t => t.Id == tripId);

            if (user == null || trip == null)
            {
                throw new ArgumentException("User or trip not found");
            }

            if (trip.TripOrganizerId==user.Id)
            {
                throw new ArgumentException("You are the Trip Organizer can not join the trip");
            }

            var userTrip = new UserTrip
            {
                Trip = trip,
                UserId = userId
            };

            if (trip.UserTrips.Contains(userTrip))
            {
                throw new ArgumentException("You have already joined to this trip.");
            }

            if (trip.Seats==0)
            {
                throw new ArgumentException("The car is full, find another trip.");
            }

            await repository.AddAsync(userTrip);

            trip.Seats -= 1;

            await repository.SaveChangesAsync();

        }

        
    }

}
