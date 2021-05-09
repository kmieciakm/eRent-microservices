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
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public DbUser(string firstname, string lastname, string email) : base()
        {
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
            UserName = Email;
        }

        public DbUser(User user) : base()
        {
            Firstname = user.Firstname;
            Lastname = user.Lastname;
            Email = user.Email;
            EmailConfirmed = user.AccountConfirmed;
            UserName = Email;
        }

        public User ToDomainUser()
        {
            return new User(
                Guid.Parse(Id),
                Firstname,
                Lastname,
                Email,
                EmailConfirmed
            );
        }
    }
}
