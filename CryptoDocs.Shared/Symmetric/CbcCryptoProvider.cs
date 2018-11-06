using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using MoreLinq;

namespace CryptoDocs.Shared.Symmetric
{
    public class CbcCryptoProvider : IDataCryptoProvider
    {
        private readonly IBlockCryptoProvider _block;
        private readonly RNGCryptoServiceProvider _rng;

        public CbcCryptoProvider
        (
            IBlockCryptoProvider block
        )
        {
            _block = block;
            _rng = new RNGCryptoServiceProvider();
        }

        public byte[] Encrypt(byte[] data, byte[] key)
        {
            var iv = BuildIv();
            var blocks = BuildBlocks(data);

            var cipherData = new List<byte>();
            var lastIv = iv;

            foreach (var block in blocks)
            {
                var cipherBlock = _block.Encrypt(XorArrays(block, lastIv), key);

                cipherData.AddRange(cipherBlock);

                lastIv = cipherBlock;
            }
            
            return iv.Concat(cipherData).ToArray();
        }

        private byte[][] BuildBlocks(byte[] data)
        {
            var sizePart = BitConverter.GetBytes(data.Length);
            var dummyBytesCount = _block.BlockSize - (sizePart.Length + data.Length) % _block.BlockSize;
            var dummyBytes = new byte[dummyBytesCount];
            _rng.GetBytes(dummyBytes);

            return sizePart.Concat(data).Concat(dummyBytes)
                .Batch(_block.BlockSize)
                .Select(x => x.ToArray())
                .ToArray();
        }

        public byte[] Decrypt(byte[] data, byte[] key)
        {
            var iv = data.Take(_block.BlockSize).ToArray();
            var blocks = data.Skip(_block.BlockSize).Batch(_block.BlockSize).Select(x=> x.ToArray());

            var sourceData = new List<byte>();

            foreach (var block in blocks)
            {
                var sourceBlock = XorArrays(_block.Decrypt(block, key), iv);

                sourceData.AddRange(sourceBlock);

                iv = block;
            }

            var size = BitConverter.ToInt32(sourceData.Take(sizeof(int)).ToArray(), 0);

            return sourceData.Skip(sizeof(int)).Take(size).ToArray();
        }

        private byte[] BuildIv()
        {
            var iv = new byte[_block.BlockSize];
            _rng.GetBytes(iv);

            return iv;
        }

        private byte[] XorArrays(byte[] first, byte[] second)
        {
            if (first.Length != second.Length)
            {
                throw new InvalidOperationException("Array length should be equal");
            }

            var result = new byte[first.Length];

            for (var i = 0; i < first.Length; i++)
            {
                result[i] = (byte)(first[i] ^ second[i]);
            }

            return result;
        }
    }
}
