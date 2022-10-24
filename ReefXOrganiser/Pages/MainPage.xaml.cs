using ReefXOrganiser.Data;
using ReefXOrganiser.Interface;
using ReefXOrganiser.ViewModel;
using System.Text.Json;

namespace ReefXOrganiser;

public partial class MainPage : ContentPage
{
	private readonly IFolderPicker _folderPicker;
	private ProjectData _projectData;

	public MainPage(MainViewModel viewModel, IFolderPicker folderPicker, ProjectData projectData)
	{
		InitializeComponent();
		_folderPicker = folderPicker;
		BindingContext = viewModel;
		_projectData = projectData;
	}

	private async void OnPickFolderClicked(object sender, EventArgs e)
	{
		var pickedFolder = await _folderPicker.PickFolder();

		if ( !String.IsNullOrWhiteSpace(pickedFolder) )

		{
			FolderLabel.Text = pickedFolder;
			_projectData.ProjectDirectory = pickedFolder;

			SemanticScreenReader.Announce(FolderLabel.Text);
		}
	}

	
	private async void OnLoadClicked(object sender, EventArgs e)
	{
		var customFileType = new FilePickerFileType(
				new Dictionary<DevicePlatform, IEnumerable<string>>
				{
                    { DevicePlatform.WinUI, new[] { ".sav", ".sav" } }, // file extension
					{ DevicePlatform.macOS, new[] { "sav", "sav" } }, // UTType values
                });

		PickOptions options = new()
		{
			PickerTitle = "Please select a project save file",
			FileTypes = customFileType,
		};

		var result = await FilePicker.Default.PickAsync(options);

		if ( !string.IsNullOrWhiteSpace(result.FullPath) )
		{
			LoadExistingProject(result.FullPath);
			await Shell.Current.GoToAsync(nameof(ProjectPage));
		}
	}

	private void LoadExistingProject(string projectPath)
	{
		string projectText = File.ReadAllText(projectPath);
		_projectData.Copy(JsonSerializer.Deserialize<ProjectData>(projectText));
	}

	private async void OnContinueClicked(object sender, EventArgs e)
	{
		if ( !String.IsNullOrWhiteSpace(_projectData.ProjectDirectory) )
		{
			await Shell.Current.GoToAsync(nameof(CreateProjectPage));
		}
		else
		{
			await DisplayAlert("Warning", "Please select a folder to load from.", "OK");
		}
	}
}

