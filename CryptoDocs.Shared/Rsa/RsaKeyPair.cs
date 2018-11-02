using System;
using System.Linq;
using System.Numerics;

namespace CryptoDocs.Shared.Rsa
{
    public class RsaKeyPair
    {
        private RsaKeyPair()
        {
        }

        public RsaKeyPair(string s)
        {
            if (s.First() != '(' || s.Last() != ')')
            {
                throw new InvalidOperationException();
            }

            var parts = s.Substring(1,s.Length-2).Split(',');

            if (parts.Length != 3)
            {
                throw new InvalidOperationException();
            }

            N = BigInteger.Parse(parts[0]);
            E = BigInteger.Parse(parts[1]);
            D = BigInteger.Parse(parts[2]);
        }

        public override string ToString()
        {
            return $"({N}, {E}, {D})";
        }

        private const string PrimesRestrictions = "Primes p and q must not be equal. Must be grater then 1";

        public static RsaKeyPair Generate(BigInteger p, BigInteger q)
        {
            if (p == q || p < 1 || q < 1)
            {
                throw new ArgumentException(PrimesRestrictions);
            }

            var euler = (p - 1) * (q - 1);
            var e = BigInteger.One;

            while (true)
            {
                e += 2;

                var gcd = BigInteger.GreatestCommonDivisor(euler, e);

                if (gcd != 1)
                {
                    continue;
                }

                for (var k = BigInteger.One; k < euler; k++)
                {
                    var mod = (k * euler + 1) % e;

                    if (mod == 0)
                    {
                        return new RsaKeyPair
                        {
                            N = p * q,
                            E = e,
                            D = (k*euler + 1) / e
                        };
                    }
                }
            }
        }

        //ax+by = d; d = gcd(a,b);
        private static (BigInteger d, BigInteger x, BigInteger y) GcdExt(BigInteger a, BigInteger b)
        {
            if (b == 0)
            {
                return (a, 1, 0);
            }

            var (d, x, y) = GcdExt(b, a % b);
            var s = y;
            y = x - a / b * y;
            x = s;

            return (d, x, y);
        }

        public BigInteger N { get; private set; }

        public BigInteger E { get; private set; }

        public BigInteger D { get; private set; }

        public RsaPublicKey PublicKey => new RsaPublicKey
        {
            E = E,
            N = N
        };

        public RsaPrivateKey PrivateKey => new RsaPrivateKey
        {
            D = D,
            N = N
        };
    }
}
