using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Contracts
{
    public interface IAuthenticationService
    {
        Task<User> GetIdentity(string email);
        Task<string> SignInAsync(SignIn signIn);
        Task<User> SignUpAsync(SignUp signUp);
        void ConfirmAccountAsync(ConfirmAccount confirmation);
    }
}
