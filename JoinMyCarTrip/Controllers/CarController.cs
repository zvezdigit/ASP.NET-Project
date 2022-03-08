using Microsoft.AspNetCore.Mvc;

namespace JoinMyCarTrip.Controllers
{
    public class CarController:Controller
    {
        public IActionResult AddCar()
        {
            return View();
        }

        public IActionResult All()
        {
            return View();
        }
    }
}
