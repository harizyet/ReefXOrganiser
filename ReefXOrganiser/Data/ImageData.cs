using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReefXOrganiser.Data
{
	public class ImageData
	{
		public string CameraImageName 
		{ 
			get; set; 
		}
		public string SpeciesCommonName
		{
			get; set;
		}
		public string ImageId
		{
			get; set;
		}
		public string ImageExt
		{
			get; set;
		}
		public string ImagePath
		{
			get; set;
		}
		public string Notes
		{
			get; set;
		}
		public string DiveSite
		{
			get; set;
		}
	}
}
