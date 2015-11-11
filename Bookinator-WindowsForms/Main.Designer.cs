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
			this.FilePath = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.Browse = new System.Windows.Forms.Button();
			this.Title = new System.Windows.Forms.TextBox();
			this.Creator = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.Save = new System.Windows.Forms.Button();
			this.SaveMessage = new System.Windows.Forms.Label();
			this.Cover = new System.Windows.Forms.PictureBox();
			this.ChangeCover = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.Cover)).BeginInit();
			this.SuspendLayout();
			// 
			// FilePath
			// 
			this.FilePath.Location = new System.Drawing.Point(50, 36);
			this.FilePath.Name = "FilePath";
			this.FilePath.Size = new System.Drawing.Size(369, 20);
			this.FilePath.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(37, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(51, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Book File";
			// 
			// Browse
			// 
			this.Browse.Location = new System.Drawing.Point(425, 36);
			this.Browse.Name = "Browse";
			this.Browse.Size = new System.Drawing.Size(50, 23);
			this.Browse.TabIndex = 2;
			this.Browse.Text = "Browse";
			this.Browse.UseVisualStyleBackColor = true;
			this.Browse.Click += new System.EventHandler(this.Browse_Click);
			// 
			// Title
			// 
			this.Title.Location = new System.Drawing.Point(50, 89);
			this.Title.Name = "Title";
			this.Title.Size = new System.Drawing.Size(304, 20);
			this.Title.TabIndex = 3;
			// 
			// Creator
			// 
			this.Creator.Location = new System.Drawing.Point(50, 134);
			this.Creator.Name = "Creator";
			this.Creator.Size = new System.Drawing.Size(304, 20);
			this.Creator.TabIndex = 5;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(37, 73);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(27, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "Title";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(37, 118);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(41, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "Creator";
			// 
			// Save
			// 
			this.Save.Location = new System.Drawing.Point(50, 301);
			this.Save.Name = "Save";
			this.Save.Size = new System.Drawing.Size(136, 23);
			this.Save.TabIndex = 8;
			this.Save.Text = "Save";
			this.Save.UseVisualStyleBackColor = true;
			this.Save.Click += new System.EventHandler(this.Save_Click);
			// 
			// SaveMessage
			// 
			this.SaveMessage.AutoSize = true;
			this.SaveMessage.Location = new System.Drawing.Point(68, 327);
			this.SaveMessage.Name = "SaveMessage";
			this.SaveMessage.Size = new System.Drawing.Size(90, 13);
			this.SaveMessage.TabIndex = 9;
			this.SaveMessage.Text = "Save Successful!";
			this.SaveMessage.Visible = false;
			// 
			// Cover
			// 
			this.Cover.Location = new System.Drawing.Point(398, 78);
			this.Cover.Name = "Cover";
			this.Cover.Size = new System.Drawing.Size(214, 246);
			this.Cover.TabIndex = 10;
			this.Cover.TabStop = false;
			// 
			// ChangeCover
			// 
			this.ChangeCover.Location = new System.Drawing.Point(453, 330);
			this.ChangeCover.Name = "ChangeCover";
			this.ChangeCover.Size = new System.Drawing.Size(75, 23);
			this.ChangeCover.TabIndex = 11;
			this.ChangeCover.Text = "Change";
			this.ChangeCover.UseVisualStyleBackColor = true;
			this.ChangeCover.Click += new System.EventHandler(this.ChangeCover_Click);
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(648, 401);
			this.Controls.Add(this.ChangeCover);
			this.Controls.Add(this.Cover);
			this.Controls.Add(this.SaveMessage);
			this.Controls.Add(this.Save);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.Creator);
			this.Controls.Add(this.Title);
			this.Controls.Add(this.Browse);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.FilePath);
			this.Name = "Main";
			this.Text = "Bookinator";
			((System.ComponentModel.ISupportInitialize)(this.Cover)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox FilePath;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button Browse;
		private System.Windows.Forms.TextBox Title;
		private System.Windows.Forms.TextBox Creator;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button Save;
		private System.Windows.Forms.Label SaveMessage;
		private System.Windows.Forms.PictureBox Cover;
		private System.Windows.Forms.Button ChangeCover;

	}
}

