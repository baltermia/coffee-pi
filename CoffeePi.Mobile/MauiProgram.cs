﻿using MudBlazor.Services;

namespace CoffeePi.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            MauiAppBuilder builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
#if DEBUG
		    builder.Services.AddBlazorWebViewDeveloperTools();
#endif

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://coffeepi/api/") });

            builder.Services.AddMudServices();

            return builder.Build();
        }
    }
}