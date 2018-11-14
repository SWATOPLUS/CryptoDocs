using System.IO;
using System.Threading.Tasks;

namespace CryptoDocs.Services
{
    public class DiskFileStorageService : IFileStorageService
    {
        private const string DirectoryName = "files";

        public DiskFileStorageService()
        {
            if (!Directory.Exists(DirectoryName))
            {
                Directory.CreateDirectory(DirectoryName);
            }
        }

        public Task SaveFileAsync(string name, byte[] content)
        {
            return Task.Run(() =>
            {
                var path = Path.Combine(DirectoryName, name);

                File.WriteAllBytes(path, content);
            });
        }

        public Task<byte[]> GetFileAsync(string name)
        {
            return Task.Run(() =>
            {
                var path = Path.Combine(DirectoryName, name);

                return File.Exists(path) ? File.ReadAllBytes(path) : new byte[0];
            });
        }
    }
}