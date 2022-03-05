﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinMyCarTrip.Data.Entities
{
    public class User
    {
        [Key]
        [MaxLength(36)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(60)]
        public string PhoneNumber { get; set; }


        [Required]
        [MaxLength(340)]
        public string Email { get; set; }

        [Required]
        [MaxLength(64)]
        public string Password { get; set; }

        [Required]
        public bool IsTripOrganizer { get; set; }

        [ForeignKey(nameof(Car))]
        public string CarId { get; set; }
        public Car Car { get; set; }

        [ForeignKey(nameof(Pet))]
        public string PetId { get; set; }
        public Pet Pet { get; set; }

        public ICollection<UserTrip> UserTrips { get; set; } = new HashSet<UserTrip>();

        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

    }
}
