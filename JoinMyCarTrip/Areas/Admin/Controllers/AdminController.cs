using Microsoft.AspNetCore.Mvc;

namespace JoinMyCarTrip.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController:Controller
    {
        public IActionResult Users()
        {
            return View();
        }


        public IActionResult Trips()
        {
            return View();
        }
    }
}
