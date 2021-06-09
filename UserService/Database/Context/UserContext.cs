using Database.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Context
{
    public class UserContext : IdentityDbContext<DbUser>
    {
        public UserContext(DbContextOptions options) : base(options)
        {
        }
    }
}
