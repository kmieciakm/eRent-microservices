using Domain.Exceptions;
using Domain.Services.Contracts;
using Domain.Settings;
using FluentEmail.Core;
using FluentEmail.Smtp;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Net;

namespace Domain.Services
{
    public class MailSender : IMailSender
    {
        private SmtpSender _Sender { get; set; }
        private TemplatesSettings _TemplatesSettings { get; }
        private SmtpSettings _SmtpSettings { get; }

        public MailSender(IOptions<TemplatesSettings> templatesSettings, IOptions<SmtpSettings> smtpSettings)
            : this(templatesSettings.Value, smtpSettings.Value) { }

        public MailSender(TemplatesSettings templatesSettings, SmtpSettings smtpSettings)
        {
            _SmtpSettings = smtpSettings;
            _TemplatesSettings = templatesSettings;
            var appPassword = Environment.GetEnvironmentVariable("MAILING_PASSWORD");
            _Sender = new SmtpSender(() => new SmtpClient(_SmtpSettings.Host)
            {
                //EnableSsl = false,
                //DeliveryMethod = SmtpDeliveryMethod.Network,
                //Port = 25
                //PickupDirectoryLocation = _SmtpSettings.TestFolder
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("uAppCorp@gmail.com", appPassword)
            });
            Email.DefaultSender = _Sender;
        }

        public async Task SendConfirmationEmailAsync(string from, string to, string subject, string token)
        {
            var templatePath = _TemplatesSettings.ConfirmationFilePath;
            var template = File.ReadAllText(templatePath);
            var body = string.Format(template, token);

            var sendResponse = await Email
                .From(from)
                .To(to)
                .Subject(subject)
                .Body(body, true)
                .SendAsync();

            if (!sendResponse.Successful)
            {
                throw new EmailException("Email was not send successfully", from, to);
            }
        }

        public async Task SendEmailAsync(string from, string to, string subject, XDocument html)
        {
            var sendResponse = await Email
                .From(from)
                .To(to)
                .Subject(subject)
                .Body(html.ToString(), true)
                .SendAsync();

            if (!sendResponse.Successful)
            {
                throw new EmailException("Email was not send successfully", from, to);
            }
        }
    }
}
