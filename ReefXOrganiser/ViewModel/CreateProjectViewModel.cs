
using CommunityToolkit.Mvvm.ComponentModel;

namespace ReefXOrganiser.ViewModel
{
	public partial class CreateProjectViewModel : ObservableObject
	{
		public DateTime GetToday()
		{
			return DateTime.Now;
		}
	}
}
