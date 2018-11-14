using System;
using CryptoDocs.Abstractions;
using CryptoDocs.Shared.Symmetric;

namespace CryptoDocs.Services
{
    public class CryptoProviderResolver : ICryptoProviderResolver
    {
        private readonly IDataCryptoProvider _ideaCbc = new CbcCryptoProvider(new IdeaCryptoProvider());
        private readonly IDataCryptoProvider _serpentCbc = new CbcCryptoProvider(new SerpentCryptoProvider());
        private readonly IDataCryptoProvider _aesOfb = new OfbCryptoProvider(new AesCryptoProvider());

        public IDataCryptoProvider GetCryptoProvider(string algorithm)
        {
            switch (algorithm)
            {
                case CryptoAlgorithm.IdeaCbc:
                    return _ideaCbc;
                case CryptoAlgorithm.SerpentCbc:
                    return _serpentCbc;
                case CryptoAlgorithm.AesOfb:
                    return _aesOfb;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}