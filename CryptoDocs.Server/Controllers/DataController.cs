using CryptoDocs.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using CryptoDocs.Shared.Dto;
using CryptoDocs.Shared.Rsa;

namespace CryptoDocs.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    public class DataController : Controller
    {
        public static RandomNumberGenerator SecureRandom = RandomNumberGenerator.Create();

        public byte[] GetEncryptedSessionKey(RsaPublicKeyDto publicKeyDto)
        {
            var sessionKey = new byte[64];
            SecureRandom.GetBytes(sessionKey);
            return publicKeyDto.ToModel().Encrypt(sessionKey);
        }
    }
}
