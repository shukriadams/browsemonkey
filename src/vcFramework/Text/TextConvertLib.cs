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
	/// <summary> Summary description for TextConvertLib.</summary>
	[Obsolete]
    public class TextConvertLib
	{
		/// <summary> Dumps the contents of a char array into a string</summary>
		/// <returns></returns>
		public static string CharArrayToString(char[] arrCharInput)
		{
            return new string(arrCharInput);
		}

	}
}
