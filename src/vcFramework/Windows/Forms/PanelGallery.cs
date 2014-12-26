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
using System.Windows.Forms;

using vcFramework.Delegates;

namespace vcFramework.Windows.Forms
{
	/// <summary>
	/// Control which contains one or more panel child controls
	/// </summary>
//	[
//	DefaultProperty("WebMenuItems"),
//	DefaultEvent("MenuItemClicked")
	//System.Drawing.ToolboxBitmap(typeof(Wizard), "Resources.ToolboxBitmaps.TabControl.bmp")
//	]
	
	public class PanelGallery : System.Windows.Forms.UserControl
	{


		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// PanelGallery
			// 
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Name = "PanelGallery";
			this.Size = new System.Drawing.Size(240, 184);

		}
		#endregion


		#region FIELDS

		/// <summary>
		/// 
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// 
		/// </summary>
		private PanelGalleryCollection _panels;
		
		/// <summary>
		/// Index of the currently selected panel. -1 if there are no panels
		/// </summary>
		private int _activePanelIndex;
		
		/// <summary>
		/// Invoked when the active panel index is changed
		/// </summary>
		public event EventHandler ActivePanelChanged;

		#endregion


		#region PROPERTIES

		/// <summary>
		/// Gets the panels collection for this control
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
		Bindable(true), 
		Browsable(true),
		Category("Page")]
		public PanelGalleryCollection Panels
		{
			get
			{
				return _panels;
			}
		}



		/// <summary>
		/// Gets the currently selected panel.Returns null if there are no panels in wizard
		/// </summary>
		public Panel ActivePanel
		{
			get
			{
				if (this._panels.Count > 0)
					return _panels[this.ActivePanelIndex];
				return null;
			}
		}



		/// <summary>
		/// Gets the index of the currently selected panel in the wizard. Returns -1 if there
		/// are no panels
		/// </summary>
		[Bindable(true),
		Browsable(true),
		Category("Page")]
		public int ActivePanelIndex
		{
			set
			{
				int currentPanelIndex = _activePanelIndex;

				if (_panels.Count == 0)
					value = -1;
				else if (value < 0 && _panels.Count > 0)
					value = 0;
				else if (value >= _panels.Count)
					value = _panels.Count - 1;

				if (_panels.Count > value)
					_activePanelIndex = value;

				if (currentPanelIndex != _activePanelIndex)
					DelegateLib.InvokeSubscribers(
						ActivePanelChanged,
						this);
			}
			get
			{
				return _activePanelIndex;
			}
		}


		#endregion


		#region CONSTRUCTORS

		/// <summary>
		/// 
		/// </summary>
		public PanelGallery(
			)
		{


			InitializeComponent();

			// force the default value to -1. -1 is always used when there are no
			// panels in this control
			_activePanelIndex = -1;

			_panels = new PanelGalleryCollection(this);

			// adds default, internal events
			_panels.PanelsCountChanged += new EventHandler(OnPanelsCountChanged);
			this.ActivePanelChanged += new EventHandler(OnActivePanelChanged);
			
			// adds a single panel to control. This is not required per se, but will
			// be more convenient for the person using this control
			//this.Panels.Add(new Panel());

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
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion


		#region EVENTS

		/// <summary>
		/// Invoked when the number of panels in this collection changes. Responsible for control
		/// internal control behaviour in response to all panel changes
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnPanelsCountChanged(
			object sender,
			EventArgs e
			)
		{
			// forces the first panel to be selected if a new panel was
			//added but no panel was selected
			if (_panels.Count > 0 && this.ActivePanelIndex == -1)
				this.ActivePanelIndex = 0;

		}



		/// <summary>
		/// Invoked when the active panel index is changed - the index is changed via the 
		/// ActivePanelIndex property of this control. When the index changes, this method
		/// redraws the contents of this control so the new active panel is visible.
		/// </summary>
		private void OnActivePanelChanged(
			object sender,
			EventArgs e
			)
		{
			for (int i = 0 ; i < _panels.Count ; i ++)
			{
				if (i != ActivePanelIndex)
					_panels[i].Visible = false;
				else
					_panels[i].Visible = true;
			}
			
		}


		#endregion
		
	}
}
