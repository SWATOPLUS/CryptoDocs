using CryptoDocs.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Buffers.Text;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Threading.Tasks;
using CryptoDocs.Shared.Dto;
using CryptoDocs.Shared.Rsa;
using CryptoDocs.Shared.Symmetric;
using Microsoft.EntityFrameworkCore.Internal;
using MoreLinq;

namespace CryptoDocs.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    public class DataController : Controller
    {
        private readonly IBlockCryptoProvider _cryptoProvider;

        private const string DirectoryName = "files";

        public DataController
        (
            IBlockCryptoProvider cryptoProvider
        )
        {
            if (!System.IO.Directory.Exists(DirectoryName))
            {
                System.IO.Directory.CreateDirectory(DirectoryName);
            }

            _cryptoProvider = cryptoProvider;
        }

        public static RandomNumberGenerator SecureRandom = RandomNumberGenerator.Create();

        public static IDictionary<(BigInteger, BigInteger), byte[]> SessionKeyCache =
            new ConcurrentDictionary<(BigInteger, BigInteger), byte[]>();

        public byte[] GetEncryptedSessionKey([FromBody]RsaPublicKeyDto publicKeyDto)
        {
            var sessionKey = new byte[64];
            SecureRandom.GetBytes(sessionKey);
            var publicKey = publicKeyDto.ToModel();
            SessionKeyCache[(publicKey.N, publicKey.E)] = sessionKey;

            return publicKey.Encrypt(sessionKey);
        }

        [HttpPost("{fileName}")]
        public byte[] GetFile(string fileName, [FromBody] RsaPublicKeyDto publicKeyDto)
        {
            fileName = $"files/{fileName.Split('/', '\\').Last()}";
            var data = System.IO.File.Exists(fileName) ? System.IO.File.ReadAllBytes(fileName) : new byte[0];
            var publicKey = publicKeyDto.ToModel();
            var sessionKey = SessionKeyCache[(publicKey.N, publicKey.E)];
            return _cryptoProvider.Encrypt(data, sessionKey);
        }

        [HttpPost("{fileName}")]
        public IActionResult SetFile(string fileName, [FromBody] FileRequest request)
        {
            fileName = $"files/{fileName.Split('/', '\\').Last()}";
            var encryptedData = Convert.FromBase64String(request.EncryptedContentBase64);
            var publicKey = request.PublicKeyDto.ToModel();
            var sessionKey = SessionKeyCache[(publicKey.N, publicKey.E)];
            var data = _cryptoProvider.Decrypt(encryptedData, sessionKey);
            System.IO.File.WriteAllBytes(fileName, data);
            return Json(true);
        }
    }
}
