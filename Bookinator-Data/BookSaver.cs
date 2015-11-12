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
			var file = book.File;
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
			File.Delete(file);
			ZipFile.CreateFromDirectory(tempFile, file);
			directory.Delete(tempFile, true);
		}
	}

	public class OverwrittingBookSaver
	{
		private IDirectoryHelper directory;
		private string TempPath;

		public OverwrittingBookSaver(IDirectoryHelper directoryHelper, string TempPath)
		{
			this.directory = directoryHelper;
			this.TempPath = TempPath;
		}

		public void Save(Book book)
		{
			var file = book.File;
			var bookZip = ZipFile.Open(file, ZipArchiveMode.Update);
			var tempFile = Path.Combine(TempPath, Path.GetFileNameWithoutExtension(file) + "SavingTemp");
			bookZip.ExtractToDirectory(tempFile);
			bookZip.Dispose();

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

			var coverInfoElement = metaDataElements.Where(e => e.Attributes("name").Where(a => a.Value == "cover").FirstOrDefault() != null).FirstOrDefault();
			if (coverInfoElement != null)
			{
				var coverValue = coverInfoElement.Attributes("content").FirstOrDefault().Value;
				var manifest = elements.Where(e => e.Name.LocalName == "manifest").FirstOrDefault();
				var manifestElements = manifest.Elements();
				var coverManifest = manifestElements.Where(e => e.Attributes("id").FirstOrDefault() != null && e.Attributes("id").FirstOrDefault().Value == coverValue).FirstOrDefault();
				var coverHrefElement = coverManifest.Attributes("href").FirstOrDefault();
				var coverHref = coverHrefElement.Value;
				var fullCoverLocation = Path.Combine(Path.GetDirectoryName(opf), coverHref.Replace("/", "\\"));
				var coverLocation = fullCoverLocation;
				if (coverLocation != book.BookCover)
				{
					File.Delete(coverLocation);
					var coverExtension = Path.GetExtension(book.BookCover).Replace(".", "");
					var coverMediaType = (coverExtension.StartsWith("jp") ? "image/jpeg" : "image/png");
					var newCover = Path.ChangeExtension(coverLocation, coverExtension);
					var localCoverPath = Path.ChangeExtension(coverHref, coverExtension);
					coverHrefElement.Value = localCoverPath;
					File.Copy(book.BookCover, newCover);
					var mediaTypeAttr = coverManifest.Attribute("media-type");
					if(mediaTypeAttr != null)
					{
						mediaTypeAttr.Value = coverMediaType;
					} else
					{
						coverManifest.Add(new XAttribute("media-type", coverMediaType));
					}

				}
			} else
			{
				if(!string.IsNullOrEmpty(book.BookCover))
				{
					var coverExtension = Path.GetExtension(book.BookCover).Replace(".", "");
					var mediaType = (coverExtension.StartsWith("jp") ? "image/jpeg" : "image/png");
					metadata.Add(new XElement("meta", new XAttribute("name", "cover"), new XAttribute("content", "bookinatorcover")));
					var manifest = elements.Where(e => e.Name.LocalName == "manifest").FirstOrDefault();
					var newCoverName = "bookinatorcover" + Path.GetExtension(book.BookCover);
					manifest.AddFirst(new XElement("item", new XAttribute("id", "bookinatorcover"), new XAttribute("href", newCoverName), new XAttribute("media-type", mediaType)));
					File.Copy(book.BookCover, Path.Combine(Path.GetDirectoryName(opf), newCoverName));
				}
			}
			File.Delete(opf);
			doc.Save(opf);

			OverwriteEpub(file, tempFile);
		}

		private void OverwriteEpub(string file, string tempFile)
		{
			File.Delete(file);
			using (var zip = new Ionic.Zip.ZipFile())
			{
				var mime = zip.AddEntry("mimetype", "application/epub+zip", Encoding.ASCII);
				zip.UseZip64WhenSaving = Ionic.Zip.Zip64Option.Never;
				mime.CompressionLevel = Ionic.Zlib.CompressionLevel.None;
				mime.CompressionMethod = Ionic.Zip.CompressionMethod.None;
				var metaDir = Path.Combine(tempFile, "META-INF");
				var meta = zip.AddDirectory(metaDir, "META-INF");
				meta.CompressionLevel = Ionic.Zlib.CompressionLevel.None;
				var contentDir = Path.Combine(tempFile, "OEBPS");
				var content = zip.AddDirectory(contentDir, "OEBPS");
				content.CompressionLevel = Ionic.Zlib.CompressionLevel.None;
				zip.Save(file);
			}
			directory.Delete(tempFile, true);
		}
	}
}
