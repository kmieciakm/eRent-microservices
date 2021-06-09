using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOAP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder().Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder() =>
            new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://*:5050")
                .UseStartup<Startup>();
    }
}
