using JoinMyCarTrip.Application.Constants;
using JoinMyCarTrip.Data.Entities;
using JoinMyCarTrip.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static JoinMyCarTrip.Data.DataConstants;

namespace JoinMyCarTrip.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private SignInManager<ApplicationUser> _signInManager;
        private UserManager<ApplicationUser> _userManager;

        public HomeController(
            ILogger<HomeController> logger, 
            SignInManager<ApplicationUser> signInManager, 
            UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            ViewData[MessageConstant.SuccessMessage] = "Welcome";

            if (_signInManager.IsSignedIn(User))
            {
                ApplicationUser user = await _userManager.GetUserAsync(User);
                if (await _userManager.IsInRoleAsync(user, SuperAdminRole) ||
                    await _userManager.IsInRoleAsync(user, AdminRole))
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}