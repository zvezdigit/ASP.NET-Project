using JoinMyCarTrip.Application.Interfaces;
using JoinMyCarTrip.Application.Models.Trips;
using JoinMyCarTrip.Data.Common;
using JoinMyCarTrip.Data.Entities;


namespace JoinMyCarTrip.Application.Services
{
    public class CarTripService : ICarTripService
    {
        private readonly IRepository repository;

        public CarTripService(IRepository _repository)
        {
            this.repository = _repository;
        }
        public void CreateTrip(CreateTripViewModel model)
        {
            
            //var trip = new Trip()
            //{
            //    StartPoint = model.StartPoint,
            //    EndPoint = model.EndPoint,
            //    Seats = model.Seats,
            //    TripType = model.TripType,
            //    TripOrganizerId = "1",
            //    DepartureTime = model.DepartureTime,
            //    CarId = model.CarId
            //};

            //repository.Add(trip);
            //repository.SaveChanges();

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
