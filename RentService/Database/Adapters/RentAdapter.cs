using Database.Repositories;
using Domain.DomainModels;
using Domain.Ports.Infrastructure;
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

        public bool Create(RentEntity rent)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid rentGuid)
        {
            throw new NotImplementedException();
        }

        public RentEntity Get(Guid rentGuid)
        {
            throw new NotImplementedException();
        }

        public List<RentEntity> GetByClient(Guid clientGuid)
        {
            throw new NotImplementedException();
        }

        public bool Update(RentEntity rent)
        {
            throw new NotImplementedException();
        }
    }
}
