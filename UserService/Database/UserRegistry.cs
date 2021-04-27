using Domain.Infrastructure;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class UserRegistry : IUserRegistry
    {
        private SignInManager<DbUser> _SignInManager { get; }
        private UserManager<DbUser> _UserManager { get; }

        public UserRegistry(SignInManager<DbUser> signInManager, UserManager<DbUser> userManager)
        {
            _SignInManager = signInManager;
            _UserManager = userManager;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var dbUser = await _UserManager
                .Users
                .FirstOrDefaultAsync(user => user.Id == id.ToString());

            return dbUser.ToDomainUser();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var dbUser = await _UserManager
                .Users
                .FirstOrDefaultAsync(user => user.Email == email);

            return dbUser.ToDomainUser();
        }

        public async Task<bool> AuthenticateAsync(string email, string password)
        {
            var signInResult = await _SignInManager
                .PasswordSignInAsync(email, password, false, false);

            return signInResult.Succeeded;
        }

        public async Task<bool> CreateAsync(User user, string password)
        {
            var dbUser = new DbUser(user.Name, user.Email);
            var result = await _UserManager.CreateAsync(dbUser, password);

            return result.Succeeded;
        }
    }
}
