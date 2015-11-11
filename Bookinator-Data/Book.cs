using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookinator_Data
{
	public class Book : Entity
	{
		public string File { get; set; }
		public string BackupFile { get; set;}
		public string Title { get; set; }
		public string Creator { get; set; }
	}
}
