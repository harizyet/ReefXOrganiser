namespace ReefXOrganiser.Data
{
	public class ProjectData
	{
		public string ProjectDirectory
		{
			get; set;
		}

		public string Date
		{
			get; set;
		}
		public string Location
		{
			get; set;
		}
		public string SurveyorName
		{
			get; set;
		}
		public string Team
		{
			get; set;
		}
		public IEnumerable<ImageData> ImageDataList
		{
			get; set;
		}

		public ProjectData() => ImageDataList = Enumerable.Empty<ImageData>();

		public void Copy(ProjectData projectData)
		{
			ProjectDirectory = projectData.ProjectDirectory;
			Date = projectData.Date;
			Location = projectData.Location;
			SurveyorName = projectData.SurveyorName;
			Team = projectData.Team;
			ImageDataList = projectData.ImageDataList;
		}
	}
}
