using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using System.Threading.Tasks;
using CryptoDocs.Abstractions;
using CryptoDocs.Shared.Dto;
using CryptoDocs.Shared.Rsa;

namespace CryptoDocs.Services
{
    public class CryptoDocsService : ICryptoDocsService
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly ICryptoProviderResolver _cryptoProviderResolver;
        private RandomNumberGenerator SecureRandom { get; } = new RNGCryptoServiceProvider();

        private readonly IDictionary<(BigInteger, BigInteger), byte[]> _sessionKeyCache =
            new ConcurrentDictionary<(BigInteger, BigInteger), byte[]>();


        public CryptoDocsService
        (
            IFileStorageService fileStorageService,
            ICryptoProviderResolver cryptoProviderResolver
        )
        {
            _fileStorageService = fileStorageService;
            _cryptoProviderResolver = cryptoProviderResolver;
        }

        public Task<byte[]> GetEncryptedSessionKeyAsync(RsaPublicKeyDto publicKeyDto)
        {
            return Task.Run(() =>
            {
                var sessionKey = new byte[64];
                SecureRandom.GetBytes(sessionKey);
                var publicKey = publicKeyDto.ToModel();
                _sessionKeyCache[(publicKey.N, publicKey.E)] = sessionKey;

                return publicKey.Encrypt(sessionKey);
            });
        }

        public async Task<byte[]> GetEncryptedFileAsync(RsaPublicKeyDto publicKeyDto, string algorithm, string fileName)
        {
            var data = await _fileStorageService.GetFileAsync(fileName);
            var publicKey = publicKeyDto.ToModel();
            var sessionKey = _sessionKeyCache[(publicKey.N, publicKey.E)];
            var cryptoProvider = _cryptoProviderResolver.GetCryptoProvider(algorithm);
            return cryptoProvider.Encrypt(data, sessionKey);
        }

        public async Task SetEncryptedFileAsync(RsaPublicKeyDto publicKeyDto, string algorithm, string fileName,
            byte[] encryptedFileContent)
        {
            var publicKey = publicKeyDto.ToModel();
            var sessionKey = _sessionKeyCache[(publicKey.N, publicKey.E)];
            var cryptoProvider = _cryptoProviderResolver.GetCryptoProvider(algorithm);
            var data = cryptoProvider.Decrypt(encryptedFileContent, sessionKey);
            await _fileStorageService.SaveFileAsync(fileName, data);
        }
    }
}
