using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using System.Threading.Tasks;
using CryptoDocs.Abstractions;
using CryptoDocs.Shared.Dto;
using CryptoDocs.Shared.Rsa;
using CryptoDocs.Shared.Symmetric;

namespace CryptoDocs.Services
{
    public class CryptoDocsService : ICryptoDocsService
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly IDataCryptoProvider _dataCryptoProvider = new OfbCryptoProvider(new AesCryptoProvider());
        private RandomNumberGenerator SecureRandom { get; } = new RNGCryptoServiceProvider();

        private readonly IDictionary<(BigInteger, BigInteger), byte[]> _sessionKeyCache =
            new ConcurrentDictionary<(BigInteger, BigInteger), byte[]>();


        public CryptoDocsService
        (
            IFileStorageService fileStorageService
        )
        {
            _fileStorageService = fileStorageService;
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
            return _dataCryptoProvider.Encrypt(data, sessionKey);
        }

        public async Task SetEncryptedFileAsync(RsaPublicKeyDto publicKeyDto, string algorithm, string fileName,
            byte[] encryptedFileContent)
        {
            var publicKey = publicKeyDto.ToModel();
            var sessionKey = _sessionKeyCache[(publicKey.N, publicKey.E)];
            var data = _dataCryptoProvider.Decrypt(encryptedFileContent, sessionKey);
            await _fileStorageService.SaveFileAsync(fileName, data);
        }
    }
}
