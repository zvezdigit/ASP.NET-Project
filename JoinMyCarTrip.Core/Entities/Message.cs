using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinMyCarTrip.Domain.Entities
{
    public class Message
    {

        [Key]
        [MaxLength(36)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(500)]
        public string Text { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [ForeignKey(nameof(Author))]
        [Required]
        public string UserId { get; set; }
        public User Author { get; set; }


        [ForeignKey(nameof(Trip))]
        [Required]
        public string TripId { get; set; }
        public Trip Trip { get; set; } 


    }
}
