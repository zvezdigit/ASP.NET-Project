using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinMyCarTrip.Application.Models.Trips
{
    public class TripCarsListViewModel
    {
        public ICollection<TripCarViewModel> MyCars { get; set; }
    }
}
