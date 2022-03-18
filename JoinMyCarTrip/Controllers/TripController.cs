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

            //var cars = new List<TripCarViewModel> {
            //    new TripCarViewModel
            //    {
            //        Model = "Audi A6", CarId = "123"
            //    },
            //    new TripCarViewModel
            //    {
            //        Model = "BMW X5", CarId = "567567"
            //    },
            //};
            var userId = await GetUserIdAsync();
            var cars = tripService.GetAllTripCars(userId).MyCars.ToList();
            var tripTypes = tripService.GetAllTripTypes().ToList();

            if (cars.Count==0)
            {
                return Redirect("/Trip/CreateTripAddCar");
            }
            return View(new { Cars = cars, TripTypes = tripTypes });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTripViewModel model)
        {

          
            if (!ModelState.IsValid)
            {
               
                return View(model);
            }

            var userId = await GetUserIdAsync();
            await tripService.CreateTrip(model, userId);


            return Redirect("/Trip/All");
        }

        public IActionResult All()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View(new TripViewModel() { AirConditioner = true }); //to be removed TripViewModel
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
