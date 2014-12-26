//////////////////////////////////////////////////////////////////////////////////////
// Author				: Shukri Adams												//
// Contact				: shukri.adams@gmail.com									//
//																					//
// vcFramework : A reuseable library of utility classes                             //
// Copyright (C)																	//
//////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace vcFramework.RandomItems
{
	public class RandomLib
	{
        /// <summary>
        /// 
        /// </summary>
        private static int _seed;

        /// <summary>
        /// Gets a random, unique range of numbers 
        /// </summary>
        /// <param name="size"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static IEnumerable<int> GetRange(int size, int min, int max)
        {
            List<int> items = new List<int>();
            while (items.Count < size)
            {
                Random r = new Random((int)RandomLib.RandomNumber(5));
                int v = r.Next(min, max);
                if (!items.Contains(v))
                    items.Add(v);
            }
            return items;
        }

		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="length"></param>
		/// <returns></returns>
		public static string RandomString(
			int length
			)
		{
			// create strong hash code
			byte[] random = new byte[length];
			RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
			
			// populate with random bytes
			rng.GetBytes(random);

			// convert random bytes to string
            string randomBase64 = Convert.ToBase64String(random);
            
            // todo : fix this ugly hack workaround to remove + and /
            randomBase64 = randomBase64.Replace("+", "a");
            randomBase64 = randomBase64.Replace("/", "b");
            if (randomBase64.Length > length)
                randomBase64 = randomBase64.Substring(0, length);
			return randomBase64;
		}


		/// <summary> </summary>
        /// <param name="randromStringLength"></param>
		/// <returns></returns>
		static private string hiding_RandomString(
			int randromStringLength
			)
		{
			int intUpperlimit;
			int intLowerlimit;
			int intTempNumber;
			string strResult = "";
			Random objRandom = null;
			

			//EXITS FUNCTION IF KEYLENGTH IS ZERO
            if (randromStringLength == 0)
				return "";

			objRandom = new Random(_seed);

			// resets seed - prevents exceeding in32 range
			if (_seed < 30000)
				_seed = objRandom.Next(_seed, _seed + 1000);
			else
				_seed = 0;


            while (strResult.Length < randromStringLength)
			{
				intUpperlimit = 122;
				intLowerlimit = 48;

				//Randomize
				intTempNumber = objRandom.Next(intLowerlimit, intUpperlimit);

				if ((intTempNumber > 47 && intTempNumber < 58) || (intTempNumber > 64 && intTempNumber < 91) || (intTempNumber > 96 && intTempNumber < 123))
					strResult = strResult + Convert.ToChar(intTempNumber);
			}



			return strResult;

		}


		/// <summary> </summary>
        /// <param name="length"></param>
		/// <returns></returns>
		static public long RandomNumber(
			int length
			)
		{
			string ouput = string.Empty;

			Random objRandom =new Random(_seed);
				
			// resets seed - prevents exceeding in32 range
			if (_seed < 30000)
				_seed = objRandom.Next(_seed, _seed + 1000);
			else
				_seed = 0;


			int MaxLimit = 9;
			int MinLimit = 0;

            while (ouput.Length < length)
                ouput += objRandom.Next(MinLimit, MaxLimit).ToString();

            return Convert.ToInt64(ouput);

		}
		
	}
}
