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
        [MinLength(10)]
        [StringLength(150, ErrorMessage ="{0} length is between {2} and {1}.", MinimumLength = 10)]
        public string StartPoint { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(150)]
        public string EndPoint { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        [MaxLength(20)]
        public string TripType { get; set; }

        [Required]
        [Range(2,7)]
        public int Seats { get; set; }

        [Required]
        public string CarId { get; set; }

    }
}
