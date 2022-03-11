using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JoinMyCarTrip.Data.Entities
{
    public class ApplicationUser : IdentityUser 
    
    {

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }


        [ForeignKey(nameof(Pet))]
        public string PetId { get; set; }
        public Pet Pet { get; set; }

        public ICollection<UserTrip> UserTrips { get; set; } = new HashSet<UserTrip>();

        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

        public ICollection<Message> Messages { get; set; } = new HashSet<Message>();

        public ICollection<Car> Cars { get; set; } = new HashSet<Car>();

    }
}
