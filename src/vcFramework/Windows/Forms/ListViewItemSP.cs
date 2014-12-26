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
using vcFramework.DataItems;

namespace vcFramework.Windows.Forms
{
	/// <summary>  </summary>
	public class ListViewItemSP : ListViewItem
	{

		#region MEMBERS

		/// <summary> 
		/// set to true if this item has hidden columns 
		/// </summary>
		private bool m_blnHasHiddenItems;
		
		/// <summary> 
		/// Array holding hidden column data 
		/// </summary>
		private StringItem[] m_arrStringItems;
		
		#endregion

		
		#region PROPERTIES

		/// <summary> 
		/// gets if this item contains hidden columns has hidden col 
		/// </summary>
		public bool HasHiddenItems					
		{
			get
			{
				return m_blnHasHiddenItems;
			}
		}
		
		
		/// <summary> 
		/// gets or sets hidden column collection 
		/// </summary>
		public StringItem[] HiddenColumCollection	
		{
			get
			{
				return m_arrStringItems;
			}
			set
			{

				m_arrStringItems = value;

				if (m_arrStringItems != null && m_arrStringItems.Length > 0)
					m_blnHasHiddenItems = true;
			}
		}


		#endregion
		

		#region CONSTRUCTORS

		/// <summary> 
		/// 
		/// </summary>
		/// <param name="arrStrRowContents"></param>
		public ListViewItemSP(
			string[] arrStrRowContents
			) : base(arrStrRowContents)
		{

		}

		
		#endregion

	}
}
