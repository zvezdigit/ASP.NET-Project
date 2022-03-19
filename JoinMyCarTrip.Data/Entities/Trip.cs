using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static JoinMyCarTrip.Data.DataConstants;

namespace JoinMyCarTrip.Data.Entities
{
    public class Trip
    {
        [Key]
        [MaxLength(GuidMaxLength)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(StartEndPointMaxLength)]
        public string StartPoint { get; set; }

        [Required]
        [MaxLength(StartEndPointMaxLength)]
        public string EndPoint { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        [ForeignKey(nameof(TripType))]
        public string TripTypeId { get; set; }
        public TripType TripType { get; set; }

        [Required]
        [Range(SeatsMin, SeatsMax)]
        public int Seats { get; set; }

        [Required]
        [ForeignKey(nameof(TripOrganizer))]
        public string TripOrganizerId { get; set; }

        public ApplicationUser TripOrganizer { get; set; }

        [Required]
        [ForeignKey(nameof(Car))]
        public string CarId { get; set; }

        public Car Car { get; set; }

        public ICollection<UserTrip> UserTrips { get; set; } = new HashSet<UserTrip>();

        public ICollection<Message> Messages { get; set; } = new HashSet<Message>();
    }
}
