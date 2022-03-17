using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static JoinMyCarTrip.Data.DataConstants;

namespace JoinMyCarTrip.Data.Entities
{

    public class ApplicationUser : IdentityUser 
    
    {

        [Required]
        [MaxLength(StandartMaxLength)]
        public string FullName { get; set; }


        public ICollection<UserTrip> UserTrips { get; set; } = new HashSet<UserTrip>();

        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

        public ICollection<Message> Messages { get; set; } = new HashSet<Message>();

        public ICollection<Car> Cars { get; set; } = new HashSet<Car>();

        public ICollection<Pet> Pets { get; set; } = new HashSet<Pet>();

    }
}
