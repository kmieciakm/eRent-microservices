using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services.Infrastructure
{
    public interface IAutomaticMailSender
    {
        void ListenRequests();
    }
}
