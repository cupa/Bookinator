using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookinator_Data.FileHelpers
{
	public interface IFileHelper
	{
		void Copy(string File1, string File2);
		void Move(string File1, string NewName);
	}

	public class FileHelper : IFileHelper
	{
		public void Copy(string File1, string File2)
		{
			File.Copy(File1, File2);
		}

		public void Move(string File1, string File2)
		{
			File.Move(File1, File2);
		}
	}
}
