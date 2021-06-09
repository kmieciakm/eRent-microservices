using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class ClientDto
    {
        public Guid Id { get; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }

        public ClientDto(ClientEntity clientEntity)
        {
            Id = clientEntity.ClientGuid;
            Firstname = clientEntity.Firstname;
            Lastname = clientEntity.Lastname;
            Email = clientEntity.Email;
        }

        public static IEnumerable<ClientDto> Map(IEnumerable<ClientEntity> clients)
        {
            return clients.Select(client => new ClientDto(client));
        }
    }
}
