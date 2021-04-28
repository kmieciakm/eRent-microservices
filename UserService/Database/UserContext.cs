using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database
{
    public class UserContext : IdentityDbContext<DbUser>
    {
        public UserContext(DbContextOptions options) : base(options)
        {
        }

        public void Seed()
        {
            var user = new DbUser("TestUser", "test@localhost.com");
            user.PasswordHash = new PasswordHasher<DbUser>().HashPassword(user, "QWERTY");
            Users.Add(user);
            SaveChanges();
        }
    }
}
