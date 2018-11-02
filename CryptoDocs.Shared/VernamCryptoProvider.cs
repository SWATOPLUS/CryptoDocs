namespace CryptoDocs.Shared
{
    public class VernamCryptoProvider : ISymmetricCryptoProvider
    {
        public byte[] Encrypt(byte[] data, byte[] key)
        {
            var result = new byte[data.Length];

            for (var i = 0; i < result.Length; i++)
            {
                result[i] = (byte)(data[i] ^ key[i % key.Length]);
            }

            return result;
        }

        public byte[] Decrypt(byte[] data, byte[] key)
        {
            return Encrypt(data, key);
        }
    }
}