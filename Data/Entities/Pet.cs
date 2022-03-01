using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinMyCarTrip.Data.Entities
{
    public class Pet
    {
        [Key]
        [MaxLength(36)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(50)]
        public string Type { get; set; }  //cat or dog or bird

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
    }
}
