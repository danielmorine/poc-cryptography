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

// char[] characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower().ToCharArray();

//using (var httpClient = new HttpClient())
//{
//    var response = await httpClient
//        .GetAsync(string.Format("https://api.codenation.dev/v1/challenge/dev-ps/generate-data?token={0}", key));

//    var res = new Response();

//    if (response.IsSuccessStatusCode)
//    {
//        res = Newtonsoft.Json.JsonConvert.DeserializeObject<Response>(await response.Content.ReadAsStringAsync());
//    }

//    var deciphered = new StringBuilder();

//    foreach (var resp in res.Cifrado.ToLower().ToCharArray())
//    {
//        var listkill = CreateAlphabetList(characters);

//        var positionI = listkill.FindIndex(x => x.Equals(resp)) + 1;
//        var removed = listkill.Count() - positionI;

//        listkill.RemoveRange(positionI, removed);

//        if (!listkill.Any(x => x.Equals(resp)))
//        {
//            deciphered.Append(resp);
//            continue;
//        }

//        for (var i = 0; i <= res.Numero_casas; i++)
//        {

//            if (i == res.Numero_casas)
//            {
//                var decif = listkill.LastOrDefault();
//                deciphered.Append(decif);
//                continue;
//            }
//            else if (listkill.FirstOrDefault().Equals('a') && listkill.ToArray().Length == 1)
//            {
//                listkill.Remove('a');
//                listkill.AddRange(CreateAlphabetList(characters));
//                continue;
//            }

//            var pos = listkill.LastOrDefault();
//            listkill.Remove(pos);
//        }
//    }

//    var d = deciphered.ToString();
