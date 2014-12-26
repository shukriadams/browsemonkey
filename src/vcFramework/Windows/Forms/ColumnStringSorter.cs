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

namespace vcFramework.Windows.Forms
{

	public class ColumnStringSorter : IComparer
	{
		
		
		#region MEMBERS

		private int m_currentColumn = 0;
		private vcFramework.Collections.SortingOrder m_sortOrder;
		
		#endregion


		#region PROPERTIES

		public vcFramework.Collections.SortingOrder sortOrder
		{
			set{m_sortOrder = value;}
		}
		public int currentColumn
		{
			set{m_currentColumn = value;}
			get{return m_currentColumn;}
		}

		#endregion


		#region CONSTRUCTORS

		public ColumnStringSorter()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#endregion


		#region METHODS

		public int Compare(object x, object y)
		{

			ListViewItem itemX = (ListViewItem)x;
			ListViewItem itemY = (ListViewItem)y;


			if (m_sortOrder == vcFramework.Collections.SortingOrder.Ascending) 
				return String.Compare(itemY.SubItems[m_currentColumn].Text, itemX.SubItems[m_currentColumn].Text);
			else
				return String.Compare(itemX.SubItems[m_currentColumn].Text,itemY.SubItems[m_currentColumn].Text);
		
		}


		
		#endregion
	}
}
