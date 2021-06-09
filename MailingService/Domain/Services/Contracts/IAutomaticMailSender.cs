using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services.Contracts
{
    public interface IAutomaticMailSender
    {
        void ListenRequests();
    }
}
