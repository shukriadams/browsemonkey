//////////////////////////////////////////////////////////////////////////////////////
// Author				: Shukri Adams												//
// Contact				: shukri.adams@gmail.com										//
// Compiler requirement : .Net 4.0													//
//																					//
// vcFramework : A reuseable library of utility classes                             //
// Copyright (C)																	//
//																					//
// This program is free software; you can redistribute it and/or modify it under	//
// the terms of the GNU General Public License as published by the Free Software	//
// Foundation; either version 2 of the License, or (at your option) any later		//
// version.																			//
//																					//
// This program is distributed in the hope that it will be useful, but WITHOUT ANY	//
// WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A	//
// PARTICULAR PURPOSE. See the GNU General Public License for more details.			//
//																					//
// You should have received a copy of the GNU General Public License along with		//
// this program; if not, write to the Free Software Foundation, Inc., 59 Temple		//
// Place, Suite 330, Boston, MA 02111-1307 USA										//
//////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;

namespace vcFramework.Windows.Forms
{
	/// <summary> Simple, reuseable "About" form. Features include : has a top and side image, which will automatically
	/// set form size. text boxes populated from an xml node. location can be set to the middle of any background form. </summary>
	public class AboutForm : System.Windows.Forms.Form
	{

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnClose = new Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.tcContents = new System.Windows.Forms.TabControl();
			this.tpAbout = new System.Windows.Forms.TabPage();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.rtbAbout = new System.Windows.Forms.RichTextBox();
			this.tpCredits = new System.Windows.Forms.TabPage();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.rtbCredits = new System.Windows.Forms.RichTextBox();
			this.tpChangeLog = new System.Windows.Forms.TabPage();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.rtbChangeLog = new System.Windows.Forms.RichTextBox();
			this.imgSplashTop = new System.Windows.Forms.PictureBox();
			this.panel1.SuspendLayout();
			this.tcContents.SuspendLayout();
			this.tpAbout.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tpCredits.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.tpChangeLog.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.Image = null;
			this.btnClose.Location = new System.Drawing.Point(376, 507);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(80, 23);
			this.btnClose.TabIndex = 0;
			this.btnClose.Text = "Close";
			this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Controls.Add(this.tcContents);
			this.panel1.Controls.Add(this.imgSplashTop);
			this.panel1.Location = new System.Drawing.Point(8, 8);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(448, 499);
			this.panel1.TabIndex = 2;
			// 
			// tcContents
			// 
			this.tcContents.Controls.Add(this.tpAbout);
			this.tcContents.Controls.Add(this.tpCredits);
			this.tcContents.Controls.Add(this.tpChangeLog);
			this.tcContents.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcContents.Location = new System.Drawing.Point(0, 168);
			this.tcContents.Name = "tcContents";
			this.tcContents.SelectedIndex = 0;
			this.tcContents.Size = new System.Drawing.Size(448, 331);
			this.tcContents.TabIndex = 1;
			// 
			// tpAbout
			// 
			this.tpAbout.Controls.Add(this.groupBox1);
			this.tpAbout.Location = new System.Drawing.Point(4, 22);
			this.tpAbout.Name = "tpAbout";
			this.tpAbout.Size = new System.Drawing.Size(392, 129);
			this.tpAbout.TabIndex = 0;
			this.tpAbout.Text = "About";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.rtbAbout);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(392, 129);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			// 
			// rtbAbout
			// 
			this.rtbAbout.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.rtbAbout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtbAbout.Location = new System.Drawing.Point(3, 16);
			this.rtbAbout.Name = "rtbAbout";
			this.rtbAbout.ReadOnly = true;
			this.rtbAbout.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.rtbAbout.Size = new System.Drawing.Size(386, 110);
			this.rtbAbout.TabIndex = 1;
			this.rtbAbout.Text = "";
			// 
			// tpCredits
			// 
			this.tpCredits.Controls.Add(this.groupBox2);
			this.tpCredits.Location = new System.Drawing.Point(4, 22);
			this.tpCredits.Name = "tpCredits";
			this.tpCredits.Size = new System.Drawing.Size(392, 129);
			this.tpCredits.TabIndex = 1;
			this.tpCredits.Text = "Credits";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.rtbCredits);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Location = new System.Drawing.Point(0, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(392, 129);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			// 
			// rtbCredits
			// 
			this.rtbCredits.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.rtbCredits.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtbCredits.Location = new System.Drawing.Point(3, 16);
			this.rtbCredits.Name = "rtbCredits";
			this.rtbCredits.ReadOnly = true;
			this.rtbCredits.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.rtbCredits.Size = new System.Drawing.Size(386, 110);
			this.rtbCredits.TabIndex = 3;
			this.rtbCredits.Text = "";
			// 
			// tpChangeLog
			// 
			this.tpChangeLog.Controls.Add(this.groupBox3);
			this.tpChangeLog.Location = new System.Drawing.Point(4, 22);
			this.tpChangeLog.Name = "tpChangeLog";
			this.tpChangeLog.Size = new System.Drawing.Size(440, 305);
			this.tpChangeLog.TabIndex = 2;
			this.tpChangeLog.Text = "Change Log";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.rtbChangeLog);
			this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox3.Location = new System.Drawing.Point(0, 0);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(440, 305);
			this.groupBox3.TabIndex = 0;
			this.groupBox3.TabStop = false;
			// 
			// rtbChangeLog
			// 
			this.rtbChangeLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.rtbChangeLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtbChangeLog.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.rtbChangeLog.Location = new System.Drawing.Point(3, 16);
			this.rtbChangeLog.Name = "rtbChangeLog";
			this.rtbChangeLog.ReadOnly = true;
			this.rtbChangeLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.rtbChangeLog.Size = new System.Drawing.Size(434, 286);
			this.rtbChangeLog.TabIndex = 3;
			this.rtbChangeLog.Text = "";
			// 
			// imgSplashTop
			// 
			this.imgSplashTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.imgSplashTop.Location = new System.Drawing.Point(0, 0);
			this.imgSplashTop.Name = "imgSplashTop";
			this.imgSplashTop.Size = new System.Drawing.Size(448, 168);
			this.imgSplashTop.TabIndex = 0;
			this.imgSplashTop.TabStop = false;
			// 
			// AboutForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(458, 536);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.btnClose);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "AboutForm";
			this.Text = "About";
			this.panel1.ResumeLayout(false);
			this.tcContents.ResumeLayout(false);
			this.tpAbout.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.tpCredits.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.tpChangeLog.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		

		#region FIELDS

		private Button btnClose;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TabPage tpAbout;
		private System.Windows.Forms.TabControl tcContents;
		private System.Windows.Forms.TabPage tpCredits;
		private System.Windows.Forms.TabPage tpChangeLog;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.RichTextBox rtbAbout;
		private System.Windows.Forms.RichTextBox rtbCredits;
		private System.Windows.Forms.RichTextBox rtbChangeLog;
		private System.Windows.Forms.PictureBox imgSplashTop;
		private System.ComponentModel.Container components = null;
		
		#endregion


		#region PROPERTIES


		#endregion


		#region CONSTRUCTORS

		/// <summary>
		/// 
		/// </summary>
		/// <param name="imgTop"></param>
		/// <param name="imgSide"></param>
		/// <param name="xXmlContents"></param>
		public AboutForm(
			Bitmap imgTop,
			string about,
			string credits,
			string changelog
			)
		{

			InitializeComponent();

			// sets properties of this form 
			this.ShowInTaskbar	= false;

			// adds images to form
			imgSplashTop.Image	= imgTop;

			
			// sets image sizes, which in turn will set the proportions of the form as a whole - docking properties have been
			// used so that the form can expand or contract according to image size
			imgSplashTop.Size	= imgTop.Size;
			
			this.Width			= imgTop.Width + 16;
			this.MaximumSize = this.Size;
			this.MinimumSize = this.Size;
 
            rtbAbout.Text = about;
			rtbCredits.Text = credits;
			rtbChangeLog.Text = changelog;

		}
		

		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="imgTop"></param>
		/// <param name="imgSide"></param>
		/// <param name="about"></param>
		/// <param name="credits"></param>
		public AboutForm(
			Bitmap imgTop, 
			string about,
			string credits
			)
		{

			InitializeComponent();

			// sets properties of this form 
			this.ShowInTaskbar	= false;

			// adds images to form
			imgSplashTop.Image	= imgTop;
			
			// sets image sizes, which in turn will set the proportions of the form as a whole - docking properties have been
			// used so that the form can expand or contract according to image size
			imgSplashTop.Size	= imgTop.Size;
			
			this.Width			= imgTop.Width + 16;
			this.MaximumSize = this.Size;
			this.MinimumSize = this.Size;

			// 
			rtbAbout.Text = about;
			rtbCredits.Text = credits;

			tcContents.Visible = false;

		}


		#endregion
		

		#region DESTRUCTORS
			

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		
		#endregion
	
		

		#region EVENTS

		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnClose_Click(
			object sender, 
			System.EventArgs e
			)
		{
			this.Close();
		}

		
		#endregion

	}
}
