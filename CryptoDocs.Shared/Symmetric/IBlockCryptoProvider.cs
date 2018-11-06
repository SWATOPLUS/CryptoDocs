namespace CryptoDocs.Shared.Symmetric
{
    public interface IBlockCryptoProvider
    {
        int BlockSize { get; }

        int KeySize { get; }

        byte[] Encrypt(byte[] block, byte[] key);

        byte[] Decrypt(byte[] block, byte[] key);
    }
}
