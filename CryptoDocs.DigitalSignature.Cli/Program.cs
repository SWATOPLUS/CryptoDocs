using System;
using System.IO;
using System.Linq;
using System.Numerics;
using CryptoDocs.DigitalSignature.Cli.EllipticCurve;
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
        private const string GenEcCommand = "genec";

        private static readonly string UsageString =
            $"Usage: {HashCommand} <file> / {SignCommand} <file> <privkey> " +
            $"/ {CheckCommand} <file> <signFile> <pubkey> / {GenRsaCommand} <keyname> / {GenEcCommand} <keyname>";

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
                case GenEcCommand when args.Length > 1:
                    GenEc(args[1]);
                    break;
                case SignCommand when args.Length > 2:
                    if (args[2].EndsWith(".ecpri"))
                    {
                        SignRsa(args[1], args[2]);
                    }
                    else
                    {
                        SignRsa(args[1], args[2]);
                    }
                    break;
                case CheckCommand when args.Length > 3:
                    CheckRsa(args[1], args[2], args[3]);
                    break;
                default:
                    Console.WriteLine(UsageString);
                    break;
            }
        }

        private static void GenEc(string name)
        {
            var g = EcPoint.DefaultInstance;
            var n = g.GeneratePrivateKey(160 / 8);
            File.WriteAllText($"{name}.ecpri", n.ToString());
            var q = g.Multiply(n);
            File.WriteAllText($"{name}.ecpub", q.ToString());
        }

        private static void SignEc(string file, string privateKeyFile)
        {
            var n = BigInteger.Parse(File.ReadAllText(privateKeyFile));
            var hash = GetFileHash(file);
            var sign = EcPoint.DefaultInstance.SingGen(n, hash);
            var dto = new EcPoint {X = sign.R, Y = sign.S};
            File.WriteAllText($"{file}.ecsign", dto.ToString());
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
            var keyPair = RsaKeyPair.Generate(provider.GetPrime(1024 / 8), provider.GetPrime(1024 / 8));

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
