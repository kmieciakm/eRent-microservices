using Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Ports.Directive
{
    public interface IRent
    {
        Rent Get(Guid rentGuid);
        List<Rent> GetByClient(Guid clientGuid);
        bool Create(Rent rent);
        bool Update(Rent rent);
        bool Delete(Guid rentGuid);
    }
}
