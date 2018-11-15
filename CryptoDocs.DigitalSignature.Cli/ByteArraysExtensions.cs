using System;
using System.Collections.Generic;
using System.Linq;

namespace CryptoDocs.DigitalSignature.Cli
{
    public static class ByteArraysExtensions
    {
        public static string ToHexString(this IEnumerable<byte> source)
        {
            var chars = source
                .SelectMany(x => new[] { DigitToChar(x / 16), DigitToChar(x % 16)})
                .ToArray();

            return new string(chars);
        }

        private static char DigitToChar(int digit)
        {
            if (0 <= digit && digit < 10)
            {
                return (char)('0' + digit);
            }

            if (10 <= digit && digit < 16)
            {
                return (char) ('a' + digit - 10);
            }

            throw new InvalidOperationException();
        }

        public static byte[] GetSha1Hash(this IEnumerable<byte> bytes)
        {
            return new Sha1Hash().Append(bytes).GetDigest();
        }
    }
}