using JoinMyCarTrip.Application.Interfaces;
using JoinMyCarTrip.Application.Models.Cars;
using JoinMyCarTrip.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JoinMyCarTrip.Controllers
{
    public class CarController : BaseController
    {
        private ICarService carService;

        public CarController(ICarService carService, UserManager<ApplicationUser> userManager) 
            : base(userManager)
        {
            this.carService = carService;
        }

        [HttpGet]
        public IActionResult AddCar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCar(AddCarFormViewModel form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }
            var userId = ApplicationUser.Id;
            await carService.AddCar(form, userId);

            return Redirect("/Car/All");
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var userId = ApplicationUser.Id;
            var allCars = carService.GetAllCars(userId);

            return View(allCars);
        }
    }
}
