using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Bookinator_Data
{
	public interface IContentReader : IDisposable
	{
		string GetTitle();
		string GetCreator();
	}

	public class EpubContentReader : IContentReader
	{
		private string tempFile;
		private string title;
		private string creator;

		public EpubContentReader(string file)
		{
			var bookZip = ZipFile.Open(file, ZipArchiveMode.Update);
			var contentInfoFile = bookZip.Entries.Where(e => e.Name.EndsWith(".opf")).FirstOrDefault();
			this.tempFile = @"C:\Users\pgathany\Desktop\Personal\Books\tempfile" + Path.GetFileNameWithoutExtension(file) + ".odf";
			contentInfoFile.ExtractToFile(tempFile);
			var doc = XDocument.Load(tempFile);
			var elements = doc.Root.Elements();
			var metadata = elements.Where(e => e.Name.LocalName == "metadata").FirstOrDefault();
			var metaDataElements = metadata.Elements();
			this.title = metaDataElements.Where(e => e.Name.LocalName == "title").FirstOrDefault().Value;
			this.creator = metaDataElements.Where(e => e.Name.LocalName == "creator").FirstOrDefault().Value;
		}

		public string GetTitle()
		{
			return title;
		}

		public string GetCreator()
		{
			return creator;
		}

		public void Dispose()
		{
			System.IO.File.Delete(tempFile);
		}
	}
}
