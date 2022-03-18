using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinMyCarTrip.Application.Models.Cars
{
    public class AllCarsViewModel
    {
        public ICollection<CarListViewModel> Cars { get; set; }
    }
}
