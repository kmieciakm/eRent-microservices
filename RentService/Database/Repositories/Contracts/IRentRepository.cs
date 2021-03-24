using Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Repositories.Contracts
{
    interface IRentRepository
    {
        DbRentEntity Get(Guid rentGuid);
        IEnumerable<DbRentEntity> GetByClient(Guid clientGuid);
        bool CreateAndSave(DbRentEntity rentEnt);
        bool UpdateAndSave(DbRentEntity rentEnt);
        bool DeleteAndSave(Guid rentGuid);
    }
}
