//////////////////////////////////////////////////////////////////////////////////////
// Author				: Shukri Adams												//
// Contact				: shukri.adams@gmail.com									//
//																					//
// vcFramework : A reuseable library of utility classes                             //
// Copyright (C)																	//
//////////////////////////////////////////////////////////////////////////////////////

namespace vcFramework.Arrays
{
	/// <summary>  Static class for working with byte arrays </summary>
	public class ByteArrayLib
	{


		/// <summary> </summary>
		/// <param name="arrFirst"></param>
		/// <param name="arrSecond"></param>
		/// <param name="blnIgnoreOrder"></param>
		/// <returns></returns>
		public static bool AreEqual(
			byte[] arrFirst, 
			byte[] arrSecond, 
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
						if (arrFirst[i] == arrSecond[j])
						{
							// if reach here, test passed for the i-j combo
							blnMemberTest= false;
							break;
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
					if (arrFirst[i] != arrSecond[i])
						return false;

				return true;

			}
			
			if (blnMemberTest)
				return false;
			
			return true;

		}


	}
}
