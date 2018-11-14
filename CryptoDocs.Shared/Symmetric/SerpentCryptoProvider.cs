using System;
using System.Collections.Generic;
using System.Linq;

namespace CryptoDocs.Shared.Symmetric
{
    public class SerpentCryptoProvider: IBlockCryptoProvider
    {
        public int BlockSize => Serpent.BlockSize;
        public int KeySize => Serpent.KeySize;

        public byte[] Encrypt(IReadOnlyCollection<byte> block, IReadOnlyCollection<byte> key)
        {
            Validate(block, key);

            var serpent = new Serpent(key.ToArray());
            var result = new byte[BlockSize];
            serpent.EncryptBlock(block.ToArray(), 0, BlockSize, result, 0);

            return result;
        }

        public byte[] Decrypt(IReadOnlyCollection<byte> block, IReadOnlyCollection<byte> key)
        {
            Validate(block, key);

            var serpent = new Serpent(key.ToArray());
            var result = new byte[BlockSize];
            serpent.DecryptBlock(block.ToArray(), 0, BlockSize, result, 0);

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
