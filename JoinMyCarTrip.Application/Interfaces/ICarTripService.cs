using JoinMyCarTrip.Application.Models;
using JoinMyCarTrip.Application.Models.Trips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinMyCarTrip.Application.Interfaces
{
    public interface ICarTripService
    {
        Task CreateTrip(CreateTripViewModel model, string userId);
        IEnumerable<TripTypeViewModel> GetAllTripTypes();

        TripCarsListViewModel GetAllTripCars(string userId);

        TripDetailsViewModel GetTripDetails(string tripId);

        // public IEnumerable<TripViewModel> GetAllTrips();
    }
}
