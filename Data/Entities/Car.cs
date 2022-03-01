using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinMyCarTrip.Data.Entities
{
    public class Car
    {
        [Key]
        [MaxLength(36)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(100)]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [MaxLength(300)]
        public string ImageUrl { get; set; }

        [Required]
        public bool IsWithAirConditioner { get; set; }

        [Required]
        public bool IsSmokingAllowed { get; set; }

        [Required]
        public bool IsPetsAllowed { get; set; }

    }
}
