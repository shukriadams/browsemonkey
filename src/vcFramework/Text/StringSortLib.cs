//////////////////////////////////////////////////////////////////////////////////////
// Author				: Shukri Adams												//
// Contact				: shukri.adams@gmail.com									//
//																					//
// vcFramework : A reuseable library of utility classes                             //
// Copyright (C)																	//
//////////////////////////////////////////////////////////////////////////////////////

using System;

namespace vcFramework.Text
{
	/// <summary> Static lib </summary>
	public class StringSortLib
	{

		/// <summary>
		/// 
		/// </summary>
		/// <param name="strFirst"></param>
		/// <param name="strSecond"></param>
		/// <returns></returns>
		public static bool IsAfter(
			string strFirst, 
			string strSecond
			)
		{
			return IsAfter(
				strFirst,
				strSecond,
				true);
		}


		/// <summary> 
		/// Compares two strings and returns true if the second
		/// string comes alphabetically after the first string. Can 
		/// ignore cases of strings
		/// </summary>
		/// <param name="strFirst"></param>
		/// <param name="strSecond"></param>
		/// <param name="blnIgnoreCase"></param>
		/// <returns></returns>
		public static bool IsAfter(
			string strFirst, 
			string strSecond, 
			bool blnIgnoreCase
			)
		{
			
			int intCheckLength = strFirst.Length;
			int intFirstStringValue = 0;
			int intSecondStringValue = 0;

			// handles zero lengths
			if (strFirst == String.Empty)
				return true;
			else if (strSecond == String.Empty)
				return false;

			// gets length of shortest string (between strFirst and strSecond)
			if (strSecond.Length < intCheckLength)
				intCheckLength = strSecond.Length;

			// if ignore case, force all strings to lowercase
			if (blnIgnoreCase)
			{
				strFirst = strFirst.ToLower();
				strSecond = strSecond.ToLower();
			}
			
			// compares corresponding chars in each string, for the length of the shortest of the 
			// two strings. if chars are not the same determines which char is first (numeric conversion)
			// and then returns a true/false based on outcome. if chars are numerically identic, proceeds
			// to next char in strings
			for (int i = 0 ; i < intCheckLength ; i ++)
			{
				intFirstStringValue = Convert.ToInt32(Convert.ToChar(strFirst.Substring(i, 1)));
				intSecondStringValue = Convert.ToInt32(Convert.ToChar(strSecond.Substring(i, 1)));

				if (intFirstStringValue < intSecondStringValue)
					return true;
				else if (intFirstStringValue > intSecondStringValue)
					return false;

				// if dblFirstStringValue == dblFirstStringValue the loop will run for another iteration
			}
			
			// if reach here the shortest string is alphabetically identical to the longest one
			if (strFirst.Length < strSecond.Length)
				return true;
			else if (strFirst.Length > strSecond.Length)
				return false;

			// if reach here strings are identical in length, and alphabetically identical too
			// can return true of false - it's arbitrary at this stage, because there is no discernible difference
			// between strings
			return true;
		}



	}
}
