using System;
using System.Numerics;

namespace CryptoDocs.Shared.Rsa
{
    public class RsaPrivateInfo
    {
        private RsaPrivateInfo() { }

        private const string PrimesRestrictions = "Primes p and q must not be equal. Must be grater then 1";

        public static RsaPrivateInfo Generate(BigInteger p, BigInteger q)
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

                if (gcd == 1)
                {
                    continue;
                }

                for (var k = BigInteger.One; k < 10000; k++)
                {
                    var mod = (k * euler + 1) % e;

                    if (mod == 0)
                    {
                        return new RsaPrivateInfo
                        {
                            P = p,
                            Q = q,
                            E = e,
                            K = k
                        };
                    }
                }
            }
        }

        public BigInteger P { get; set; }

        public BigInteger Q { get; set; }

        public BigInteger E { get; set; }

        public BigInteger K { get; set; }

        public RsaPublicKey PublicKey => new RsaPublicKey
        {
            E = E,
            N = P * Q
        };

        public RsaPrivateKey PrivateKey => new RsaPrivateKey
        {
            D = (K * (P-1) * (Q-1) + 1) / E,
            N = P * Q
        };
    }
}
