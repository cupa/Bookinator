using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookinator_Data
{
	public abstract class SettingsBase
	{
		public string LibraryDirectory { get; set; }
		public string BackupDirectory { get; set; }
		public string DataDirectory { get; set; }
		public string TempDirectory { get; set; }
	}

	public class Settings : SettingsBase
	{
		public Settings()
		{
			LibraryDirectory = @"C:\Users\pgathany\Desktop\Personal\Books";
			BackupDirectory = @"C:\Users\pgathany\Desktop\Personal\Backup";
			TempDirectory = System.IO.Path.GetTempPath();
			DataDirectory = @"C:\Users\pgathany\Desktop\Personal\Json";
		}
	}
}
