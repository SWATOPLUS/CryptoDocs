using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CryptoDocs.Abstractions;
using CryptoDocs.Shared.Dto;

namespace CryptoDocs.RestClient
{
    public class CryptoDocsServiceClient : ICryptoDocsService
    {
        private readonly HttpClient _http;
        private readonly Uri _baseUri;

        public CryptoDocsServiceClient(HttpClient http, string baseUrl)
        {
            _http = http;
            _baseUri = new Uri(baseUrl);
        }

        public async Task<byte[]> GetEncryptedSessionKeyAsync(RsaPublicKeyDto publicKeyDto)
        {
            var url = new Uri(_baseUri, "api/Data/GetEncryptedSessionKey");

            var sessionKeyEncryptedBase64 = await _http.PostJsonAsync<string>(url.AbsoluteUri, publicKeyDto);

            var sessionKeyEncrypted = Convert.FromBase64String(sessionKeyEncryptedBase64);

            return sessionKeyEncrypted;
        }

        public async Task<byte[]> GetEncryptedFileAsync(RsaPublicKeyDto publicKeyDto, string algorithm, string fileName)
        {
            var url = new Uri(_baseUri, $"api/Data/GetFile/{algorithm}/{fileName}");
            var encryptedContentBase64 = await _http.PostJsonAsync<string>(url.AbsoluteUri, publicKeyDto);
            var encryptedContent = Convert.FromBase64String(encryptedContentBase64);

            return encryptedContent;
        }

        public async Task SetEncryptedFileAsync(RsaPublicKeyDto publicKeyDto, string algorithm, string fileName,
            byte[] encryptedFileContent)
        {
            var encryptedContentBase64 = Convert.ToBase64String(encryptedFileContent);
            var url = new Uri(_baseUri, $"api/Data/SetFile/{algorithm}/{fileName}");
            await _http.PostJsonAsync(url.AbsoluteUri, new FileRequest
            {
                EncryptedContentBase64 = encryptedContentBase64,
                PublicKeyDto = publicKeyDto
            });
        }
    }
}
