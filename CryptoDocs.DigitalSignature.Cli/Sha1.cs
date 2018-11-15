using System;
using System.Collections.Generic;
using System.Linq;

namespace CryptoDocs.DigitalSignature.Cli
{
    public class Sha1Hash
    {
        public const int DigestSize = 20;

        public const int BlockSize = 64;

        private uint[] _h = {0x67452301, 0xEFCDAB89, 0x98BADCFE, 0x10325476, 0xC3D2E1F0};
        private byte[] _unprocessedBatch = { };
        private int _messageLength = 0;


        public Sha1Hash Append(IEnumerable<byte> messageBatch)
        {
            var batch = _unprocessedBatch.Concat(messageBatch).ToArray();
            var processedBytes = 0;

            while (batch.Length - processedBytes > BlockSize)
            {
                _h = ProcessChunk(batch.Skip(processedBytes).Take(BlockSize).ToArray(), _h);
                processedBytes += BlockSize;
                _messageLength += BlockSize;
            }

            _unprocessedBatch = batch.Skip(processedBytes).ToArray();
            return this;
        }

        // Produce the final hash value (big-endian) as a bytes object
        public byte[] GetDigest()
        {
            return GetDigestUInt32().SelectMany(UInt32_To_BE).ToArray();
        }

        // Return finalized digest variables for the data processed so far.
        private uint[] GetDigestUInt32()
        {
            var message = new List<byte>(_unprocessedBatch);
            var messageLength = _messageLength + message.Count;
            // Append the bit '1' to the messageBatch
            message.Add(0x80);
            // Append 0 <= k < 512 bits '0', so that the resulting messageBatch length (in bytes)
            // is congruent to 56 (mod 64)
            var zeroBytesCount = (56 - (messageLength + 1) % BlockSize) % BlockSize;
            message.AddRange(Enumerable.Range(0, zeroBytesCount).Select(x => (byte) 0));
            // Append length of messageBatch (before pre-processing), in bits, as 64-bit big-endian integer
            var messageBitLength = messageLength * 8;
            message.AddRange(BitConverter.GetBytes((ulong) messageBitLength));
            // Process the final chunk
            // At this point, the length of the messageBatch is either 64 or 128 bytes.
            var h = ProcessChunk(message.Take(64).ToArray(), _h);

            if (message.Count == 64)
            {
                return h;
            }

            return ProcessChunk(message.Skip(64).ToArray(), h);
        }

        private static uint[] ProcessChunk(byte[] chunk, uint[] h)
        {
            h = h.ToArray();
            var w = new uint[80];

            // Break chunk into sixteen 4-byte big-endian words w[i]
            foreach (var i in Enumerable.Range(0, 16))
            {
                //w[i] = chunk[i + 3] + (uint)256 * chunk[i + 2] + (uint)256 * 256 * chunk[i + 1] + (uint)256 * 256 * 256 * chunk[i + 0];
                w[i] = BE_To_UInt32(chunk.Skip(i * 4).Take(4).ToArray());
            }

            foreach (var i in Enumerable.Range(16, 80 - 16))
            {
                w[i] = LeftRotate(w[i - 3] ^ w[i - 8] ^ w[i - 14] ^ w[i - 16], 1);
            }

            var a = h[0];
            var b = h[1];
            var c = h[2];
            var d = h[3];
            var e = h[4];

            foreach (var i in Enumerable.Range(0, 80))
            {
                var k = (uint)0;
                var f = (uint)0;

                if (0 <= i && i <= 19)
                {
                    f = (b & c) | (~b & d);
                    k = 0x5A827999;
                }
                else if (20 <= i && i <= 39)
                {
                    f = b ^ c ^ d;
                    k = 0x6ED9EBA1;
                }
                else if (40 <= i && i <= 59)
                {
                    f = b & c | b & d | c & d;
                    k = 0x8F1BBCDC;
                }
                else if (60 <= i && i <= 79)
                {
                    f = b ^ c ^ d;
                    k = 0xCA62C1D6;
                }

                var temp = LeftRotate(a, 5) + f + e + k + w[i];
                e = d;
                d = c;
                c = LeftRotate(b, 30);
                b = a;
                a = temp;
            }

            h[0] = h[0] + a;
            h[1] = h[1] + b;
            h[2] = h[2] + c;
            h[3] = h[3] + d;
            h[4] = h[4] + e;

            return h;
        }

        private static uint LeftRotate(uint n, int b)
        {
            return (n << b) | (n >> (32 - b));
        }
        private static uint BE_To_UInt32(byte[] bs)
        {
            uint n = (uint)bs[0] << 24;
            n |= (uint)bs[1] << 16;
            n |= (uint)bs[2] << 8;
            n |= (uint)bs[3];
            return n;
        }

        private static byte[] UInt32_To_BE(uint n)
        {
            var bs = new byte[4];

            bs[0] = (byte)(n >> 24);
            bs[1] = (byte)(n >> 16);
            bs[2] = (byte)(n >> 8);
            bs[3] = (byte)(n);

            return bs;
        }
    }
}
