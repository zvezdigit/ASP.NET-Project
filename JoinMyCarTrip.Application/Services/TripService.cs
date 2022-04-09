using JoinMyCarTrip.Application.Interfaces;
using JoinMyCarTrip.Application.Models.Trips;
using JoinMyCarTrip.Data.Common;
using JoinMyCarTrip.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace JoinMyCarTrip.Application.Services
{
    public class TripService : ITripService
    {
        private const string TripTypesCacheKey = "tripTypes";

        private readonly IRepository repository;
        private readonly IMemoryCache memoryCache;

        public TripService(IRepository _repository, IMemoryCache _memoryCache)
        {
           repository = _repository;
           memoryCache = _memoryCache;
        }
        public async Task CreateTrip(CreateTripFormViewModel model, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("userId cannot be null or empty", nameof(userId));
            }

            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var trip = new Trip()
            {
                StartPoint = model.StartPoint,
                EndPoint = model.EndPoint,
                Seats = model.Seats.Value,
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
            var tripTypes = new List<TripType>();

            if(!memoryCache.TryGetValue(TripTypesCacheKey, out tripTypes))
            {
                tripTypes = repository.All<TripType>().ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

                memoryCache.Set(TripTypesCacheKey, tripTypes, cacheOptions);
            }

            return tripTypes.Select(x => new TripTypeViewModel
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
                         GravatarLink = Utils.Gravаtar.GetUrl(u.User.Email),
                         UserId = u.UserId,
                         FullName = u.User.FullName

                     }).ToList()
                 }).ToList();
        }

        public AllTripsViewModel GetMyTrips(string userId)
        {
            return repository.All<ApplicationUser>()
               .Include(user => user.UserTrips)
               .Where(user => user.Id == userId)
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
                           GravatarLink = Utils.Gravаtar.GetUrl(u.User.Email),
                           UserId = u.UserId,
                           FullName = u.User.FullName
                       }).ToList()
                   }).ToList()

               }).FirstOrDefault();
        }

        public async Task AddUserToTrip(string tripId, string userId)
        {
            var user = repository.All<ApplicationUser>()
               .FirstOrDefault(u => u.Id == userId);

            var trip = repository.All<Trip>()
                .Include(x => x.UserTrips)
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

            if (UserAlreadyJoinedTrip(trip, userId))
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

        private bool UserAlreadyJoinedTrip(Trip trip, string userId)
        {
            foreach(var t in trip.UserTrips)
            {
                if(t.UserId == userId)
                {
                    return true;
                }
            }

            return false;

        }
    }

}
