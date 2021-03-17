using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DomainModels
{
    public class Client
    {
        public Client(Guid clientGuid, string firstname, string lastname, string email)
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
            return obj is Client client &&
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
