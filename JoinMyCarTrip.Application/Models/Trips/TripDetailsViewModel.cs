using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinMyCarTrip.Application.Models.Trips
{
    public class TripDetailsViewModel
    {
        public string TripId { get; set; }
        public string TripOrganizer { get; set; }

        public string TripOrganizerId { get; set; }

        public string StartPoint { get; set; }

        public string EndPoint { get; set; }

        public DateTime DepartureTime { get; set; }

        public int Seats { get; set; }

        public string TripType { get; set; }

        public string CarModel { get; set; }

        public string CarImageUrl { get; set; }

        public bool AirConditioner { get; set; }

        public bool Smoking { get; set; }

        public bool PetsAllowed { get; set; }

        public bool LuggageAllowed { get; set; }
    }
}
