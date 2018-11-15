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
        private const string CheckCommand = "check";
        private const string HashCommand = "hash";
        private const string GenRsaCommand = "genrsa";

        private const string UsageString =
            "Usage: hash <file> / signFile <file> <privkey> / validate <file> <signFile> <pubkey> / genrsa <keyname>";

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
                    SignRsa(args[1], args[2]);
                    break;
                case CheckCommand when args.Length > 3:
                    CheckRsa(args[1], args[2], args[3]);
                    break;
                default:
                    Console.WriteLine(UsageString);
                    break;
            }
        }

        private static void CheckRsa(string file, string signFile, string publicKeyFile)
        {
            var bigIntegerPair = ReadBigIntegerPair(publicKeyFile);
            var publicKey = new RsaPublicKey {N = bigIntegerPair.Item1, E = bigIntegerPair.Item2};
            var hash = GetFileHash(file);
            var sign = File.ReadAllBytes(signFile);
            var signHash = publicKey.Decrypt(sign);

            if (signHash.SequenceEqual(hash))
            {
                Console.WriteLine("Sign is valid");
            }
            else
            {
                Console.WriteLine("Sign is invalid");
            }
        }

        private static void SignRsa(string file, string privateKeyFile)
        {
            var bigIntegerPair = ReadBigIntegerPair(privateKeyFile);
            var privateKey = new RsaPrivateKey {N = bigIntegerPair.Item1, D = bigIntegerPair.Item2};
            var hash = GetFileHash(file);
            var sign = privateKey.Encrypt(hash);

            File.WriteAllBytes($"{file}.rsign", sign);
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
