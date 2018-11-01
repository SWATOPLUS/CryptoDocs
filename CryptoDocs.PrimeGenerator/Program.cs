using System;
using CryptoDocs.Shared;
using CryptoDocs.Shared.Rsa;

namespace CryptoDocs.PrimeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var generator = new BigIntegerService();

            var p = generator.GetPrime();
            var q = generator.GetPrime();

            var info = RsaPrivateInfo.Generate(p, q);

            Console.WriteLine("P");
            Console.WriteLine(info.P);
            Console.WriteLine("Q");
            Console.WriteLine(info.Q);
            Console.WriteLine("E");
            Console.WriteLine(info.E);
            Console.WriteLine("K");
            Console.WriteLine(info.K);
        }
    }
}
