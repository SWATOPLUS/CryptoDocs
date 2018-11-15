using System;
using System.IO;
using System.Linq;
using System.Numerics;
using CryptoDocs.Shared;
using CryptoDocs.Shared.Rsa;

namespace CryptoDocs.DigitalSignature.Cli
{
    internal static class Program
    {
        private const string SignCommand = "sign";
        private const string ValidateCommand = "command";
        private const string HashCommand = "hash";
        private const string GenRsaCommand = "genrsa";

        private const string UsageString =
            "Usage: hash <file> / sign <file> <privkey> / validate <file> <sign> <pubkey> / genrsa <keyname>";

        private static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine(UsageString);
                return;
            }

            switch (args[0])
            {
                case HashCommand when args.Length > 1:
                    Hash(args[1]);
                    break;
                case GenRsaCommand when args.Length > 1:
                    GenRsa(args[1]);
                    break;
                case SignCommand when args.Length > 2:
                    Sign(args[1], args[2]);
                    break;
                case ValidateCommand when args.Length > 3:
                    Validate(args[1], args[2], args[3]);
                    break;
                default:
                    Console.WriteLine(UsageString);
                    break;
            }
        }

        private static void Validate(string s, string s1, string ss)
        {
            throw new NotImplementedException();
        }

        private static void Sign(string s, string b)
        {
            throw new NotImplementedException();
        }

        private static void GenRsa(string keyName)
        {
            var provider = new BigIntegerService();
            var keyPair = RsaKeyPair.Generate(provider.GetPrime(), provider.GetPrime());

            WriteBigIntegerPair($"{keyName}.rpub", keyPair.N, keyPair.E);
            WriteBigIntegerPair($"{keyName}.rpri", keyPair.N, keyPair.D);
        }

        private static (BigInteger, BigInteger) ReadBigIntegerPair(string fileName)
        {
            var lines = File.ReadLines(fileName).Take(2).ToArray();

            var a = BigInteger.Parse(lines[0]);
            var b = BigInteger.Parse(lines[1]);

            return (a, b);
        }

        private static void WriteBigIntegerPair(string fileName, BigInteger a, BigInteger b)
        {
            var lines = new[] {a.ToString(), b.ToString()};

            File.WriteAllLines(fileName, lines);
        }

        private static void Hash(string fileName)
        {
            var digest = GetFileHash(fileName);

            File.WriteAllText($"{fileName}.sha1", digest.ToHexString());
        }

        private static byte[] GetFileHash(string path)
        {
            var sha = new Sha1Hash();

            using (var stream = File.OpenRead(path))
            {
                var batch = new byte[1024*1024];

                var count = stream.Read(batch, 0, batch.Length);

                sha.Append(batch.Take(count).ToArray());
            }

            return sha.GetDigest();
        }
    }
}
