﻿using JoinMyCarTrip.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JoinMyCarTrip.Controllers
{
    public class TripController : Controller
    {
        private readonly ICarTripService tripService;

        public TripController(ICarTripService tripService)
        {
            this.tripService = tripService;
        }

        public IActionResult Index()
        {
            var trips = tripService.GetAllTrips();

            return View(trips);
        }
    }
}
