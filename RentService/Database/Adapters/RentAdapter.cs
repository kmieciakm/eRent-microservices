using Database.Helpers;
using Database.Repositories.Contracts;
using Domain.DomainModels;
using Domain.Ports.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database.Adapters
{
    class RentAdapter : IRent
    {
        private IRentRepository _RentRepository { get; set; }
        public RentAdapter(IRentRepository rentRepository)
        {
            _RentRepository = rentRepository;
        }

        public bool Create(RentEntity rent)
        {
            var dbRent = EntitiesMapper.MapToDbRentEntity(rent);
            return _RentRepository.CreateAndSave(dbRent);
        }

        public bool Delete(Guid rentGuid)
        {
            return _RentRepository.DeleteAndSave(rentGuid);
        }

        public RentEntity Get(Guid rentGuid)
        {
            var dbRent = _RentRepository.Get(rentGuid);
            return EntitiesMapper.MapToRentEntity(dbRent);
        }

        public List<RentEntity> GetRentsOfClient(Guid clientGuid)
        {
            var dbRents = _RentRepository.GetByClient(clientGuid);
            return EntitiesMapper
                .MapToRentEntity(dbRents)
                .ToList();
        }

        public bool Update(RentEntity rent)
        {
            var dbRent = EntitiesMapper.MapToDbRentEntity(rent);
            return _RentRepository.UpdateAndSave(dbRent);
        }
    }
}
