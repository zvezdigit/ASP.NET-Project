using JoinMyCarTrip.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinMyCarTrip.Domain.Interfaces
{
    public interface IRepository
    {
        public IEnumerable<Trip> GetAllTrips();

        public IEnumerable<Car> GetAllCars();
    }
}
