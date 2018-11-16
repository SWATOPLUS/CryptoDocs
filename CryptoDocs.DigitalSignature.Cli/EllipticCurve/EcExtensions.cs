using System;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using CryptoDocs.Shared;

namespace CryptoDocs.DigitalSignature.Cli.EllipticCurve
{
    public static class EcExtensions
    {
        public static bool SingVer(this EcPoint g, EcPoint p, byte[] hash, EcSign sign)
        {
            var r = sign.R;
            var s = sign.S;

            if (r < 1 || r > (g.M - 1) || s < 1 || s > g.M - 1)
            {
                return false;
            }

            var w = s.ModInverse(g.M);
            var u1 = hash.ToUnsignedBigInteger() % g.M;
            var u2 = r * w % g.M;

            var rStar = (g.Multiply(u1) + p.Multiply(u2)).X;

            return rStar == r;
        }

        public static EcSign SingGen(this EcPoint g, BigInteger n, byte[] hash)
        {
            var h = hash.ToUnsignedBigInteger();
            var k = GetRandomBetween(2, g.M);
            var kg = g.Multiply(k);
            BigInteger r;
            BigInteger s;

            while (true)
            {
                r = kg.X % g.M;

                if (r == 2)
                {
                    continue;
                }

                s = (k.ModInverse(g.M) * (h + n * r)).PositiveMod(g.M);

                if (s != 0)
                {
                    break;
                }
            }

            return new EcSign
            {
                R = r,
                S = s
            };
        }

        public static BigInteger GeneratePrivateKey(this EcPoint g, int byteSize)
        {
            while (true)
            {
                var d = BigIntegerService.GetRandom(byteSize);

                if (0 < d && d < g.M)
                {
                    return d;
                }
            }
        }

        private static readonly IBigIntegerService BigIntegerService = new BigIntegerService();
        private static readonly RandomNumberGenerator SecureRandom = new RNGCryptoServiceProvider();

        internal static BigInteger GetRandomBetween(BigInteger min, BigInteger border)
        {
            var bytes = border.ToUnsignedByteArray();
            var lastByte = bytes.Last();


            while (true)
            {
                SecureRandom.GetBytes(bytes);
                bytes[bytes.Length - 1] = lastByte;
                var k = bytes.ToUnsignedBigInteger();

                if (min <= k && k < border)
                {
                    return k;
                }
            }
        }
    }
}
