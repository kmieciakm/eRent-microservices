using Database.Entities;
using Domain.DomainModels.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Repositories.Contracts
{
    interface ICarRepository
    {
        DbCarEntity Get(Vin vin);
        bool Exist(Vin vin);
        bool CreateAndSave(DbCarEntity car);
        bool UpdateAndSave(DbCarEntity car);
        bool DeleteAndSave(Vin vin);
    }
}
