using JoinMyCarTrip.Application.Interfaces;
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

        //public IActionResult Index()
        //{
        //    var trips = tripService.GetAllTrips();

        //    return View(trips);
        //}

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
        public async Task<IActionResult> Create(CreateTripViewModel model)
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

        public IActionResult All()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(string tripId)
        {
            TripDetailsViewModel tripDetailsViewModel = tripService.GetTripDetails(tripId);

            return View(tripDetailsViewModel);
        }

        public IActionResult MyTrips()
        {
            return View();
        }

        public IActionResult CreateTripAddCar()
        {
            return View();
        }
    }
}
