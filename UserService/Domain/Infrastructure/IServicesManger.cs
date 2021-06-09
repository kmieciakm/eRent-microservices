using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Infrastructure
{
    public interface IServicesManger
    {
        void CreateAccount(User user);
    }
}
