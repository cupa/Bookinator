using Bookinator_Data;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
namespace WindowsFormsApplication1
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Books = new System.Windows.Forms.ListView();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// Books
			// 

			var db = new JsonDataContext<Book>();
			var books = LoadBooksFromLibrary(db);
			var items = books.Select(b => new ListViewItem(new string[] { b.ID.ToString(), b.Title, b.Creator })).ToArray();

			this.Books.Columns.Add("ID", 50);
			this.Books.Columns.Add("Title", 100);
			this.Books.Columns.Add("Creator", 100);
			foreach (var item in items)
			{
				this.Books.Items.Add(item);
			}

			this.Books.Location = new System.Drawing.Point(111, 122);
			this.Books.Name = "Books";
			this.Books.Size = new System.Drawing.Size(321, 119);
			this.Books.TabIndex = 0;
			this.Books.UseCompatibleStateImageBehavior = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(119, 97);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(37, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Books";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(553, 387);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.Books);
			this.Name = "MainForm";
			this.Text = "Main Form";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		private static List<Book> LoadBooksFromLibrary(IDataContext<Book> db)
		{
			var loader = new LibraryLoader(db);
			return loader.LoadLibrary().ToList();
		}

		#endregion

		private System.Windows.Forms.ListView Books;
		private System.Windows.Forms.Label label1;

	}
}

