using Domain.Models;
using Domain.Models.Requests;
using Domain.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Contracts
{
    public interface IUserService
    {
        Task<TokenResponse> SignInAsync(SignInRequest signInRequest);
        Task<User> SignUpAsync(SignUpRequest signUpRequest);
    }
}
