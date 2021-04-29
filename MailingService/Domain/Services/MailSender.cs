using Domain.Exceptions;
using Domain.Services.Contracts;
using Domain.Settings;
using FluentEmail.Core;
using FluentEmail.Smtp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Services
{
    public class MailSender : IMailSender
    {
        private SmtpSender _Sender { get; set; }
        private TemplatesSettings _TemplatesSettings { get; }
        private SmtpSettings _SmtpSettings { get; }
        public MailSender(TemplatesSettings templatesSettings, SmtpSettings smtpSettings)
        {
            _SmtpSettings = smtpSettings;
            _TemplatesSettings = templatesSettings;
            _Sender = new SmtpSender(() => new SmtpClient(_SmtpSettings.Host)
            {
                //EnableSsl = false,
                //DeliveryMethod = SmtpDeliveryMethod.Network,
                //Port = 25
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                PickupDirectoryLocation = _SmtpSettings.TestFolder
            });
            Email.DefaultSender = _Sender;
        }

        public async Task SendConfirmationEmailAsync(string from, string to, string subject, string token)
        {
            try
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
            catch (Exception exception)
            {
                throw new EmailException("Email sender encountered unexpected error", exception, from, to);
            }
        }

        public async Task SendEmailAsync(string from, string to, string subject, XDocument html)
        {
            try
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
            catch (Exception exception)
            {
                throw new EmailException("Email sender encountered unexpected error", exception, from, to);
            }
        }
    }
}
