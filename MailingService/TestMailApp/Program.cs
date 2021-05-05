using System;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Domain.Services;
using Domain.Settings;
using FluentEmail.Core;
using FluentEmail.Smtp;
using Microsoft.Extensions.Configuration;

namespace TestMailApp
{
    class Program
    {
        static async Task  Main(string[] args)
        {
            ExtractSettings(
                out TemplatesSettings emailTemplatesSettings,
                out SmtpSettings smtpSettings);

            var mailService = new MailSender(emailTemplatesSettings, smtpSettings);
            await mailService.SendConfirmationEmailAsync(
                "noreplay@rentservice.com",
                "customer@customer.com",
                "Email Confiramtion ;)",
                "hfuigsdrcyq48ty3289rtrcbwgq87863467qYE");

            var html = XDocument.Parse("<html><body>Thank you. :)</body></html>");
            await mailService.SendEmailAsync(
                "noreplay@rentservice.com",
                "customer@customer.com",
                "Thank you for delicious pizza",
                html);
        }

        private static void ExtractSettings(out TemplatesSettings emailTemplatesSettings, out SmtpSettings smtpSettings)
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var appSettings = builder.Build();

            var emailSettingsSection = appSettings.GetSection("TemplatesSettings");
            emailTemplatesSettings = new TemplatesSettings();
            emailSettingsSection.Bind(emailTemplatesSettings);

            var smtpSettingsSection = appSettings.GetSection("SmtpSettings");
            smtpSettings = new SmtpSettings();
            smtpSettingsSection.Bind(smtpSettings);
        }
    }
}
