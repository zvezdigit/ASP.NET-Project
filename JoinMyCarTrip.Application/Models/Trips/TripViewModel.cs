using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinMyCarTrip.Application.Models
{
    public class TripViewModel
    {
        public string From { get; set; }

        public string To { get; set; }

        public bool AirConditioner { get; set; } = true; //to be added to the car, not here and view Trip Details to be changed
    }
}
