using Domain.DomainModels;
using Domain.DomainModels.ValueObjects;
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
        private readonly ILogger<RentsController> _logger;

        public RentsController(ILogger<RentsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{clientId}")]
        public ActionResult<IEnumerable<RentDto>> GetRentsOfClient(Guid clientId)
        {
            var rent = new RentEntity(
                Guid.NewGuid(),
                new ClientEntity (
                    Guid.NewGuid(),
                    "Nathaniel",
                    "Flynn",
                    "dignissim@tempor.net"
                ),
                DateTime.Now,
                DateTime.Now.AddDays(2),
                Vin.FromString("DRT12343256912305"),
                123m
            );

            return new List<RentDto>()
            {
                new RentDto(rent)
            };
        }
    }
}
