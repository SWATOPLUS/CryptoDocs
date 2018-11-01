using System;
using CryptoDocs.Shared;

namespace CryptoDocs.PrimeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var generator = new BigIntegerService();

            var num = generator.GetPrime();
            Console.WriteLine(num);
            Console.ReadLine();
        }
    }
}
