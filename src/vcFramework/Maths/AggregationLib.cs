//////////////////////////////////////////////////////////////////////////////////////
// Author				: Shukri Adams												//
// Contact				: shukri.adams@gmail.com									//
//																					//
// vcFramework : A reuseable library of utility classes                             //
// Copyright (C)																	//
//////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections;

namespace vcFramework.Maths
{

	public class AggregationLib
	{

		/// <summary> </summary>
		/// <param name="arrListNumbers"></param>
		/// <returns></returns>
		public static double Average(
			ArrayList arrListNumbers
			)
		{
    		double dblOutput;
			double dlbarrListNumbersCount;

			//GETS TOTAL OF ARRLISTNUMBERS
			dblOutput = Sum(arrListNumbers);
			dlbarrListNumbersCount = arrListNumbers.Count ;

			//CALCS AVERAGE
			dblOutput = dblOutput / dlbarrListNumbersCount;


			return dblOutput;
		}



		/// <summary> </summary>
		/// <param name="arrNumbers"></param>
		/// <returns></returns>
		public static double Average(
			double[] arrNumbers
			)
		{
			double dblOutput = 0;
			double arrNumbersCount = 0;

			//GETS TOTAL OF ARRLISTNUMBERS
			dblOutput = Sum(arrNumbers);
			arrNumbersCount = arrNumbers.Length;

			//CALCS AVERAGE
			dblOutput = dblOutput / arrNumbersCount ;


			return dblOutput;
		}		



		/// <summary></summary>
		/// <param name="arrListNumbers"></param>
		/// <returns></returns>
		public static double Sum(
			ArrayList arrListNumbers
			)
		{
			int i;
			string strTest = "";
			double dblOutput = 0;
			
			for (i = 0; i < arrListNumbers.Count ; i ++)
			{
				strTest = Convert.ToString(arrListNumbers[i]);
				if (Convert.ToString(arrListNumbers[i]) != "NaN")
				{
						
					dblOutput += Convert.ToDouble(arrListNumbers[i]);
				}
			}			

			return dblOutput;
		}



		/// <summary> </summary>
		/// <param name="arrIntNumbers"></param>
		/// <returns></returns>
		public static int Sum(
			int[] arrIntNumbers
			)
		{
			int intOutput = 0;
			
			for (int i = 0; i < arrIntNumbers.Length ; i ++)
			{
				intOutput += arrIntNumbers[i];
			}
			return intOutput;
		}



		/// <summary></summary>
		/// <param name="arrNumbers"></param>
		/// <returns></returns>
		public static double Sum(
			double[] arrNumbers
			)
		{
			int i;
			double dblOutput = 0;
			

			for (i = 0; i < arrNumbers.Length ; i ++)
				if (Convert.ToString(arrNumbers[i]) != "NaN")
					dblOutput += Convert.ToDouble(arrNumbers[i]);

			return dblOutput;
		}






	}
}
