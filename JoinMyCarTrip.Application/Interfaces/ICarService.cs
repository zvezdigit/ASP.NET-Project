using JoinMyCarTrip.Application.Models.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinMyCarTrip.Application.Interfaces
{
    public interface ICarService
    {
        Task AddCar(AddCarFormViewModel model, string userId);
        AllCarsViewModel GetAllCars(string userId);
    }
}
