using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Contracts
{
    public interface ITokenService
    {
        string GenerateSecurityToken(string email);
        Task<string> GenerateAccountConfirmationTokenAsync(Guid userGuid);
    }
}
