using Domain.Models;
using Domain.Models.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Contracts
{
    public interface IAuthenticationService
    {
        Task<string> SignInAsync(SignInRequest signInRequest);
        Task<User> SignUpAsync(SignUpRequest signUpRequest);
        void ConfirmAccountAsync(ConfirmAccountRequest confirmationRequest);
    }
}
