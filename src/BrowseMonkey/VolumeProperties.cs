///////////////////////////////////////////////////////////////
// BrowseMonkey - An offline filesystem browser              //
// Shukri Adams (shukri.adams@gmail.com)                     //
// https://github.com/shukriadams/browsemonkey               //
// MIT License (MIT) Copyright (c) 2014 Shukri Adams         //
///////////////////////////////////////////////////////////////

using System;
using System.IO;
using System.Windows.Forms;
using vcFramework.DataItems;
using BrowseMonkeyData;

namespace BrowseMonkey
{
	/// <summary> 
	/// 
	/// </summary>
	public class VolumeProperties : UserControl
	{


		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lblFilesInVolume = new System.Windows.Forms.Label();
			this.lblSizeOfData = new System.Windows.Forms.Label();
			this.lblVolFileSize = new System.Windows.Forms.Label();
			this.lblDateCreated = new System.Windows.Forms.Label();
			this.lblVersion = new System.Windows.Forms.Label();
			this.lblFoldersInVolume = new System.Windows.Forms.Label();
			this.labelXP9 = new System.Windows.Forms.Label();
			this.labelXP8 = new System.Windows.Forms.Label();
			this.labelXP7 = new System.Windows.Forms.Label();
			this.labelXP6 = new System.Windows.Forms.Label();
			this.labelXP5 = new System.Windows.Forms.Label();
			this.labelXP4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblFilesInVolume
			// 
			this.lblFilesInVolume.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblFilesInVolume.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblFilesInVolume.Location = new System.Drawing.Point(112, 48);
			this.lblFilesInVolume.Name = "lblFilesInVolume";
			this.lblFilesInVolume.Size = new System.Drawing.Size(136, 16);
			this.lblFilesInVolume.TabIndex = 16;
			this.lblFilesInVolume.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblSizeOfData
			// 
			this.lblSizeOfData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblSizeOfData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblSizeOfData.Location = new System.Drawing.Point(112, 32);
			this.lblSizeOfData.Name = "lblSizeOfData";
			this.lblSizeOfData.Size = new System.Drawing.Size(136, 16);
			this.lblSizeOfData.TabIndex = 15;
			this.lblSizeOfData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblVolFileSize
			// 
			this.lblVolFileSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblVolFileSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblVolFileSize.Location = new System.Drawing.Point(112, 16);
			this.lblVolFileSize.Name = "lblVolFileSize";
			this.lblVolFileSize.Size = new System.Drawing.Size(136, 16);
			this.lblVolFileSize.TabIndex = 14;
			this.lblVolFileSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblDateCreated
			// 
			this.lblDateCreated.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblDateCreated.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblDateCreated.Location = new System.Drawing.Point(112, 0);
			this.lblDateCreated.Name = "lblDateCreated";
			this.lblDateCreated.Size = new System.Drawing.Size(136, 16);
			this.lblDateCreated.TabIndex = 13;
			this.lblDateCreated.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblVersion
			// 
			this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblVersion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblVersion.Location = new System.Drawing.Point(112, 80);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(136, 16);
			this.lblVersion.TabIndex = 18;
			this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblFoldersInVolume
			// 
			this.lblFoldersInVolume.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblFoldersInVolume.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblFoldersInVolume.Location = new System.Drawing.Point(112, 64);
			this.lblFoldersInVolume.Name = "lblFoldersInVolume";
			this.lblFoldersInVolume.Size = new System.Drawing.Size(136, 16);
			this.lblFoldersInVolume.TabIndex = 17;
			this.lblFoldersInVolume.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelXP9
			// 
			this.labelXP9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelXP9.Location = new System.Drawing.Point(0, 64);
			this.labelXP9.Name = "labelXP9";
			this.labelXP9.Size = new System.Drawing.Size(112, 16);
			this.labelXP9.TabIndex = 9;
			this.labelXP9.Text = "Folders in volume :";
			this.labelXP9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelXP8
			// 
			this.labelXP8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelXP8.Location = new System.Drawing.Point(0, 48);
			this.labelXP8.Name = "labelXP8";
			this.labelXP8.Size = new System.Drawing.Size(112, 16);
			this.labelXP8.TabIndex = 8;
			this.labelXP8.Text = "Files in volume :";
			this.labelXP8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelXP7
			// 
			this.labelXP7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelXP7.Location = new System.Drawing.Point(0, 80);
			this.labelXP7.Name = "labelXP7";
			this.labelXP7.Size = new System.Drawing.Size(112, 16);
			this.labelXP7.TabIndex = 7;
			this.labelXP7.Text = "Version :";
			this.labelXP7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelXP6
			// 
			this.labelXP6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelXP6.Location = new System.Drawing.Point(0, 32);
			this.labelXP6.Name = "labelXP6";
			this.labelXP6.Size = new System.Drawing.Size(112, 16);
			this.labelXP6.TabIndex = 6;
			this.labelXP6.Text = "Size of original data:";
			this.labelXP6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelXP5
			// 
			this.labelXP5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelXP5.Location = new System.Drawing.Point(0, 16);
			this.labelXP5.Name = "labelXP5";
			this.labelXP5.Size = new System.Drawing.Size(112, 16);
			this.labelXP5.TabIndex = 5;
			this.labelXP5.Text = "Volume file size :";
			this.labelXP5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelXP4
			// 
			this.labelXP4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelXP4.Location = new System.Drawing.Point(0, 0);
			this.labelXP4.Name = "labelXP4";
			this.labelXP4.Size = new System.Drawing.Size(112, 16);
			this.labelXP4.TabIndex = 4;
			this.labelXP4.Text = "Date created :";
			this.labelXP4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnBrowseVolume
			// 
			// 
			// btnDeleteVolume
			// 
			// 
			// VolumeProperties
			// 
			this.Controls.Add(this.labelXP4);
			this.Controls.Add(this.labelXP5);
			this.Controls.Add(this.labelXP6);
			this.Controls.Add(this.labelXP7);
			this.Controls.Add(this.labelXP8);
			this.Controls.Add(this.labelXP9);
			this.Controls.Add(this.lblFoldersInVolume);
			this.Controls.Add(this.lblVersion);
			this.Controls.Add(this.lblDateCreated);
			this.Controls.Add(this.lblVolFileSize);
			this.Controls.Add(this.lblSizeOfData);
			this.Controls.Add(this.lblFilesInVolume);
			this.Name = "VolumeProperties";
			this.Size = new System.Drawing.Size(248, 96);
			this.ResumeLayout(false);

		}

		#endregion


		#region FIELDS

		private System.ComponentModel.Container components = null;
		private Label labelXP4;
		private Label labelXP5;
		private Label labelXP6;
		private Label labelXP7;
		private Label labelXP8;
		private Label labelXP9;
		private Label lblDateCreated;
		private Label lblVolFileSize;
		private Label lblSizeOfData;
		private Label lblFilesInVolume;
		private Label lblFoldersInVolume;
		private Label lblVersion;
	
		/// <summary>
		/// 
		/// </summary>
		private Volume _volume;

		/// <summary>
		/// 
		/// </summary>
		private string _volumePath;

		/// <summary>
		/// 
		/// </summary>
		private bool _localVolumeObject;

		#endregion
		

		#region CONSTRUCTORS


		/// <summary> </summary>
		public VolumeProperties(
			)
		{
			InitializeComponent();			
		}
		


		#endregion


		#region DESTRUCTORS
		
		/// <summary> </summary>
		/// <param name="disposing"></param>
		protected override void Dispose(
			bool disposing
			)
		{
			try
			{
				if(disposing)
				{
					if(components != null)
						components.Dispose();

				}

				base.Dispose(
					disposing);

			}
			catch(Exception ex)
			{
				MainForm.ConsoleAdd(ex);			
			}
		}
		

		#endregion

		
		#region METHODS



		/// <summary>
		///		
		/// </summary>
		/// <param name="volume"></param>
		public void DisplayProperties(
			Volume volume
			)
		{
			_volume = volume;

			DisplayProperties();
			
			_localVolumeObject = false;
		}



		/// <summary> 
		/// Display properties for the given file 
		/// </summary>
		/// <param name="intLinkLockerIndex"></param>
		public void DisplayProperties(
			string volumePath
			)
		{
			_volumePath = volumePath;

			_volume = new Volume(
				_volumePath);

			DisplayProperties();

			_localVolumeObject = true;

		}



		/// <summary> 
		/// Display properties for the given file 
		/// </summary>
		/// <param name="intLinkLockerIndex"></param>
		private void DisplayProperties(
			)
		{


			if (_volumePath != null)
			{
				FileInfo objVolumeFileInfo		= new FileInfo(_volumePath);
				ByteSizeItem volumeFileSize		= new ByteSizeItem(objVolumeFileInfo.Length);
				lblVolFileSize.Text				= volumeFileSize.BytesParsed + " " + volumeFileSize.SizeUnit;

			}

			if (_volume.BoundToFile)
			{
				FileInfo fileInfo = new FileInfo(_volume.Path);
				ByteSizeItem volumeFileSize = new ByteSizeItem(fileInfo.Length);
				lblVolFileSize.Text	= volumeFileSize.BytesParsed + " " + volumeFileSize.SizeUnit;
			}
			else
			{
				lblVolFileSize.Text = "(n/a)";
			}
			
			ByteSizeItem volumeContentsInfo = new ByteSizeItem(_volume.VolumeContentsSize);
			

			// presents data in index
			lblDateCreated.Text		= _volume.DateCreated.ToString();
			lblFilesInVolume.Text	= _volume.FileCount.ToString();
			lblFoldersInVolume.Text	= _volume.DirectoryCount.ToString();
			lblVersion.Text			= _volume.Version;
			lblSizeOfData.Text		= volumeContentsInfo.BytesParsed + " " + volumeContentsInfo.SizeUnit;
			
			// destroys volume
			if (_localVolumeObject)
				_volume.Dispose();

		}



		#endregion



	}
}
