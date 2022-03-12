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
        void CreateTrip(CreateTripViewModel model);
        IEnumerable<TripTypeViewModel> GetAllTripTypes();

        // public IEnumerable<TripViewModel> GetAllTrips();
    }
}
