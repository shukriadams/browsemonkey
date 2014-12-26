//////////////////////////////////////////////////////////////////////////////////////
// Author				: Shukri Adams												//
// Contact				: shukri.adams@gmail.com									//
//																					//
// vcFramework : A reuseable library of utility classes                             //
// Copyright (C)																	//
//////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace vcFramework.Arrays
{
	/// <summary>
	/// Summary description for ArrayLib.
	/// </summary>
	public class ArrayLib
	{

		/// <summary>
		/// Copies an array's contents to an arraylist
		/// </summary>
		/// <param name="array"></param>
		/// <returns></returns>
		public static ArrayList ToArrayList(
			Array array
			)
		{
			ArrayList list = new ArrayList();

			foreach(object o in array)
				list.Add(o);

			return list;

		}

        /// <summary> 
        /// Dumps the contents of a char array into a string
        /// </summary>
        /// <returns></returns>
        public static string CharArrayToString(
            char[] chars
            )
        {
            StringBuilder output = new StringBuilder();

            for (int i = 0; i < chars.Length; i++)
                output.Append(chars[i]);

            return output.ToString();
        }

        public static void testc<T>(IEnumerable<T> test)
        {

        }

        /// <summary>
        /// Returns the index in a collection where a given value occurs. Returns -1 if the collection
        /// does not contain the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int IndexAtValue<T>(IEnumerable<T> array, object value)
        {
            for (int i = 0 ; i < array.Count() ; i ++)
            {
                T v = array.ElementAt(i);
                if ((v == null && value == null) || (v != null && v.Equals(value)))
                    return i;
            }

            return -1;
        }
	}
}
