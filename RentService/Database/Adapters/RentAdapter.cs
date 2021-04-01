using Database.Helpers.Mappers;
using Database.Repositories.Contracts;
using Domain.DomainModels;
using Domain.Ports.Infrastructure.Rent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database.Adapters
{
    class RentAdapter : IRentQuery, IRentCreate, IRentCancel, IRentModify
    {
        private IRentRepository _RentRepository { get; set; }
        public RentAdapter(IRentRepository rentRepository)
        {
            _RentRepository = rentRepository;
        }

        bool IRentCreate.Create(RentEntity rent)
        {
            var dbRent = Mapper.Rent.MapToDbRentEntity(rent);
            return _RentRepository.CreateAndSave(dbRent);
        }

        bool IRentCancel.Delete(Guid rentGuid)
        {
            return _RentRepository.DeleteAndSave(rentGuid);
        }

        RentEntity IRentQuery.Get(Guid rentGuid)
        {
            var dbRent = _RentRepository.Get(rentGuid);
            return Mapper.Rent.MapToRentEntity(dbRent);
        }

        List<RentEntity> IRentQuery.GetRentsOfClient(Guid clientGuid)
        {
            var dbRents = _RentRepository.GetByClient(clientGuid);
            return Mapper.Rent
                .MapToRentEntity(dbRents)
                .ToList();
        }

        bool IRentModify.Update(RentEntity rent)
        {
            var dbRent = Mapper.Rent.MapToDbRentEntity(rent);
            return _RentRepository.UpdateAndSave(dbRent);
        }
    }
}
