using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinMyCarTrip.Data.Entities
{
    public class Trip
    {
        [Key]
        [MaxLength(36)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(150)]
        public string StartPoint { get; set; }

        [Required]
        [MaxLength(150)]
        public string EndPoint { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public string TripType { get; set; }

        [Required]
        public int Seats { get; set; }

        [Required]
        public bool CanTakeLuggage { get; set; }
        public ICollection<UserTrip> UserTrips { get; set; } = new HashSet<UserTrip>();

        public ICollection<Message> Messages { get; set; } = new HashSet<Message>();
    }
}
