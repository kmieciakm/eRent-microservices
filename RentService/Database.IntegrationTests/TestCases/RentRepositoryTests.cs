﻿using Database.Exceptions;
using Database.IntegrationTests.TestData;
using Domain.Ports.Infrastructure.Rent;
using System;
using Xunit;

namespace Database.IntegrationTests.TestCases
{
    public class RentRepositoryTests
    {
        private IRentQuery _Rent { get; set; }
        private IRentCreate _RentCreate { get; set; }
        private IRentCancel _RentCancel { get; set; }

        /// <remarks>
        /// Constructor is called before each test.
        /// </remarks>
        public RentRepositoryTests(IRentQuery rentQuery, IRentCreate rentCreate, IRentCancel rentCancel)
        {
            _Rent = rentQuery;
            _RentCreate = rentCreate;
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
            var rent = RentsFactory.GetSampleRentEntity();

            var createdCorrectly = _RentCreate.Create(rent);

            Assert.True(createdCorrectly);
            Assert.NotNull(_Rent.Get(rent.RentGuid));
        }

        [Fact]
        public void CreateRent_RentDataIncorrect()
        {
            var rent = RentsFactory.GetIncorrectRentEntity();

            Assert.Throws<MapperException>(() =>
                _RentCreate.Create(rent)
            );
        }

        [Fact]
        public void GetRent()
        {
            var rent = RentsFactory.GetSampleRentEntity();
            _RentCreate.Create(rent);

            Assert.Equal(rent, _Rent.Get(rent.RentGuid));
        }

        [Fact]
        public void DeleteRent_RentExists()
        {
            var rent = RentsFactory.GetSampleRentEntity();
            _RentCreate.Create(rent);

            var deletedCorrectly = _RentCancel.Delete(rent.RentGuid);

            Assert.True(deletedCorrectly);
            Assert.Null(_Rent.Get(rent.RentGuid));
        }

        [Fact]
        public void DeleteRent_RentDoesNotExist()
        {
            var rent = RentsFactory.GetSampleRentEntity();

            var deletedCorrectly = _RentCancel.Delete(rent.RentGuid);

            Assert.False(deletedCorrectly);
        }

        // TODO: Test update method of _IRent
    }
}
