using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace poc_cryptography.Services.HttpRequestService
{
    public interface IHttpRequestService
    {
        Task<string> GetJsonAsync();
    }
    public class HttpRequestService : IHttpRequestService
    {
        private readonly string _key;

        public HttpRequestService(IConfiguration configuration)
        {
            _key = configuration.GetConnectionString("Key");
        }
        public async Task<string> GetJsonAsync()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient
                    .GetAsync(string.Format("https://api.codenation.dev/v1/challenge/dev-ps/generate-data?token={0}", _key));

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }

                throw new Exception("An error happened, please try later.");               
            }
        }
    }
}
