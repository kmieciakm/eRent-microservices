using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SOAP.Models
{
    [DataContract]
    public class ClientDto
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string Firstname { get; set; }
        [DataMember]
        public string Lastname { get; set; }
        [DataMember]
        public string Email { get; set; }

        public ClientDto() { }

        public ClientDto(ClientEntity clientEntity)
        {
            Id = clientEntity.ClientGuid;
            Firstname = clientEntity.Firstname;
            Lastname = clientEntity.Lastname;
            Email = clientEntity.Email;
        }
    }
}
