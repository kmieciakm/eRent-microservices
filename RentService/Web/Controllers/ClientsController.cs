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
    public class ClientsController : ControllerBase
    {
        private ILogger<ClientsController> _Logger { get; }
        private IClientService _ClientService { get; }

        public ClientsController(ILogger<ClientsController> logger, IClientService clientService)
        {
            _Logger = logger;
            _ClientService = clientService;
        }

        [HttpGet("")]
        [ActionName("GetClients")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<ClientDto>> GetClients()
        {
            var clients = _ClientService.GetClients();
            if (clients == null || clients.Count() == 0)
            {
                return NotFound();
            }

            var clientsDto = ClientDto.Map(clients);
            return Ok(clientsDto);
        }

        [HttpGet("{email}")]
        [ActionName("GetClient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ClientDto> GetClient(string email)
        {
            var client = _ClientService.GetClientByEmail(email);
            if (client == null) return NotFound();
            return new ClientDto(client);
        }

        [HttpPost("")]
        [ActionName("CreateClient")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult CreateClient([FromBody] ClientCreateRequest clientRequest)
        {
            var client = _ClientService.CreateClient(
                    clientRequest.ClientGuid,
                    clientRequest.Firstname,
                    clientRequest.Lastname,
                    clientRequest.Email
                );
            var clientDto = new ClientDto(client);
            return CreatedAtAction(nameof(GetClient), new { email = clientDto.Email }, clientDto);
        }
    }
}
