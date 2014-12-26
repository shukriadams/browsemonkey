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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace vcFramework.Windows.Forms
{
	/// <summary> Simple generic splash screen </summary>
	public class Splash : System.Windows.Forms.Form
	{

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.imgSplash = new System.Windows.Forms.PictureBox();
			this.lblSplashText = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// imgSplash
			// 
			this.imgSplash.Dock = System.Windows.Forms.DockStyle.Top;
			this.imgSplash.Location = new System.Drawing.Point(0, 0);
			this.imgSplash.Name = "imgSplash";
			this.imgSplash.Size = new System.Drawing.Size(296, 232);
			this.imgSplash.TabIndex = 0;
			this.imgSplash.TabStop = false;
			// 
			// lblSplashText
			// 
			this.lblSplashText.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblSplashText.Location = new System.Drawing.Point(0, 232);
			this.lblSplashText.Name = "lblSplashText";
			this.lblSplashText.Size = new System.Drawing.Size(296, 24);
			this.lblSplashText.TabIndex = 1;
			// 
			// Splash
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(296, 256);
			this.Controls.Add(this.lblSplashText);
			this.Controls.Add(this.imgSplash);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Splash";
			this.Load += new System.EventHandler(this.Splash_Load);
			this.ResumeLayout(false);

		}
		#endregion
		

		#region MEMBERS

		private PictureBox imgSplash;
		private Container components = null;
		private Label lblSplashText;

		/// <summary> Timer object used to control spash timeout </summary>
        private Timer m_Timer;

		/// <summary> timer object used to control the automatic shutdown of splash screen </summary>
		private int m_intSplashTimeOut;
		
		/// <summary> 
		/// Preset height of the "label" area of splash. 
		/// </summary>
		private const int M_INTSPLASHTEXTHEIGHT = 30;
		
		private int m_intSplashTextHeight;

		#endregion
		

		#region PROPERTIES
		
		/// <summary> Gets or sets text on splash screen </summary>
		public string SplashText
		{
			set{lblSplashText.Text = value;}
			get{return lblSplashText.Text;}
		}
		
		
		/// <summary> Sets the backcolor of the text area on the splash screen </summary>
		public Color SplashTextBackColor
		{
			set{lblSplashText.BackColor = value;}
		}


		#endregion


		#region CONSTRUCTORS

		public Splash(
			Form frmApplication,
			Bitmap imgSplashPicture, 
			int intTimeOut,
			bool DisplaySplashText)
		{

			InitializeComponent();

			// stores arguments in members where necessary
			m_intSplashTimeOut = intTimeOut;
			imgSplash.Image = imgSplashPicture;
			
			m_intSplashTextHeight = 0;
			lblSplashText.Visible = false;
			if (DisplaySplashText)
			{
				m_intSplashTextHeight = M_INTSPLASHTEXTHEIGHT;
				lblSplashText.Visible = true;
			}

			// sets proportions of form and label on form
			this.Size = imgSplash.Size = imgSplashPicture.Size;
			this.lblSplashText.Height = m_intSplashTextHeight;
			this.ShowInTaskbar = false;

			// sets start location - splash is forced to middle of the "application" form that is passed in to constructor
			FormLib.ScreenCenterForm(this);
			
			

			// sets up events
			imgSplash.MouseDown += new MouseEventHandler(imgSplash_MouseDown);
			this.LostFocus += new EventHandler(Splash_LostFocus);
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
		

		#region METHODS
		
		/// <summary> Writes a text message to splash's visible label </summary>
		/// <param name="strMessage"></param>
		public void WriteTextMessage(string strMessage)
		{
			lblSplashText.Text = strMessage;
		}


		#endregion


		#region EVENTS


		private void Splash_Load(object sender, EventArgs e)
		{
            m_Timer = new Timer 
            {
                Interval = m_intSplashTimeOut
            };
			m_Timer.Tick += SplashTimeout;
			m_Timer.Start();
		}
		
	

		/// <summary> 
        /// Invoked when splash loses focus - forces focus back on form for the duration of its life 
        /// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Splash_LostFocus(object sender, EventArgs e)
		{
			this.Focus();
		}

		
		
		/// <summary> 
		/// Called by timer - handles timeout behaviour of splash 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SplashTimeout(object sender, EventArgs e)
		{
			m_Timer.Stop();
			m_Timer.Dispose();
			this.Visible = false;
			this.Close();
		}
		
		/// <summary> 
		/// Allows a mouse click on splash form to shut it down 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void imgSplash_MouseDown(object sender, MouseEventArgs e)
		{
			this.Visible = false;
			this.Close();
		}

		#endregion

	}
}
