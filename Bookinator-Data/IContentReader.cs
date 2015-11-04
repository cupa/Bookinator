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
	public interface IContentReader
	{
		string GetTitle();
		string GetCreator();
	}

	public class EpubContentReader : IContentReader, IDisposable
	{
		private IEnumerable<XElement> metaDataElements;
		private string tempFile;

		public EpubContentReader(string file)
		{
			var bookZip = ZipFile.Open(file, ZipArchiveMode.Update);
			var contentInfoFile = bookZip.Entries.Where(e => e.Name.EndsWith(".opf")).FirstOrDefault();
			this.tempFile = @"C:\Users\pgathany\Desktop\Personal\Books\tempfile" + Path.GetFileNameWithoutExtension(file) + ".odf";
			contentInfoFile.ExtractToFile(tempFile);
			var doc = XDocument.Load(tempFile);
			var elements = doc.Root.Elements();
			var metadata = elements.Where(e => e.Name.LocalName == "metadata").FirstOrDefault();
			this.metaDataElements = metadata.Elements();
		}

		public string GetTitle()
		{
			var title = metaDataElements.Where(e => e.Name.LocalName == "title").FirstOrDefault();
			return title.Value;
		}

		public string GetCreator()
		{
			var creator = metaDataElements.Where(e => e.Name.LocalName == "creator").FirstOrDefault();
			return creator.Value;
		}

		public void Dispose()
		{
			System.IO.File.Delete(tempFile);
		}
	}
}
