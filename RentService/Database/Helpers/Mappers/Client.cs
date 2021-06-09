using Database.Entities;
using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database.Helpers.Mappers
{
    static partial class Mapper
    {
        public static class Client
        {
            public static DbClientEntity MapToDbClientEntity(ClientEntity clientEntity)
            {
                if (clientEntity == null) return null;
                return new DbClientEntity()
                {
                    ClientGuid = clientEntity.ClientGuid,
                    Firstname = clientEntity.Firstname,
                    Lastname = clientEntity.Lastname,
                    Email = clientEntity.Email
                };
            }

            public static ClientEntity MapToClientEntity(DbClientEntity dbClientEntity)
            {
                if (dbClientEntity == null) return null;
                return new ClientEntity
                (
                    dbClientEntity.ClientGuid,
                    dbClientEntity.Firstname,
                    dbClientEntity.Lastname,
                    dbClientEntity.Email
                );
            }

            public static IEnumerable<ClientEntity> MapToClientEntity(IEnumerable<DbClientEntity> dbClientsEntity)
            {
                return dbClientsEntity.Select(dbClient => MapToClientEntity(dbClient));
            }
        }
    }
}
