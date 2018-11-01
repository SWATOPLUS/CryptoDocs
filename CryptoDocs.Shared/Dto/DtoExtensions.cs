using System.Numerics;
using CryptoDocs.Shared.Rsa;

namespace CryptoDocs.Shared.Dto
{
    public static class DtoExtensions
    {
        public static RsaPublicKey ToModel(this RsaPublicKeyDto dto)
        {
            return new RsaPublicKey
            {
                E = BigInteger.Parse(dto.E),
                N = BigInteger.Parse(dto.N)
            };
        }

        public static RsaPublicKeyDto ToDto(this RsaPublicKey model)
        {
            return new RsaPublicKeyDto
            {
                E = model.E.ToString(),
                N = model.N.ToString()
            };
        }
    }
}
