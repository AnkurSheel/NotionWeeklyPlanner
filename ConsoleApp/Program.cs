using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notion.Client;

namespace ConsoleApp
{
    namespace ConsoleApp
    {
        public class Program
        {
            private static async Task Main()
            {
                var config = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").AddUserSecrets<Program>().Build();

                var clientOptions = new ClientOptions
                {
                    AuthToken = config["Notion:IntegrationToken"]
                };
                var weeklyDatabaseId = config["Notion:WeeklyDatabaseId"];
            }
        }
    }
}
