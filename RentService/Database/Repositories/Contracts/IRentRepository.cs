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
        bool CreateAndSave(DbRentEntity rent);
        bool UpdateAndSave(DbRentEntity rent);
        bool DeleteAndSave(Guid rentGuid);
    }
}
