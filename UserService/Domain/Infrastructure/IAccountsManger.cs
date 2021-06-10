using Domain.Models;
using Domain.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Infrastructure
{
    public interface IAccountsManger
    {
        void CreateAccount(User user, Action<User> onSuccess, Action<User> onFailure);
        void Connect();
    }
}
