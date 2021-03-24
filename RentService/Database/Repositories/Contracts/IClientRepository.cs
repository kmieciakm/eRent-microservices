using Database.Entities;
using System;

namespace Database.Repositories.Contracts
{
    interface IClientRepository
    {
        DbClientEntity GetClient(Guid clientGuid);
        bool CreateAndSave(DbClientEntity clientEnt);
        bool UpdateAndSave(DbClientEntity clientEnt);
        bool DeleteAndSave(Guid clientGuid);
    }
}