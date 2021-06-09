using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Ports.Infrastructure
{
    public interface IAutomaticAccountCreator
    {
        void ListenRequests();
    }
}
