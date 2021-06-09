using Domain.Infrastructure;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.Web;

namespace MessageQueue
{
    public class ConsoleMailSender : IMailSender
    {
        private MailingSettings _Settings { get; }

        public ConsoleMailSender(IOptions<MailingSettings> mailingSettings)
        {
            _Settings = mailingSettings.Value;
        }

        public ConsoleMailSender(MailingSettings mailingSettings)
        {
            _Settings = mailingSettings;
        }

        public void SendConfirmationEmail(string to, string token)
        {
            var tokenEncoded = HttpUtility.UrlEncode(token);
            string url = $"{_Settings.AccountUrl}confirm?email={to}&token={tokenEncoded}";
            Console.WriteLine(url);
            Debug.WriteLine(url);
        }
    }
}
