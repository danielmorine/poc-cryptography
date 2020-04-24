using poc_cryptography.Helper;
using System.Linq;
using System.Text;

namespace poc_cryptography.Services.DecryptService
{
    public interface IDecryptService
    {
        string Decrypt(string decryptString);
    }

    public class DecryptService : IDecryptService
    {
        public string Decrypt(string decryptString)
        {
            var decrypted = Newtonsoft.Json.JsonConvert.DeserializeObject<Response>(decryptString);

            var deciphered = new StringBuilder();

            foreach (var resp in decrypted.Cifrado.ToLower().ToCharArray())
            {
                var alphabetList = AlphabetHelper.CreateAlphabet();

                var positionI = alphabetList.FindIndex(x => x.Equals(resp)) + 1;
                var removed = alphabetList.Count() - positionI;

                alphabetList.RemoveRange(positionI, removed);

                if (!alphabetList.Any(x => x.Equals(resp)))
                {
                    deciphered.Append(resp);
                    continue;
                }

                for (var i = 0; i <= decrypted.Numero_casas; i++)
                {

                    if (i == decrypted.Numero_casas)
                    {
                        var decif = alphabetList.LastOrDefault();
                        deciphered.Append(decif);
                        continue;
                    }
                    else if (alphabetList.FirstOrDefault().Equals('a') && alphabetList.ToArray().Length == 1)
                    {
                        alphabetList.Remove('a');
                        alphabetList.AddRange(AlphabetHelper.CreateAlphabet());
                        continue;
                    }

                    var remove = alphabetList.LastOrDefault();
                    alphabetList.Remove(alphabetList.LastOrDefault());
                }
            }
            return deciphered.ToString();
        }
    }
}
