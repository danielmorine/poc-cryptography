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
            .AddSingleton<IDecryptService, DecryptService> ()
            .BuildServiceProvider();

            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            serviceCollection.AddSingleton<IConfiguration>(configuration);
        }
    }

    //public class Job
    //{
    //    public async Task GetJson()
    //    {
    //        char[] characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower().ToCharArray();

    //        string key = "73a97040798f3ce7a2f17d88f26b1662dfc764c6";

    //        using (var httpClient = new HttpClient())
    //        {
    //            var response = await httpClient
    //                .GetAsync(string.Format("https://api.codenation.dev/v1/challenge/dev-ps/generate-data?token={0}", key));

    //            var res = new Response();

    //            if (response.IsSuccessStatusCode)
    //            {
    //                res = Newtonsoft.Json.JsonConvert.DeserializeObject<Response>(await response.Content.ReadAsStringAsync());
    //            }

    //            var deciphered = new StringBuilder();

    //            foreach (var resp in res.Cifrado.ToLower().ToCharArray())
    //            {
    //                var listkill = CreateAlphabetList(characters);

    //                var positionI = listkill.FindIndex(x => x.Equals(resp)) + 1;
    //                var removed = listkill.Count() - positionI;

    //                listkill.RemoveRange(positionI, removed);

    //                if (!listkill.Any(x => x.Equals(resp)))
    //                {
    //                    deciphered.Append(resp);
    //                    continue;
    //                }

    //                for (var i = 0; i <= res.Numero_casas; i++)
    //                {
                       
    //                    if (i == res.Numero_casas)
    //                    {
    //                        var decif = listkill.LastOrDefault();
    //                        deciphered.Append(decif);
    //                        continue;
    //                    } else if (listkill.FirstOrDefault().Equals('a') && listkill.ToArray().Length == 1)
    //                    {
    //                        listkill.Remove('a');
    //                        listkill.AddRange(CreateAlphabetList(characters));
    //                        continue;
    //                    }   
                            
    //                    var pos = listkill.LastOrDefault();
    //                    listkill.Remove(pos);                                                    
    //                }
    //            }

    //            var d = deciphered.ToString();
    //        }
    //    }
        
        //private List<char> CreateAlphabetList(char[] characters)
        //{            
        //    var list = new List<char>();

        //    foreach (var ca in characters)
        //    {
        //        list.Add(ca);
        //    }

        //    return list;
        //}
//}
}

