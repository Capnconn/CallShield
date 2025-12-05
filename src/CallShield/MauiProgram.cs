using CallShield.DataAccess.Repositories;
using CallShield.UI.Processors;
using CallShield.UI.ViewModels;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Toolkit.Hosting;

namespace CallShield.UI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .ConfigureSyncfusionToolkit();

            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<ICallProcessor, CallProcessor>();
            builder.Services.AddSingleton<ICallShieldRepository, CallShieldRepository>();

#if DEBUG
            builder.Logging.AddDebug();
#endif
            var app = builder.Build();
            //var databaseBuilderService = app.Services.GetRequiredService<ICallShieldRepository>();
            //databaseBuilderService.BuildDatabase();

            return app;
        }
    }
}
