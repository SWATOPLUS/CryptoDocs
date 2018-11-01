using System.Numerics;

namespace CryptoDocs.Shared.Rsa
{
    public class RsaPublicKey
    {
        public BigInteger N { get; set; }

        public BigInteger E { get; set; }
    }
}
