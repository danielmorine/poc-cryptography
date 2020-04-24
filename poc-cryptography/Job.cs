using poc_cryptography.Services.DecryptService;
using poc_cryptography.Services.FileService;
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
        private readonly IFileService _fileService;

        public Job(IHttpRequestService httpRequestService, IDecryptService decryptService, ISecurityService securityService, IFileService fileService)
        {
            _httpRequestService = httpRequestService;
            _decryptService = decryptService;
            _securityService = securityService;
            _fileService = fileService;
        }

        public async Task GetJson()
        {
            try
            {
                var result = await _httpRequestService.GetJsonAsync();
                _fileService.CreateFile(result);
                var decrypted = _decryptService.Decrypt(result);
                var hash = _securityService.CreateHash(decrypted);

                _fileService.UpdateFile(decrypted, hash);

                await _httpRequestService.PostAsync();

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

