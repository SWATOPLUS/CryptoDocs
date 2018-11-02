using System;
using CryptoDocs.Shared;
using CryptoDocs.Shared.Rsa;

namespace CryptoDocs.PrimeGenerator
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var generator = new BigIntegerService();

            var p = generator.GetPrime();
            var q = generator.GetPrime();

            var keyPair = RsaKeyPair.Generate(p, q);
            Console.WriteLine("primes:");
            Console.WriteLine(p);
            Console.WriteLine(q);
            Console.WriteLine("key pair:");
            Console.WriteLine(keyPair);
        }
    }
}
