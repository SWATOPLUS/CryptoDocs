using System;
using CryptoDocs.Shared.Symmetric;
using Xunit;

namespace CryptoDocs.Shared.Tests
{
    public class CbcWithIdeaCryptoProviderTests
    {
        private IDataCryptoProvider Subject { get; }
        private Random Random { get; }

        public CbcWithIdeaCryptoProviderTests()
        {
            Subject = new CbcCryptoProvider(new IdeaCryptoProvider());
            Random = new Random();
        }

        [Fact]
        public void Long1031LengthData_EncryptAndDecrypt_SourceAndDecryptedAreSame()
        {
            var key = new byte[16];
            var sourceData = new byte[1031];
            Random.NextBytes(sourceData);
            Random.NextBytes(key);

            var encryptedData = Subject.Encrypt(sourceData, key);

            var decryptedData = Subject.Decrypt(encryptedData, key);

            Assert.Equal(sourceData, decryptedData);
        }

        [Fact]
        public void Short5LengthData_EncryptAndDecrypt_SourceAndDecryptedAreSame()
        {
            var key = new byte[16];
            var sourceData = new byte[5];
            Random.NextBytes(sourceData);
            Random.NextBytes(key);

            var encryptedData = Subject.Encrypt(sourceData, key);

            var decryptedData = Subject.Decrypt(encryptedData, key);

            Assert.Equal(sourceData, decryptedData);
        }


        [Fact]
        public void Short2LengthData_EncryptAndDecrypt_SourceAndDecryptedAreSame()
        {
            var key = new byte[16];
            var sourceData = new byte[2];
            Random.NextBytes(sourceData);
            Random.NextBytes(key);

            var encryptedData = Subject.Encrypt(sourceData, key);

            var decryptedData = Subject.Decrypt(encryptedData, key);

            Assert.Equal(sourceData, decryptedData);
        }

        [Fact]
        public void ZeroLengthData_EncryptAndDecrypt_SourceAndDecryptedAreSame()
        {
            var key = new byte[16];
            var sourceData = new byte[0];
            Random.NextBytes(sourceData);
            Random.NextBytes(key);

            var encryptedData = Subject.Encrypt(sourceData, key);

            var decryptedData = Subject.Decrypt(encryptedData, key);

            Assert.Equal(sourceData, decryptedData);
        }
    }
}
