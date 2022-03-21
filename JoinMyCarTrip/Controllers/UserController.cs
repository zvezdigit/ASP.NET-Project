﻿using JoinMyCarTrip.Application.Interfaces;
using JoinMyCarTrip.Application.Models.Users;
using JoinMyCarTrip.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JoinMyCarTrip.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService userService;
        
        public UserController(IUserService _userService
            ,UserManager<ApplicationUser> userManager) 
            : base(userManager)
        {
            userService = _userService;
        }

        [HttpGet("/User/Profile/{userId}")]
        public IActionResult Profile(string userId)
        {
            var profile = userService.Profile(userId);

            return View(profile);
        }

        [HttpGet]
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

        [HttpPost]
        public async Task<IActionResult> AddPet(AddPetFormViewModel form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            var userId = await GetUserIdAsync();
            await userService.AddPet(form, userId);

            return Redirect("/User/Profile");
        }

        [HttpGet]
        public IActionResult AddComment([FromQuery]string tripOrganizerId)
        {
            ViewBag.TripOrganizerId = tripOrganizerId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(AddCommentFormViewModel form 
            ,[FromQuery] string tripOrganizerId)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.TripOrganizerId = tripOrganizerId;
                return View(form);
            }

            var userId = await GetUserIdAsync();
            await userService.AddComment(form, tripOrganizerId, userId);

            return Redirect($"/User/Profile/{tripOrganizerId}");
        }


    }
}
