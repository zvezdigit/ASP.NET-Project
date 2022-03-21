using JoinMyCarTrip.Application.Interfaces;
using JoinMyCarTrip.Application.Models.Users;
using JoinMyCarTrip.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JoinMyCarTrip.Areas.Admin.Controllers
{

    public class AdminController:BaseController
    {
        private readonly IUserService userService;

        private readonly RoleManager<IdentityRole> roleManager;

        private readonly UserManager<ApplicationUser> userManager;

        public AdminController(IUserService _userService,
            RoleManager<IdentityRole> _roleManager,
            UserManager<ApplicationUser> _userManager)
            
        {
            userService = _userService;
            roleManager = _roleManager;
            userManager = _userManager;
        }
        public async Task<IActionResult> Users()
        {
            var users = await userService.GetUsers();

            return View(users);
        }

        //public async Task<IActionResult> CreateRole()
        //{
        //    await roleManager.CreateAsync(new IdentityRole()
        //    {
        //        Name = "Admin"
        //    });

        //    return Ok();
        //}

        public async Task<IActionResult> Roles(string id)
        {
            var user = await userService.GetUserById(id);
            var model = new UserRoleViewModel()
            {
                UserId = user.Id,
                FullName = user.FullName
            };


            ViewBag.RoleItems = roleManager.Roles
                .ToList()
                .Select(r => new SelectListItem()
                {
                    Text = r.Name,
                    Value = r.Id,
                    Selected = userManager.IsInRoleAsync(user, r.Name).Result
                }).ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Roles (UserRoleViewModel model)
        {
            var user = await userService.GetUserById(model.UserId);
            await userManager.AddToRoleAsync(user, model.RoleId);

            return Ok(model);
        }
    }
}
