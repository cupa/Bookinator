using Bookinator_Data.FileHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookinator_Data
{
	public class LibraryLoader
	{
		private IDataContext<Book> db;
		private IDirectoryHelper directory;
		private SettingsBase settings;

		public LibraryLoader(IDataContext<Book> db, SettingsBase settings)
		{
			this.db = db;
			this.directory = new DirectoryHelper();
			this.settings = settings;
		}

		public IEnumerable<Book> LoadLibrary()
		{
			var books = db.GetItems();
			var bookpath = settings.LibraryDirectory;
			var bookfiles = directory.GetFiles(bookpath);
			foreach (var bookfile in bookfiles)
			{
				if (!books.Select(b => b.File).Contains(bookfile))
				{
					AddBook(bookfile);
				}
			}
			RemoveDeletedBooks(books, bookfiles);
			db.Save();
			return books;
		}

		private void RemoveDeletedBooks(List<Book> books, string[] bookfiles)
		{
			var deletedBooks = books.Where(b => !bookfiles.Contains(b.File));
			if (deletedBooks.Any())
			{
				for (var i = deletedBooks.Count() - 1; i >= 0; i--)
				{
					var book = deletedBooks.ElementAt(i);
					db.Remove(book);
				}
			}
		}

		private void AddBook(string bookfile)
		{
			var contentReader = new EpubContentReader(bookfile, new Settings(), new DirectoryHelper());
			var title = contentReader.GetTitle();
			var creator = contentReader.GetCreator();
			var book = new Book()
			{
				File = bookfile,
				Title = title,
				Creator = creator
			};
			db.Add(book);
		}
	}
}
