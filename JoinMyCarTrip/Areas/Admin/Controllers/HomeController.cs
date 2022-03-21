using Microsoft.AspNetCore.Mvc;

namespace JoinMyCarTrip.Areas.Admin.Controllers
{

    public class HomeController: BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
