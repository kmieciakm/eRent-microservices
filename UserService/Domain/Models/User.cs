using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class User
    {
        public Guid Guid { get; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public bool AccountConfirmed { get; set; }

        public User(Guid guid, string firstname, string lastname, string email, bool accountConfirmed = false)
        {
            Guid = guid;
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
            AccountConfirmed = accountConfirmed;
        }
    }
}
