///////////////////////////////////////////////////////////////
// BrowseMonkey - An offline filesystem browser              //
// Shukri Adams (shukri.adams@gmail.com)                     //
// https://github.com/shukriadams/browsemonkey               //
// MIT License (MIT) Copyright (c) 2014 Shukri Adams         //
///////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using vcFramework.Arrays;
using vcFramework.IO;
using vcFramework.Windows.Forms;

using BrowseMonkeyData;

namespace BrowseMonkey
{

	/// <summary> 
	/// Form from which searches are started
	/// </summary>
    public class SearchArguments : Form
	{

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.txtSearchPhrase = new System.Windows.Forms.TextBox();
			this.cbAllowPartialMatches = new System.Windows.Forms.CheckBox();
			this.lbRecentSearchFolders = new System.Windows.Forms.ListBox();
			this.lblSearchHistory = new System.Windows.Forms.Label();
			this.rbSearchInFolder = new System.Windows.Forms.RadioButton();
			this.rbSearchInVolumes = new System.Windows.Forms.RadioButton();
			this.lblSearchVolumes = new System.Windows.Forms.Label();
			this.lblSearchFiles = new System.Windows.Forms.Label();
			this.txtSearchFolder = new System.Windows.Forms.TextBox();
			this.txtSearchInVolumes = new System.Windows.Forms.TextBox();
			this.panel9 = new System.Windows.Forms.Panel();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.panel3 = new System.Windows.Forms.Panel();
			this.btnGetSearhFolder = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.btnGetVolumePaths = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel4 = new System.Windows.Forms.Panel();
			this.btnSearch = new System.Windows.Forms.Button();
			this.panel5 = new System.Windows.Forms.Panel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.panel9.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel5.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtSearchPhrase
			// 
			this.txtSearchPhrase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtSearchPhrase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtSearchPhrase.Location = new System.Drawing.Point(0, 0);
			this.txtSearchPhrase.Multiline = true;
			this.txtSearchPhrase.Name = "txtSearchPhrase";
			this.txtSearchPhrase.Size = new System.Drawing.Size(144, 20);
			this.txtSearchPhrase.TabIndex = 4;
			this.txtSearchPhrase.Text = "";
			this.txtSearchPhrase.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchPhrase_KeyDown);
			this.txtSearchPhrase.TextChanged += new System.EventHandler(this.txtSearchPhrase_TextChanged);
			// 
			// cbAllowPartialMatches
			// 
			this.cbAllowPartialMatches.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.cbAllowPartialMatches.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.cbAllowPartialMatches.Location = new System.Drawing.Point(8, 40);
			this.cbAllowPartialMatches.Name = "cbAllowPartialMatches";
			this.cbAllowPartialMatches.Size = new System.Drawing.Size(200, 16);
			this.cbAllowPartialMatches.TabIndex = 6;
			this.cbAllowPartialMatches.Text = "Allow partial word matches";
			this.cbAllowPartialMatches.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			// 
			// lbRecentSearchFolders
			// 
			this.lbRecentSearchFolders.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lbRecentSearchFolders.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lbRecentSearchFolders.Location = new System.Drawing.Point(0, 0);
			this.lbRecentSearchFolders.Name = "lbRecentSearchFolders";
			this.lbRecentSearchFolders.Size = new System.Drawing.Size(184, 184);
			this.lbRecentSearchFolders.TabIndex = 23;
			this.lbRecentSearchFolders.DoubleClick += new System.EventHandler(this.SetSearchFolderFromHistory);
			this.lbRecentSearchFolders.SelectedIndexChanged += new System.EventHandler(this.SetSearchFolderFromHistory);
			// 
			// lblSearchHistory
			// 
			this.lblSearchHistory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblSearchHistory.Location = new System.Drawing.Point(24, 120);
			this.lblSearchHistory.Name = "lblSearchHistory";
			this.lblSearchHistory.Size = new System.Drawing.Size(184, 16);
			this.lblSearchHistory.TabIndex = 14;
			this.lblSearchHistory.Text = "Recent search folders (click to select) :";
			// 
			// rbSearchInFolder
			// 
			this.rbSearchInFolder.Location = new System.Drawing.Point(8, 104);
			this.rbSearchInFolder.Name = "rbSearchInFolder";
			this.rbSearchInFolder.Size = new System.Drawing.Size(16, 16);
			this.rbSearchInFolder.TabIndex = 2;
			this.rbSearchInFolder.CheckedChanged += new System.EventHandler(this.SearchInFolderOrFiles_Changed);
			// 
			// rbSearchInVolumes
			// 
			this.rbSearchInVolumes.Location = new System.Drawing.Point(8, 48);
			this.rbSearchInVolumes.Name = "rbSearchInVolumes";
			this.rbSearchInVolumes.Size = new System.Drawing.Size(16, 16);
			this.rbSearchInVolumes.TabIndex = 0;
			this.rbSearchInVolumes.CheckedChanged += new System.EventHandler(this.SearchInFolderOrFiles_Changed);
			// 
			// lblSearchVolumes
			// 
			this.lblSearchVolumes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblSearchVolumes.Location = new System.Drawing.Point(24, 24);
			this.lblSearchVolumes.Name = "lblSearchVolumes";
			this.lblSearchVolumes.Size = new System.Drawing.Size(184, 16);
			this.lblSearchVolumes.TabIndex = 18;
			this.lblSearchVolumes.Text = "Files(s) :";
			// 
			// lblSearchFiles
			// 
			this.lblSearchFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblSearchFiles.Location = new System.Drawing.Point(24, 80);
			this.lblSearchFiles.Name = "lblSearchFiles";
			this.lblSearchFiles.Size = new System.Drawing.Size(184, 16);
			this.lblSearchFiles.TabIndex = 17;
			this.lblSearchFiles.Text = "All files nested under folder :";
			// 
			// txtSearchFolder
			// 
			this.txtSearchFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtSearchFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtSearchFolder.Location = new System.Drawing.Point(0, 0);
			this.txtSearchFolder.Multiline = true;
			this.txtSearchFolder.Name = "txtSearchFolder";
			this.txtSearchFolder.ReadOnly = true;
			this.txtSearchFolder.Size = new System.Drawing.Size(160, 20);
			this.txtSearchFolder.TabIndex = 15;
			this.txtSearchFolder.TabStop = false;
			this.txtSearchFolder.Text = "";
			// 
			// txtSearchInVolumes
			// 
			this.txtSearchInVolumes.AllowDrop = true;
			this.txtSearchInVolumes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtSearchInVolumes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtSearchInVolumes.Location = new System.Drawing.Point(0, 0);
			this.txtSearchInVolumes.Multiline = true;
			this.txtSearchInVolumes.Name = "txtSearchInVolumes";
			this.txtSearchInVolumes.ReadOnly = true;
			this.txtSearchInVolumes.Size = new System.Drawing.Size(160, 20);
			this.txtSearchInVolumes.TabIndex = 7;
			this.txtSearchInVolumes.TabStop = false;
			this.txtSearchInVolumes.Text = "";
			// 
			// panel9
			// 
			this.panel9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel9.Controls.Add(this.groupBox2);
			this.panel9.Location = new System.Drawing.Point(0, 0);
			this.panel9.Name = "panel9";
			this.panel9.Size = new System.Drawing.Size(232, 344);
			this.panel9.TabIndex = 31;
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.groupBox3);
			this.groupBox2.Controls.Add(this.lblSearchHistory);
			this.groupBox2.Controls.Add(this.rbSearchInFolder);
			this.groupBox2.Controls.Add(this.rbSearchInVolumes);
			this.groupBox2.Controls.Add(this.lblSearchVolumes);
			this.groupBox2.Controls.Add(this.lblSearchFiles);
			this.groupBox2.Controls.Add(this.panel3);
			this.groupBox2.Controls.Add(this.panel2);
			this.groupBox2.Controls.Add(this.panel1);
			this.groupBox2.Location = new System.Drawing.Point(8, 8);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(216, 328);
			this.groupBox2.TabIndex = 26;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Search In";
			// 
			// groupBox3
			// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox3.Location = new System.Drawing.Point(16, 64);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(184, 8);
			this.groupBox3.TabIndex = 26;
			this.groupBox3.TabStop = false;
			// 
			// panel3
			// 
			this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel3.Controls.Add(this.btnGetSearhFolder);
			this.panel3.Controls.Add(this.txtSearchFolder);
			this.panel3.Location = new System.Drawing.Point(24, 96);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(184, 20);
			this.panel3.TabIndex = 22;
			// 
			// btnGetSearhFolder
			// 
			this.btnGetSearhFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnGetSearhFolder.Location = new System.Drawing.Point(162, 0);
			this.btnGetSearhFolder.Name = "btnGetSearhFolder";
			this.btnGetSearhFolder.Size = new System.Drawing.Size(20, 20);
			this.btnGetSearhFolder.TabIndex = 16;
			this.btnGetSearhFolder.Text = "..";
			this.btnGetSearhFolder.Click += new System.EventHandler(this.btnGetSearhFolder_Click);
			// 
			// panel2
			// 
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel2.Controls.Add(this.btnGetVolumePaths);
			this.panel2.Controls.Add(this.txtSearchInVolumes);
			this.panel2.Location = new System.Drawing.Point(24, 40);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(184, 20);
			this.panel2.TabIndex = 21;
			// 
			// btnGetVolumePaths
			// 
			this.btnGetVolumePaths.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnGetVolumePaths.Location = new System.Drawing.Point(162, 0);
			this.btnGetVolumePaths.Name = "btnGetVolumePaths";
			this.btnGetVolumePaths.Size = new System.Drawing.Size(20, 20);
			this.btnGetVolumePaths.TabIndex = 17;
			this.btnGetVolumePaths.Text = "..";
			this.btnGetVolumePaths.Click += new System.EventHandler(this.btnGetVolumePaths_Click);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Controls.Add(this.lbRecentSearchFolders);
			this.panel1.Location = new System.Drawing.Point(24, 136);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(184, 184);
			this.panel1.TabIndex = 25;
			// 
			// panel4
			// 
			this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel4.Controls.Add(this.btnSearch);
			this.panel4.Controls.Add(this.txtSearchPhrase);
			this.panel4.Location = new System.Drawing.Point(8, 16);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(200, 20);
			this.panel4.TabIndex = 23;
			// 
			// btnSearch
			// 
			this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSearch.Location = new System.Drawing.Point(144, 0);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(56, 20);
			this.btnSearch.TabIndex = 6;
			this.btnSearch.Text = "Search";
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// panel5
			// 
			this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel5.Controls.Add(this.groupBox1);
			this.panel5.Location = new System.Drawing.Point(0, 344);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(232, 72);
			this.panel5.TabIndex = 33;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.panel4);
			this.groupBox1.Controls.Add(this.cbAllowPartialMatches);
			this.groupBox1.Location = new System.Drawing.Point(8, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(216, 64);
			this.groupBox1.TabIndex = 34;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Search For";
			// 
			// SearchArguments
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(232, 414);
			this.Controls.Add(this.panel5);
			this.Controls.Add(this.panel9);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "SearchArguments";
			this.Text = "Search";
			this.Load += new System.EventHandler(this.SearchArguments_Load);
			this.panel9.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel5.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


		#region FIELDS

		private System.Windows.Forms.TextBox txtSearchPhrase;
		private System.Windows.Forms.CheckBox cbAllowPartialMatches;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TextBox txtSearchInVolumes;
		private System.Windows.Forms.TextBox txtSearchFolder;
		private System.Windows.Forms.RadioButton rbSearchInVolumes;
		private System.Windows.Forms.RadioButton rbSearchInFolder;
		private System.Windows.Forms.ListBox lbRecentSearchFolders;
		private System.Windows.Forms.Label lblSearchHistory;
		private System.Windows.Forms.Label lblSearchVolumes;
		private System.Windows.Forms.Label lblSearchFiles;
		private System.Windows.Forms.Panel panel9;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel panel4;
		private ContextMenu cmSearchHistory;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button btnGetSearhFolder;
		private System.Windows.Forms.Button btnGetVolumePaths;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Button btnSearch;
		private MenuItem mnuClearSearchHistory;

		#endregion
	

		#region CONSTRUCTORS

		/// <summary> 
		/// 
		/// </summary>
		public SearchArguments(
			)
		{

			InitializeComponent();
			txtSearchInVolumes.AllowDrop = true;
			txtSearchInVolumes.DragEnter += new DragEventHandler(txtSearchInVolumes_DragEnter);
			txtSearchInVolumes.DragDrop += new DragEventHandler(txtSearchInVolumes_DragDrop);
			txtSearchFolder.AllowDrop = true;
			txtSearchFolder.DragEnter += new DragEventHandler(txtSearchFolder_DragEnter);
			txtSearchFolder.DragDrop += new DragEventHandler(txtSearchFolder_DragDrop);
		}
		

		#endregion
		

		#region DESTUCTORS

		/// <summary> </summary>
		/// <param name="disposing"></param>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				
				// save state
				MainForm.StateBag.Add("SearchArguments.rbSearchInFolder", rbSearchInFolder.Checked);
				MainForm.StateBag.Add("SearchArguments.rbSearchInVolumes", rbSearchInVolumes.Checked);
				MainForm.StateBag.Add("SearchArguments.cbAllowPartialMatches", cbAllowPartialMatches.Checked);
					
				
				if(components != null)
					components.Dispose();

			}
			
			base.Dispose( disposing );
		}
		

		#endregion


		#region METHODS

		
		/// <summary> 
		/// Sets the contents of the volumes text field to contain the items
		/// in the text array - array is assumed to contain valid file names
		/// </summary>
		public void SetSearchVolumes(
			string[] volumes
			)
		{

			txtSearchInVolumes.Text = "";

			foreach(string volume in volumes)
				txtSearchInVolumes.Text += volume + "|";
				
				
		}



		/// <summary> 
		/// Responsible for launching a searchresults form 
		/// </summary>
		public void Search(
			)
		{

			string[] volumes		= null;
			string[] searchWords	= new string[1];
			string failMessage		= string.Empty;				// contains message to user for why search invocation has failed
			bool proceed			= true;

		
			// #######################################################
			// 1. populates search argument array - only one argument 
			// used, for now
			// -------------------------------------------------------
			searchWords = txtSearchPhrase.Text.Split(new char[]{' '});
			searchWords = StringArrayLib.RemoveEmptyStrings(searchWords);
			if (searchWords.Length == 0)
			{
				failMessage = "You haven't entered any text to search for.";
				proceed = false;					
			}



			// #######################################################
			// Gets a list of volumes to search in. These files are 
			// either specified directly in volumes field, or a 
			// search folder is given, in which case we get a list
			// of all files in that search folder
			// -------------------------------------------------------
			if (rbSearchInFolder.Checked)
			{
				if (txtSearchFolder.Text.Length == 0)
				{
					failMessage = "Please enter a search folder";
					proceed = false;						
				}
				
				if (proceed)
					if (!Directory.Exists(txtSearchFolder.Text))
					{
						failMessage = txtSearchFolder.Text + " is not a valid folder name, or the folder does not exist";
						proceed = false;						
					}

				if (proceed)
					volumes = FileSystemLib.GetFilesUnder(txtSearchFolder.Text);
			}
			else if (rbSearchInVolumes.Checked)
			{

				volumes = txtSearchInVolumes.Text.Split('|');
				volumes = StringArrayLib.RemoveEmptyStrings(volumes);

				// Ensures that at least one volume has been selected
				if (volumes.Length == 0)
				{	
					failMessage = "You need to select at least one volume to search in.";
					proceed = false;				
				}
			}


			// #######################################################
			// checks for valid volumes
			// -------------------------------------------------------
			if (proceed)
			{
				ArrayList tmpVolumes = ArrayLib.ToArrayList(volumes);
				int count = tmpVolumes.Count;
				for (int i = 0 ; i < count ; i ++)
				{
					if (!VolumeIdentificationLib.FileIsVolume(tmpVolumes[count - i - 1].ToString()))
					{
						// if searching in specific files, inform the user which
						// files are invalid
						if (rbSearchInVolumes.Checked)
							MainForm.ConsoleAdd(
								"'" + tmpVolumes[count - i - 1].ToString() + "' is not a valid volume");
					
						tmpVolumes.RemoveAt(count - i - 1);
					}
				}

				volumes = (string[])tmpVolumes.ToArray(typeof(string));

				if (volumes.Length == 0)
				{	
					// we dont need to report a general search failure when searching
					// in specific files, as each invalid file is already reported
					// to console (see above)
					if (rbSearchInFolder.Checked)
						failMessage = "No valid volumes were found in the specified location";

					proceed = false;				
				}
			}


			// #######################################################
			// Exits if search conditions not met
			// -------------------------------------------------------
			if (!proceed)
			{
				if (failMessage != string.Empty)
					MainForm.ConsoleAdd(failMessage);
	
				return;
			}
			


			// #######################################################
			// starts search
			// -------------------------------------------------------
			// spawns form which runs, and dislays results of, the search
			FormFactory.SpawnSearch(
				searchWords,
				volumes,
				cbAllowPartialMatches.Checked);
				
			// adds search folder to "history"
			if (rbSearchInFolder.Checked)
				MainForm.RecentSearchFolders.Add(
					txtSearchFolder.Text);


		}

			

		/// <summary>
		/// Sets the folder in which to search. All volumes in this folder will be
		/// searched in. This method is invoked from amonst the others the "select
		/// search folder" button on this form
		/// </summary>
		private void SetSearchFolder(
			)
		{
			
			FolderBrowserDialog dialog = new FolderBrowserDialog();

			// tries to find last selected folder from state bag
			if (MainForm.StateBag.Contains("SearchArguments.LastSearchFolder"))
				dialog.SelectedPath = Convert.ToString(MainForm.StateBag.Retrieve("SearchArguments.LastSearchFolder"));

			dialog.ShowDialog();

			if (dialog.SelectedPath != String.Empty)
			{
				txtSearchFolder.Text = dialog.SelectedPath;

				// saves selected folder path to statebag
				MainForm.StateBag.Add("SearchArguments.LastSearchFolder", dialog.SelectedPath);
			}

			if (dialog != null)
				dialog.Dispose();

		}



		/// <summary>
		/// Sets the visibility/enabled status of search elements. Elements are of 
		/// two types - file-based and folder-based. 
		/// </summary>
		private void SetSearchElementVisibility(
			)
		{
			
			if (rbSearchInFolder.Checked)
			{
				txtSearchFolder.Enabled = true;
				lbRecentSearchFolders.Enabled = true;
				btnGetSearhFolder.Enabled = true;
				lblSearchHistory.Enabled = true;
				lblSearchFiles.Enabled = true;

				txtSearchInVolumes.Enabled = false;
				btnGetVolumePaths.Enabled = false;

				lblSearchVolumes.Enabled = false;
			}
			else if (rbSearchInVolumes.Checked)
			{
				txtSearchFolder.Enabled = false;
				lbRecentSearchFolders.Enabled = false;
				btnGetSearhFolder.Enabled = false;
			
				lblSearchHistory.Enabled = false;
				lblSearchFiles.Enabled = false;

				txtSearchInVolumes.Enabled = true;
				btnGetVolumePaths.Enabled = true;
				lblSearchVolumes.Enabled = true;
			}

		}

		

		#endregion

		
		#region EVENTS


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SearchArguments_Load(
			object sender, 
			System.EventArgs e
			)
		{

			// ################################################################
			// load state
			// ----------------------------------------------------------------
			if(MainForm.StateBag.Contains("SearchArguments.rbSearchInFolder"))
				rbSearchInFolder.Checked = Convert.ToBoolean(MainForm.StateBag.Retrieve("SearchArguments.rbSearchInFolder"));
			if (MainForm.StateBag.Contains("SearchArguments.rbSearchInVolumes"))
				rbSearchInVolumes.Checked = Convert.ToBoolean(MainForm.StateBag.Retrieve("SearchArguments.rbSearchInVolumes"));
			if (MainForm.StateBag.Contains("SearchArguments.cbAllowPartialMatches"))
				cbAllowPartialMatches.Checked = Convert.ToBoolean(MainForm.StateBag.Retrieve("SearchArguments.cbAllowPartialMatches"));


			// ################################################################
			// set up context menu for search history
			// ----------------------------------------------------------------
			cmSearchHistory = new ContextMenu();
			mnuClearSearchHistory = new MenuItem("Clear history");
			mnuClearSearchHistory.Click += new EventHandler(ClearSearchHistory_Click);
			cmSearchHistory.MenuItems.Add(mnuClearSearchHistory);
			lbRecentSearchFolders.ContextMenu = cmSearchHistory;


			// ################################################################
			// sets the default search args if nothing is selected
			// ----------------------------------------------------------------
			if (!rbSearchInVolumes.Checked && !rbSearchInFolder.Checked)
				rbSearchInVolumes.Checked = true;



			// ################################################################
			// binds to the "onItemsCountChange" event in recent search folders
			// collection. This is used to update the contents of the recent
			// search folders listbox on this form
			// ----------------------------------------------------------------
			MainForm.RecentSearchFolders.ItemCountChanged += new EventHandler(RecentSearchFolders_ItemCountChanged);


			
			// ################################################################
			// force events
			// ----------------------------------------------------------------
			RecentSearchFolders_ItemCountChanged(null,null);
			txtSearchPhrase_TextChanged(null,null);

		}



		/// <summary>
		/// Invoked when the "clear history" context menu is clicked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ClearSearchHistory_Click(
			object sender,
			EventArgs e
			)
		{
			MainForm.RecentSearchFolders.Clear();
		}



		/// <summary>
		/// Invoked whenever item(s) are added ro removed from the recent
		/// search folders collection. This typically happens whenever a 
		/// search is conducted in a folder. This method will update the
		/// contents of the listbox on this form to reflect the contents
		/// of the recent search folders collection
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RecentSearchFolders_ItemCountChanged(
			object sender, 
			EventArgs e
			)
		{
			vcFramework.Windows.Forms.ListBoxLib.PopulateListBox(
				lbRecentSearchFolders,
				MainForm.RecentSearchFolders.Items);

		}



		/// <summary>
		/// Invoked when the "clear recent search folders" button is clicked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnClearRecentSearchFolders_Click(
			object sender, 
			System.EventArgs e
			)
		{
			MainForm.RecentSearchFolders.Clear();
		}



		/// <summary>
		/// Invoked by changing index in or clicking on an item in lbRecentSearchFolders 
		/// (recent search folders listbox). Sets the selected item in the listbox as
		/// the search folder.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SetSearchFolderFromHistory(
			object sender, 
			System.EventArgs e
			)
		{
			
			if (lbRecentSearchFolders.Items.Count == 0 || lbRecentSearchFolders.SelectedValue == null)
				return;

			// listbox populated using DisplayValueItem structs. Sometimes the selected value
			// is a struct, other times the value is a string.
			if (lbRecentSearchFolders.SelectedValue is DisplayValueItem)
			{
				DisplayValueItem item = (DisplayValueItem)lbRecentSearchFolders.SelectedValue;
				txtSearchFolder.Text = item.ValueMember;
			}
			else if (lbRecentSearchFolders.SelectedValue is String)
				txtSearchFolder.Text = lbRecentSearchFolders.SelectedValue.ToString();

			

		}



		/// <summary>
		/// Fired when the "Search" button is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSearch_Click(
			object sender, 
			EventArgs e
			)
		{
			try
			{
				Search();
			}
			catch(Exception ex)
			{
				MainForm.ConsoleAdd(ex);			
			}

		}




		/// <summary> 
		/// Invokes search in response to enter key being pressed on 
		/// search argument text box. A search commences only if there
		/// is text in the search field
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtSearchPhrase_KeyDown(
			object sender, 
			KeyEventArgs e
			)
		{
			try
			{
				if (e.KeyCode == System.Windows.Forms.Keys.Enter && txtSearchPhrase.Text.Length > 0)
					Search();
			}
			catch(Exception ex)
			{
				MainForm.ConsoleAdd(ex);			
			}

		}




		/// <summary> 
		/// Throws up dialogue for selecting one or more volume files for 
		/// searching in. Volume files are saved to a text field, and upon 
		/// search start, will be used as search volumes. 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnGetVolumePaths_Click(
			object sender, 
			EventArgs e
			)
		{
			try
			{

				OpenFileDialog objVolumeSelectDialog	= new OpenFileDialog();
				objVolumeSelectDialog.Multiselect		= true;

				// try to get the previous selected volume path
				if (MainForm.StateBag.Contains("SearchArguments.LastSearchVolumePath"))
					objVolumeSelectDialog.InitialDirectory = Convert.ToString(MainForm.StateBag.Retrieve("SearchArguments.LastSearchVolumePath"));

				objVolumeSelectDialog.ShowDialog();

				// stores selected volume files in array for searching.
				if (objVolumeSelectDialog.FileNames.Length > 0)
				{
					this.SetSearchVolumes(
						objVolumeSelectDialog.FileNames);

					// save the selected path to statebag
					MainForm.StateBag.Add("SearchArguments.LastSearchVolumePath", Path.GetDirectoryName(objVolumeSelectDialog.FileNames[0]));
				}


				objVolumeSelectDialog.Dispose();

			}
			catch(Exception ex)
			{
				MainForm.ConsoleAdd(ex);			
			}

		}



		/// <summary>
		/// Throws up dialogue for selecting a folder in which to search. All
		/// volumes in this folder will be searched in.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnGetSearhFolder_Click(
			object sender, 
			System.EventArgs e
			)
		{

			SetSearchFolder();

		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SearchInFolderOrFiles_Changed(
			object sender, 
			System.EventArgs e
			)
		{
			SetSearchElementVisibility();
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtSearchPhrase_TextChanged(
			object sender, 
			System.EventArgs e
			)
		{
			
			btnSearch.Enabled = false;
			 
			if (txtSearchPhrase.Text.Length > 0)
				btnSearch.Enabled = true;

		}


		#endregion


		#region EVENTS - DragDrop


		/// <summary>
		/// Handles dragging of items onto "search in volumes" text field.
		/// Allows only files to be handled - everything is ignored
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtSearchInVolumes_DragEnter(
			object sender, 
			DragEventArgs e
			)
		{
			try
			{
				// checks if the items being dropped are files (or folders) -
				// dragging behaviour is only allowed for files and folders
				if( e.Data.GetDataPresent(DataFormats.FileDrop, false) == true )
				{

					// gets array of files dropped on this form
					string[] droppedFiles = (string[])e.Data.GetData(DataFormats.FileDrop);

					// ensure that only files are dropped
					for (int i = 0 ; i < droppedFiles.Length ; i ++)
						if (!File.Exists(droppedFiles[i]))
							droppedFiles[i] = string.Empty;
				
					// remove empty members
					droppedFiles = vcFramework.Arrays.StringArrayLib.RemoveEmptyStrings(
						droppedFiles);
			
					if (droppedFiles.Length == 0)
						return;

					e.Effect = DragDropEffects.All;
				}
			}
			catch(Exception ex)
			{
				MainForm.ConsoleAdd(ex);		
			}
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtSearchInVolumes_DragDrop(
			object sender, 
			DragEventArgs e
			)
		{
			
			try
			{

				// gets array of files dropped on this form
				string[] droppedFiles = (string[])e.Data.GetData(DataFormats.FileDrop);

				// ensure that only files are dropped
				for (int i = 0 ; i < droppedFiles.Length ; i ++)
					if (!File.Exists(droppedFiles[i]))
						droppedFiles[i] = string.Empty;
				
				// remove empty members
				droppedFiles = vcFramework.Arrays.StringArrayLib.RemoveEmptyStrings(
					droppedFiles);
			
				if (droppedFiles.Length == 0)
					return;

				this.SetSearchVolumes(
					droppedFiles);

			}
			catch(Exception ex)
			{
				MainForm.ConsoleAdd(ex);		
			}
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtSearchFolder_DragEnter(
			object sender, 
			DragEventArgs e
			)
		{
			try
			{
				// checks if the items being dropped are files (or folders) -
				// dragging behaviour is only allowed for files and folders
				if( e.Data.GetDataPresent(DataFormats.FileDrop, false) == true )
				{

					// gets array of files dropped on this form
					string[] droppedFolders = (string[])e.Data.GetData(DataFormats.FileDrop);

					// only 1 folder can be searched in at a time
					if (droppedFolders.Length != 1)
						return;

					if (!Directory.Exists(droppedFolders[0]))
						return;

					e.Effect = DragDropEffects.All;

				}
			}
			catch(Exception ex)
			{
				MainForm.ConsoleAdd(ex);		
			}
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtSearchFolder_DragDrop(
			object sender, 
			DragEventArgs e
			)
		{
			
			try
			{

				// gets array of files dropped on this form
				string[] droppedFolders = (string[])e.Data.GetData(DataFormats.FileDrop);

				// only 1 folder can be searched in at a time
				if (droppedFolders.Length != 1)
					return;

				if (!Directory.Exists(droppedFolders[0]))
					return;

				txtSearchFolder.Text = droppedFolders[0];

				// saves selected folder path to statebag
				MainForm.StateBag.Add("SearchArguments.LastSearchFolder", droppedFolders[0]);

			}
			catch(Exception ex)
			{
				MainForm.ConsoleAdd(ex);		
			}
		}



		#endregion


	}
}
