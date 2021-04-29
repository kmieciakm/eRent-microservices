using Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Database.Models
{
    public class DbUser : IdentityUser
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public DbUser(string name, string email) : base()
        {
            Name = name;
            Email = email;
            UserName = Email;
        }

        public DbUser(User user) : base()
        {
            Name = user.Name;
            Email = user.Email;
            UserName = Email;
        }

        public User ToDomainUser()
        {
            return new User()
            {
                Name = Name,
                Email = Email
            };
        }
    }
}
