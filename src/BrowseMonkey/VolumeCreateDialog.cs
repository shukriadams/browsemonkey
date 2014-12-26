///////////////////////////////////////////////////////////////
// BrowseMonkey - An offline filesystem browser              //
// Shukri Adams (shukri.adams@gmail.com)                     //
// https://github.com/shukriadams/browsemonkey               //
// MIT License (MIT) Copyright (c) 2014 Shukri Adams         //
///////////////////////////////////////////////////////////////

using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using vcFramework.Delegates;
using vcFramework.Threads;
using vcFramework.Windows.Forms;
using FSXml;
using BrowseMonkeyData;

namespace BrowseMonkey
{
	/// <summary>
	/// Form which gives a "GUI" wrapping to volume creation process. Requires a path
	/// from which to create the volume. While the volume is being created this
	/// form, which is meant to tbe displayed as a dialogue, gives continuous feedback
	/// on the status of the created volume, and also allows the volume creation to be
	/// cancelled by the user.
	/// </summary>
	public class VolumeCreateDialog : Form, ISpawned
	{

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.panels = new vcFramework.Windows.Forms.PanelGallery();
			this.pnlScanning = new System.Windows.Forms.Panel();
			this.signOfLife = new vcFramework.Windows.Forms.SignOfLife();
			this.btnCancelScan = new System.Windows.Forms.Button();
			this.lblStatus = new System.Windows.Forms.Label();
			this.pnlCreating = new System.Windows.Forms.Panel();
			this.pbCreatingFSXml = new System.Windows.Forms.ProgressBar();
			this.label1 = new System.Windows.Forms.Label();
			this.btnCancelCreate = new System.Windows.Forms.Button();
			this.pnlSaving = new System.Windows.Forms.Panel();
			this.signOfLife2 = new vcFramework.Windows.Forms.SignOfLife();
			this.label2 = new System.Windows.Forms.Label();
			this.lblPath = new System.Windows.Forms.Label();
			this.panels.SuspendLayout();
			this.pnlScanning.SuspendLayout();
			this.pnlCreating.SuspendLayout();
			this.pnlSaving.SuspendLayout();
			this.SuspendLayout();
			// 
			// panels
			// 
			this.panels.ActivePanelIndex = 0;
			this.panels.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panels.BackColor = System.Drawing.SystemColors.Control;
			this.panels.Controls.Add(this.pnlScanning);
			this.panels.Controls.Add(this.pnlCreating);
			this.panels.Controls.Add(this.pnlSaving);
			this.panels.Location = new System.Drawing.Point(0, 40);
			this.panels.Name = "panels";
			this.panels.Panels.Add(this.pnlScanning);
			this.panels.Panels.Add(this.pnlCreating);
			this.panels.Panels.Add(this.pnlSaving);
			this.panels.Size = new System.Drawing.Size(344, 72);
			this.panels.TabIndex = 3;
			// 
			// pnlScanning
			// 
			this.pnlScanning.Controls.Add(this.signOfLife);
			this.pnlScanning.Controls.Add(this.btnCancelScan);
			this.pnlScanning.Controls.Add(this.lblStatus);
			this.pnlScanning.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlScanning.Location = new System.Drawing.Point(0, 0);
			this.pnlScanning.Name = "pnlScanning";
			this.pnlScanning.Size = new System.Drawing.Size(344, 72);
			this.pnlScanning.TabIndex = 0;
			// 
			// signOfLife
			// 
			this.signOfLife.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.signOfLife.Location = new System.Drawing.Point(40, 24);
			this.signOfLife.Name = "signOfLife";
			this.signOfLife.Size = new System.Drawing.Size(286, 8);
			this.signOfLife.TabIndex = 6;
			// 
			// btnCancelScan
			// 
			this.btnCancelScan.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnCancelScan.Location = new System.Drawing.Point(132, 40);
			this.btnCancelScan.Name = "btnCancelScan";
			this.btnCancelScan.TabIndex = 5;
			this.btnCancelScan.Text = "Cancel";
			this.btnCancelScan.Click += new System.EventHandler(this.Cancel_Click);
			// 
			// lblStatus
			// 
			this.lblStatus.Location = new System.Drawing.Point(112, 8);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(128, 16);
			this.lblStatus.TabIndex = 4;
			this.lblStatus.Text = "Scanning drive contents";
			// 
			// pnlCreating
			// 
			this.pnlCreating.Controls.Add(this.pbCreatingFSXml);
			this.pnlCreating.Controls.Add(this.label1);
			this.pnlCreating.Controls.Add(this.btnCancelCreate);
			this.pnlCreating.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlCreating.Location = new System.Drawing.Point(0, 0);
			this.pnlCreating.Name = "pnlCreating";
			this.pnlCreating.Size = new System.Drawing.Size(344, 72);
			this.pnlCreating.TabIndex = 0;
			// 
			// pbCreatingFSXml
			// 
			this.pbCreatingFSXml.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.pbCreatingFSXml.Location = new System.Drawing.Point(48, 24);
			this.pbCreatingFSXml.Name = "pbCreatingFSXml";
			this.pbCreatingFSXml.Size = new System.Drawing.Size(248, 8);
			this.pbCreatingFSXml.TabIndex = 5;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(120, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 15);
			this.label1.TabIndex = 3;
			this.label1.Text = "Generating image";
			// 
			// btnCancelCreate
			// 
			this.btnCancelCreate.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnCancelCreate.Location = new System.Drawing.Point(132, 41);
			this.btnCancelCreate.Name = "btnCancelCreate";
			this.btnCancelCreate.TabIndex = 4;
			this.btnCancelCreate.Text = "Cancel";
			this.btnCancelCreate.Click += new System.EventHandler(this.Cancel_Click);
			// 
			// pnlSaving
			// 
			this.pnlSaving.Controls.Add(this.signOfLife2);
			this.pnlSaving.Controls.Add(this.label2);
			this.pnlSaving.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlSaving.Location = new System.Drawing.Point(0, 0);
			this.pnlSaving.Name = "pnlSaving";
			this.pnlSaving.Size = new System.Drawing.Size(344, 72);
			this.pnlSaving.TabIndex = 0;
			// 
			// signOfLife2
			// 
			this.signOfLife2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.signOfLife2.Location = new System.Drawing.Point(48, 32);
			this.signOfLife2.Name = "signOfLife2";
			this.signOfLife2.Size = new System.Drawing.Size(248, 8);
			this.signOfLife2.TabIndex = 6;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(120, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(96, 23);
			this.label2.TabIndex = 5;
			this.label2.Text = "Writing data to file";
			// 
			// lblPath
			// 
			this.lblPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblPath.Location = new System.Drawing.Point(16, 8);
			this.lblPath.Name = "lblPath";
			this.lblPath.Size = new System.Drawing.Size(320, 32);
			this.lblPath.TabIndex = 4;
			this.lblPath.Text = "[set in code]";
			// 
			// VolumeCreateDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(346, 112);
			this.Controls.Add(this.lblPath);
			this.Controls.Add(this.panels);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximumSize = new System.Drawing.Size(352, 136);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(352, 136);
			this.Name = "VolumeCreateDialog";
			this.Text = "Creating Volume";
			this.Load += new System.EventHandler(this.VolumeCreateDialog_Load);
			this.panels.ResumeLayout(false);
			this.pnlScanning.ResumeLayout(false);
			this.pnlCreating.ResumeLayout(false);
			this.pnlSaving.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


		#region FIELDS

		private System.Windows.Forms.Panel pnlScanning;
		private System.Windows.Forms.Panel pnlCreating;
		private System.Windows.Forms.Label label1;
		private System.ComponentModel.Container components = null;
		private PanelGallery panels;		
		private System.Windows.Forms.ProgressBar pbCreatingFSXml;
		private System.Windows.Forms.Label lblPath;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.Button btnCancelScan;
		private System.Windows.Forms.Button btnCancelCreate;
		private SignOfLife signOfLife;
		private System.Windows.Forms.Panel pnlSaving;
		private System.Windows.Forms.Label label2;
		private SignOfLife signOfLife2;

		/// <summary>
		/// The path which volume will be created from. This is passed in through the Start()
		/// method
		/// </summary>
		private string _path;

		/// <summary>
		/// Object used to bind a progress bar control with an IProgess object whose progress
		/// we wish to display in the progress bar
		/// </summary>
		private ProgressBarHelper _progressBarHelper;

		/// <summary>
		/// Volume created by this dialog
		/// </summary>
		private Volume _volume;

		/// <summary>
		/// 
		/// </summary>
		private ThreadCollection _threadCollection;

		/// <summary>
		/// Set to true if the process on this dialog is cancelled
		/// </summary>
		private bool _cancelled;

		/// <summary>
		/// Set to true if the volume creation failed
		/// </summary>
		private bool _failed;

		#endregion


		#region PROPERTIES

		/// <summary>
		/// Gets the volume produced by this dialog
		/// </summary>
		public Volume Volume
		{
			get
			{
				return _volume;
			}
		}

	
		/// <summary>
		/// Gets if dialog process has been cancelled
		/// </summary>
		public bool Cancelled
		{
			get
			{
				return _cancelled;
			}
		}

		
		/// <summary>
		/// Gets if the volume creation process in dialog has failed
		/// </summary>
		public bool Failed
		{
			get
			{
				return _failed;
			}
		}

		
		#endregion


		#region CONSTRUCTORS

		/// <summary>
		/// 
		/// </summary>
		public VolumeCreateDialog(
			string path
			)
		{
			_path = path;

			InitializeComponent();
			this.ShowInTaskbar = false;
			_threadCollection = new ThreadCollection();

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

				// ensure that background thread process is 
				// killed when shutting this form down
				_threadCollection.AbortAndRemoveAllThreads();

				if(components != null)
					components.Dispose();

			}
			base.Dispose( disposing );
		}

		#endregion

		
		#region METHODS

		/// <summary> 
		/// First method called in volume creation sequence. Responsible for checking 
		/// if user input is valid. If valid, calls method which does initial scanning 
		/// of capture folder.
		/// </summary>
		private void CreateVolume(
			)
		{
			
			// ##################################################
			// 1. first do checks to make sure all entered 
			// information is valid checks if path is valid
			// --------------------------------------------------
			if (_path.Length == 0)
			{
				MainForm.ConsoleAdd("Please give a path to archive");
				this.Dispose();
				return;
			}
			else if (!Directory.Exists(_path))
			{
				MainForm.ConsoleAdd("The path for the directory image is invalid");
				this.Dispose();
				return;
			}

		

			// ##################################################
			// start sign-of-life control so user can see app
			// is alive while volume creation's "scanning" 
			// process runs. note that we dont know how long 
			// this phase will last
			// --------------------------------------------------
			panels.ActivePanelIndex = 0;
			lblPath.Text = "Processing path : " + _path;
			signOfLife.AutoLivingStart();
			signOfLife2.AutoLivingStart();


			// ##################################################
			// starts volume creation in its own thread
			// --------------------------------------------------
			Thread th		= new Thread(new ThreadStart(VolumeCreation_BGThread));
			th.Name			= "GenerateXmlVolume";
			th.IsBackground	= true;
			_threadCollection.AddThread(th);
			th.Start();

		}
		


		/// <summary> 
		/// Second phase of volume creation process. Runs as backgronud thread. 
		/// </summary>
		private void VolumeCreation_BGThread(
			)
		{

			try
			{
				WinFormActionDelegate guiInvoker	= null;
				FSXmlWriter writer					= null;


				// ################################################
				// instantiate XmlfileStructure generator. must be 
				// run as bakground thread because on instantation 
				// it scans target path and counts how many folders
				// will be processed. while this is being done, 
				// the "sign of life" control keeps the user aware 
				// that the application is busy
				// ------------------------------------------------
				writer = new FSXmlWriter(_path);
			

				// ################################################
				// Now need to change groupbox visibility again, this time hiding the "busy 
				// scannig" groupbox and displaying the "building data" progress bar. NOTE 
				// - this MUST be done before trying to create the progress bar helper. If
				// the progress bar object is not visible when creating the progressbar 
				// helper, bad things happen ....
				// ------------------------------------------------
				guiInvoker = new WinFormActionDelegate(
					FocusBuildPanel);
				this.Invoke(guiInvoker);



				// set up progressbarhelper for volume building process
				_progressBarHelper = new ProgressBarHelper(
					pbCreatingFSXml,
					writer);


				// ################################################
				// generates fsXml data
				// ------------------------------------------------
				writer.Start();


				// ################################################
				// writes failed dir + file reads to console
				// ------------------------------------------------
				foreach (ReadError dir in writer.FailedFolders)
					MainForm.ConsoleAdd(
						"An error occured reading folder : " + dir.Item + "  (" + dir.Exception.Message + ")");

				foreach (ReadError file in writer.FailedFiles)
					MainForm.ConsoleAdd(
						"An error occured reading file : " + file.Item + "  (" + file.Exception.Message + ")");


				// ################################################
				// must now update GUI - to make this thread-safe, have to 
				// call the method which does updating useing a delegate
				// ------------------------------------------------
				guiInvoker = new WinFormActionDelegate(
					SetGUISaveProgressItems);

				this.Invoke(guiInvoker);



				// ################################################
				// 
				// ------------------------------------------------
				_volume = new Volume(
					writer,
					Application.CurrentCulture);

				_volume.OnSave += new EventHandler(MainForm.VolumeSavedOrClosed);
				_volume.OnDispose += new EventHandler(MainForm.VolumeSavedOrClosed);



				// ################################################
				// must now update GUI - to make this thread-safe, 
				// have to call the method which does updating 
				// using a delegate
				// ------------------------------------------------
				guiInvoker = new WinFormActionDelegate(VolumeCreation_GUIThread);
				this.Invoke(guiInvoker);
			}
			catch(Exception ex)
			{
				// unexpected error occurred
				_failed = true;
				this.Close();
				throw ex;
			}
		}
		


		/// <summary>
		/// Focuses the panel which contains build-specific controls. This is invoked
		/// when the volume creation process has reached the "build" phase. This
		/// method is cross-thread invoked
		/// </summary>
		private void FocusBuildPanel(
			)
		{
			panels.ActivePanelIndex = 1;
		}



		/// <summary>
		/// Invokes creation of a new VolumeBrowser form and passes reference of created
		/// volume to that form
		/// </summary>
		private void VolumeCreation_GUIThread(
			)
		{

			this.Close();

		}



		/// <summary> 
		/// Focuses the "saving" panel in panel collection. Invoked when the volume is
		/// being saved. This method is cross-thread invoked.
		/// </summary>
		private void SetGUISaveProgressItems(
			)
		{
			panels.ActivePanelIndex = 2;
		}
		

		
		#endregion 


		#region EVENTS

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void VolumeCreateDialog_Load(
			object sender, 
			System.EventArgs e)
		{
			CreateVolume();
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Cancel_Click(
			object sender, 
			System.EventArgs e)
		{
			_cancelled = true;
			this.Dispose();
		}


		#endregion


	}
}
