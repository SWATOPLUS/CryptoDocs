using System;

namespace CryptoDocs.Shared
{
    public interface ISymmetricCryptoProvider
    {
        byte[] Encrypt(byte[] data, byte[] key, byte[] iv);
        byte[] Decrypt(byte[] data, byte[] key, byte[] iv);
    }
}
