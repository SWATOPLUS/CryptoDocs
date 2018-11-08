using System.Collections.Generic;

namespace CryptoDocs.Shared.Symmetric
{
    public interface IBlockCryptoProvider
    {
        int BlockSize { get; }

        int KeySize { get; }

        byte[] Encrypt(IReadOnlyCollection<byte> block, IReadOnlyCollection<byte> key);

        byte[] Decrypt(IReadOnlyCollection<byte> block, IReadOnlyCollection<byte> key);
    }
}
