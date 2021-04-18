using Database.Entities;
using System;

namespace Database.Repositories.Contracts
{
    interface IClientRepository
    {
        DbClientEntity Get(Guid clientGuid);
        bool CreateAndSave(DbClientEntity client);
        bool UpdateAndSave(DbClientEntity client);
        bool DeleteAndSave(Guid clientGuid);
    }
}