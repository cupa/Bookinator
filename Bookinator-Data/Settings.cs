using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookinator_Data
{
	public abstract class SettingsBase
	{
		public string LibraryDirectory { get; set; }
		public string TempDirectory { get; set; }
	}

	public class Settings : SettingsBase
	{
		public Settings()
		{
			var defaultLibrary = ConfigurationManager.AppSettings["DefaultLibrary"];
			if(string.IsNullOrEmpty(defaultLibrary))
			{
				defaultLibrary = @"C:\";
			}
			LibraryDirectory = defaultLibrary;
			TempDirectory = Path.Combine(System.IO.Path.GetTempPath(), @"Bookinator");
			if(!Directory.Exists(TempDirectory))
			{
				Directory.CreateDirectory(TempDirectory);
			}
		}
	}
}
