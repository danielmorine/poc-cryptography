using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using poc_cryptography.IoC;
using System;
using System.IO;

namespace poc_cryptography
{
   public class Program
    {        
        public static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            serviceProvider.GetService<IJob>().GetJson().Wait();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection
                .ServiceInjection()
                .BuildServiceProvider();

            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            serviceCollection.AddSingleton<IConfiguration>(configuration);
        }
    }
}

