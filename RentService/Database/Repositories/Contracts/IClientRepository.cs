using Database.Entities;
using System;
using System.Collections.Generic;

namespace Database.Repositories.Contracts
{
    interface IClientRepository
    {
        DbClientEntity Get(Guid clientGuid);
        DbClientEntity Get(string email);
        IEnumerable<DbClientEntity> GetAll();
        bool CreateAndSave(DbClientEntity client);
        bool UpdateAndSave(DbClientEntity client);
        bool DeleteAndSave(Guid clientGuid);
    }
}