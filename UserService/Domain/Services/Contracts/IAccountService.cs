using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Contracts
{
    public interface IAccountService
    {
        Task DeleteAccountAsync(Guid guid);
    }
}
