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
	public interface IContentReader
	{
		string GetTitle();
		string GetCreator();
		string GetCoverLocation();
	}

	public class EpubContentReader : IContentReader
	{
		private string title;
		private string creator;
		private string coverLocation;

		public EpubContentReader(string file, SettingsBase settings, IDirectoryHelper directory)
		{
			var bookZip = ZipFile.Open(file, ZipArchiveMode.Update);
			var tempFile = Path.Combine(settings.TempDirectory, Path.GetFileNameWithoutExtension(file));
			bookZip.ExtractToDirectory(tempFile);
			bookZip.Dispose();
			var contentInfoFile = directory.GetFiles(tempFile, "*.opf", SearchOption.AllDirectories).FirstOrDefault(f => f.EndsWith(".opf"));
			
			var doc = XDocument.Load(contentInfoFile);
			var elements = doc.Root.Elements();
			var metadata = elements.Where(e => e.Name.LocalName == "metadata").FirstOrDefault();
			var metaDataElements = metadata.Elements();
			this.title = metaDataElements.Where(e => e.Name.LocalName == "title").FirstOrDefault().Value;
			this.creator = metaDataElements.Where(e => e.Name.LocalName == "creator").FirstOrDefault().Value;
			var coverInfoElement = metaDataElements.Where(e => e.Attributes("name").Where(a => a.Value == "cover").FirstOrDefault() != null).FirstOrDefault();
			coverLocation = "";
			if (coverInfoElement != null)
			{
				var coverValue = coverInfoElement.Attributes("content").FirstOrDefault().Value;
				var manifest = elements.Where(e => e.Name.LocalName == "manifest").FirstOrDefault();
				var manifestElements = manifest.Elements();
				var coverManifest = manifestElements.Where(e => e.Attributes("id").FirstOrDefault() != null && e.Attributes("id").FirstOrDefault().Value == coverValue).FirstOrDefault();
				var coverHref = coverManifest.Attributes("href").FirstOrDefault().Value;
				var fullCoverLocation = Path.Combine(Path.GetDirectoryName(contentInfoFile), coverHref.Replace("/", "\\"));
				coverLocation = fullCoverLocation;
			}
		}

		public string GetTitle()
		{
			return title;
		}

		public string GetCreator()
		{
			return creator;
		}

		public string GetCoverLocation()
		{
			return coverLocation;
		}
	}
}
