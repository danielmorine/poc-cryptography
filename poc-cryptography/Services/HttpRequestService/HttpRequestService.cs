using Microsoft.Extensions.Configuration;
using poc_cryptography.Services.FileService;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace poc_cryptography.Services.HttpRequestService
{
    public interface IHttpRequestService
    {
        Task<string> GetJsonAsync();
        Task PostAsync();
    }
    public class HttpRequestService : IHttpRequestService
    {
        private readonly string _key;
        private readonly IFileService _fileService;

        public HttpRequestService(IConfiguration configuration, IFileService fileService)
        {
            _key = configuration.GetConnectionString("Key");
            _fileService = fileService;
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

        public async Task PostAsync()
        {
            var file = await _fileService.GetFile();

            using (var httpClient = new HttpClient())
            {
                var url = string.Format("https://api.codenation.dev/v1/challenge/dev-ps/submit-solution?token={0}", _key);
                var content = new MultipartFormDataContent();
                var byteArrayContent = new ByteArrayContent(file);

                content.Add(byteArrayContent, "answer", "answer.json");
               
                var response = await httpClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Task Completed");
                }
            }
        }
    }
}
