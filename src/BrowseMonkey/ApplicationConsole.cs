///////////////////////////////////////////////////////////////
// BrowseMonkey - An offline filesystem browser              //
// Shukri Adams (shukri.adams@gmail.com)                     //
// https://github.com/shukriadams/browsemonkey               //
// MIT License (MIT) Copyright (c) 2014 Shukri Adams         //
///////////////////////////////////////////////////////////////

using System.Windows.Forms;

namespace BrowseMonkey
{
	/// <summary>
	/// Console form. The actual console logic is in the MessageConsole 
	/// user control
	/// </summary>
	public class ApplicationConsole : Form
	{

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.messageConsole = new vcFramework.UserControls.MessageConsole();
			this.SuspendLayout();
			// 
			// messageConsole
			// 
			this.messageConsole.Dock = System.Windows.Forms.DockStyle.Fill;
			this.messageConsole.Location = new System.Drawing.Point(0, 0);
			this.messageConsole.Name = "messageConsole";
			this.messageConsole.ShowButtons = false;
			this.messageConsole.Size = new System.Drawing.Size(584, 268);
			this.messageConsole.TabIndex = 0;
			// 
			// ApplicationConsole
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(584, 268);
			this.Controls.Add(this.messageConsole);
			this.Name = "ApplicationConsole";
			this.Text = "ApplicationConsole";
			this.ResumeLayout(false);

		}
		#endregion
		
		
		#region FIELDS

		public vcFramework.UserControls.MessageConsole messageConsole;
		private System.ComponentModel.Container components = null;

		#endregion


		#region CONSTRUCTORS

		/// <summary>
		/// 
		/// </summary>
		public ApplicationConsole(
			)
		{

			InitializeComponent();

		}


		#endregion


		#region DESTRUCTORS

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( 
			bool disposing)
		{
			if( disposing )
			{
				if(components != null)
					components.Dispose();
			}
			base.Dispose( disposing );
		}


		#endregion


	}
}
