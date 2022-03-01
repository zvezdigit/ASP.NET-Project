using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinMyCarTrip.Data.Entities
{
    public class TripType
    {
        [Key]
        [MaxLength(36)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } //regular and non-regular

        [Required]
        [MaxLength(50)]
        public string SubName { get; set; } //regular - daily; weekly; monthly
    }
}
