using System.Numerics;

namespace CryptoDocs.Shared.Rsa
{
    public class RsaPublicKey : RsaKeyBase
    {
        public BigInteger E { get; set; }

        public override BigInteger Pow => E;
    }
}
