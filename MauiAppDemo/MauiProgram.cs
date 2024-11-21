using MauiAppDemo.Pages;
using MauiAppDemo.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MauiAppDemo.Core.Services;

namespace MauiAppDemo
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });


            var filePath = Path.Combine(FileSystem.Current.AppDataDirectory, "appsettings.json");

            if (!File.Exists(filePath))
            {
                Console.WriteLine("No se encontró el archivo appsettings.json");
            }
            else
            {
                // Configurar el archivo appsettings.json
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(FileSystem.Current.AppDataDirectory) // Directorio donde estará el archivo
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true) // Cargar appsettings.json
                    .Build();

                // Registrar IConfiguration en el contenedor de dependencias
                builder.Services.AddSingleton<IConfiguration>(configuration);
            }

       
            // Add services to the container.
            builder.Services.RegisterServices(builder.Configuration);
            builder.Services.AddScoped<AuthService>();

            // Registrar el ViewModel
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddTransient<LoginPage>();

            builder.Services.AddSingleton<HomeViewModel>();
            builder.Services.AddTransient<HomePage>();

            builder.Services.AddSingleton<EmployerListViewModel>();
            builder.Services.AddTransient<EmployeerList>();
            // builder.Services.AddSingleton(App.Settings);
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();


        }
    }
}
