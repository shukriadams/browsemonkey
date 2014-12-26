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

namespace vcFramework.Windows.Forms
{

	/// <summary>Struct Used to populate Combobox </summary>
	public struct ComboBoxDataItem
	{

		#region MEMBERS

		private string m_strValueMember;
		private string m_strDisplayMember;

		#endregion


		#region PROPERTIES

		public string ValueMember	
		{ 
			get 
			{ 
				return m_strValueMember; 
			}
			
		}
		
		public string DisplayMember 
		{ 
			get 
			{ 
				return m_strDisplayMember; 
			} 
			set
			{
				m_strDisplayMember = value;
			}
		}


		#endregion


		#region CONSTRUCTORS

		public ComboBoxDataItem(string strValueMember, string strDisplayMember)
		{
			this.m_strValueMember = strValueMember;
			this.m_strDisplayMember = strDisplayMember;
		}

		#endregion

	}
}
