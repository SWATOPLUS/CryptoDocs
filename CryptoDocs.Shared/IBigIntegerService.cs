using System.Numerics;

namespace CryptoDocs.Shared
{
    public interface IBigIntegerService
    {
        bool IsProbablyPrime(BigInteger value, uint witnesses = 10);
        BigInteger GetRandom();

        BigInteger GetPrime();
    }
}