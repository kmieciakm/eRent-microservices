using Domain.DomainModels.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Domain.UnitTests.TestCases
{
    public class VinTests
    {
        [Theory]
        [InlineData("12345678909876543")]
        [InlineData("zxcdsaqwerfvbgtyh")]
        public void Vin_FromString_CorrectVinNumber(string vin)
        {
            Assert.IsType<Vin>(Vin.FromString(vin));
        }

        [Theory]
        [InlineData("12345678")]
        [InlineData("zxcdsaqwerfvbgtyhsdfdsfsdfd")]
        public void Vin_FromString_InCorrectVinNumber(string vin)
        {
            Assert.Throws<ArgumentException>(() => Vin.FromString(vin));
        }
    }
}
