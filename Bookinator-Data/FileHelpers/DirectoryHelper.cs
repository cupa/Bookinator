using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookinator_Data.FileHelpers
{
	public interface IDirectoryHelper
	{
		bool Exists(string Path);
		void CreateDirectory(string Path);
		string[] GetFiles(string Path);
		string[] GetFiles(string Path, string SearchPattern, SearchOption SearchOption);
		void Delete(string Path);
		void Delete(string Path, bool Recursive);
	}

	public class DirectoryHelper : IDirectoryHelper
	{
		public bool Exists(string Path)
		{
			return Directory.Exists(Path);
		}
		public void CreateDirectory(string Path)
		{
			Directory.CreateDirectory(Path);
		}
		public string[] GetFiles(string Path)
		{
			return Directory.GetFiles(Path);
		}
		public string[] GetFiles(string Path, string SearchPattern, SearchOption SearchOption)
		{
			return Directory.GetFiles(Path, SearchPattern, SearchOption);
		}
		public void Delete(string Path)
		{
			Directory.Delete(Path);
		}

		public void Delete(string Path, bool Recursive)
		{
			Directory.Delete(Path, Recursive);
		}
	}
}
