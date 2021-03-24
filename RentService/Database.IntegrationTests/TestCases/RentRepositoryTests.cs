using Database.Adapters;
using Database.DatabaseContext;
using Database.Exceptions;
using Database.IntegrationTests.TestData;
using Database.Repositories;
using Domain.Ports.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Database.IntegrationTests.TestCases
{
    public class RentRepositoryTests
    {
        private IRent _Rent { get; set; }

        /// <remarks>
        /// Constructor is called before each test.
        /// Tests run slower, but data separation for each test is ensured.
        /// </remarks>
        public RentRepositoryTests()
        {
            var dbContext = DbContextFactory
                .CreateInMemoryRentDatabase()
                .CreateDbContext();

            var dbContextSeed = new RentDbContextSeed(dbContext);
            dbContextSeed.SeedData();

            _Rent = new RentAdapter(
                new RentRepository(dbContext)
            );
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
            var createdCorrectly = _Rent.Create(rent);
            Assert.True(createdCorrectly);
            Assert.NotNull(_Rent.Get(rent.RentGuid));
        }

        [Fact]
        public void CreateRent_RentDataIncorrect()
        {
            var rent = RentsFactory.GetIncorrectRentEntity();
            Assert.Throws<MapperException>(() =>
                _Rent.Create(rent)
            );
        }

        [Fact]
        public void GetRent()
        {
            var rent = RentsFactory.GetSampleRentEntity();
            _Rent.Create(rent);
            Assert.Equal(rent, _Rent.Get(rent.RentGuid));
        }

        [Fact]
        public void DeleteRent_RentExists()
        {
            var rent = RentsFactory.GetSampleRentEntity();
            _Rent.Create(rent);
            var deletedCorrectly = _Rent.Delete(rent.RentGuid);
            Assert.True(deletedCorrectly);
            Assert.Null(_Rent.Get(rent.RentGuid));
        }

        [Fact]
        public void DeleteRent_RentDoesNotExist()
        {
            var rent = RentsFactory.GetSampleRentEntity();
            var deletedCorrectly = _Rent.Delete(rent.RentGuid);
            Assert.False(deletedCorrectly);
        }

        // TODO: Test update method of _IRent
    }
}
