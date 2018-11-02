using System;
using System.Linq;

namespace CryptoDocs.Shared
{
    public class IdeaCryptoProvider : ISymmetricCryptoProvider
    {
        public byte[] Encrypt(byte[] data, byte[] key)
        {
            var size = BitConverter.GetBytes(data.Length);
            var empties = new byte[8 - (data.Length + size.Length) % Idea.BlockSizeBytes];
            var idea = new Idea(key, true);

            var result = size.Concat(data).Concat(empties).ToArray();
            idea.crypt(data);

            return result;
        }

        public byte[] Decrypt(byte[] data, byte[] key)
        {
            var idea = new Idea(key, false);
            var result = data.ToArray();
            idea.crypt(data);

            var size = BitConverter.ToInt32(result, 0);

            return result
                .Skip(sizeof(int))
                .Take(size)
                .ToArray();
        }
    }
}