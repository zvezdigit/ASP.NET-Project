
using System.ComponentModel.DataAnnotations;
using static JoinMyCarTrip.Data.DataConstants;

namespace JoinMyCarTrip.Application.Models.Trips
{
    public class CreateTripFormViewModel
    {
        [Required]
        [StringLength(StartEndPointMaxLength, ErrorMessage ="{0} length is between {2} and {1}.", MinimumLength = StartEndPointMinLength)]
        public string StartPoint { get; set; }

        [Required]
        [StringLength(StartEndPointMaxLength, ErrorMessage = "{0} length is between {2} and {1}.", MinimumLength = StartEndPointMinLength)]
        public string EndPoint { get; set; }

        [Required]
        public DateTime? DepartureTime { get; set; }

        [Required]
        public string TripTypeId { get; set; }

        [Required]
        [Range(2,7)]
        public int Seats { get; set; }

        [Required]
        public string CarId { get; set; }

    }
}
