///////////////////////////////////////////////////////////////
// BrowseMonkey - An offline filesystem browser              //
// Shukri Adams (shukri.adams@gmail.com)                     //
// https://github.com/shukriadams/browsemonkey               //
// MIT License (MIT) Copyright (c) 2014 Shukri Adams         //
///////////////////////////////////////////////////////////////

using System.Text;
using System.Windows.Forms;
using vcFramework.Parsers;

namespace BrowseMonkey
{
	/// <summary>
	/// Summary description for FileListExporter.
	/// </summary>
	public class FileListExportDialog : Form
	{

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.cbName = new System.Windows.Forms.CheckBox();
            this.cbDateCreated = new System.Windows.Forms.CheckBox();
            this.cbDateModified = new System.Windows.Forms.CheckBox();
            this.cbSize = new System.Windows.Forms.CheckBox();
            this.cbPath = new System.Windows.Forms.CheckBox();
            this.cbAllFiles = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbName
            // 
            this.cbName.Location = new System.Drawing.Point(15, 28);
            this.cbName.Name = "cbName";
            this.cbName.Size = new System.Drawing.Size(104, 16);
            this.cbName.TabIndex = 0;
            this.cbName.Text = "Name";
            // 
            // cbDateCreated
            // 
            this.cbDateCreated.Location = new System.Drawing.Point(15, 50);
            this.cbDateCreated.Name = "cbDateCreated";
            this.cbDateCreated.Size = new System.Drawing.Size(104, 16);
            this.cbDateCreated.TabIndex = 1;
            this.cbDateCreated.Text = "Date Created";
            // 
            // cbDateModified
            // 
            this.cbDateModified.Location = new System.Drawing.Point(15, 72);
            this.cbDateModified.Name = "cbDateModified";
            this.cbDateModified.Size = new System.Drawing.Size(104, 16);
            this.cbDateModified.TabIndex = 2;
            this.cbDateModified.Text = "Date Modified";
            // 
            // cbSize
            // 
            this.cbSize.Location = new System.Drawing.Point(166, 28);
            this.cbSize.Name = "cbSize";
            this.cbSize.Size = new System.Drawing.Size(104, 16);
            this.cbSize.TabIndex = 3;
            this.cbSize.Text = "Size";
            // 
            // cbPath
            // 
            this.cbPath.Location = new System.Drawing.Point(166, 50);
            this.cbPath.Name = "cbPath";
            this.cbPath.Size = new System.Drawing.Size(104, 16);
            this.cbPath.TabIndex = 4;
            this.cbPath.Text = "Path";
            // 
            // cbAllFiles
            // 
            this.cbAllFiles.Location = new System.Drawing.Point(15, 109);
            this.cbAllFiles.Name = "cbAllFiles";
            this.cbAllFiles.Size = new System.Drawing.Size(344, 22);
            this.cbAllFiles.TabIndex = 5;
            this.cbAllFiles.Text = "Show all files (if unchecked, only selected files will be included)";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Include Properties :";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(284, 137);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(206, 137);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(72, 23);
            this.btnExport.TabIndex = 6;
            this.btnExport.Text = "Ok";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // FileListExportDialog
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(368, 170);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.cbDateCreated);
            this.Controls.Add(this.cbDateModified);
            this.Controls.Add(this.cbSize);
            this.Controls.Add(this.cbPath);
            this.Controls.Add(this.cbAllFiles);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(384, 204);
            this.MinimumSize = new System.Drawing.Size(384, 204);
            this.Name = "FileListExportDialog";
            this.Text = "File List";
            this.Load += new System.EventHandler(this.FileListExportDialog_Load);
            this.ResumeLayout(false);

		}
		#endregion


		#region FIELDS

		private System.Windows.Forms.CheckBox cbName;
		private System.Windows.Forms.CheckBox cbDateCreated;
		private System.Windows.Forms.CheckBox cbDateModified;
		private System.Windows.Forms.CheckBox cbSize;
		private System.Windows.Forms.CheckBox cbPath;
		private System.Windows.Forms.CheckBox cbAllFiles;
		private System.Windows.Forms.Button btnExport;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;

		/// <summary>
		/// Listview from which data will be exported
		/// </summary>
		private ListView _listView;

		/// <summary>
		/// The minimum amount of padding forced on all columns
		/// </summary>
		private string _forcePadding;

		/// <summary>
		/// 
		/// </summary>
		private StringBuilder _x;

		/// <summary>
		/// 
		/// </summary>
		private bool _cancelled;

		#endregion


		#region PROPERTIES

		/// <summary>
		/// 
		/// </summary>
		public string Output
		{
			get
			{
				return _x.ToString();
			}
		}
		

		/// <summary>
		/// 
		/// </summary>
		public bool Cancelled
		{
			get
			{
				return _cancelled;
			}
		}

		
		#endregion


		#region CONSTRUCTORS

		/// <summary>
		/// 
		/// </summary>
		public FileListExportDialog(
			ListView listview
			)
		{
			this.ShowInTaskbar = false;
			InitializeComponent();
			_listView = listview;
			_forcePadding = "   ";
			_x = new StringBuilder();
			_cancelled = true;
		}


		#endregion


		#region DESTRUCTORS

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{

				// save state
				MainForm.StateBag.Add(this.Name + ".cbAllFiles.Checked", cbAllFiles.Checked);
				MainForm.StateBag.Add(this.Name + ".cbDateCreated.Checked", cbDateCreated.Checked);
				MainForm.StateBag.Add(this.Name + ".cbDateModified.Checked", cbDateModified.Checked);
				MainForm.StateBag.Add(this.Name + ".cbName.Checked", cbName.Checked);
				MainForm.StateBag.Add(this.Name + ".cbPath.Checked", cbPath.Checked);
				MainForm.StateBag.Add(this.Name + ".cbSize.Checked", cbSize.Checked);


				if(components != null)
					components.Dispose();

			}
			base.Dispose( disposing );
		}

		#endregion


		#region METHODS

		/// <summary>
		/// 
		/// </summary>
		private void Export(
			)
		{
		
			ListViewItem[] items = null;


			// ########################################
			// checks if there are any items selected
			// in listview if only selected items are
			// to be exported
			// ----------------------------------------
			if (!cbAllFiles.Checked && _listView.SelectedItems.Count == 0)
			{
				MainForm.ConsoleAdd("No files selected in list");
				this.Close();
				return;
			}

			
			// holds max length of each of the 5 colums in listview
			int[] maxColumnLengths = new int[5];
			bool[] processColumn = new bool[5];


			// ########################################
			// 
			// ----------------------------------------
			if (cbName.Checked)processColumn[0] = true;
			if (cbDateCreated.Checked)processColumn[1] = true;
			if (cbDateModified.Checked)processColumn[2] = true;
			if (cbSize.Checked)processColumn[3] = true;
			if (cbPath.Checked)processColumn[4] = true;


			if (cbAllFiles.Checked)
			{
				items = new ListViewItem[_listView.Items.Count];
				_listView.Items.CopyTo(items,0);
			}
			else
			{
				items = new ListViewItem[_listView.SelectedItems.Count];
				_listView.SelectedItems.CopyTo(items,0);
			}

			// ########################################
			// determines max length of each column
			// ----------------------------------------
			foreach(ListViewItem item in items)
				for (int i = 0 ; i < _listView.Columns.Count ; i ++)
					if (item.SubItems[i].Text.Length > maxColumnLengths[i])maxColumnLengths[i] = item.SubItems[i].Text.Length;
			

			// ########################################
			// gets text from listview
			// ----------------------------------------
			foreach(ListViewItem item in items)
			{
				for (int i = 0 ; i < _listView.Columns.Count ; i ++)
				{
					if (!processColumn[i])
						continue;

					// adds cell text
					_x.Append(item.SubItems[i].Text);
					
					// adds padding
					_x.Append(StringFormatLib.CharLine(" ", maxColumnLengths[i] - item.SubItems[i].Text.Length));
					_x.Append(_forcePadding);
				}

				_x.Append("\r\n");
			}

			_cancelled = false;
			this.Close();

		}


		#endregion


		#region EVENTS


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FileListExportDialog_Load(
			object sender, 
			System.EventArgs e
			)
		{
			// retrieves state
			if (MainForm.StateBag.Contains(this.Name + ".cbAllFiles.Checked"))cbAllFiles.Checked = (bool)MainForm.StateBag.Retrieve(this.Name + ".cbAllFiles.Checked");
			if (MainForm.StateBag.Contains(this.Name + ".cbDateCreated.Checked"))cbDateCreated.Checked = (bool)MainForm.StateBag.Retrieve(this.Name + ".cbDateCreated.Checked");
			if (MainForm.StateBag.Contains(this.Name + ".cbDateModified.Checked"))cbDateModified.Checked = (bool)MainForm.StateBag.Retrieve(this.Name + ".cbDateModified.Checked");
			if (MainForm.StateBag.Contains(this.Name + ".cbName.Checked"))cbName.Checked = (bool)MainForm.StateBag.Retrieve(this.Name + ".cbName.Checked");
			if (MainForm.StateBag.Contains(this.Name + ".cbPath.Checked"))cbPath.Checked = (bool)MainForm.StateBag.Retrieve(this.Name + ".cbPath.Checked");
			if (MainForm.StateBag.Contains(this.Name + ".cbSize.Checked"))cbSize.Checked = (bool)MainForm.StateBag.Retrieve(this.Name + ".cbSize.Checked");
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnExport_Click(
			object sender, 
			System.EventArgs e)
		{
			Export();
		}

		

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCancel_Click(
			object sender, 
			System.EventArgs e)
		{
			this.Close();
		}


		#endregion

	}
}

