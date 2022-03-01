using JoinMyCarTrip.Application.Models;
using JoinMyCarTrip.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinMyCarTrip.Application.Interfaces
{
    public interface ICarTripService
    {
        public IEnumerable<TripViewModel> GetAllTrips();
    }
}
