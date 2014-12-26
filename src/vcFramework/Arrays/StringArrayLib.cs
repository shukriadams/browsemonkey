//////////////////////////////////////////////////////////////////////////////////////
// Author				: Shukri Adams												//
// Contact				: shukri.adams@gmail.com									//
//																					//
// vcFramework : A reuseable library of utility classes                             //
// Copyright (C)																	//
//////////////////////////////////////////////////////////////////////////////////////

using System;
using vcFramework.Parsers;
using vcFramework.Text;

namespace vcFramework.Arrays
{
	/// <summary> Summary description for ArrayHelper. </summary>
	public class StringArrayLib
	{
		
		/// <summary> Compares to string arrays </summary>
		/// <param name="arrFirst"></param>
		/// <param name="arrSecond"></param>
		/// <returns></returns>
		public static bool AreEqual(
			string[] arrFirst, 
			string[] arrSecond, 
			bool blnIgnoreOrder
			)
		{
			// TODO ! blnIgnoreOrder is not emplemented yet

			// null test
			if (arrFirst == null && arrSecond == null)
				return true;

			// length test
			if (arrFirst.Length != arrSecond.Length)
				return false;
		
			// member test
			bool blnMemberTest = false;
			
			if (blnIgnoreOrder)
			{
				// in this test, only the contents of the two arrays
				// are compared, not the order. therefore each
				// item of one array will be compared with each item
				// of the other array, and only after if no match
				// for one can be found in another, will the test fail
				// Note that the blnMemberTest assignments are opposite
				// to those used in the !blnIgnoreOrder test further on.


				for (int i = 0 ; i < arrFirst.Length ; i ++)
				{

					// default - must be set for each i item
					blnMemberTest = true;

					for (int j = 0 ; j < arrSecond.Length ; j ++)
					{
						if (arrFirst[i] == arrSecond[j])
						{
							// if reach here, test passed for the i-j combo
							blnMemberTest= false;
							break;
						}
					}
					
					// tests the results of i-j combo test
					if (blnMemberTest)
						break;

				}
			}
			else
			{
				// in this test, the content and content order
				// of both arrays must be identical. if any
				// discrepency is found, the test is aborted
				// and the result is failure

				// default
				blnMemberTest = false;

				for (int i = 0 ; i < arrFirst.Length ; i ++)
				{
					if (arrFirst[i] != arrSecond[i])
					{
						// if reach here, test must fail - abort
						blnMemberTest= true;
						break;
					}
				}
			}
			
			if (blnMemberTest)
				return false;
			
			return true;
		}
	
		
			
		/// <summary>
		/// "Removes" all String.Empty and zero length strings from an array.
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string[] RemoveEmptyStrings(
			string[] input
			)
		{
			
			string[] output = null;
			int outputSize = 0;

			for (int i = 0 ; i < input.Length ; i ++)
				if (input[i] != String.Empty && input[i] != "")
					outputSize ++;


			output = new string[outputSize];

			int j = 0;

			for (int i = 0 ; i < input.Length ; i ++)
				if (input[i] != String.Empty && input[i] != "")
				{
					output[j] = input[i];
					j++;
				}

			return output;

		}



		/// <summary> Returns a string array's contents as a 
		/// single concatenated string </summary>
		/// <param name="arrStrItems"></param>
		/// <returns></returns>
		public static string ConcatStringArrayContents(
			string[] arrStrItems, 
			string strDelimiter
			)
		{
			string strOutput = "";
			
			if (arrStrItems.Length == 0)
				return "";

			for (int i = 0 ; i < arrStrItems.Length ; i ++)
				strOutput += arrStrItems[i] + strDelimiter;

			// removes last delimiter from string
			if (strOutput.Length > strDelimiter.Length)
				strOutput = ParserLib.ClipFromEnd(
					strOutput,
					strDelimiter.Length
					);

			return strOutput;
		}


		
		/// <summary> Returns true if the string array contains 
		/// the specified string </summary>
		/// <param name="arrStrHolder"></param>
		/// <param name="strItem"></param>
		/// <returns></returns>
		public static bool Contains(
			string[] arrStrHolder,
			string strItem
			)
		{
			if (arrStrHolder == null)
				return false;

			for (int i = 0 ; i < arrStrHolder.Length ; i ++)
			{
				if (arrStrHolder[i] == strItem)
					return true;
			}

			return false;
		}



		/// <summary> Adds a new value to a string array - can 
		/// be inserted in either top or bottom of array </summary>
		/// <param name="arrStrStrings"></param>
		/// <param name="strNewValue"></param>
		/// <param name="arrayPosition"></param>
		/// <returns></returns>
		public static string[] AppendString(
			string[] arrStrStrings,
			string strNewValue,
			ArrayPositions arrayPosition
			)
		{
			string[] arrStrTemp = null;
			int intOffset = 0;
		
			// creates new temporary array, and transfers data from existing array to temp array if necessary
			if (arrStrStrings == null)
				arrStrTemp = new string[1];
			else
			{
				arrStrTemp = new string[arrStrStrings.Length + 1];
				
				// if new value will be inserted at top of array, need to shift all existing 
				// values down one. use offset for this
				if (arrayPosition == ArrayPositions.Top)
					intOffset = 1;

				// copy array contents
				for (int i = 0 ; i < arrStrStrings.Length ; i ++)
					arrStrTemp[intOffset + i] = arrStrStrings[i];
			}
			
			// add new string value at correct position
			if (arrayPosition == ArrayPositions.Top)
				arrStrTemp[0] = strNewValue;
			if (arrayPosition == ArrayPositions.Bottom)
				arrStrTemp[arrStrTemp.Length - 1] = strNewValue;

			// return temp array
			return arrStrTemp;

		}

		

		/// <summary> Implements bubble sort on string array </summary>
		/// <param name="arrStr"></param>
		/// <returns></returns>
		public static string[] SortStringArray(
			string[] arrStr,
			bool blnAscending
			)
		{
			
			string strTempStringHolder = "";
			

			for (int i = 0 ; i < arrStr.Length ; i ++)
			{
				for (int j = 0  ; j < arrStr.Length ; j ++)
				{

					if (blnAscending)
					{

						if (StringSortLib.IsAfter(arrStr[i], arrStr[j], true))
						{
							strTempStringHolder = arrStr[j];
							arrStr[j] = arrStr[i];
							arrStr[i] = strTempStringHolder;
						}

						
					}
					else
					{

						if (StringSortLib.IsAfter(arrStr[j], arrStr[i], true))
						{
							strTempStringHolder = arrStr[j];
							arrStr[j] = arrStr[i];
							arrStr[i] = strTempStringHolder;
						}

					}
				}
			}


			


			return arrStr;

		}


	}
}
