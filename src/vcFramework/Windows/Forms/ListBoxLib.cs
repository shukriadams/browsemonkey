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


	/// <summary>
	/// Summary description for ListBoxLib.
	/// </summary>
	public class ListBoxLib
	{


		/// <summary>
		/// Fills a listbox object with value+display type
		/// data items, which in turn are derived from a 2D string array where the first
		/// "column" in array is the value and the second is the display
		/// </summary>
		/// <param name="listBox"></param>
		/// <param name="arrStrData"></param>
		static public void PopulateListBox(
			ListBox listBox, 
			string[,] arrStrData
			)
		{
			if (arrStrData.GetLength(0) == 0)
				return;

			// defines array - note "intBlankLineModifier" - if adding a blank line to combobox, need to offset other data by 1
			ArrayList dataSource = new ArrayList();

			// populates array with structs 
			// note "intBlankLineModifier" - if adding blank line to combobox need to offset other data by 1
			for (int i = 0 ; i < arrStrData.GetLength(0) ; i ++)
				dataSource.Add(
					new ComboBoxDataItem(arrStrData[i, 0], arrStrData[i,1]));
				
			listBox.DataSource = null;

			// Populate the list
			listBox.DataSource = dataSource;

			// Define the field to be displayed
			listBox.DisplayMember = "DisplayMember";

			// Define the field to be used as the value
			listBox.ValueMember = "ValueMember";

		}	


	
		/// <summary>
		/// Fills a listbox object with value+display type
		/// data items, which in turn are derived from a 2D string array where the first
		/// "column" in array is the value and the second is the display.
		/// </summary>
		/// <param name="listBox"></param>
		/// <param name="arrStrData"></param>
		static public void PopulateListBox(
			ListBox listBox, 
			string[] arrStrData
			)
		{

			// defines array - note "intBlankLineModifier" - if adding a blank line to combobox, need to offset other data by 1
			ArrayList dataSource = new ArrayList();
			
			// populates array with structs 
			for (int i = 0 ; i < arrStrData.GetLength(0) ; i ++)
				dataSource.Add(
					new DisplayValueItem(arrStrData[i], arrStrData[i]));

			listBox.DataSource = null;
			
			// if the new datasource is empty, a bind failure will occur.
			// abort population after nulling the datasource, as this
			// will blank the listbox and "simulate" that is has been
			// populated from an empty datasource
			if (dataSource.Count == 0)
				return;

			// Populate the list
			listBox.DataSource = dataSource;

			// Define the field to be displayed
			listBox.DisplayMember = "DisplayMember";

			// Define the field to be used as the value
			listBox.ValueMember = "ValueMember";

		}	



	}
}
