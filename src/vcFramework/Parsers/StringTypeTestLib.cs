//////////////////////////////////////////////////////////////////////////////////////
// Author				: Shukri Adams												//
// Contact				: shukri.adams@gmail.com									//
//																					//
// vcFramework : A reuseable library of utility classes                             //
// Copyright (C)																	//
//////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Net;
using System.Text.RegularExpressions;

namespace vcFramework.Parsers
{
	public class StringTypeTestLib
	{
		
		/// <summary> 
		/// Returns true if string is a valid date 
		/// </summary>
        /// <param name="test"></param>
		/// <returns></returns>
		public static bool IsDate(string test)
		{
			try
			{
                DateTime.Parse(test);
                return true;
			}
			catch(Exception)
			{
				return false;
			}
		}


		/// <summary> 
		/// Returns true if string is an int 
		/// </summary>
        /// <param name="test"></param>
		/// <returns></returns>
		public static bool IsInt(string test)
		{
			try
			{
                Convert.ToInt32(test);
                return true;
			}
			catch(Exception)
			{
				return false;
			}
		}

		
		/// <summary>
		/// Returns true of the given string is a valid ip number
		/// </summary>
		/// <param name="test"></param>
		/// <returns></returns>
		public static bool IsValidIPNumber(string test)
		{
			try
			{
				IPAddress.Parse(test);
				return true;
			}
			catch
			{
				return false;
			}
			
		}
        

		/// <summary> 
		/// Returns true if string is a valid datetime 
		/// </summary>
        /// <param name="test"></param>
		/// <returns></returns>
		public static bool IsDateTime(string test
			)
		{
			try
			{
                Convert.ToDateTime(test);
				return true;
			}
			catch(Exception)
			{
				return false;
			}
		}

        
        /// <summary>
        /// Returns true if text contains only white space( space or tabs)
        /// </summary>
        /// <param name="test"></param>
		/// <returns></returns>
		public static bool IsWhiteSpace(
            string test
			)
		{
            for (int i = 0; i < test.Length; i++)
			{
                if ((test.Substring(i, 1) != " ") && (test.Substring(i, 1) != "\t"))
					return false;	
			}

			// zero length check
            return test.Length != 0;
		}
        

		/// <summary> 
        /// Returns true if text is made entirely of alphanumeric characters 
        /// </summary>
        /// <param name="test"></param>
		/// <returns></returns>
		public static bool IsAlphanumeric(
			string test
			)
		{
			// zero length check
            if (test.Length == 0)
				return false;

            Regex reg = new Regex(@"^[a-zA-Z0-9]*$");

            return reg.IsMatch(test);
		}

		

		/// <summary> 
        /// Returns true if a string consists of only
		/// alphabetica characters 
        /// </summary>
        /// <param name="test"></param>
		/// <returns></returns>
		public static bool IsAlphabetic(string test)
		{
            // zero length check
            if (test.Length == 0)
                return false;

			Regex reg = new Regex(@"^[a-zA-Z]*$");
            return reg.IsMatch(test);
		}


		/// <summary> 
		/// Returns true if text contains only non-alphanumeric chars (non-letters, non-number or non-underscores) - 
		/// will not return true for white space 
		/// </summary>
        /// <param name="test"></param>
		/// <returns></returns>
		public static bool IsNonAlpaNumericAndNotWhiteSpace(string test)
		{
			Regex reg = new Regex(@"\W");
            for (int i = 0; i < test.Length; i++)
			{
                if ((reg.IsMatch(test.Substring(i, 1)) == false) || (StringTypeTestLib.IsWhiteSpace(test.Substring(i, 1))))
				{
					return false;
				}
			}

			// zero length check
            if (test.Length == 0)
				return false;
			return true;
		}



	}
}
