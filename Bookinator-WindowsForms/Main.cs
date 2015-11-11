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
			var settings = new Settings();
			var context = new JsonDataContext<Book>(settings);
			var books = new LibraryLoader(context, settings).LoadLibrary();
			BookList.DataSource = books.Select(b => new DisplayBook { Title = b.Title, Creator = b.Creator }).ToList();
		}

		public class DisplayBook
		{
			public string Title { get; set; }
			public string Creator { get; set; }
		}
	}
}
