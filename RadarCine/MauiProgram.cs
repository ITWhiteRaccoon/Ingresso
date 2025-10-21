using System.Globalization;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using IngressoApi.Services;

namespace RadarCine {
    public static class MauiProgram {
        public static MauiApp CreateMauiApp() {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources/Localization");
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("pt-BR");
            
            builder.Services.AddMudServices();

            builder.Services.AddSingleton(sp =>
                new IngressoClient(
                    "v0",
                    "github.com_itwhiteraccoon"
                )
            );

            return builder.Build();
        }
    }
}