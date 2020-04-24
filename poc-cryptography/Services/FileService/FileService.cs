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
    }
    public class FileService : IFileService
    {
        public void CreateFile(string text)
        {
            string path = @"C:\json\";
            Directory.CreateDirectory(path);

            using (FileStream fs = File.Create(Path.Combine(path, "answer.json")))
            {
                byte[] data = Encoding.UTF8.GetBytes(text);
                fs.Write(data, 0, data.Length);
                fs.Close();
            }
        }
    }
}
