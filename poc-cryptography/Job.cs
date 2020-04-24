using poc_cryptography.Services.DecryptService;
using poc_cryptography.Services.HttpRequestService;
using System;
using System.Threading.Tasks;

namespace poc_cryptography
{
    public interface IJob
    {
        Task GetJson();
    }
    public class Job : IJob
    {
        private readonly IHttpRequestService _httpRequestService;
        private readonly IDecryptService _decryptService;

        public Job(IHttpRequestService httpRequestService, IDecryptService decryptService)
        {
            _httpRequestService = httpRequestService;
            _decryptService = decryptService;
        }

        public async Task GetJson()
        {
            try
            {
                var result = await _httpRequestService.GetJsonAsync();
                var decrypted = _decryptService.Decrypt(result);               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            Console.ReadKey();
        }
    }
}

