namespace CryptoDocs.Shared.Symmetric
{
    public interface IDataCryptoProvider
    {
        byte[] Encrypt(byte[] data, byte[] key);

        byte[] Decrypt(byte[] data, byte[] key);
    }
}
