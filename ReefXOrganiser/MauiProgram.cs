using Microsoft.Extensions.DependencyInjection;
using ReefXOrganiser.Data;
using ReefXOrganiser.Interface;
using ReefXOrganiser.ViewModel;
using Microsoft.Maui.LifecycleEvents;
#if WINDOWS
    using Microsoft.UI;
    using Microsoft.UI.Windowing;
    using Windows.Graphics;
#endif

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
            builder.ConfigureLifecycleEvents(events =>
            {
                events.AddWindows(wndLifeCycleBuilder =>
                {
                    wndLifeCycleBuilder.OnWindowCreated(window =>
                    {
                        IntPtr nativeWindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                        var win32WindowsId = Win32Interop.GetWindowIdFromWindow(nativeWindowHandle);
                        var winuiAppWindow = AppWindow.GetFromWindowId(win32WindowsId);

                        int width = 1680;
                        int height = 1050;
                        winuiAppWindow.MoveAndResize(new RectInt32(0, 0, width, height));
                    });
                });
            });
#elif MACCATALYST
		builder.Services.AddTransient<IFolderPicker, Platforms.MacCatalyst.FolderPicker>();
#endif
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
