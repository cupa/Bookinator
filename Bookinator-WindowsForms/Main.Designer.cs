namespace Bookinator_WindowsForms
{
	partial class Main
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
			this.BookList = new System.Windows.Forms.DataGridView();
			this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Creator = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.BookList)).BeginInit();
			this.SuspendLayout();
			// 
			// BookList
			// 
			this.BookList.AccessibleName = "BookList";
			this.BookList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.BookList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Title,
            this.Creator});
			this.BookList.Location = new System.Drawing.Point(12, 67);
			this.BookList.Name = "BookList";
			this.BookList.Size = new System.Drawing.Size(624, 253);
			this.BookList.TabIndex = 0;
			// 
			// Title
			// 
			this.Title.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.Title.DataPropertyName = "Title";
			this.Title.HeaderText = "Title";
			this.Title.Name = "Title";
			// 
			// Creator
			// 
			this.Creator.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.Creator.DataPropertyName = "Creator";
			this.Creator.HeaderText = "Creator";
			this.Creator.Name = "Creator";
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(648, 401);
			this.Controls.Add(this.BookList);
			this.Name = "Main";
			this.Text = "Bookinator";
			((System.ComponentModel.ISupportInitialize)(this.BookList)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView BookList;
		private System.Windows.Forms.DataGridViewTextBoxColumn Title;
		private System.Windows.Forms.DataGridViewTextBoxColumn Creator;
	}
}

