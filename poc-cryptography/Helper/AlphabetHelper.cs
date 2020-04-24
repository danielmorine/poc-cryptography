using System.Collections.Generic;

namespace poc_cryptography.Helper
{
    public static class AlphabetHelper
    {
        public static List<char> CreateAlphabet()
        {
            char[] characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower().ToCharArray();
            
            var list = new List<char>();

            foreach (var ca in characters)
            {
                list.Add(ca);
            }
            return list;            
        }
    }
}
