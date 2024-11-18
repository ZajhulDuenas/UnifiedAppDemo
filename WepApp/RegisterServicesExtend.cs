using Front.Infrastructure.ClientApi;
using Front.Infrastructure.Settings;

namespace WepApp
{
    public static class RegisterServicesExtend
    {
        public static void RegisterServices(this IServiceCollection services,
          IConfiguration configuration)
        {
            
            if (services == null || configuration == null)
                return;

            services.AddTransient<IAuthServiceClient, Front.Infrastructure.ClientApi.AuthServiceClient>();
            services.AddTransient<IEmployeersClient, EmployeersClient>();
            services.Configure<TokenServiceSettings>(configuration.GetSection("TokenServiceSettings"));


        }
    }
}
