using Domain.Models.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services.Contracts
{
    interface ITokenService
    {
        string GenerateSecurityToken(SignInRequest request);
    }
}
