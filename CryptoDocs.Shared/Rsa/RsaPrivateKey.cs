using System.Numerics;

namespace CryptoDocs.Shared.Rsa
{
    public class RsaPrivateKey
    {
        public BigInteger N { get; set; }

        public BigInteger D { get; set; }
    }
}
