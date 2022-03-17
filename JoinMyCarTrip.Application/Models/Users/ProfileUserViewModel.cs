using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinMyCarTrip.Application.Models.Users
{
    public class ProfileUserViewModel
    {

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

     
        public UserPetViewModel Pet { get; set; }

        public ICollection<CommentViewModel> Comments { get; set; }
    }
}
