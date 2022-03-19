using System.ComponentModel.DataAnnotations;
using static JoinMyCarTrip.Data.DataConstants;

namespace JoinMyCarTrip.Application.Models.Messages
{
    public class TextMessageFormViewModel
    {
        [Required]
        [StringLength(TextMaxLength, ErrorMessage = "Must be between {2} and {1} characters long.", MinimumLength = StandartMinLength)]
        public string Text { get; set; }
    }
}
