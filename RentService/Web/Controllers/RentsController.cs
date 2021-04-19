using Domain.DomainModels.ValueObjects;
using Domain.Exceptions;
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
    public class RentsController : ControllerBase
    {
        private ILogger<RentsController> _Logger { get; }
        private ICarRentService _CarRentService { get; }

        public RentsController(ILogger<RentsController> logger, ICarRentService carRentService)
        {
            _Logger = logger;
            _CarRentService = carRentService;
        }

        [HttpGet("{rentId}")]
        [ActionName("GetRent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<RentDto> GetRent(Guid rentId)
        {
            var rental = _CarRentService.GetRental(rentId);
            return new RentDto(rental);
        }

        [HttpGet("client/{clientId}")]
        [ActionName("GetRentalsOfClient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<RentDto>> GetRentalsOfClient(Guid clientId)
        {
            var rents = new List<RentDto>();
            _CarRentService
                .GetClientRentals(clientId).ToList()
                .ForEach(rent => rents.Add(new RentDto(rent)));

            return rents;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult RentACar([FromBody] RentalRequest rentalRequest)
        {
            try
            {
                var rental = _CarRentService
                    .RentCar(
                        rentalRequest.ClientId,
                        Vin.FromString(rentalRequest.CarVin),
                        rentalRequest.StartDate,
                        rentalRequest.EndDate
                    );
                return CreatedAtAction(nameof(GetRent), new { rentId = rental.RentGuid }, new RentDto(rental));
            }
            catch (RentException rentException)
            {
                _Logger.LogWarning(rentException, $"Cannot rent a car.");
                return Conflict(rentException.Message);
            }
        }
    }
}
