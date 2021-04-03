using Database.Exceptions;
using Domain.DomainModels;
using Domain.DomainModels.ValueObjects;
using Domain.Ports.Infrastructure.Rent;
using System;
using Xunit;

namespace Database.IntegrationTests.TestCases
{
    public class RentTests
    {
        private IRentQuery _Rent { get; set; }
        private IRentCreate _RentCreate { get; set; }
        private IRentModify _RentModify { get; set; }
        private IRentCancel _RentCancel { get; set; }

        /// <remarks>
        /// Constructor is called before each test.
        /// </remarks>
        public RentTests(IRentQuery rentQuery, IRentCreate rentCreate, IRentModify rentModify, IRentCancel rentCancel)
        {
            _Rent = rentQuery;
            _RentCreate = rentCreate;
            _RentModify = rentModify;
            _RentCancel = rentCancel;
        }

        [Fact]
        public void Init_Test()
        {
            Assert.True(true);
        }

        [Fact]
        public void CreateRent_RentDataCorrect()
        {
            var rent = new RentEntity(
                Guid.NewGuid(),
                new ClientEntity(
                        Guid.NewGuid(),
                        "Client firstname",
                        "Client lastname",
                        "client@email.com"
                    ),
                DateTime.Now,
                DateTime.Now.AddDays(2),
                Vin.FromString("DRT12343256912305"),
                123m
            );

            var createdCorrectly = _RentCreate.Create(rent);

            Assert.True(createdCorrectly);
            Assert.NotNull(_Rent.Get(rent.RentGuid));
        }

        [Fact]
        public void CreateRent_RentDataIncorrect()
        {
            var rent = new RentEntity(
                Guid.NewGuid(),
                null,
                DateTime.Now,
                DateTime.Now.AddDays(2),
                Vin.FromString("DRT12343256912305"),
                123m
            );

            Assert.Throws<MapperException>(() =>
                _RentCreate.Create(rent)
            );
        }

        [Fact]
        public void GetRent()
        {
            var rentGuid = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var rent = _Rent.Get(rentGuid);

            Assert.Equal(rentGuid, rent.RentGuid);
        }

        [Fact]
        public void UpdateRent_RentDataCorrect()
        {
            var rentGuid = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var rent = _Rent.Get(rentGuid);
            rent.ExtendRentTime(2);
            var updatedCorrectly = _RentModify.Update(rent);

            Assert.True(updatedCorrectly);
            Assert.Equal(rent, _Rent.Get(rent.RentGuid));
        }

        [Fact]
        public void DeleteRent_RentExists()
        {
            var rentGuid = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var deletedCorrectly = _RentCancel.Delete(rentGuid);

            Assert.True(deletedCorrectly);
            Assert.Null(_Rent.Get(rentGuid));
        }

        [Fact]
        public void DeleteRent_RentDoesNotExist()
        {
            var rentGuid = Guid.NewGuid();
            var deletedCorrectly = _RentCancel.Delete(rentGuid);

            Assert.False(deletedCorrectly);
        }
    }
}
