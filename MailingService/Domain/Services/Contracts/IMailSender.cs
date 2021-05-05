using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Services.Contracts
{
    public interface IMailSender
    {
        Task SendConfirmationEmailAsync(string from, string to, string subject, string token);
        Task SendEmailAsync(string from, string to, string subject, XDocument html);
    }
}
