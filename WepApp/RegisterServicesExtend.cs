using Front.Infrastructure.ClientApi;
using Front.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using System.Runtime;

namespace WepApp
{
    public static class RegisterServicesExtend
    {
        public static void RegisterServices(this IServiceCollection services,
          IConfiguration configuration)
        {
            
            if (services == null || configuration == null)
                return;

            services.AddTransient<ITokenServiceClient, TokenServiceClient>();

            services.Configure<TokenServiceSettings>(configuration.GetSection("TokenServiceSettings"));


        }
    }
}
