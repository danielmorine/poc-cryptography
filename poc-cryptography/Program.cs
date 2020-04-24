using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using poc_cryptography.Services.DecryptService;
using poc_cryptography.Services.HttpRequestService;
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
            serviceCollection.AddSingleton<IHttpRequestService, HttpRequestService>()
            .AddSingleton<IJob, Job>()
            .AddSingleton<IDecryptService, DecryptService>()
            .BuildServiceProvider();

            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            serviceCollection.AddSingleton<IConfiguration>(configuration);
        }
    }
}

