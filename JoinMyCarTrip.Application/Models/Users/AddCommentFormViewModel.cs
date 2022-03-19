using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static JoinMyCarTrip.Data.DataConstants;

namespace JoinMyCarTrip.Application.Models.Users
{
    public class AddCommentFormViewModel
    {
        [Required]
        [StringLength(DescriptionCommentMaxLength, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = StandartMinLength)]
        public string Description { get; set; }

        public bool IsNiceTripOrganizer { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public string AuthorId { get; set; }
    }
}
