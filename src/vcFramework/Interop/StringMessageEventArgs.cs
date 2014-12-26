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

namespace vcFramework.Interop
{
	/// <summary>
	/// Summary description for StringMessageEventArgs.
	/// </summary>
	public class StringMessageEventArgs : EventArgs
	{
		
		#region FIELDS

		/// <summary>
		/// 
		/// </summary>
		private string _message;

		#endregion


		#region PROPERTIES

		public string Message
		{
			get
			{
				return _message;
			}
		}

		#endregion


		#region CONSTRUCTORS
        
		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		public StringMessageEventArgs(
			string message
			)
		{
			_message = message;
		}

		#endregion

	}
}
