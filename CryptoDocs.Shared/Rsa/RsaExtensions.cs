using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using MoreLinq;

namespace CryptoDocs.Shared.Rsa
{
    public static class RsaExtensions
    {
        public static byte[] Encrypt(this RsaKeyBase key, byte[] sourceData)
        {
            var batches = sourceData.Batch(127);
            var encryptedData = new List<byte>();

            foreach (var batch in batches)
            {
                var m = new BigInteger(MoreEnumerable.Append(batch, (byte)127).ToArray());
                var c = BigInteger.ModPow(m, key.Pow, key.N);

                var dataBatch = c.ToByteArray();
                var zerosCount = 256 - dataBatch.Length;

                encryptedData.AddRange(dataBatch);
                encryptedData.AddRange(Enumerable.Range(0, zerosCount).Select(x => (byte) 0));
            }

            return encryptedData.ToArray();
        }

        public static byte[] Decrypt(this RsaKeyBase key, byte[] encryptedData)
        {
            var batches = encryptedData.Batch(256);
            var sourceData = new List<byte>();

            foreach (var batch in batches)
            {
                var c = new BigInteger(batch.ToArray());
                var m = BigInteger.ModPow(c, key.Pow, key.N);

                var dataBatch = m.ToByteArray();

                sourceData.AddRange(dataBatch.Take(dataBatch.Length - 1));
            }

            return sourceData.ToArray();
        }
    }
}
