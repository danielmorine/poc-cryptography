using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace poc_cryptography.Services.FileService
{
    public interface IFileService
    {
        void CreateFile(string text);
        void UpdateFile(string decrypted, string hash);
        Task<byte[]> GetFile();
        FileStream GetFileStream();
    }
    public class FileService : IFileService
    {
        private static readonly string _folder = @"C:\json\";
        private static readonly string _fileName = "answer.json";
        private readonly string _path;

        public FileService()
        {
            _path = Path.Combine(_folder, _fileName);
        }

        public void CreateFile(string text)
        {
            Directory.CreateDirectory(_folder);

            using (FileStream fs = File.Create(_path))
            {
                byte[] data = Encoding.UTF8.GetBytes(text);
                fs.Write(data, 0, data.Length);
                fs.Close();
            }
        }

        public async Task<byte[]> GetFile()
        {
            using (FileStream fs = new FileStream(_path, FileMode.Open, FileAccess.Read))
            {
                byte[] bytes = await File.ReadAllBytesAsync(_path);
                fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
                fs.Close();
                return bytes; 
            }
        }

        public FileStream GetFileStream()
        {
            return new FileStream(_path, FileMode.Open);
        }

        public void UpdateFile(string decrypted, string hash)
        {
            var json = new Response();

            using (FileStream fs = File.OpenRead(_path))
            {
                var result = new StringBuilder();

                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);

                while (fs.Read(b, 0, b.Length) > 0)
                {                    
                    result.Append(temp.GetString(b));                    
                }
                json = JsonConvert.DeserializeObject<Response>(result.ToString());
                json.Decifrado = decrypted;
                json.Resumo_criptografico = hash;      
            }
            using (FileStream fs = File.OpenWrite(_path))
            {
                byte[] data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(json));
                fs.Write(data, 0, data.Length);               
                fs.Close();
            }
        }
    }
}
