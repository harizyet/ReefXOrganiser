namespace ReefXOrganiser;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(CreateProjectPage), typeof(CreateProjectPage));
		Routing.RegisterRoute(nameof(ProjectPage), typeof(ProjectPage));
	}
}
