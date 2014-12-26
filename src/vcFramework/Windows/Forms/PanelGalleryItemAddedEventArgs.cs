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
using System.Windows.Forms;

namespace vcFramework.Windows.Forms
{
	/// <summary>
	/// Event args for when when a panel is added to the WizardPanelCollection.
	/// Is used to carry a reference to the panel which was added.
	/// </summary>
	public class PanelGalleryItemAddedEventArgs : EventArgs
	{

		#region Fields

		/// <summary>
		/// The panel which was added to the collection
		/// </summary>
		Panel _panel;

		#endregion


		#region PROPERTIES

		/// <summary>
		/// Gets the panel which was added to the collection
		/// </summary>
		public Panel Panel
		{
			get
			{
				return _panel;
			}
		}

		#endregion


		/// <summary>
		/// Requires reference to the panel which was added to the collection and thus
		/// triggered the event which this eventargs obect is accompanying
		/// </summary>
		/// <param name="panel"></param>
		public PanelGalleryItemAddedEventArgs(
			Panel panel
			)
		{
			_panel = panel;
		}


	}
}
