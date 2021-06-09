using Domain.Exceptions;
using Domain.Models;
using Domain.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private ILogger<AccountController> _Logger { get; }
        private IAuthenticationService _AuthenticationService { get; }

        public AccountController(ILogger<AccountController> logger, IAuthenticationService authenticationService)
        {
            _Logger = logger;
            _AuthenticationService = authenticationService;
        }

        [HttpGet("confirm")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string email, [FromQuery] string token)
        {
            var user = await _AuthenticationService.GetIdentity(email);
            if (user == null)
            {
                return NotFound();
            }

            try
            {
                var confirmation = new ConfirmAccount()
                {
                    User = user,
                    ConfirmationToken = token
                };
                await _AuthenticationService.ConfirmAccountAsync(confirmation);

                return Ok("Account confirmed successfully.");
            }
            catch (AuthenticationException authEx)
            {
                _Logger.LogWarning(authEx, "Account confirmation failed.");
                return BadRequest();
            }
        }
    }
}
