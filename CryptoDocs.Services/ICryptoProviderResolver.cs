using System;
using CryptoDocs.Abstractions;
using CryptoDocs.Shared.Symmetric;

namespace CryptoDocs.Services
{
    public interface ICryptoProviderResolver
    {
        IDataCryptoProvider GetCryptoProvider(string algorithm);
    }

    public class CryptoProviderResolver : ICryptoProviderResolver
    {
        private readonly IDataCryptoProvider _ideaCbc = new CbcCryptoProvider(new IdeaCryptoProvider());
        private readonly IDataCryptoProvider _serpentCbc = new CbcCryptoProvider(new SerpentCryptoProvider());

        public IDataCryptoProvider GetCryptoProvider(string algorithm)
        {
            switch (algorithm)
            {
                case CryptoAlgorithm.IdeaCbc:
                    return _ideaCbc;
                case CryptoAlgorithm.SerpentCbc:
                    return _serpentCbc;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}