using Bookinator_Data;
using Bookinator_Data.FileHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bookinator_WindowsForms
{
	public partial class Main : Form
	{
		private Timer timer;

		public Main()
		{
			InitializeComponent();
			DeleteEverythingInTemp();
			Cover.Visible = false;
		}

		private void DeleteEverythingInTemp()
		{
			var settings = new Settings();
			var temp = settings.TempDirectory;
			System.IO.DirectoryInfo downloadedMessageInfo = new DirectoryInfo(temp);
			foreach (FileInfo file in downloadedMessageInfo.GetFiles())
			{
				file.Delete();
			}
			foreach (DirectoryInfo dir in downloadedMessageInfo.GetDirectories())
			{
				dir.Delete(true);
			}
		}

		private void Browse_Click(object sender, EventArgs e)
		{
			var browseFileDialog = new OpenFileDialog();
			browseFileDialog.Title = "Open ePub File";
			browseFileDialog.Filter = "EPUB Files|*.epub";
			browseFileDialog.InitialDirectory = new Settings().LibraryDirectory;
			if(browseFileDialog.ShowDialog() == DialogResult.OK)
			{
				DeleteEverythingInTemp();
				var fileName = browseFileDialog.FileName.ToString();
				FilePath.Text = fileName;
				var contentReaderFactory = new ContentReaderFactory();
				var contentReader = contentReaderFactory.GetForFile(fileName);
				Title.Text = contentReader.GetTitle();
				Creator.Text = contentReader.GetCreator();
				var coverImageLocation = contentReader.GetCoverLocation();
				if (!string.IsNullOrEmpty(coverImageLocation))
				{
					Cover.ImageLocation = coverImageLocation;
					Cover.SizeMode = PictureBoxSizeMode.Zoom;
					Cover.Visible = true;
				} else 
				{
					Cover.Visible = false;
				}
			}
		}

		private void Save_Click(object sender, EventArgs e)
		{
			Save.Enabled = false;
			var fileName = FilePath.Text;
			var title = Title.Text;
			var creator = Creator.Text;
			var cover = Cover.ImageLocation;
			var book = new Book() { File = fileName, Title = title, Creator = creator, BookCover = cover };
			var bookSaver = new OverwrittingBookSaver(new DirectoryHelper(), new Settings().TempDirectory);
			bookSaver.Save(book);
			Save.Enabled = true;
			SaveMessage.Visible = true;
			StartTimer();
		}

		private void StartTimer()
		{
			timer = new Timer();
			timer.Interval = 2500;
			timer.Tick += StopTimer;
			timer.Start();
		}

		private void StopTimer(Object source, EventArgs e)
		{
			SaveMessage.Visible = false;
			timer.Stop();
		}

		private void ChangeCover_Click(object sender, EventArgs e)
		{
			var browseFileDialog = new OpenFileDialog();
			browseFileDialog.Title = "Select A Cover Image";
			browseFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
			browseFileDialog.InitialDirectory = new Settings().LibraryDirectory;
			if (browseFileDialog.ShowDialog() == DialogResult.OK)
			{
				Cover.ImageLocation = browseFileDialog.FileName.ToString();
				Cover.SizeMode = PictureBoxSizeMode.Zoom;
				Cover.Visible = true;
			}
		}
	}
}
