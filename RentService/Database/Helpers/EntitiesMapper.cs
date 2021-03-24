using Database.Entities;
using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Helpers
{
    static class EntitiesMapper
    {
        public static DbClientEntity MapToDbClientEntity(ClientEntity clientEntity)
        {
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
    }
}
