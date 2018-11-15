using System;

namespace CryptoDocs.DigitalSignature.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new byte[0].GetSha1Hash().ToHexString());
        }
    }
}
