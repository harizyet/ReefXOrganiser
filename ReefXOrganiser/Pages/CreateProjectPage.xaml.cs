using ReefXOrganiser.Data;
using ReefXOrganiser.ViewModel;

namespace ReefXOrganiser;

public partial class CreateProjectPage : ContentPage
{
	private ProjectData _projectData;

	public CreateProjectPage(CreateProjectViewModel vm, ProjectData projectData)
	{
		InitializeComponent();
		BindingContext = vm;
		_projectData = projectData;
	}

	private async void OnContinueClicked(object sender, EventArgs e)
	{
		if ( await VerifyUserEntryAsync())
		{
			_projectData.Date = entry_surveydate.Text;
			_projectData.Location = entry_divelocation.Text;
			_projectData.SurveyorName = entry_surveyorname.Text;
			_projectData.Team = entry_surveyorteam.Text;
			GenerateImageData();
			await Shell.Current.GoToAsync(nameof(ProjectPage));
		}
	}
	
	private async void OnBackClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync($"///{ nameof(MainPage)}");
	}

	private async Task<bool> VerifyUserEntryAsync()
	{
		if ( String.IsNullOrWhiteSpace(entry_surveydate.Text) )
		{
			await DisplayAlert("Warning", "Please enter a Survey Date", "OK");
			return false;
		}

		if ( String.IsNullOrWhiteSpace(entry_divelocation.Text) )
		{
			await DisplayAlert("Warning", "Please enter a Dive Location", "OK");
			return false;
		}

		if ( String.IsNullOrWhiteSpace(entry_surveyorname.Text) )
		{
			await DisplayAlert("Warning", "Please enter a Surveyor Name", "OK");
			return false;
		}
		return true;
	}

	private void GenerateImageData()
	{
		string supportedExtensions = "*.jpg,*.gif,*.png,*.bmp,*.jpe,*.jpeg,*.tif,*.tiff";
		var entries = Directory.GetFiles(_projectData.ProjectDirectory, "*.*", SearchOption.AllDirectories).Where(s => supportedExtensions.Contains(Path.GetExtension(s).ToLower()));

		var imageData = new List<ImageData>();

		foreach ( var entry in entries )
		{
			imageData.Add(new ImageData
			{
				CameraImageName = Path.GetFileNameWithoutExtension(entry),
				ImagePath = entry,
				ImageExt = Path.GetExtension(entry).Replace(".", "")
			});
		}
		_projectData.ImageDataList = imageData;
	}
}