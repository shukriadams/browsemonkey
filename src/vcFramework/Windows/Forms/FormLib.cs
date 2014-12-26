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
using System.Drawing;
using System.Windows.Forms;

namespace vcFramework.Windows.Forms
{
	/// <summary>  
	/// 
	/// </summary>
	public class FormLib
	{

		/// <summary> 
		/// 
		/// </summary>
		/// <param name="objFixedform"></param>
		/// <param name="frmFloatForm"></param>
		/// <returns></returns>
		static public Point GetMiddleLocation(
			Form objFixedform, 
			Form frmFloatForm
			)
		{

			// gets screen cordinates of middle of fixedform
			Point pntFixedFormMiddle = new Point(
				objFixedform.DesktopLocation.X + (objFixedform.Width/2),
				objFixedform.DesktopLocation.Y + (objFixedform.Height/2));

			Point pntFloatFormNewLocation = new Point(
				pntFixedFormMiddle.X - (frmFloatForm.Width/2),
				pntFixedFormMiddle.Y - (frmFloatForm.Height/2));

			return pntFloatFormNewLocation;
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="fixedform"></param>
		/// <param name="floatForm"></param>
		static public void PositionFormInMiddle(
			Form fixedform, 
			Form floatForm
			)
		{
			PositionFormInMiddle(
				fixedform,
				floatForm,
				false);
		}



		/// <summary> 
		/// 
		/// </summary>
		/// <param name="objFixedform"></param>
		/// <param name="frmFloatForm"></param>
		/// <returns></returns>
		static public void PositionFormInMiddle(
			Form fixedform, 
			Form floatForm, 
			bool ensureVisible
			)
		{

			floatForm.StartPosition = System.Windows.Forms.FormStartPosition.Manual;

			// gets screen cordinates of middle of fixedform
			Point pntFixedFormMiddle = new Point(
				fixedform.DesktopLocation.X + (fixedform.Width/2),
				fixedform.DesktopLocation.Y + (fixedform.Height/2));

			Point newLocation = new Point(
				pntFixedFormMiddle.X - (floatForm.Width/2),
				pntFixedFormMiddle.Y - (floatForm.Height/2));
			
			if (ensureVisible)
			{
				// prevents floating form from extending beyond uppper left corner
				// of screen
				if (newLocation.Y < 0) 
					newLocation.Y = 0;
				if (newLocation.X < 0) 
					newLocation.X = 0;

				// prevents floating form from extending beyond lower right
				// corner of screen
				Screen screen = Screen.FromControl(fixedform);

				if(newLocation.Y + floatForm.Height > screen.Bounds.Height)
					newLocation.Y = screen.Bounds.Height- floatForm.Height;
				if(newLocation.X + floatForm.Width> screen.Bounds.Width)
					newLocation.X = screen.Bounds.Width - floatForm.Width;
			}

			floatForm.Location = newLocation;
		}

		

		/// <summary>
		/// Positions the given form in the middle of the screen
		/// </summary>
		/// <param name="form"></param>
		static public void ScreenCenterForm(
			Form form
			)
		{
			form.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Screen screen = Screen.FromControl(form);

			Point screenCenter = new Point(
				screen.WorkingArea.X + (screen.WorkingArea.Width/2),
				screen.WorkingArea.Y + (screen.WorkingArea.Height/2));

			form.Location = new Point(
				screenCenter.X - (form.Width/2),
				screenCenter.Y - (form.Height/2));
		}


	}
}
