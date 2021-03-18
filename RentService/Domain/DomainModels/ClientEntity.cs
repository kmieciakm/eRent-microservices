using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DomainModels
{
    /// <summary>
    /// Information about the rental client.
    /// </summary>
    public class ClientEntity
    {
        public ClientEntity(Guid clientGuid, string firstname, string lastname, string email)
        {
            ClientGuid = clientGuid;
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
        }

        public Guid ClientGuid { get; }
        public string Firstname { get; private set; }
        public string Lastname { get; private set; }
        public string Email { get; private set; }

        public override bool Equals(object obj)
        {
            return obj is ClientEntity client &&
                   ClientGuid.Equals(client.ClientGuid) &&
                   Firstname == client.Firstname &&
                   Lastname == client.Lastname &&
                   Email == client.Email;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ClientGuid, Firstname, Lastname, Email);
        }
    }
}
