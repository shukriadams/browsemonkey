///////////////////////////////////////////////////////////////
// BrowseMonkey - An offline filesystem browser              //
// Shukri Adams (shukri.adams@gmail.com)                     //
// https://github.com/shukriadams/browsemonkey               //
// MIT License (MIT) Copyright (c) 2014 Shukri Adams         //
///////////////////////////////////////////////////////////////

using System;
using System.IO;
using System.Windows.Forms;

namespace BrowseMonkey
{
	/// <summary> 
	/// Form used to select path from which volume will be created
	/// </summary>
	public class VolumePathSelectDialog : Form
	{

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.txtPath = new System.Windows.Forms.TextBox();
			this.lblStepOne = new System.Windows.Forms.Label();
			this.btnAbortVolumeCreation = new Button();
			this.btnGetPath = new System.Windows.Forms.Button();
			this.btnListIt = new System.Windows.Forms.Button();
			this.btnBuildVolumeXml = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.gbVolume = new System.Windows.Forms.GroupBox();
			this.label3 = new System.Windows.Forms.Label();
			this.btnQuit = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.gbVolume.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtPath
			// 
			this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtPath.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtPath.Location = new System.Drawing.Point(8, 24);
			this.txtPath.Name = "txtPath";
			this.txtPath.ReadOnly = true;
			this.txtPath.Size = new System.Drawing.Size(326, 20);
			this.txtPath.TabIndex = 0;
			this.txtPath.TabStop = false;
			this.txtPath.Text = "";
			this.txtPath.TextChanged += new System.EventHandler(this.txtPath_TextChanged);
			// 
			// lblStepOne
			// 
			this.lblStepOne.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblStepOne.Location = new System.Drawing.Point(8, 8);
			this.lblStepOne.Name = "lblStepOne";
			this.lblStepOne.Size = new System.Drawing.Size(152, 16);
			this.lblStepOne.TabIndex = 49;
			this.lblStepOne.Text = "Select the path to read from :";
			// 
			// btnAbortVolumeCreation
			// 
			this.btnAbortVolumeCreation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAbortVolumeCreation.Image = null;
			this.btnAbortVolumeCreation.Location = new System.Drawing.Point(0, 0);
			this.btnAbortVolumeCreation.Name = "btnAbortVolumeCreation";
			this.btnAbortVolumeCreation.Size = new System.Drawing.Size(75, 23);
			this.btnAbortVolumeCreation.TabIndex = 0;
			this.btnAbortVolumeCreation.Text = "ButtonXP102";
			this.btnAbortVolumeCreation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnGetPath
			// 
			this.btnGetPath.Location = new System.Drawing.Point(336, 24);
			this.btnGetPath.Name = "btnGetPath";
			this.btnGetPath.Size = new System.Drawing.Size(20, 20);
			this.btnGetPath.TabIndex = 0;
			this.btnGetPath.Text = "...";
			this.btnGetPath.Click += new System.EventHandler(this.btnGetPath_Click);
			// 
			// btnListIt
			// 
			this.btnListIt.Location = new System.Drawing.Point(8, 16);
			this.btnListIt.Name = "btnListIt";
			this.btnListIt.Size = new System.Drawing.Size(56, 23);
			this.btnListIt.TabIndex = 1;
			this.btnListIt.Text = "&Text";
			this.btnListIt.Click += new System.EventHandler(this.btnListIt_Click);
			// 
			// btnBuildVolumeXml
			// 
			this.btnBuildVolumeXml.Location = new System.Drawing.Point(8, 16);
			this.btnBuildVolumeXml.Name = "btnBuildVolumeXml";
			this.btnBuildVolumeXml.Size = new System.Drawing.Size(56, 23);
			this.btnBuildVolumeXml.TabIndex = 2;
			this.btnBuildVolumeXml.Text = "&Volume";
			this.btnBuildVolumeXml.Click += new System.EventHandler(this.btnBuildVolumeXml_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 48);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 16);
			this.label1.TabIndex = 55;
			this.label1.Text = "Generate :";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.btnListIt);
			this.groupBox1.Location = new System.Drawing.Point(8, 64);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(344, 48);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(72, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(200, 16);
			this.label2.TabIndex = 54;
			this.label2.Text = "Text list of folder and all nested items.";
			// 
			// gbVolume
			// 
			this.gbVolume.Controls.Add(this.label3);
			this.gbVolume.Controls.Add(this.btnBuildVolumeXml);
			this.gbVolume.Location = new System.Drawing.Point(8, 112);
			this.gbVolume.Name = "gbVolume";
			this.gbVolume.Size = new System.Drawing.Size(344, 48);
			this.gbVolume.TabIndex = 2;
			this.gbVolume.TabStop = false;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(72, 24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(264, 16);
			this.label3.TabIndex = 55;
			this.label3.Text = "Fully browseable \"image\" of directory and contents.";
			// 
			// btnQuit
			// 
			this.btnQuit.Location = new System.Drawing.Point(280, 164);
			this.btnQuit.Name = "btnQuit";
			this.btnQuit.Size = new System.Drawing.Size(72, 23);
			this.btnQuit.TabIndex = 3;
			this.btnQuit.Text = "&Cancel";
			this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
			// 
			// VolumePathSelectDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(362, 192);
			this.Controls.Add(this.btnQuit);
			this.Controls.Add(this.gbVolume);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnGetPath);
			this.Controls.Add(this.lblStepOne);
			this.Controls.Add(this.txtPath);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximumSize = new System.Drawing.Size(368, 216);
			this.MinimumSize = new System.Drawing.Size(368, 216);
			this.Name = "VolumePathSelectDialog";
			this.Text = "Volume Creator";
			this.Load += new System.EventHandler(this.VolumeCreator_Load);
			this.groupBox1.ResumeLayout(false);
			this.gbVolume.ResumeLayout(false);
			this.ResumeLayout(false);

		}


		#endregion
		

		#region FIELDS

		private System.Windows.Forms.TextBox txtPath;
		private Label lblStepOne;
		private System.ComponentModel.Container components = null;
		private Button btnAbortVolumeCreation;
		private System.Windows.Forms.Button btnGetPath;
		private System.Windows.Forms.Button btnListIt;
		private System.Windows.Forms.Button btnBuildVolumeXml;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnQuit;
		private System.Windows.Forms.GroupBox gbVolume;
		
		/// <summary>
		/// 
		/// </summary>
		private string _forcePath;

		#endregion


		#region PROPERTIES

		/// <summary>
		/// Gets or sets the path to be read as volume
		/// </summary>
		public string Path
		{
			get
			{
				return txtPath.Text;
			}
			set
			{
				if (Directory.Exists(value))
					txtPath.Text = value;
			}
		}


		#endregion


		#region CONSTRUCTORS

		/// <summary> 
		/// 
		/// </summary>
		public VolumePathSelectDialog(
			string forcePath
			)
		{
			InitializeComponent();
			_forcePath = forcePath;
			this.ShowInTaskbar = false;
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

				base.Dispose(disposing);

			}
			catch(Exception ex)
			{
				MainForm.ConsoleAdd(ex);			
			}
		}

		
		#endregion


		#region METHODS

		/// <summary> 
		/// Generates a folderbrowser dialog and inserts selected folder path into path text field 
		/// </summary>
		private void GetTargetPath(
			)
		{

			FolderBrowserDialog dialog = new FolderBrowserDialog();
			
			// try to get last volume capture path from state holder
			if (MainForm.StateBag.Contains("VolumePathSelector.LastVolumePath"))
				dialog.SelectedPath = Convert.ToString(
					MainForm.StateBag.Retrieve("VolumePathSelector.LastVolumePath"));
			
			dialog.ShowDialog();

			txtPath.Text = dialog.SelectedPath;
			txtPath.SelectedText = "";
			
			//gbVolume.Select();
			btnBuildVolumeXml.Select();

			// cleans up filedialogue
			dialog.Dispose();

			// dump currently selected volume path into statebag
			MainForm.StateBag.Add(
				"VolumePathSelector.LastVolumePath", 
				dialog.SelectedPath);

		}
		
		

		/// <summary>
		/// 
		/// </summary>
		private void CreateVolume(
			)
		{
			if (txtPath.Text.Length == 0)
			{
				MainForm.ConsoleAdd("Please specify a path to archive");
				return;
			}
			else if (!Directory.Exists(txtPath.Text))
			{
				MainForm.ConsoleAdd("'" + txtPath.Text + "' is not a valid directory path");
				return;
			}
			
			// hide the "path select" dialog, as we dont want to have two dialogs shown 
			// at the same time
			this.Visible = false;

			FormFactory.SpawnVolumeCreator(
				txtPath.Text);

			this.Close();
		}



		/// <summary>
		/// 
		/// </summary>
		private void CreateList(
			)
		{
			if (txtPath.Text.Length == 0)
			{
				MainForm.ConsoleAdd("Please specify a path to archive");
				return;
			}
			else if (!Directory.Exists(txtPath.Text))
			{
				MainForm.ConsoleAdd("'" + txtPath.Text + "' is not a valid directory path");
				return;
			}
			
			FormFactory.SpawnPathList(
				txtPath.Text);

			this.Close();
	
		}
		

		#endregion


		#region EVENTS

		/// <summary> 
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void VolumeCreator_Load(
			object sender, 
			EventArgs e
			)
		{

			try
			{


				// ###############################################
				// sets path or restores it from state holder
				// -----------------------------------------------
				if (_forcePath != String.Empty && Directory.Exists(_forcePath))
					txtPath.Text = _forcePath;
				else if (MainForm.StateBag.Contains("VolumePathSelector.LastVolumePath"))
				{
					string path = MainForm.StateBag.Retrieve("VolumePathSelector.LastVolumePath").ToString();
					if (Directory.Exists(path))
					{
						txtPath.Text = path;
						btnBuildVolumeXml.Select();
					}
				}


				// force a text changed event to ensure that
				// txtPath-dependant properties are set
				txtPath_TextChanged(null,null);

			}
			catch(Exception ex)
			{
				MainForm.ConsoleAdd(ex);			
			}
		}



		/// <summary> 
		/// Invokes process which gathers data from "new volume" form, so a new volume can be produced. 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnBuildVolumeXml_Click(
			object sender, 
			EventArgs e
			)
		{
			try
			{
				CreateVolume();
			}
			catch(Exception ex)
			{
				MainForm.ConsoleAdd(ex);			
			}
		}
	

		
		/// <summary> 
		/// Invokes process which throws up a folder path dialogue - this will be used to produce a new volume 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnGetPath_Click(
			object sender, 
			EventArgs e
			)
		{
			try
			{
				GetTargetPath();
			}
			catch(Exception ex)
			{
				MainForm.ConsoleAdd(ex);			
			}
		}
		


		/// <summary> 
		/// Invoked by "cancel" button - abandons current work and closes this form
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAnyClose_Click(
			object sender, 
			EventArgs e
			)
		{
			this.Dispose();
		}

		

		/// <summary>
		/// Invoked when txtPath value changes. Disables the "create" button if no path is selected
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtPath_TextChanged(
			object sender, 
			System.EventArgs e
			)
		{

			btnBuildVolumeXml.Enabled = false;
			btnListIt.Enabled = false;

			if (txtPath.Text.Length > 0 && Directory.Exists(txtPath.Text))
			{
				btnListIt.Enabled = true;
				btnBuildVolumeXml.Enabled = true;
			}
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnListIt_Click(
			object sender, 
			System.EventArgs e)
		{
			CreateList();
			
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnQuit_Click(
			object sender, 
			System.EventArgs e)
		{
			this.Dispose();
		}



		#endregion


	}
}
