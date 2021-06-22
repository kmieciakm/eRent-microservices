using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Infrastructure
{
    public interface IMailSender
    {
        void Connect();
        void SendConfirmationEmail(string to, string token);
    }
}
