using CryptoDocs.Abstractions;

namespace CryptoDocs.Shared.Dto
{
    public class FileRequest
    {
        public RsaPublicKeyDto PublicKeyDto { get; set; }

        public string EncryptedContentBase64 { get; set; }
    }
}
