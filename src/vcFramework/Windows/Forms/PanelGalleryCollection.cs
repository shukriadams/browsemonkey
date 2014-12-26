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
using System.Windows.Forms;
using vcFramework.Delegates;

namespace vcFramework.Windows.Forms
{

	/// <summary> 
	/// Collection object made specifically for panels displays in a PanelGallery
	/// control. Panels are added to this collection, not the parent PanelGallery control,
	/// and this collection manages adding the panel to the parent control's Controls
	/// collection. Additionally, this collection sets most of the properties and events
	/// for each panel in the PanelGallery.
	/// </summary>
	public class PanelGalleryCollection : CollectionBase
	{

		#region FIELDS

		/// <summary>
		/// Panel Gallery control which this collection services
		/// </summary>
		private PanelGallery _panelGallery;

		/// <summary>
		/// Invoked when a panel is added to or removed from this collection
		/// </summary>
		public event EventHandler PanelsCountChanged;

		/// <summary>
		/// Invoked when a panel is added to this collection
		/// </summary>		
		public event EventHandler PanelAdded;

		/// <summary>
		/// Invoked when a panel is removed from this collection
		/// </summary>		
		public event EventHandler PanelRemoved;

		
		#endregion


		#region CONSTRUCTORS

		/// <summary>
		/// 
		/// </summary>
		/// <param name="panelGallery"></param>
		public PanelGalleryCollection(
			PanelGallery panelGallery
			)
		{
			_panelGallery = panelGallery;
		}


		#endregion


		#region METHODS

		/// <summary> 
		/// Gets the panel at the given index
		/// </summary>
		public Panel this[int intIndex]
		{
			get
			{
				return (Panel) base.List[intIndex];
			}
		}



		/// <summary> 
		/// Adds a panel to this collection
		/// </summary>
		/// <param name="objWebMenuItem"></param>
		public void Add(
			Panel panel
			)
		{

			panel.Dock = DockStyle.Fill;
			panel.DockChanged += new EventHandler(OnPanelDockStyleChanged);

			// need to add the panel to wizard's control manually from this location -
			// panels are not added to the control collection from the designer
			_panelGallery.Controls.Add(panel);

			base.List.Add(
				panel);

			DelegateLib.InvokeSubscribers(
				PanelsCountChanged,
				this);

			DelegateLib.InvokeSubscribers(
				PanelAdded,
				this,
				new PanelGalleryItemAddedEventArgs(panel));

		}



		/// <summary> 
		/// Removes the given panel from this collection
		/// </summary>
		/// <param name="objWebMenuItem"></param>
		public void Remove(
			Control objWebMenuItem
			)
		{

			base.List.Remove(objWebMenuItem);

			DelegateLib.InvokeSubscribers(
				PanelsCountChanged,
				this);

			DelegateLib.InvokeSubscribers(
				PanelRemoved,
				this);
		}



		/// <summary> 
		/// Gets the position of the given panel
		/// </summary>
		/// <param name="objWebMenuItem"></param>
		/// <returns></returns>
		public int IndexOf(
			Panel panel
			)
		{
			return base.List.IndexOf(panel);
		}



		#endregion


		#region EVENTS

		/// <summary>
		/// Invoked whenever the dock style of any panel in the panel gallery changes - forces
		/// dock style to remain as "fill". This must be done or panels can lose their ability
		/// to occupy the full client rectangle area of the parent panelgallery control
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnPanelDockStyleChanged(
			object sender,
			EventArgs e
			)
		{

			Panel panel = (Panel)sender;
			panel.Dock = DockStyle.Fill;

		}


		#endregion

	}
}
