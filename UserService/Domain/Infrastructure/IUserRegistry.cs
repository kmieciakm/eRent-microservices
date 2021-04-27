using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Infrastructure
{
    public interface IUserRegistry
    {
        Task<User> GetByIdAsync(Guid id);
        Task<User> GetByEmailAsync(string email);
        Task<bool> AuthenticateAsync(string email, string password);
        Task<bool> CreateAsync(User user, string password);
    }
}
