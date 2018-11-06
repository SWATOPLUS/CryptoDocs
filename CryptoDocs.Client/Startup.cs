using CryptoDocs.Shared;
using CryptoDocs.Shared.Symmetric;
using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoDocs.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDataCryptoProvider>(new CbcCryptoProvider(new IdeaCryptoProvider()));
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
