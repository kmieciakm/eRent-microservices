using Domain.Ports.Presenters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

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

        [HttpGet("{clientId}")]
        public ActionResult<IEnumerable<RentDto>> GetRentsOfClient(Guid clientId)
        {
            var rents = new List<RentDto>();
            _CarRentService
                .GetClientRentals(clientId).ToList()
                .ForEach(rent => rents.Add(new RentDto(rent)));

            return rents;
        }
    }
}
