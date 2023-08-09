using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using TestApp.Models;
using TestApp.ViewModels;

namespace TestApp;

public static class MauiProgram
{
	public static  MauiApp CreateMauiApp()
	{
        AppDbContext appDb = new AppDbContext();
        appDb.Database.EnsureCreated();

        var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		builder.Services.AddSingleton<DataModel>();
		builder.Services.AddSingleton<DataViewModel>();
		builder.Services.AddSingleton<AppDbContext>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}

  
}
