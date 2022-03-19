﻿using JoinMyCarTrip.Application.Interfaces;
using JoinMyCarTrip.Application.Models;
using JoinMyCarTrip.Application.Models.Cars;
using JoinMyCarTrip.Application.Models.Trips;
using JoinMyCarTrip.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JoinMyCarTrip.Controllers
{
    public class TripController : BaseController

    {
        private readonly ICarTripService tripService;

        public TripController(ICarTripService _tripService, UserManager<ApplicationUser> userManager)
            : base(userManager)
        {
            this.tripService = _tripService;
        }

      

        public async Task<IActionResult> Create()
        {
            var userId = await GetUserIdAsync();
            var cars = tripService.GetAllTripCars(userId).MyCars.ToList();

            if (cars.Count == 0)
            {
                return Redirect("/Trip/CreateTripAddCar");
            }

            var tripTypes = tripService.GetAllTripTypes().ToList();

            ViewBag.TripTypes = tripTypes;
            ViewBag.Cars = cars;
                        
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTripFormViewModel model)
        {
            var userId = await GetUserIdAsync();

            if (!ModelState.IsValid)
            {
                ViewBag.Cars = tripService.GetAllTripCars(userId).MyCars.ToList();
                ViewBag.TripTypes = tripService.GetAllTripTypes().ToList();

                return View(model);
            }

            await tripService.CreateTrip(model, userId);

            return Redirect("/Trip/All");
        }

        [HttpGet]
        public IActionResult All()
        {
            var allTrips = tripService.GetAllTrips();

            return View(allTrips);
        }

        [HttpGet]
        public IActionResult Details([FromQuery]string tripId)
        {
            TripDetailsViewModel tripDetailsViewModel = tripService.GetTripDetails(tripId);

            return View(tripDetailsViewModel);
        }

        public async Task<IActionResult> MyTrips()
        {
            var userId = await GetUserIdAsync();
            var trips = tripService.GetMyTrips(userId);

            return View(trips);
        }

        public async Task<IActionResult> JoinTrip([FromQuery]string tripId)
        {
            var userId = await GetUserIdAsync();
            await tripService.AddUserToTrip(tripId, userId);

            return Redirect("/Trip/MyTrips");
        }

        public IActionResult CreateTripAddCar()
        {
            return View();
        }
    }
}
