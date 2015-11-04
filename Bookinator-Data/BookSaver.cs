using Bookinator_Data.FileHelpers;
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
	public class BookSaver
	{
		private IDirectoryHelper directory;

		public BookSaver()
		{
			this.directory = new DirectoryHelper();
		}

		public void Save(Book book)
		{
			var file = book.FilePath;
			var bookZip = ZipFile.Open(file, ZipArchiveMode.Update);
			var tempFile = @"C:\Users\pgathany\Desktop\Personal\Books\tempDirectory" + Path.GetFileNameWithoutExtension(file);
			bookZip.ExtractToDirectory(tempFile);

			var tempBookFiles = directory.GetFiles(tempFile, "*.opf", SearchOption.AllDirectories);
			var opf = tempBookFiles.FirstOrDefault(f => f.EndsWith(".opf"));
			var doc = XDocument.Load(opf);
			var elements = doc.Root.Elements();
			var metadata = elements.Where(e => e.Name.LocalName == "metadata").FirstOrDefault();
			var metaDataElements = metadata.Elements();

			var title = metaDataElements.Where(e => e.Name.LocalName == "title").FirstOrDefault();
			title.Value = book.Title;

			var creator = metaDataElements.Where(e => e.Name.LocalName == "creator").FirstOrDefault();
			creator.Value = book.Creator;
			File.Delete(opf);
			doc.Save(opf);
			ZipFile.CreateFromDirectory(tempFile, tempFile + ".epub");
			directory.Delete(tempFile, true);
		}
	}
}
