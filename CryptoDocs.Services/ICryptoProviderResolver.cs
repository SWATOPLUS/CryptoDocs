using CryptoDocs.Shared.Symmetric;

namespace CryptoDocs.Services
{
    public interface ICryptoProviderResolver
    {
        IDataCryptoProvider GetCryptoProvider(string algorithm);
    }
}