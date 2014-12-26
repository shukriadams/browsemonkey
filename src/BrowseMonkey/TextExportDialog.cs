///////////////////////////////////////////////////////////////
// BrowseMonkey - An offline filesystem browser              //
// Shukri Adams (shukri.adams@gmail.com)                     //
// https://github.com/shukriadams/browsemonkey               //
// MIT License (MIT) Copyright (c) 2014 Shukri Adams         //
///////////////////////////////////////////////////////////////

using System;
using System.Threading;
using System.Windows.Forms;
using vcFramework.Delegates;
using vcFramework.Windows.Forms;
using vcFramework.Threads;
using vcFramework.Time;
using FSXml;
using BrowseMonkeyData;

namespace BrowseMonkey
{
	/// <summary>
	/// Summary description for TextExporter.
	/// </summary>
    public class TextExportDialog : Form
	{

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.panel1 = new System.Windows.Forms.Panel();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.btnExport = new System.Windows.Forms.Button();
			this.btnExit = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.cbFolderName = new System.Windows.Forms.CheckBox();
			this.cbFolderPath = new System.Windows.Forms.CheckBox();
			this.cbFolderDateModified = new System.Windows.Forms.CheckBox();
			this.cbFolderDateCreated = new System.Windows.Forms.CheckBox();
			this.cbFilePath = new System.Windows.Forms.CheckBox();
			this.cbFileDateCreated = new System.Windows.Forms.CheckBox();
			this.cbFileName = new System.Windows.Forms.CheckBox();
			this.cbFileExtension = new System.Windows.Forms.CheckBox();
			this.cbFileSize = new System.Windows.Forms.CheckBox();
			this.cbFileDateModified = new System.Windows.Forms.CheckBox();
			this.panels = new vcFramework.Windows.Forms.PanelGallery();
			this.pnlStandardContents = new System.Windows.Forms.Panel();
			this.pnlProgress = new System.Windows.Forms.Panel();
			this.btnCancel = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.pbExport = new System.Windows.Forms.ProgressBar();
			this.btnClose = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.panels.SuspendLayout();
			this.pnlStandardContents.SuspendLayout();
			this.pnlProgress.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Controls.Add(this.groupBox2);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.btnExport);
			this.panel1.Controls.Add(this.btnExit);
			this.panel1.Controls.Add(this.groupBox1);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.cbFolderName);
			this.panel1.Controls.Add(this.cbFolderPath);
			this.panel1.Controls.Add(this.cbFolderDateModified);
			this.panel1.Controls.Add(this.cbFolderDateCreated);
			this.panel1.Controls.Add(this.cbFilePath);
			this.panel1.Controls.Add(this.cbFileDateCreated);
			this.panel1.Controls.Add(this.cbFileName);
			this.panel1.Controls.Add(this.cbFileExtension);
			this.panel1.Controls.Add(this.cbFileSize);
			this.panel1.Controls.Add(this.cbFileDateModified);
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(248, 256);
			this.panel1.TabIndex = 1;
			// 
			// groupBox2
			// 
			this.groupBox2.Location = new System.Drawing.Point(8, 136);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(232, 8);
			this.groupBox2.TabIndex = 25;
			this.groupBox2.TabStop = false;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(8, 120);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(36, 16);
			this.label3.TabIndex = 24;
			this.label3.Text = "Files:";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(8, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 16);
			this.label2.TabIndex = 23;
			this.label2.Text = "Folders:";
			// 
			// btnExport
			// 
			this.btnExport.Location = new System.Drawing.Point(72, 224);
			this.btnExport.Name = "btnExport";
			this.btnExport.TabIndex = 10;
			this.btnExport.Text = "&Ok";
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			// 
			// btnExit
			// 
			this.btnExit.Location = new System.Drawing.Point(160, 224);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(75, 24);
			this.btnExit.TabIndex = 11;
			this.btnExit.Text = "&Cancel";
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Location = new System.Drawing.Point(8, 40);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(232, 8);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(240, 32);
			this.label1.TabIndex = 22;
			this.label1.Text = "Select the file / folder properties to include in text export";
			// 
			// cbFolderName
			// 
			this.cbFolderName.Location = new System.Drawing.Point(24, 80);
			this.cbFolderName.Name = "cbFolderName";
			this.cbFolderName.Size = new System.Drawing.Size(64, 16);
			this.cbFolderName.TabIndex = 0;
			this.cbFolderName.Text = "Name";
			// 
			// cbFolderPath
			// 
			this.cbFolderPath.Location = new System.Drawing.Point(24, 96);
			this.cbFolderPath.Name = "cbFolderPath";
			this.cbFolderPath.Size = new System.Drawing.Size(64, 16);
			this.cbFolderPath.TabIndex = 1;
			this.cbFolderPath.Text = "Path";
			// 
			// cbFolderDateModified
			// 
			this.cbFolderDateModified.Location = new System.Drawing.Point(144, 96);
			this.cbFolderDateModified.Name = "cbFolderDateModified";
			this.cbFolderDateModified.Size = new System.Drawing.Size(96, 16);
			this.cbFolderDateModified.TabIndex = 3;
			this.cbFolderDateModified.Text = "Date Modified";
			// 
			// cbFolderDateCreated
			// 
			this.cbFolderDateCreated.Location = new System.Drawing.Point(144, 80);
			this.cbFolderDateCreated.Name = "cbFolderDateCreated";
			this.cbFolderDateCreated.Size = new System.Drawing.Size(96, 16);
			this.cbFolderDateCreated.TabIndex = 2;
			this.cbFolderDateCreated.Text = "Date Created";
			// 
			// cbFilePath
			// 
			this.cbFilePath.Location = new System.Drawing.Point(24, 168);
			this.cbFilePath.Name = "cbFilePath";
			this.cbFilePath.Size = new System.Drawing.Size(96, 16);
			this.cbFilePath.TabIndex = 5;
			this.cbFilePath.Text = "Path";
			// 
			// cbFileDateCreated
			// 
			this.cbFileDateCreated.Location = new System.Drawing.Point(24, 184);
			this.cbFileDateCreated.Name = "cbFileDateCreated";
			this.cbFileDateCreated.Size = new System.Drawing.Size(96, 16);
			this.cbFileDateCreated.TabIndex = 6;
			this.cbFileDateCreated.Text = "Date Created";
			// 
			// cbFileName
			// 
			this.cbFileName.Location = new System.Drawing.Point(24, 152);
			this.cbFileName.Name = "cbFileName";
			this.cbFileName.Size = new System.Drawing.Size(96, 16);
			this.cbFileName.TabIndex = 4;
			this.cbFileName.Text = "Name";
			// 
			// cbFileExtension
			// 
			this.cbFileExtension.Location = new System.Drawing.Point(144, 184);
			this.cbFileExtension.Name = "cbFileExtension";
			this.cbFileExtension.Size = new System.Drawing.Size(96, 16);
			this.cbFileExtension.TabIndex = 9;
			this.cbFileExtension.Text = "Extension";
			// 
			// cbFileSize
			// 
			this.cbFileSize.Location = new System.Drawing.Point(144, 168);
			this.cbFileSize.Name = "cbFileSize";
			this.cbFileSize.Size = new System.Drawing.Size(96, 16);
			this.cbFileSize.TabIndex = 8;
			this.cbFileSize.Text = "Size";
			// 
			// cbFileDateModified
			// 
			this.cbFileDateModified.Location = new System.Drawing.Point(144, 152);
			this.cbFileDateModified.Name = "cbFileDateModified";
			this.cbFileDateModified.Size = new System.Drawing.Size(96, 16);
			this.cbFileDateModified.TabIndex = 7;
			this.cbFileDateModified.Text = "Date Modified";
			// 
			// panels
			// 
			this.panels.ActivePanelIndex = 0;
			this.panels.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panels.BackColor = System.Drawing.SystemColors.Control;
			this.panels.Controls.Add(this.pnlStandardContents);
			this.panels.Controls.Add(this.pnlProgress);
			this.panels.Location = new System.Drawing.Point(0, 8);
			this.panels.Name = "panels";
			this.panels.Panels.Add(this.pnlStandardContents);
			this.panels.Panels.Add(this.pnlProgress);
			this.panels.Size = new System.Drawing.Size(248, 256);
			this.panels.TabIndex = 17;
			this.panels.TabStop = false;
			// 
			// pnlStandardContents
			// 
			this.pnlStandardContents.Controls.Add(this.panel1);
			this.pnlStandardContents.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlStandardContents.Location = new System.Drawing.Point(0, 0);
			this.pnlStandardContents.Name = "pnlStandardContents";
			this.pnlStandardContents.Size = new System.Drawing.Size(248, 256);
			this.pnlStandardContents.TabIndex = 0;
			// 
			// pnlProgress
			// 
			this.pnlProgress.Controls.Add(this.btnCancel);
			this.pnlProgress.Controls.Add(this.label5);
			this.pnlProgress.Controls.Add(this.pbExport);
			this.pnlProgress.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlProgress.Location = new System.Drawing.Point(0, 0);
			this.pnlProgress.Name = "pnlProgress";
			this.pnlProgress.Size = new System.Drawing.Size(248, 256);
			this.pnlProgress.TabIndex = 0;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnCancel.Location = new System.Drawing.Point(68, 148);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(112, 23);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.Location = new System.Drawing.Point(8, 100);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(232, 16);
			this.label5.TabIndex = 1;
			this.label5.Text = "Exporting ...";
			this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// pbExport
			// 
			this.pbExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.pbExport.Location = new System.Drawing.Point(8, 120);
			this.pbExport.Name = "pbExport";
			this.pbExport.Size = new System.Drawing.Size(232, 16);
			this.pbExport.TabIndex = 0;
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(0, 0);
			this.btnClose.Name = "btnClose";
			this.btnClose.TabIndex = 0;
			this.btnClose.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// TextExportDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(250, 264);
			this.Controls.Add(this.panels);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximumSize = new System.Drawing.Size(256, 288);
			this.MinimumSize = new System.Drawing.Size(256, 288);
			this.Name = "TextExportDialog";
			this.Text = "Export As Text";
			this.Load += new System.EventHandler(this.TextExporter_Load);
			this.panel1.ResumeLayout(false);
			this.panels.ResumeLayout(false);
			this.pnlStandardContents.ResumeLayout(false);
			this.pnlProgress.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


		#region FIELDS

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnExport;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Panel pnlStandardContents;
		private System.Windows.Forms.Panel pnlProgress;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ProgressBar pbExport;
		private vcFramework.Windows.Forms.PanelGallery panels;
		private System.Windows.Forms.CheckBox cbFolderName;
		private System.Windows.Forms.CheckBox cbFolderPath;
		private System.Windows.Forms.CheckBox cbFolderDateCreated;
		private System.Windows.Forms.CheckBox cbFolderDateModified;
		private System.Windows.Forms.CheckBox cbFileName;
		private System.Windows.Forms.CheckBox cbFilePath;
		private System.Windows.Forms.CheckBox cbFileDateCreated;
		private System.Windows.Forms.CheckBox cbFileDateModified;
		private System.Windows.Forms.CheckBox cbFileSize;
		private System.Windows.Forms.CheckBox cbFileExtension;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox2;

		/// <summary> 
		/// Used to hold all threads started on this form 
		/// </summary>
		private ThreadCollection _threadCollection;

		/// <summary>
		/// Xml source which will be exported to text
		/// </summary>
		private Volume _volume;

		/// <summary>
		/// Exported text generated by the background process on this form
		/// </summary>
		private string _exportedText;

		/// <summary>
		/// Then name the exported file will have. Cosmetic in nature, but visually
		/// ties the export form with the volume browser in the context of which it
		/// was spawned
		/// </summary>
		private string _exportName;

		/// <summary>
		///  set to true if dump was cancelled
		/// </summary>
		private bool _cancelled;

		/// <summary>
		/// 
		/// </summary>
		private FSXmlTextExporter _exporter;
		
		/// <summary>
		/// 
		/// </summary>
		private string[] _folderItemsToShow;
		
		/// <summary>
		/// 
		/// </summary>
		private string[] _fileItemsToShow;

		/// <summary>
		/// 
		/// </summary>
		private ProgressBarHelper _progressBarHelper;

		#endregion


		#region CONSTRUCTORS

		/// <summary>
		/// 
		/// </summary>
		public TextExportDialog(
			Volume volume,
			string exportName
			)
		{

			InitializeComponent();
			_exportName = exportName;
			_volume = volume;
		
			this.ShowInTaskbar = false;
			_threadCollection = new ThreadCollection();

			// retrieves state for form
			if (MainForm.StateBag.Contains("BrowseMonkey.Textdump.cbFileDateCreated.Checked"))cbFileDateCreated.Checked = Convert.ToBoolean(MainForm.StateBag.Retrieve("BrowseMonkey.Textdump.cbFileDateCreated.Checked"));
			if (MainForm.StateBag.Contains("BrowseMonkey.Textdump.cbFileDateModified.Checked"))cbFileDateModified.Checked = Convert.ToBoolean(MainForm.StateBag.Retrieve("BrowseMonkey.Textdump.cbFileDateModified.Checked"));
			if (MainForm.StateBag.Contains("BrowseMonkey.Textdump.cbFileExtension.Checked"))cbFileExtension.Checked = Convert.ToBoolean(MainForm.StateBag.Retrieve("BrowseMonkey.Textdump.cbFileExtension.Checked"));
			if (MainForm.StateBag.Contains("BrowseMonkey.Textdump.cbFileName.Checked"))cbFileName.Checked = Convert.ToBoolean(MainForm.StateBag.Retrieve("BrowseMonkey.Textdump.cbFileName.Checked"));
			if (MainForm.StateBag.Contains("BrowseMonkey.Textdump.cbFilePath.Checked"))cbFilePath.Checked = Convert.ToBoolean(MainForm.StateBag.Retrieve("BrowseMonkey.Textdump.cbFilePath.Checked"));
			if (MainForm.StateBag.Contains("BrowseMonkey.Textdump.cbFileSize.Checked"))cbFileSize.Checked = Convert.ToBoolean(MainForm.StateBag.Retrieve("BrowseMonkey.Textdump.cbFileSize.Checked"));
			if (MainForm.StateBag.Contains("BrowseMonkey.Textdump.cbFolderDateCreated.Checked"))cbFolderDateCreated.Checked = Convert.ToBoolean(MainForm.StateBag.Retrieve("BrowseMonkey.Textdump.cbFolderDateCreated.Checked"));
			if (MainForm.StateBag.Contains("BrowseMonkey.Textdump.cbFolderDateModified.Checked"))cbFolderDateModified.Checked = Convert.ToBoolean(MainForm.StateBag.Retrieve("BrowseMonkey.Textdump.cbFolderDateModified.Checked"));
			if (MainForm.StateBag.Contains("BrowseMonkey.Textdump.cbFolderName.Checked"))cbFolderName.Checked = Convert.ToBoolean(MainForm.StateBag.Retrieve("BrowseMonkey.Textdump.cbFolderName.Checked"));
			if (MainForm.StateBag.Contains("BrowseMonkey.Textdump.cbFolderPath.Checked"))cbFolderPath.Checked = Convert.ToBoolean(MainForm.StateBag.Retrieve("BrowseMonkey.Textdump.cbFolderPath.Checked"));

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
				MainForm.StateBag.Add("BrowseMonkey.Textdump.cbFileDateCreated.Checked",cbFileDateCreated.Checked);
				MainForm.StateBag.Add("BrowseMonkey.Textdump.cbFileDateModified.Checked",cbFileDateModified.Checked);
				MainForm.StateBag.Add("BrowseMonkey.Textdump.cbFileExtension.Checked",cbFileExtension.Checked);
				MainForm.StateBag.Add("BrowseMonkey.Textdump.cbFileName.Checked",cbFileName.Checked);
				MainForm.StateBag.Add("BrowseMonkey.Textdump.cbFilePath.Checked",cbFilePath.Checked);
				MainForm.StateBag.Add("BrowseMonkey.Textdump.cbFileSize.Checked",cbFileSize.Checked);
				MainForm.StateBag.Add("BrowseMonkey.Textdump.cbFolderDateCreated.Checked",cbFolderDateCreated.Checked);
				MainForm.StateBag.Add("BrowseMonkey.Textdump.cbFolderDateModified.Checked",cbFolderDateModified.Checked);
				MainForm.StateBag.Add("BrowseMonkey.Textdump.cbFolderName.Checked",cbFolderName.Checked);
				MainForm.StateBag.Add("BrowseMonkey.Textdump.cbFolderPath.Checked",cbFolderPath.Checked);

				// destroys all threads started on this object
				_threadCollection.AbortAndRemoveAllThreads();

				if(components != null)
					components.Dispose();

			}
			base.Dispose( disposing );
		}

		
		#endregion


		#region METHODS


		/// <summary>
		/// Start of export process. Calls Export_bgprocess() in a background thread
		/// </summary>
		private void Export(
			)
		{

			// ##########################################
			// sets up GUI
			// ------------------------------------------
			panels.ActivePanelIndex = 1;



			// ##########################################
			// sets up exporter and support items
			// ------------------------------------------
			_folderItemsToShow = new string[4];
			_fileItemsToShow = new string[6];

			
			// collects folder properties to display in dump
			if (cbFolderName.Checked)_folderItemsToShow[0] =			"n";
			if (cbFolderPath.Checked)_folderItemsToShow[1] =			"p";
			if (cbFolderDateCreated.Checked)_folderItemsToShow[2] =	"dc";
			if (cbFolderDateModified.Checked)_folderItemsToShow[3] =	"dm";
		
			// collects file properties to display in dump
			if (cbFileName.Checked)_fileItemsToShow[0] =			"n";
			if (cbFilePath.Checked)_fileItemsToShow[1] =			"p";
			if (cbFileDateCreated.Checked)_fileItemsToShow[2] =	"dc";
			if (cbFileDateModified.Checked)_fileItemsToShow[3] =	"dm";
			if (cbFileSize.Checked)_fileItemsToShow[4] =			"s";
			if (cbFileExtension.Checked)_fileItemsToShow[5] =	"e";
		
			
			// create xml-to-text dumper
			_exporter = new FSXmlTextExporter(
				_volume.VolumeData,
				_folderItemsToShow,
				_fileItemsToShow,		
				"    ",					// indent with
				" ",					// space with
				true,					// align text (hardcoded)
				true,					// use dividing lines
				true,					// show directories headers
				true);					// show files headers					
				

			// sets up progress bar helper 
			_progressBarHelper = new ProgressBarHelper(
				pbExport,
				_exporter);



			Thread th		= new Thread(new ThreadStart(Export_bgprocess));
			th.Name			= "TextDump";
			th.IsBackground = true;
			
			_threadCollection.AddThread(th);
			th.Start();

		}



		/// <summary>
		/// Second method in export process chain. Is invoked in a background thread
		/// from Export(). Most of the CPU-intense work in exporting is done in this
		/// method
		/// </summary>
		private void Export_bgprocess(
			)
		{

			// dump process
			_exporter.Start();
			_exportedText = _exporter.Output;

			// makes header 
			string header = 
				"--------------------------------------------------------------------\r\n" + 
				" File Structure Dump by BrowserMonkey\r\n" + 
				" DateTime : " + DateTimeLib.DateNow() + "\r\n" + 
				"--------------------------------------------------------------------\r\n\r\n";
			_exportedText = header + _exportedText;
			
			WinFormActionDelegate dlgGUIInvoke = new WinFormActionDelegate(
				Export_guiprocess);
			this.Invoke(dlgGUIInvoke);
		
		}



		/// <summary>
		/// Third and final methód in export process chain. Is invoked from 
		/// Export_bgprocess() in the GUI thread. Displays data which was produced 
		/// by the Export_bgprocess() method
		/// </summary>
		private void Export_guiprocess(
			)
		{

			// ##########################################
			// handles 
			// ------------------------------------------
			if (_cancelled)
				return;


			FormFactory.SpawnTextDump(
				_exportName,
				_exportedText);
				
			// close this form after exporting text
			this.Dispose();

		}


		#endregion


		#region EVENTS


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TextExporter_Load(
			object sender, 
			System.EventArgs e
			)
		{
			btnExport.Select();
		}


		/// <summary>
		/// Invoked when the "export" button is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnExport_Click(
			object sender, 
			System.EventArgs e
			)
		{
			Export();
		}



		/// <summary>
		/// Invoked when the "Cancel" buttons is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCancel_Click(
			object sender, 
			System.EventArgs e
			)
		{
			_cancelled = true;
			this.Dispose();
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnExit_Click(
			object sender, 
			System.EventArgs e
			)
		{
			this.Dispose();
		}


		#endregion




	}
}
