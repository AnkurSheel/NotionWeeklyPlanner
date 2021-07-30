using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotionWeeklyPlanner;

namespace ConsoleApp
{
    namespace ConsoleApp
    {
        public class Program
        {
            private static async Task Main()
            {
                var serviceProvider = ConfigureServices();
                await serviceProvider.GetRequiredService<App>().Run();
            }

            private static ServiceProvider ConfigureServices()
            {
                var config = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json", false).AddUserSecrets<Program>().Build();

                var notionSettings = config.GetSection("Notion").Get<NotionSettings>();

                var serviceCollection = new ServiceCollection();
                serviceCollection.AddSingleton(_ => notionSettings);
                serviceCollection.AddTransient<Client>();
                serviceCollection.AddTransient<App>();
                serviceCollection.AddTransient<WeeklyPageCreator>();
                serviceCollection.AddTransient<JournalPagesCreator>();
                serviceCollection.AddTransient<PageLinker>();

                return serviceCollection.BuildServiceProvider();
            }
        }
    }
}
