using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static JoinMyCarTrip.Data.DataConstants;

namespace JoinMyCarTrip.Application.Models.Cars
{
    public class AddCarViewModel
    {
        [Required]
        [StringLength(StandartMaxLength, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = StandartMinLength)]
        public string BrandAndModel { get; set; }

        [Required]
        [Range(MinYear, MaxYear, ErrorMessage = "Year should be between {1} and {2}")]
        public int Year { get; set; }

        [Required]
        [StringLength(ImageUrlMaxLength, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = StandartMinLength)]
        public string ImageUrl { get; set; }

        [Required]
        public bool IsWithAirConditioner { get; set; }

        [Required]
        public bool LuggageAllowed { get; set; }

        [Required]
        public bool Smoking { get; set; }

        [Required]
        public bool PetsAllowed { get; set; }
    }
}
