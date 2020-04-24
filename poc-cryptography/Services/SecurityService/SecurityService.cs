using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace poc_cryptography.Services.SecurityService
{
    public interface ISecurityService
    {
        string CreateHash(string text);
    }
    public class SecurityService : ISecurityService
    {
        public string CreateHash(string text)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(text);
            var sha1CryptoServiceProvider = new SHA1CryptoServiceProvider();
            return BitConverter.ToString(sha1CryptoServiceProvider.ComputeHash(buffer)).Replace("-", "").Replace(" ", "").ToLower();
        }
    }
}
