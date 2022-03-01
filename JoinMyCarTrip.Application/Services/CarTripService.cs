using JoinMyCarTrip.Application.Interfaces;
using JoinMyCarTrip.Application.Models;
using JoinMyCarTrip.Domain.Entities;
using JoinMyCarTrip.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinMyCarTrip.Application.Services
{
    public class CarTripService : ICarTripService
    {
        private readonly IRepository repository;

        public CarTripService(IRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<TripViewModel> GetAllTrips()
        {
            var trips = repository.GetAllTrips();

            var viewModels = trips.Select(t =>
            {
                return new TripViewModel
                {
                    // ...
                };
            }).ToList();

            return viewModels;
        }
    }
}
