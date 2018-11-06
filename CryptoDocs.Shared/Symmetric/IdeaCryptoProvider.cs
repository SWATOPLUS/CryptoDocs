using System;

namespace CryptoDocs.Shared.Symmetric
{
    public class IdeaCryptoProvider : IBlockCryptoProvider
    {
        public int BlockSize => Idea.BlockSizeBytes;
        public int KeySize => Idea.KeySizeBytes;

        public byte[] Encrypt(byte[] block, byte[] key)
        {
            Validate(block, key);

            var idea = new Idea(key, true);
            var result = block;
            idea.crypt(block);

            return result;
        }

        public byte[] Decrypt(byte[] block, byte[] key)
        {
            Validate(block, key);

            var idea = new Idea(key, false);
            var result = block;
            idea.crypt(block);

            return result;
        }

        private void Validate(byte[] block, byte[] key)
        {
            if (block.Length != BlockSize)
            {
                throw new ArgumentException(nameof(block), $"{nameof(block)} size should be {BlockSize} bytes");
            }

            if (key.Length != BlockSize)
            {
                throw new ArgumentException(nameof(key), $"{nameof(key)} size should be {BlockSize} bytes");
            }
        }
    }
}