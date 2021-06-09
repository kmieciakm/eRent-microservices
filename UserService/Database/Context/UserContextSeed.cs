using Database.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Context
{
    public class UserContextSeed
    {
        public UserContext _Context { get; set; }
        public UserContextSeed(UserContext userContext)
        {
            _Context = userContext;
        }

        public void Seed()
        {
            var user = new DbUser("TestUser", "Test", "test@localhost.com");
            user.PasswordHash = new PasswordHasher<DbUser>().HashPassword(user, "QWERTY");

            _Context.Users.Add(user);
            _Context.SaveChanges();
            _Context.ChangeTracker.Clear();
        }
    }
}
