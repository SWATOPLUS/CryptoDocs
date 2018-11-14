using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Threading.Tasks;
using CryptoDocs.Abstractions;
using CryptoDocs.Shared.Dto;
using Microsoft.AspNetCore.Mvc;

namespace CryptoDocs.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class DataController : Controller
    {
        private readonly ICryptoDocsService _cryptoDocsService;

        private static readonly RandomNumberGenerator SecureRandom = RandomNumberGenerator.Create();

        private static readonly IDictionary<(BigInteger, BigInteger), byte[]> SessionKeyCache =
            new ConcurrentDictionary<(BigInteger, BigInteger), byte[]>();

        public DataController
        (
            ICryptoDocsService cryptoDocsService
        )
        {
            _cryptoDocsService = cryptoDocsService;
        }

        public Task<byte[]> GetEncryptedSessionKey([FromBody]RsaPublicKeyDto publicKeyDto)
        {
            return _cryptoDocsService.GetEncryptedSessionKeyAsync(publicKeyDto);
        }

        [HttpPost("{algorithm}/{fileName}")]
        public Task<byte[]> GetFile(string algorithm, string fileName, [FromBody] RsaPublicKeyDto publicKeyDto)
        {
            return _cryptoDocsService.GetEncryptedFileAsync(publicKeyDto, algorithm, ClearFileName(fileName));
        }

        [HttpPost("{algorithm}/{fileName}")]
        public Task SetFile(string algorithm, string fileName, [FromBody] FileRequest request)
        {
            var encryptedFileContent = Convert.FromBase64String(request.EncryptedContentBase64);

            return _cryptoDocsService.SetEncryptedFileAsync(request.PublicKeyDto, algorithm, ClearFileName(fileName), encryptedFileContent);
        }

        private static string ClearFileName(string fileName)
        {
            return fileName.Split('/', '\\').Last();
        }
    }
}
