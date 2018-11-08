using System.Collections.Generic;

namespace CryptoDocs.Shared.Symmetric
{
    public interface IDataCryptoProvider
    {
        byte[] Encrypt(IReadOnlyCollection<byte> sourceData, IEnumerable<byte> key);

        byte[] Decrypt(IReadOnlyCollection<byte> encryptedData, IEnumerable<byte> key);
    }
}
