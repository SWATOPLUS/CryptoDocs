using System.Numerics;

namespace CryptoDocs.Shared.Rsa
{
    public class RsaPrivateKey : RsaKeyBase
    {
        public BigInteger D { get; set; }

        public override BigInteger Pow => D;
    }

    public abstract class RsaKeyBase
    {
        public BigInteger N { get; set; }

        public abstract BigInteger Pow { get; }
    }
}
