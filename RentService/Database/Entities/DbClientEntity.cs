using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Database.Entities
{
    class DbClientEntity
    {
        [Key]
        public Guid ClientGuid { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Email { get; set; }

        public override bool Equals(object obj)
        {
            return obj is DbClientEntity entity &&
                   ClientGuid.Equals(entity.ClientGuid) &&
                   Firstname == entity.Firstname &&
                   Lastname == entity.Lastname &&
                   Email == entity.Email;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ClientGuid, Firstname, Lastname, Email);
        }
    }
}
