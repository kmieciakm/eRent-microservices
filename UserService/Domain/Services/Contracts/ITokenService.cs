using Domain.Models.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services.Contracts
{
    public interface ITokenService
    {
        string GenerateSecurityToken(string email);
    }
}
