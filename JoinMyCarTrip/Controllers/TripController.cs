using JoinMyCarTrip.Application.Interfaces;
using JoinMyCarTrip.Application.Models;
using JoinMyCarTrip.Application.Models.Cars;
using JoinMyCarTrip.Application.Models.Trips;
using Microsoft.AspNetCore.Mvc;

namespace JoinMyCarTrip.Controllers
{
    public class TripController : Controller

    {
        private readonly ICarTripService tripService;

        public TripController(ICarTripService _tripService)
        {
            this.tripService = _tripService;
        }

        //public IActionResult Index()
        //{
        //    var trips = tripService.GetAllTrips();

        //    return View(trips);
        //}

        public IActionResult Create()
        {
            //if (!ModelState.IsValid)
            //{
            //    return ;
            //}

            var cars = new List<TripCarViewModel> {
                new TripCarViewModel
                {
                    Model = "Audi A6", CarId = "123"
                },
                new TripCarViewModel
                {
                    Model = "BMW X5", CarId = "567567"
                },
            };

            var tripTypes = tripService.GetAllTripTypes().ToList();

            return View(new { Cars = cars, TripTypes = tripTypes });
        }

        [HttpPost]
        public IActionResult Create(CreateTripViewModel model)
        {
          
            tripService.CreateTrip(model);

            return Redirect("/Trip/AddCar");
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
