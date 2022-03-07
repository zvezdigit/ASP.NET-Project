using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinMyCarTrip.Application.Models.Trips
{
    public class CreateTripViewModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(150)]
        public string StartPoint { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(150)]
        public string EndPoint { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public string TripType { get; set; }

        [Required]
        [Range(1,6)]
        public int Seats { get; set; }

        [Required]
        public string CarId { get; set; }

    }
}
