using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
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
        }

        public User ToDomainUser()
        {
            return new User()
            {
                Name = this.Name,
                Email = this.Email
            };
        }
    }
}
