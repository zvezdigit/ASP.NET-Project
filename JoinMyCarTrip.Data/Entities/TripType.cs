using System.ComponentModel.DataAnnotations;
using static JoinMyCarTrip.Data.DataConstants;

namespace JoinMyCarTrip.Data.Entities
{
    public class TripType
    {

        [Key]
        [MaxLength(GuidMaxLength)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(TripTypeMaxLength)]
        public string Type { get; set; }
    }
}
