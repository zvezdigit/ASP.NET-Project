using JoinMyCarTrip.Domain.Entities;
using JoinMyCarTrip.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinMyCarTrip.Data
{
    public class Repository : IRepository
    {
        public IEnumerable<Car> GetAllCars()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Trip> GetAllTrips()
        {
            throw new NotImplementedException();
        }
    }
}
