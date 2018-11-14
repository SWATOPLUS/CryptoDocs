using System.Threading.Tasks;

namespace CryptoDocs.Abstractions
{
    public interface ICryptoDocsService
    {
        Task<byte[]> GetEncryptedSessionKeyAsync(RsaPublicKeyDto publicKeyDto);

        Task<byte[]> GetEncryptedFileAsync(RsaPublicKeyDto publicKeyDto, string algorithm, string fileName);

        Task SetEncryptedFileAsync(RsaPublicKeyDto publicKeyDto, string algorithm, string fileName,
            byte[] encryptedFileContent);
    }
}
