using Domain.DomainModels;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Database.IntegrationTests.TestData
{
    static class ClientFactory
    {
        public static ClientEntity GetSampleClientEntity()
        {
            return new ClientEntity(
               Guid.NewGuid(),
               "Test Firstname",
               "Test Lastname",
               "correctEmail@address.com"
           );
        }

        public static ClientEntity GetIncorrectClientEntity()
        {
            return new ClientEntity(
               Guid.NewGuid(),
               "Test Firstname",
               "Test Lastname",
               null
           );
        }
    }
}
