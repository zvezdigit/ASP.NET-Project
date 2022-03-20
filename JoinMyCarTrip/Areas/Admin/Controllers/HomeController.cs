using Microsoft.AspNetCore.Mvc;

namespace JoinMyCarTrip.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController: Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
