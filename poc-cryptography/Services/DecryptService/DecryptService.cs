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
                var listkill = AlphabetHelper.CreateAlphabet();

                var positionI = listkill.FindIndex(x => x.Equals(resp)) + 1;
                var removed = listkill.Count() - positionI;

                listkill.RemoveRange(positionI, removed);

                if (!listkill.Any(x => x.Equals(resp)))
                {
                    deciphered.Append(resp);
                    continue;
                }

                for (var i = 0; i <= decrypted.Numero_casas; i++)
                {

                    if (i == decrypted.Numero_casas)
                    {
                        var decif = listkill.LastOrDefault();
                        deciphered.Append(decif);
                        continue;
                    }
                    else if (listkill.FirstOrDefault().Equals('a') && listkill.ToArray().Length == 1)
                    {
                        listkill.Remove('a');
                        listkill.AddRange(AlphabetHelper.CreateAlphabet());
                        continue;
                    }

                    var pos = listkill.LastOrDefault();
                    listkill.Remove(pos);
                }
            }
            return deciphered.ToString();
        }
    }
}
