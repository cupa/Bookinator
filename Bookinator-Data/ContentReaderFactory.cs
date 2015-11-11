using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookinator_Data
{
	public interface IContentReaderFactory
	{
		IContentReader GetForFile(string FilePath);
	}

	public class ContentReaderFactory : IContentReaderFactory
	{
		public IContentReader GetForFile(string FilePath)
		{
			return new EpubContentReader(FilePath);
		}
	}
}
