//////////////////////////////////////////////////////////////////////////////////////
// Author				: Shukri Adams												//
// Contact				: shukri.adams@gmail.com									//
//																					//
// vcFramework : A reuseable library of utility classes                             //
// Copyright (C)																	//
//////////////////////////////////////////////////////////////////////////////////////

using vcFramework.ErrorHandlers;
using vcFramework.Parsers;

namespace vcFramework.DataItems
{
	/// <summary> Struct for byte data </summary>
	public struct ByteSizeItem
	{

		#region MEMBERS

		public enum SizeUnits : int	
		{
			By,
			Kb,
			Mb,
			Gb,
			Tb
		}
		
		public long Bytes;
		public string BytesParsed;
		public SizeUnits SizeUnit;
		
		#endregion


		#region CONSTRUCTORS
		

		public ByteSizeItem(long lngBytes)
		{
			// handles impossible byte sizes
			if (lngBytes < 0)
				throw new ByteSizeException(ByteSizeException.ExceptionTypes.InvalidByteSize, "Size " + lngBytes.ToString() + " is negative and therefore an invalid size");
			else if(lngBytes >=1099511627776)
				throw new ByteSizeException(ByteSizeException.ExceptionTypes.InvalidByteSize, "Size " + lngBytes.ToString() + " (terabyte order) is too large to be supported by this struct.");

			// stores member value
			Bytes = lngBytes;

			// have to force set a value for the SizeUnit member outside of an if statement
			SizeUnit = SizeUnits.By;
			
			// determine actual value of SizeUnit
			if (lngBytes >= 0 && lngBytes < 1012)
				SizeUnit = SizeUnits.By;
			else if (lngBytes >= 1012 && lngBytes < 1048576)
				SizeUnit = SizeUnits.Kb;
			else if (lngBytes >= 1048576 && lngBytes < 1073741824)
				SizeUnit = SizeUnits.Mb;
			else if (lngBytes >= 1073741824 && lngBytes < 1099511627776)
				SizeUnit = SizeUnits.Gb;
	//		else if (lngBytes >= 1099511627776 && lngBytes < 1125899906842624)
	//			SizeUnit = SizeUnits.Tb;
	// TODO . add terabyte support


			double dblByesUnrounded = 0;
			double dblByes = 0;


			if (SizeUnit == SizeUnits.By)
			{
				dblByesUnrounded = Bytes;
			}
			else if(SizeUnit == SizeUnits.Kb)
			{
				dblByes = (Bytes/(1024));
				dblByesUnrounded = Bytes%(1024);
			}
			else if(SizeUnit == SizeUnits.Mb)
			{
				dblByes = (Bytes/(1024*1024));
				dblByesUnrounded = Bytes%(1024*1024);
			}
			else if(SizeUnit == SizeUnits.Gb)
			{
				dblByes = (Bytes/(1024*1024*1024));
				dblByesUnrounded = Bytes%(1024*1024*1024);
			}

			// REDFLAG - still not accurate!
			BytesParsed = dblByes.ToString() + "." + ParserLib.ReturnMaxChars(dblByesUnrounded.ToString(), 2) ;//+ "." + Math.Round(Convert.ToDouble(lngBytes%1024), 2);


		}

		#endregion

	}
}
