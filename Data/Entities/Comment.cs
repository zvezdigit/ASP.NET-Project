using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinMyCarTrip.Data.Entities
{
    public class Comment
    {
        [Key]
        [MaxLength(36)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(300)]
        public string Description { get; set; }

        [Required]
        public bool IsNiceOrganizer { get; set; } //likes and dislikes to be counted and presented on the dashboard

        [Required]
        [ForeignKey(nameof(Author))]
        public string AuthorId { get; set; }

        public User Author { get; set; }

        [Required]
        [ForeignKey(nameof(TripOrganizer))]
        public string TripOrganizerId { get; set; }

        public User TripOrganizer { get; set; }
    }
}
