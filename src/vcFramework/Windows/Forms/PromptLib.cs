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
	/// <summary> Static class of prompts </summary>
	public class PromptLib
	{
		
		#region METHODS
		
		/// <summary> Throws up standard yes/no prompt, and returns result of user choice as boolean. </summary>
		/// <param name="strTitle"></param>
		/// <param name="strMessage"></param>
		/// <returns></returns>
		static public bool DialoguePrompt(
			string strTitle, 
			string strMessage
			)
		{
			DialogResult dlgObject;
			bool blnOutput = false;

			dlgObject = MessageBox.Show(strMessage, strTitle, MessageBoxButtons.OKCancel);

			if(dlgObject == DialogResult.OK)
				blnOutput = true;
			else if(dlgObject == DialogResult.Cancel)
				blnOutput = false;
			
			return blnOutput;
		}
		
		
		
		#endregion

	}
}
