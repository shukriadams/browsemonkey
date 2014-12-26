///////////////////////////////////////////////////////////////
// BrowseMonkey - An offline filesystem browser              //
// Shukri Adams (shukri.adams@gmail.com)                     //
// https://github.com/shukriadams/browsemonkey               //
// MIT License (MIT) Copyright (c) 2014 Shukri Adams         //
///////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using BrowseMonkeyData;
using vcFramework.Delegates;

namespace BrowseMonkey
{
	/// <summary>
	/// Dialog for prompting user for confirmation of volume version change
	/// </summary>
	public class VolumeConvertDialog : Form
	{

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lblMessage = new System.Windows.Forms.Label();
			this.btnConvert = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnConvertAll = new System.Windows.Forms.Button();
			this.lblVolumeName = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnSkip = new System.Windows.Forms.Button();
			this.cbCreateBackups = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// lblMessage
			// 
			this.lblMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblMessage.Location = new System.Drawing.Point(8, 40);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(336, 56);
			this.lblMessage.TabIndex = 0;
			this.lblMessage.Text = "[text set from code]";
			// 
			// btnConvert
			// 
			this.btnConvert.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnConvert.Location = new System.Drawing.Point(8, 136);
			this.btnConvert.Name = "btnConvert";
			this.btnConvert.TabIndex = 0;
			this.btnConvert.Text = "Convert";
			this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnCancel.Location = new System.Drawing.Point(272, 136);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnConvertAll
			// 
			this.btnConvertAll.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnConvertAll.Location = new System.Drawing.Point(96, 136);
			this.btnConvertAll.Name = "btnConvertAll";
			this.btnConvertAll.TabIndex = 1;
			this.btnConvertAll.Text = "Convert All";
			this.btnConvertAll.Click += new System.EventHandler(this.btnConvertAll_Click);
			// 
			// lblVolumeName
			// 
			this.lblVolumeName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblVolumeName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblVolumeName.Location = new System.Drawing.Point(8, 8);
			this.lblVolumeName.Name = "lblVolumeName";
			this.lblVolumeName.Size = new System.Drawing.Size(336, 23);
			this.lblVolumeName.TabIndex = 5;
			this.lblVolumeName.Text = "[text set from code]";
			this.lblVolumeName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Location = new System.Drawing.Point(8, 120);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(344, 8);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			// 
			// btnSkip
			// 
			this.btnSkip.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.btnSkip.Location = new System.Drawing.Point(184, 136);
			this.btnSkip.Name = "btnSkip";
			this.btnSkip.TabIndex = 2;
			this.btnSkip.Text = "Skip";
			this.btnSkip.Click += new System.EventHandler(this.btnSkip_Click);
			// 
			// cbCreateBackups
			// 
			this.cbCreateBackups.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cbCreateBackups.Location = new System.Drawing.Point(16, 104);
			this.cbCreateBackups.Name = "cbCreateBackups";
			this.cbCreateBackups.Size = new System.Drawing.Size(176, 16);
			this.cbCreateBackups.TabIndex = 7;
			this.cbCreateBackups.Text = "Create a backup of original file";
			// 
			// VolumeConvertDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(354, 168);
			this.Controls.Add(this.cbCreateBackups);
			this.Controls.Add(this.btnSkip);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnConvert);
			this.Controls.Add(this.btnConvertAll);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.lblMessage);
			this.Controls.Add(this.lblVolumeName);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximumSize = new System.Drawing.Size(360, 192);
			this.MinimumSize = new System.Drawing.Size(360, 192);
			this.Name = "VolumeConvertDialog";
			this.Load += new System.EventHandler(this.VolumeConvertDialog_Load);
			this.ResumeLayout(false);

		}
		#endregion


		#region FIELDS

		private System.Windows.Forms.Button btnConvert;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label lblMessage;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button btnConvertAll;
		private System.Windows.Forms.Label lblVolumeName;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnSkip;
		private System.Windows.Forms.CheckBox cbCreateBackups;

		/// <summary>
		/// List of volumes which were successfully converted by this dialog instance
		/// </summary>
		private ArrayList _convertedVolumes;

		/// <summary>
		/// List of volumes which failed conversion process
		/// </summary>
		private ArrayList _failedVolumes;

		/// <summary>
		/// A list of all volumes to convert. Stored as class member because it must be
		/// accessible from several methods spanning events.
		/// </summary>
		private string[] _volumesToConvert;

		/// <summary>
		/// Item in _volumesToConvert array which is currently being processed
		/// </summary>
		private int _currentVolumeIndex;
		
		/// <summary>
		/// Is true if the user specificies that all volumes in queue are to be converted
		/// without individual confirmation
		/// </summary>
		private bool _processAll;

		/// <summary>
		/// 
		/// </summary>
		private Thread _thread;

		#endregion


		#region PROPERTIES

		/// <summary>
		/// Gets a list of all volumes successfully converted by this dialog instance
		/// </summary>
		public string[] ConvertedVolumes
		{
			get
			{
				string[] volumes = new string[_convertedVolumes.Count];

				for (int i = 0 ; i < _convertedVolumes.Count ; i ++)
					volumes[i] = _convertedVolumes[i].ToString();

				return volumes;
			}
		}


		/// <summary>
		/// Gets or sets the volumes which will be converted
		/// </summary>
		public string[] VolumesToConvert
		{
			get
			{
				return _volumesToConvert;
			}
		}


		#endregion


		#region CONSTUCTORS

		/// <summary>
		/// 
		/// </summary>
		public VolumeConvertDialog(
			string[] volumesToConvert
			)
		{
			InitializeComponent();
			this.ShowInTaskbar = false;

			_volumesToConvert = volumesToConvert;

			// disables "convert all" button if there is only 1 volume to process
			if (_volumesToConvert.Length == 1)
				btnConvertAll.Enabled = false;
			_currentVolumeIndex = 0;

			_convertedVolumes = new ArrayList();
			_failedVolumes = new ArrayList();

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
				MainForm.StateBag.Add(this.Name + ".BackupOriginal", cbCreateBackups.Checked);

				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		
		#endregion


		#region METHODS


		/// <summary>
		/// Presents information about the next volume to the user - the user can then opt to 
		/// convert the given volume. This process is automatically bypassed if the user
		/// clicks "convert all"
		/// </summary>
		/// <param name="volume"></param>
		private void QueueNextVolume(
			)
		{
	
			if (!_processAll)
				this.Enabled = true;

			
			// #######################################################
			// exit if no more volumes to process
			// -------------------------------------------------------
			if (_currentVolumeIndex >= _volumesToConvert.Length)
			{
				CleanUp();
				return;
			}


			// #######################################################
			// gets volume to convert from class-member array
			// -------------------------------------------------------
			string volume = _volumesToConvert[_currentVolumeIndex];


			// #######################################################
			// if current volume path is invalid, or volume does not 
			// need to be updated, proceed to next volume
			// -------------------------------------------------------
			if (!File.Exists(volume) ||
				VolumeIdentificationLib.GetVolumeVersion(volume) == BrowseMonkeyData.Constants.CurrentVolumeVersion
				)
			{
				_currentVolumeIndex ++;
				QueueNextVolume();
				return;
			}

	

			// #######################################################
			// updates label on dialogue
			// -------------------------------------------------------
			lblVolumeName.Text = Path.GetFileName(volume);

			lblMessage.Text = 
				@"This file must be updated to make it readable by this version of BrowseMonkey. Converting is permanent,  
				and will make the file unreadable by older BrowseMonkey versions. No data will be lost.";

		

			// #######################################################
			// if user has pressed the "convert all" button, this 
			// automatically starts converting
			// -------------------------------------------------------
			if (_processAll)
				ConvertCurrentVolume();

		}

		

		/// <summary>
		/// 
		/// </summary>
		private void ConvertCurrentVolume(
			)
		{
			lblVolumeName.Text = "Processing : " + lblVolumeName.Text;
			this.Enabled = false;

			_thread			= new Thread(new ThreadStart(ConvertCurrentVolume_Bgthread));
			_thread.Name			= "ConvertCurrentVolume";
			_thread.IsBackground = true;
			_thread.Start();
		}

		
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private void ConvertCurrentVolume_Bgthread(
			)
		{

			string origVolume = _volumesToConvert[_currentVolumeIndex];
			string newVolume = "";
			string movedOrigVolume = "";
			string backupPrefix = "original~";
			
			try
			{

				// make temp files
				newVolume = Path.GetTempFileName();
				File.Delete(newVolume);// need to delete that stupid 0-byte file Windows puts in tmp location
				
				// write converted volume data to temp file. Can throw exception if original volume
				// is invalid
				VolumeConversionLib.Convert101_102(
					origVolume,
					newVolume,
					Application.CurrentCulture);

				// test if new volume is valid. Can throw exception if conversion failed.
				Volume test = new Volume(
					newVolume);
				test.Dispose();


				// #############################################
				// move original file
				// ---------------------------------------------
				if (cbCreateBackups.Checked)
					File.Move(
						origVolume,
						Path.GetDirectoryName(origVolume) + Path.DirectorySeparatorChar + backupPrefix + Path.GetFileName(origVolume));
				else
				{
					movedOrigVolume = Path.GetTempFileName();
					File.Delete(movedOrigVolume);	// need to delete that stupid 0-byte file Windows puts in tmp location

					File.Move(
						origVolume,
						movedOrigVolume);
				}


				// #############################################
				// move new file to where original one used to be
				// ---------------------------------------------
				File.Move(
					newVolume,
					origVolume);

				// delete original file
				if (!cbCreateBackups.Checked)
					File.Delete(movedOrigVolume);

				// add to list of sucessfully processed volumes
				_convertedVolumes.Add(origVolume);

			}
			catch(Exception ex)
			{

				// even though the conversion process is complex, and files are moved around,
				// the only parts which have any real risk of throwing an exception are those
				// which take part BEFORE the original file is moved, and therefore, if an 
				// error HAS occurred, we can count on only having to delete the new volume
				if (File.Exists(newVolume))
					File.Delete(newVolume);
				
				string errorMessage = "";
				if (ex is FormatException && ex.Message == "String was not recognized as a valid DateTime.")
					errorMessage = 
						@" (An invalid datetime was detected. This could be because the volume 
						being converted was created with different regional settings. Try
						setting your system to the original locale and retry converting the volume.
						Once converted, the volume will work under all locales.)";

				_failedVolumes.Add(origVolume + errorMessage);

			}

			_currentVolumeIndex ++;

			// exit if no more volumes to process
			WinFormActionDelegate dlgt = null;

			if (_currentVolumeIndex >= _volumesToConvert.Length)
				dlgt = new WinFormActionDelegate(CleanUp);
			else
				dlgt = new WinFormActionDelegate(QueueNextVolume);

			this.Invoke(dlgt);

		}



		/// <summary>
		/// To be called whenever this form is finished with ALL its conversion jobs. 
		/// Presents error messages if any, and cleans up
		/// </summary>
		private void CleanUp(
			)
		{

			lblMessage.Text = "";
			lblVolumeName.Text = "";

			if (_failedVolumes.Count > 0)
			{
				string errorMessage = "Errors occured and the following files were not converted : \r\n\r\n" ;
				foreach(string volume in _failedVolumes)
					errorMessage += volume + "\r\n";

				MessageBox.Show(errorMessage);
			}

			this.Dispose();
		}


		#endregion


		#region EVENTS


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void VolumeConvertDialog_Load(
			object sender, 
			System.EventArgs e)
		{

			// restores state
			if (MainForm.StateBag.Contains(this.Name + ".BackupOriginal"))
				cbCreateBackups.Checked = (bool)MainForm.StateBag.Retrieve(this.Name + ".BackupOriginal");

			// starts the conversion process by queuing the first volume for converting
			QueueNextVolume();

		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnConvert_Click(
			object sender, 
			System.EventArgs e
			)
		{

			// starts process of converting dialogue
			ConvertCurrentVolume();

		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnConvertAll_Click(
			object sender, 
			System.EventArgs e
			)
		{
			// starts process of converting dialogue
			_processAll = true;
			ConvertCurrentVolume();	
		}



		/// <summary>
		/// Skips over the currently queued volume by queuing the next one
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSkip_Click(
			object sender, 
			System.EventArgs e
			)
		{
			_currentVolumeIndex ++;
			QueueNextVolume();
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCancel_Click(
			object sender, 
			System.EventArgs e
			)
		{
			this.Dispose();
		}



		/// <summary>
		/// Invoked after previous volume conversation failed, but user decides
		/// to continue with next volume
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnContinue_Click(
			object sender, 
			System.EventArgs e
			)
		{

			// proceed to next volume, after previous volume caused error
			QueueNextVolume();

		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAbort_Click(
			object sender, 
			System.EventArgs e
			)
		{
			this.Dispose();
		}



		#endregion


	}
}
