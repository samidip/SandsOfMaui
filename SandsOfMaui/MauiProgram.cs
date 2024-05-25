namespace SandsOfMaui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseTelerik()
			.UseMauiApp<App>()
			.RegisterViews()
			.RegisterViewModels()
			.RegisterServices()
			.RegisterUtilities()
			.UseLocalNotification()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		return builder.Build();
	}

    public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<IssueListView>();
		mauiAppBuilder.Services.AddSingleton<IssueDetailView>();
		mauiAppBuilder.Services.AddSingleton<MauiView>();
		mauiAppBuilder.Services.AddSingleton<TelerikUIView>();
		mauiAppBuilder.Services.AddSingleton<AboutView>();
        return mauiAppBuilder;        
    }

	public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<IssueListViewModel>();
		mauiAppBuilder.Services.AddSingleton<IssueViewModel>();
        return mauiAppBuilder;        
    }

	public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<CosmosService>();
		mauiAppBuilder.Services.AddSingleton<LocalDatabaseService>();
        return mauiAppBuilder;        
    }

	public static MauiAppBuilder RegisterUtilities(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<HTMLBuilder>();
        return mauiAppBuilder;        
    }
}
