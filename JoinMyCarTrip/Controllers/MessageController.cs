using Microsoft.AspNetCore.Mvc;

namespace JoinMyCarTrip.Controllers
{
    public class MessageController: Controller
    {
        public IActionResult TextMessage()
        {
            return View();
        }

        public IActionResult All()
        {
            return View();
        }

    }
}
