using Microsoft.Extensions.Configuration;
using System.IO;

namespace SeleniumCsharpE2e.Config
{
    public static class TestConfig
    {
        private static IConfiguration Configuration;

        static TestConfig()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public static string BaseUrl => Configuration["Test:BaseUrl"];
        public static int Timeout => int.Parse(Configuration["Test:Timeout"]);
    }
}
