using System.Numerics;

namespace CryptoDocs.DigitalSignature.Cli.EllipticCurve
{
    public static class BigIntegerExtensions
    {
        public static BigInteger ModInverse(this BigInteger a, BigInteger n)
        {
            var i = n;
            var v = BigInteger.Zero;
            var d = BigInteger.One;

            while (a > 0)
            {
                var t = i / a;
                var x = a;
                a = i % x;
                i = x;
                x = d;
                d = v - t * x;
                v = x;
            }

            v %= n;

            if (v < 0)
            {
                v = (v + n) % n;
            }

            return v;
        }

        public static BigInteger PositiveMod(this BigInteger a, BigInteger mod)
        {
            return (a % mod + mod) % mod;
        }

        public static byte[] ToUnsignedByteArray(this BigInteger a)
        {
            return a.ToByteArray(true);
        }

        public static BigInteger ToUnsignedBigInteger(this byte[] a)
        {
            return new BigInteger(a, true);
        }
    }
}