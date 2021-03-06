using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Infrastructure
{
    public interface IUserRegistry
    {
        Task<User> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<User> GetAsync(string email);
        Task<bool> CreateAsync(User user, string password);
        Task<bool> AuthenticateAsync(string email, string password);
        Task<ValidationResult> ValidatePasswordAsync(string password);
        Task<string> GenerateConfirmationTokenAsync(User user);
        Task<bool> ConfirmationAccountAsync(User user, string confirmationToken);
    }
}
