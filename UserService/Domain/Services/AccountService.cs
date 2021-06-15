using Domain.Infrastructure;
using Domain.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class AccountService : IAccountService
    {
        private IUserRegistry _UserRepository { get; }

        public AccountService(IUserRegistry userRepository)
        {
            _UserRepository = userRepository;
        }

        public async Task DeleteAccountAsync(Guid guid)
        {
            await _UserRepository.DeleteAsync(guid);
        }
    }
}
