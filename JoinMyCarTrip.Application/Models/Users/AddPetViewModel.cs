using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static JoinMyCarTrip.Data.DataConstants;


namespace JoinMyCarTrip.Application.Models.Users
{
    public class AddPetViewModel
    {
        [Required]
       
        public string Type { get; set; }

        [Required]
        [StringLength(PetDescriptionMaxLength, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = PetDescriptionMinLength)]
        public string Description { get; set; }
    }
}
