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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Timers;
using System.Windows.Forms;
using vcFramework.Delegates;
using vcFramework.Drawing;


namespace vcFramework.Windows.Forms
{
	/// <summary> </summary>
	public class SignOfLife : System.Windows.Forms.UserControl
	{

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lblBar = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblBar
			// 
			this.lblBar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblBar.Location = new System.Drawing.Point(0, 0);
			this.lblBar.Name = "lblBar";
			this.lblBar.Size = new System.Drawing.Size(152, 16);
			this.lblBar.TabIndex = 0;
			this.lblBar.Text = "..............";
			this.lblBar.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// SignOfLife
			// 
			this.Controls.Add(this.lblBar);
			this.Name = "SignOfLife";
			this.Size = new System.Drawing.Size(152, 16);
			this.ResumeLayout(false);

		}
		#endregion


		#region FIELDS

		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label lblBar;
		
		private System.Timers.Timer m_objTimer;
	
		#endregion


		#region CONSTRUCTORS

		public SignOfLife()
		{

			InitializeComponent();
			lblBar.Text = ".";
		}
		
		#endregion


		#region DESTRUCTORS

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{

				// stops timer just incaseit's still running
				AutoLivingStop();

				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		#endregion


		#region METHODS
		
		/// <summary> Not thread-safe  </summary>
		public void Live(
			)
		{
			if (DrawingLib.TextPixelWidth(lblBar.Text, lblBar.Font) < lblBar.Width)
				lblBar.Text += ".";
			else
				lblBar.Text = ".";
		}
		



		public void AutoLivingStart(
			//int intLifeSpeedInterval
			)
		{
			if (m_objTimer == null)
			{
				m_objTimer = new System.Timers.Timer(
					500	
					);

				m_objTimer.Elapsed += new ElapsedEventHandler(objTimer_Elapsed);
				m_objTimer.Start();
			}
		}
		


		/// <summary> </summary>
		public void AutoLivingStop(
			)
		{
			if (m_objTimer != null)
			{
				m_objTimer.Stop();
				m_objTimer.Dispose();
				m_objTimer = null;
			}
		}



		/// <summary> </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void objTimer_Elapsed(
			object sender,
			ElapsedEventArgs e
			)
		{
			Live();
		}


		#endregion

	}
}
