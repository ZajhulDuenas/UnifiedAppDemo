using infrastructure.DataBase;

namespace WebApi
{
    public static class RegisterServicesExtend
    {
        public static void RegisterServices(this IServiceCollection services,
           IConfiguration configuration)
        {

            if (services == null || configuration == null)
                return;

            services.RegisterDataBase(configuration, "ConnectionStrings:DefaultConnection", 1024);
            services.AddHttpContextAccessor();


        }

    }
}
