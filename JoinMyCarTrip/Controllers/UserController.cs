using JoinMyCarTrip.Application.Interfaces;
using JoinMyCarTrip.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JoinMyCarTrip.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService userService;
        
        public UserController(IUserService _userService, UserManager<ApplicationUser> userManager) 
            : base(userManager)
        {
            userService = _userService;
        }

        public async Task<IActionResult> Profile()
        {
            var userId = await GetUserIdAsync();
            var profile = userService.Profile(userId);

            return View(profile);
        }

        public IActionResult AddPet()
        {
            return View();
        }

        public IActionResult AddComment()
        {
            return View();
        }

      
    }
}
