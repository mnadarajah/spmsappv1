using DevExpress.Maui;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using SPMSCAV1.Services.Interface;
using SPMSCAV1.Services.Repository;
using SPMSCAV1.Views;

namespace SPMSCAV1
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseDevExpress()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("roboto-regular.ttf", "Roboto");
                    fonts.AddFont("roboto-medium.ttf", "Roboto-Medium");
                    fonts.AddFont("roboto-bold.ttf", "Roboto-Bold");
                    fonts.AddFont("univia-pro-regular.ttf", "Univia-Pro");
                    fonts.AddFont("univia-pro-medium.ttf", "Univia-Pro Medium");
                })
                .UseMauiCompatibility()
                .ConfigureEffects((effects) =>
                {
                    effects.AddCompatibilityEffects(AppDomain.CurrentDomain.GetAssemblies());
                });
            //

            builder.Services.AddScoped<IGenderService, GenderService>();

            builder.Services.AddSingleton<MainPage>();

            builder.Services.AddSingleton<GenderPage>();

            #if ANDROID && DEBUG
            Platforms.Android.DangerousAndroidMessageHandlerEmitter.Register();
            Platforms.Android.DangerousTrustProvider.Register();
            #endif

            return builder.Build();
        }
    }
}