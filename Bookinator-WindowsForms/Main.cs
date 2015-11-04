using Bookinator_Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bookinator_WindowsForms
{
	public partial class Main : Form
	{
		public Main()
		{
			InitializeComponent();
			var context = new JsonDataContext<Book>();
			var books = new LibraryLoader(context).LoadLibrary();
			BookList.DataSource = books.Select(b => new DisplayBook { Title = b.Title, Creator = b.Creator }).ToList();
		}

		public class DisplayBook
		{
			public string Title { get; set; }
			public string Creator { get; set; }
		}
	}
}
