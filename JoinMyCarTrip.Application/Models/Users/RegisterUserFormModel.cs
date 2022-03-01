using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinMyCarTrip.Application.Models.Users
{
    public class RegisterUserFormModel
    {
        [Required]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(60)]
        public string PhoneNumber { get; set; }


        [MaxLength(300)]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(340)]
        public string Email { get; set; }

        [Required]
        [MaxLength(64)]
        public string Password { get; set; }
        
        [Compare("Password", ErrorMessage ="")] // to be added error message
        public string ConfirmPassword { get; set; }

        [Required]
        public bool IsTripOrganizer { get; set; }

      

    }
}
