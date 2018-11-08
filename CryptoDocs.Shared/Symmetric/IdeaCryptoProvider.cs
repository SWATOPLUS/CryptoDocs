using System;
using System.Collections.Generic;
using System.Linq;

namespace CryptoDocs.Shared.Symmetric
{
    public class IdeaCryptoProvider : IBlockCryptoProvider
    {
        public int BlockSize => Idea.BlockSizeBytes;
        public int KeySize => Idea.KeySizeBytes;

        public byte[] Encrypt(IReadOnlyCollection<byte> block, IReadOnlyCollection<byte> key)
        {
            Validate(block, key);

            var idea = new Idea(key.ToArray(), true);
            var result = block.ToArray();
            idea.crypt(result);

            return result;
        }

        public byte[] Decrypt(IReadOnlyCollection<byte> block, IReadOnlyCollection<byte> key)
        {
            Validate(block, key);

            var idea = new Idea(key.ToArray(), false);
            var result = block.ToArray();
            idea.crypt(result);

            return result;
        }

        private void Validate(IReadOnlyCollection<byte> block, IReadOnlyCollection<byte> key)
        {
            if (block.Count != BlockSize)
            {
                throw new ArgumentException($"{nameof(block)} size should be {BlockSize} bytes", nameof(block));
            }

            if (key.Count != KeySize)
            {
                throw new ArgumentException($"{nameof(key)} size should be {KeySize} bytes", nameof(key));
            }
        }
    }
}