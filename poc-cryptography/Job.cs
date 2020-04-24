using poc_cryptography.Services.DecryptService;
using poc_cryptography.Services.HttpRequestService;
using poc_cryptography.Services.SecurityService;
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
        private readonly ISecurityService _securityService;

        public Job(IHttpRequestService httpRequestService, IDecryptService decryptService, ISecurityService securityService)
        {
            _httpRequestService = httpRequestService;
            _decryptService = decryptService;
            _securityService = securityService;
        }

        public async Task GetJson()
        {
            try
            {
                var result = await _httpRequestService.GetJsonAsync();
                var decrypted = _decryptService.Decrypt(result);
                var hash = _securityService.CreateHash(decrypted);
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

