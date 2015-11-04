using Bookinator_Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookinator
{
	class Program
	{
		static void Main(string[] args)
		{
			var db = new JsonDataContext<Book>();
			var books = LoadBooksFromLibrary(db);
			foreach(var book in books)
			{
				Console.WriteLine("{0} by {1}", book.Title, book.Creator);
			}

			var bookSaver = new BookSaver();
			//bookSaver.Save(books.First());
		}

		private static List<Book> LoadBooksFromLibrary(IDataContext<Book> db)
		{
			var loader = new LibraryLoader(db);
			return loader.LoadLibrary().ToList();
		}
	}
}
