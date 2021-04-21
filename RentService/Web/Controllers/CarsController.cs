using Domain.DomainModels.ValueObjects;
using Domain.Ports.Presenters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;
using Web.Models.Requests;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private ILogger<CarsController> _Logger { get; }
        private ICarService _CarService { get; }

        public CarsController(ILogger<CarsController> logger, ICarService carService)
        {
            _Logger = logger;
            _CarService = carService;
        }

        [HttpGet("{carVin}")]
        [ActionName("GetCars")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CarDto> GetCars(string carVin)
        {
            Vin vin = Vin.FromString(carVin);
            var car = _CarService.GetCar(vin);
            if (car == null) return NotFound();
            return new CarDto(car);
        }
    }
}
