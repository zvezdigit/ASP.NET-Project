using JoinMyCarTrip.Application.Interfaces;
using JoinMyCarTrip.Application.Models;
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

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult AddCar()
        {
            return View();
        }

        public IActionResult All()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View(new TripViewModel() {  AirConditioner = true }); //to be removed TripViewModel
        }

        public IActionResult MyTrips()
        {
            return View();
        }


    }
}
