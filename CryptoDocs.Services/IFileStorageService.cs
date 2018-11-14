using System.Threading.Tasks;

namespace CryptoDocs.Services
{
    public interface IFileStorageService
    {
        Task SaveFileAsync(string name, byte[] content);

        Task<byte[]> GetFileAsync(string name);
    }
}