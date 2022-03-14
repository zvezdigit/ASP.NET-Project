﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static JoinMyCarTrip.Data.DataConstants;

namespace JoinMyCarTrip.Data.Entities
{
    public class Car
    {
        [Key]
        [MaxLength(GuidMaxLength)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(StandartMaxLength)]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; }

        [Required]
        public bool IsWithAirConditioner { get; set; }

        [Required]
        public bool LuggageAllowed { get; set; }

        [Required]
        public bool Smoking { get; set; }

        [Required]
        public bool PetsAllowed { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

    }
}
