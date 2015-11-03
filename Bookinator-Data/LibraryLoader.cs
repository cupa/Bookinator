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
		public LibraryLoader(IDataContext<Book> db)
		{
			this.db = db;
		}

		public IEnumerable<Book> LoadLibrary()
		{
			var books = db.GetItems();
			var bookpath = @"C:\Users\pgathany\Desktop\Personal\Books";
			var bookfiles = Directory.GetFiles(bookpath);
			foreach (var bookfile in bookfiles)
			{
				if (!books.Select(b => b.FilePath).Contains(bookfile))
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
			var deletedBooks = books.Where(b => !bookfiles.Contains(b.FilePath));
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
			using (var contentReader = new EpubContentReader(bookfile))
			{
				var title = contentReader.GetTitle();
				var creator = contentReader.GetCreator();
				var book = new Book()
				{
					FilePath = bookfile,
					Title = title,
					Creator = creator
				};
				db.Add(book);
			}
		}
	}
}
