using Domain.Exceptions;
using Domain.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;
using System.Xml.Linq;
using System.Xml;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private ILogger<MailController> _Logger { get; }
        private IMailSender _MailSender { get; }

        public MailController(ILogger<MailController> logger, IMailSender mailSender)
        {
            _Logger = logger;
            _MailSender = mailSender;
        }

        [HttpPost()]
        [ActionName("SendMail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> SendMail([FromBody] RegularMailRequest mail)
        {
            try
            {
                var html = XDocument.Parse(mail.BodyHtml);
                await _MailSender.SendEmailAsync(
                mail.From,
                mail.To,
                mail.Subject,
                html);

                return Ok();
            }
            // TODO: Bad request when emails not valid, subject exceded max length
            catch (XmlException xmlEx)
            {
                var message = "Mail body is not valid HTML format.";
                _Logger.LogInformation(xmlEx, message);
                return BadRequest(message);
            }
            catch (EmailException emailExc)
            {
                _Logger.LogError(emailExc, $"Email send failed.");
                return Conflict(emailExc.Message);
            }
        }

        [HttpPost("confirm")]
        [ActionName("SendConfirmationMail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> SendConfirmationMail([FromBody] ConfirmMailRequest mail)
        {
            try
            {
                // TODO: Bad request when emails not valid, subject exceded max length
                await _MailSender.SendConfirmationEmailAsync(
                    mail.From,
                    mail.To,
                    mail.Subject,
                    mail.Token);

                return Ok();
            }
            catch (EmailException emailExc)
            {
                _Logger.LogError(emailExc, $"Email send failed.");
                return Conflict(emailExc.Message);
            }
        }
    }
}
