using Database.Repositories;
using Domain.DomainModels;
using Domain.Ports.Directive;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Adapters
{
    class RentAdapter : IRent
    {
        private RentRepository RentRepository { get; set; }
        public RentAdapter(RentRepository rentRepository)
        {
            RentRepository = rentRepository;
        }

        public bool Create(Rent rent)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid rentGuid)
        {
            throw new NotImplementedException();
        }

        public Rent Get(Guid rentGuid)
        {
            throw new NotImplementedException();
        }

        public List<Rent> GetByClient(Guid clientGuid)
        {
            throw new NotImplementedException();
        }

        public bool Update(Rent rent)
        {
            throw new NotImplementedException();
        }
    }
}
