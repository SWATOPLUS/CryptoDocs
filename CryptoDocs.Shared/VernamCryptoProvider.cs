namespace CryptoDocs.Shared
{
    public class VernamCryptoProvider : ISymmetricCryptoProvider
    {
        public byte[] Encrypt(byte[] data, byte[] key, byte[] iv)
        {
            var superKey = new byte[key.Length];

            for (var i = 0; i < superKey.Length; i++)
            {
                superKey[i] = (byte)(key[i] ^ iv[i]);
            }

            var result = new byte[data.Length];

            for (var i = 0; i < result.Length; i++)
            {
                result[i] = (byte)(data[i] ^ superKey[i % superKey.Length]);
            }

            return result;
        }

        public byte[] Decrypt(byte[] data, byte[] key, byte[] iv)
        {
            return Encrypt(data, key, iv);
        }
    }
}