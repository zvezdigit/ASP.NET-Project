using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinMyCarTrip.Application.Models.Trips
{
    public class TripListViewModel
    {
        public string TripId { get; set; }

        public string UserId { get; set; }

        public string TripOrganizerId { get; set; }

        public string StartPoint { get; set; }

        public string EndPoint { get; set; }

        public DateTime DepartureTime { get; set; }

        public ICollection<UserTripViewModel> Passengers { get; set; }
    }
}
