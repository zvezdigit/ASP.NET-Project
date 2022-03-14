using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static JoinMyCarTrip.Data.DataConstants;

namespace JoinMyCarTrip.Data.Entities
{
    public class Comment
    {
        [Key]
        [MaxLength(GuidMaxLength)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(DescriptionCommentMaxLength)]
        public string Description { get; set; }

        [Required]
        public bool IsNiceOrganizer { get; set; } //likes and dislikes to be counted and presented on the dashboard

        [Required]
        [ForeignKey(nameof(Author))]
        public string AuthorId { get; set; }

        public ApplicationUser Author { get; set; }

        [Required]
        [ForeignKey(nameof(TripOrganizer))]
        public string TripOrganizerId { get; set; }

        public ApplicationUser TripOrganizer { get; set; }
    }
}
