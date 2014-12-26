///////////////////////////////////////////////////////////////
// BrowseMonkey - An offline filesystem browser              //
// Shukri Adams (shukri.adams@gmail.com)                     //
// https://github.com/shukriadams/browsemonkey               //
// MIT License (MIT) Copyright (c) 2014 Shukri Adams         //
///////////////////////////////////////////////////////////////

using System.IO;
using System.Windows.Forms;
using System.Xml;
using vcFramework.Xml;

namespace BrowseMonkey
{
	/// <summary>
	/// Form used to display content exported from volumes
	/// </summary>
	public class ExportViewer : Form, ISpawned
	{

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.txtContents = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// txtContents
			// 
			this.txtContents.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtContents.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtContents.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtContents.Location = new System.Drawing.Point(0, 0);
			this.txtContents.Multiline = true;
			this.txtContents.Name = "txtContents";
			this.txtContents.ReadOnly = true;
			this.txtContents.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtContents.Size = new System.Drawing.Size(424, 277);
			this.txtContents.TabIndex = 1;
			this.txtContents.Text = "";
			this.txtContents.WordWrap = false;
			// 
			// ExportViewer
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(424, 277);
			this.Controls.Add(this.txtContents);
			this.Name = "ExportViewer";
			this.Text = "TextDump";
			this.ResumeLayout(false);

		}
		#endregion


		#region FIELDS

		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.TextBox txtContents;
		
		/// <summary>
		/// 
		/// </summary>
		private ExportTypes _exportType;

		#endregion
	

		#region PROPERTIES
		
		/// <summary>
		/// Gets or sets the text contents of this form
		/// </summary>
		public string Contents
		{
			get
			{
				return txtContents.Text;
			}
			set
			{
				txtContents.Text = value;
			}
		}

		
		#endregion


		#region CONSTRUCTORS

		/// <summary>
		/// 
		/// </summary>
		/// <param name="exportType"></param>
		public ExportViewer(
			ExportTypes exportType
			)
		{
			InitializeComponent();
			_exportType = exportType;
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
				if(components != null)
					components.Dispose();

			}



			base.Dispose( disposing );
		}


		#endregion


		#region METHODS

		/// <summary>
		/// Attempts to indent format the text contents of this form. Process
		/// will fail if the form does not contain well-formed xml
		/// </summary>
		public void IndentFormatXml(
			)
		{

			XmlDocument d = new XmlDocument();
			d.LoadXml(txtContents.Text);
			txtContents.Text = XmlLib.IndentFormatXml(d);

		}


		/// <summary>
		/// Wraps process for saving the contents of this form down to file
		/// </summary>
		public void Save(
			)
		{
			
			SaveFileDialog dialog = new SaveFileDialog();
			
			if (_exportType == ExportTypes.Text)
				dialog.Filter = "Text (*.txt)|*.txt|All files (*.*)|*.*";
			else if (_exportType == ExportTypes.Xml)
				dialog.Filter = "Xml (*.xml)|*.xml|All files (*.*)|*.*";

			dialog.ShowDialog();

			if (dialog.FileName.Length != 0)
			{
				File.WriteAllText(
					dialog.FileName,
					txtContents.Text);	// overwrite existing

				MainForm.ConsoleAdd(
					Path.GetFileName(dialog.FileName) + " has been saved");
			}

			dialog.Dispose();

		}


		#endregion



	}
}
