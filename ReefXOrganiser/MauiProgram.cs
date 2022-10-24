using Microsoft.Extensions.DependencyInjection;
using ReefXOrganiser.Data;
using ReefXOrganiser.Interface;
using ReefXOrganiser.ViewModel;

namespace ReefXOrganiser;

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

#if WINDOWS
        builder.Services.AddTransient<IFolderPicker, Platforms.Windows.FolderPicker>();
#elif MACCATALYST
		builder.Services.AddTransient<IFolderPicker, Platforms.MacCatalyst.FolderPicker>();
#endif
		builder.Services.AddSingleton<ImageData>();
		builder.Services.AddSingleton<ProjectData>();

		builder.Services.AddTransient<MainPage>();
		builder.Services.AddTransient<MainViewModel>();

		builder.Services.AddTransient<CreateProjectPage>();
		builder.Services.AddTransient<CreateProjectViewModel>();

		builder.Services.AddTransient<ProjectPage>();
		builder.Services.AddTransient<ProjectViewModel>();

		builder.Services.AddTransient<App>();

		return builder.Build();
	}
}
