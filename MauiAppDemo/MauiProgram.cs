using MauiAppDemo.Pages;
using MauiAppDemo.ViewModels;
using Microsoft.Extensions.Logging;

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

          
            // Add services to the container.
            builder.Services.RegisterServices(builder.Configuration);

            // Registrar el ViewModel
            builder.Services.AddTransient<LoginViewModel>();

            builder.Services.AddSingleton(App.Settings);
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();


        }
    }
}
