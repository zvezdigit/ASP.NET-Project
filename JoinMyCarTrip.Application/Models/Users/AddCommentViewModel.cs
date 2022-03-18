using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinMyCarTrip.Application.Models.Users
{
    public class AddCommentViewModel
    {
        [Required]
        public int Description { get; set; }

        public bool IsNiceTripOrganizer { get; set; }

        public int MyProperty { get; set; }

        public string Author { get; set; }
    }
}
