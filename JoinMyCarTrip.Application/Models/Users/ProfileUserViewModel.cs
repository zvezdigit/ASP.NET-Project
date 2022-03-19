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
        public string UserId { get; set; }
        public string GravatarLink { get; set; }
        
        public string FullName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
        public int? Likes { get; set; }
        public ICollection<CommentViewModel> Comments { get; set; }

        public ICollection<UserPetViewModel> Pets { get; set; }
    }
}
