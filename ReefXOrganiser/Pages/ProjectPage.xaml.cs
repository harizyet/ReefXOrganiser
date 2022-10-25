using ReefXOrganiser.Data;
using ReefXOrganiser.Interface;
using ReefXOrganiser.ViewModel;
using System.Text.Json;

namespace ReefXOrganiser;

public partial class ProjectPage : ContentPage
{
	private int _imageId = 0;
	private readonly IFolderPicker _folderPicker;
	private ProjectData _projectData;

	public ProjectPage(ProjectViewModel vm, IFolderPicker folderPicker, ProjectData projectData)
	{
		InitializeComponent();

		_folderPicker = folderPicker;
		_projectData = projectData;
		BindingContext = vm;
		UpdateUI(_imageId);
	}

	private async void OnExportClicked(object sender, EventArgs e)
	{
		var pickedFolder = await _folderPicker.PickFolder();

		ExportFolder(pickedFolder);
		ExportCSVAsync(pickedFolder);
	}

	private async void OnNextClicked(object sender, EventArgs e)
	{
		UpdateData();
		UpdateUI(++_imageId);
	}

	private async void OnPreviousClicked(object sender, EventArgs e)
	{
		UpdateData();
		UpdateUI(--_imageId);
	}

	private async void OnDeleteClicked(object sender, EventArgs e)
	{
		var data = _projectData.ImageDataList.ElementAt(_imageId);

		bool answer = await DisplayAlert("Warning", $"Are you sure you want to remove {data.CameraImageName} from the database?", "Yes", "No");

		if ( answer )
		{
			var temp = _projectData.ImageDataList.ToList();
			temp.RemoveAt(_imageId);
			_projectData.ImageDataList = temp;
			UpdateUI(_imageId);
		}
	}

	private async void OnSaveClicked(object sender, EventArgs e)
	{
		var pickedFolder = await _folderPicker.PickFolder();
		if(!string.IsNullOrWhiteSpace(pickedFolder))
			await SerializeProjectAsync(pickedFolder);
	}

	private async Task SerializeProjectAsync(string savePath)
	{
		string jsonOutput = JsonSerializer.Serialize(_projectData);
		await File.WriteAllTextAsync(Path.Combine(savePath, $"{_projectData.Date}_{_projectData.SurveyorName}.sav"), jsonOutput);
	}

	private void UpdateData()
	{
		var data = _projectData.ImageDataList.ElementAt(_imageId);

		data.CameraImageName = entry_cameraimagename.Text;
		data.SpeciesCommonName = entry_commonspeciesname.Text;
		data.ImageId = entry_samespeciesid.Text;
		data.DiveSite = entry_divesite.Text;
		data.Notes = entry_notes.Text;
	}

	private void UpdateUI(int ImageId)
	{
		if ( ImageId >= _projectData.ImageDataList.Count() )
		{
			ImageId = 0;
			_imageId = ImageId;
		}
		else if ( ImageId < 0 )
		{
			ImageId = _projectData.ImageDataList.Count()-1;
			_imageId = ImageId;
		}

		var data = _projectData.ImageDataList.ElementAt(ImageId);
		ImageView1.Source = ImageSource.FromFile(data.ImagePath);
		entry_cameraimagename.Text = data.CameraImageName;
		entry_commonspeciesname.Text = data.SpeciesCommonName;
		entry_samespeciesid.Text = data.ImageId;
		entry_divesite.Text = data.DiveSite;
		entry_notes.Text = data.Notes;
	}

	private async Task ExportCSVAsync(string savePath)
	{
		string csvOutput = String.Empty;
		var surveyorName = _projectData.SurveyorName.ToLower();
        surveyorName = surveyorName.Trim();


        foreach ( var data in _projectData.ImageDataList )
		{
			csvOutput += _projectData.Team;
			csvOutput += ",";
			csvOutput += $"{_projectData.Date.ToString("yyMMdd")}_{_projectData.Location}{data.DiveSite}_{data.CameraImageName}_{surveyorName}_{data.SpeciesCommonName.ToLower()}_{data.ImageId.ToLower()}.{data.ImageExt}";
			csvOutput += ",";
			csvOutput += $"{data.SpeciesCommonName}";
			csvOutput += ",";
			csvOutput += $"{data.Notes}";
			csvOutput += "\n";
		}

		await File.WriteAllTextAsync(Path.Combine(savePath, $"{_projectData.Date.ToString("yyMMdd")}_{surveyorName}.csv"), csvOutput);
	}

	private void ExportFolder(string savePath)
	{
        var surveyorName = _projectData.SurveyorName.ToLower();
        surveyorName = surveyorName.Trim();

        var finalPath = Path.Combine(savePath, $"{_projectData.Date.ToString("yyMMdd")}_{surveyorName}");
		Directory.CreateDirectory(finalPath);

		foreach ( var data in _projectData.ImageDataList )
		{
			var finalFileName = $"{_projectData.Date.ToString("yyMMdd")}_{_projectData.Location}{data.DiveSite}_{data.CameraImageName}_{_projectData.SurveyorName.ToLower()}_{data.SpeciesCommonName.ToLower()}_{data.ImageId.ToLower()}.{data.ImageExt}";
			File.Copy(data.ImagePath, Path.Combine(finalPath,finalFileName), true);
		}
	}
}