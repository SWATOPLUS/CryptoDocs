using System;
using System.Numerics;
using System.Security.Cryptography;

namespace CryptoDocs.Shared
{
    public class BigIntegerService : IBigIntegerService
    {
        private readonly RNGCryptoServiceProvider _rng;

        public BigIntegerService()
        {
            _rng = new RNGCryptoServiceProvider();
        }

        public BigInteger GetPrime()
        {
            var tries = 0;

            while (true)
            {
                Console.WriteLine($"trying {tries++}");
                var candidate = GetRandom();

                if (IsProbablyPrime(candidate, 100))
                {
                    return candidate;
                }
            }
        }

        public BigInteger GetRandom()
        {
            var bytes = new byte[1024 / 8];
            _rng.GetBytes(bytes);
            bytes[bytes.Length - 1] = (byte)((bytes[bytes.Length - 1] >> 2) | 0b_0100_0000);
            return new BigInteger(bytes);
        }
        public bool IsProbablyPrime(BigInteger value, uint witnesses = 10)
        {
            if (value <= 1)
            {
                return false;
            }

            var d = value - 1;
            var s = 0;

            while (d % 2 == 0)
            {
                d /= 2;
                s += 1;
            }

            var bytes = new byte[value.ToByteArray().Length];
            for (var i = 0; i < witnesses; i++)
            {
                BigInteger a;
                do
                {
                    _rng.GetBytes(bytes);

                    a = new BigInteger(bytes);
                }
                while (a < 2 || a >= value - 2);

                var x = BigInteger.ModPow(a, d, value);
                if (x == 1 || x == value - 1)
                {
                    continue;
                }

                for (var r = 1; r < s; r++)
                {
                    x = BigInteger.ModPow(x, 2, value);

                    if (x == 1)
                    {
                        return false;
                    }

                    if (x == value - 1)
                    {
                        break;
                    }
                }

                if (x != value - 1)
                {
                    return false;
                }
            }

            return true;
        }
    }
}