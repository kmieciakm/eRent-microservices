using Database.Seed;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Database.IntegrationTests.Fixture
{
    /// <summary>
    /// Supplies different tests environment settings.
    /// </summary>
    /// <remarks>
    /// Require settings.json file in main project directory.
    /// </remarks>
    public class TestSettings : ISeedSettings
    {
        public string ClientDataRelativePath { get; set; }
        public string RentsDataRelativePath { get; set; }
        public string CarsDataRelativePath { get; set; }

        public TestSettings()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("settings.json", optional: true, reloadOnChange: true);
            var configuration = builder.Build();

            var seedSection = configuration.GetSection("SeedSettings");
            seedSection.Bind(this);
        }
    }
}
