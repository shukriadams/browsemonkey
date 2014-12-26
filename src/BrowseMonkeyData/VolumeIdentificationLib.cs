///////////////////////////////////////////////////////////////
// BrowseMonkeyData - A class library for the data file      // 
// format of BrowseMonkey.                                   //
// Shukri Adams (shukri.adams@gmail.com)                     //
// https://github.com/shukriadams/browsemonkey               //
// MIT License (MIT) Copyright (c) 2014 Shukri Adams         //
///////////////////////////////////////////////////////////////

using System;
using System.IO;
using System.Text;
using vcFramework.Arrays;
using vcFramework.IO.Streams;
using vcFramework.Text;

namespace BrowseMonkeyData
{
	/// <summary>
	/// Static class containing methods for identifying volume versions, validity of 
	/// binary files, etc.
	/// </summary>
	public class VolumeIdentificationLib
	{


		/// <summary>
		/// Gets the version of the given file. The version is returned as a string to make
		/// it easier for a given version of the application to read past AND future volume
		/// versions. We dont return an enumerator value because an old app will not contain
		/// enum members for later volume versions. Note that when encountering an invalid
		/// volume this method returns "Invalid", which is also a member of the 
		/// VolumeVersions enumerator
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		public static string GetVolumeVersion(
			string file
			)
		{
			
			FileStream fileStream = null;

			try
			{
			
				
				Decoder decoder = Encoding.Default.GetDecoder(); 


				// 1) Check if file exists
				if (file.Length == 0 || !File.Exists(file))
					return VolumeVersions.Invalid.ToString();


				fileStream = new FileStream(
					file, 
					FileMode.Open);
			
				byte[] startTag = StreamsLib.StringToByteArray("<v>");
				byte[] endTag = StreamsLib.StringToByteArray("</v>");

				// gets position of divider flag in volume file
				int startPosition = (int)StreamsLib.ByteArrayPositionInStream(
					fileStream,	
					startTag);

				int endPosition  = (int)StreamsLib.ByteArrayPositionInStream(
					fileStream,	
					endTag);
			
				if (startPosition == -1 || endPosition == -1 || startPosition >= endPosition)
					return VolumeVersions.Invalid.ToString();
			
				byte[] bytes = new byte[endPosition - startPosition - 3];	// 3 for "<v>" length
				char[] chars = new char[endPosition - startPosition - 3];	// 3 for "<v>" length
			
				// set stream position
				fileStream.Seek(
					startPosition + 3,	//3 for "<v>" length
					SeekOrigin.Begin);
			
				// red from stream
				fileStream.Read(
					bytes,
					0,
					bytes.Length);	

				// convert byte array to string
				decoder.GetChars(
					bytes,
					0, 
					endPosition - startPosition - 3,	// 3 for "<v>" length
					chars, 
					0);

				// writes chars to string
				return TextConvertLib.CharArrayToString(chars);
					
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Flush();
					fileStream.Close();
				}
			}

		}

		

		/// <summary>
		/// Returns true of the given file is a volume. All volume versions are supported
		/// by this method. 
		/// </summary>
		/// <returns></returns>
		public static bool FileIsVolume(
			string file
			)
		{
			
			FileStream volume = null;

			try
			{
				// ###########################################################
				// There are two tests that need to be done. The first is for 
				// version 1.01, which is messy, and the other test is good 
				// for all subsequent versions.
				// -----------------------------------------------------------
				try
				{
					volume = new FileStream(file,FileMode.Open, FileAccess.Read);
				}
				catch
				{
					return false;
				}


				// ###########################################################
				// vers 1.01 didnt really have a "UniversalVolumeIdenfifier"
				// embedded in it, so we need to use a messy way of id'ing it.
				// We assume that the starting text of a 1.01 volume is
				// "<i><!-- volume name (alias) -->"
				// -----------------------------------------------------------
				byte[] flag = StreamsLib.StringToByteArray("<i><!-- volume name (alias) -->");
				byte[] read = new byte[flag.Length];
                volume.Read(read,0, flag.Length);
				if (ByteArrayLib.AreEqual(flag, read, false))
					return true;


				// ###########################################################
				// if reach here, the 101 version test failed. Do post-101 test
				// -----------------------------------------------------------
				flag = StreamsLib.StringToByteArray("<i><!-- " + Constants.UniversalVolumeIdenfifier + " -->");
				read = new byte[flag.Length];
				volume.Seek(0, SeekOrigin.Begin);
				volume.Read(read,0, flag.Length);
				if (ByteArrayLib.AreEqual(flag, read, false))
					return true;


				return false;

			}
			finally
			{
				if (volume != null)
					volume.Close();
			}
		}

		

		/// <summary>
		/// Returns true of the given volume version can be upgraded by this version of the
		/// Data code.
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		public static bool FileIsConvertable(
			string file
			)
		{

			string version = GetVolumeVersion(file);

			// note - if volume version is the same as the current supported
			// version, converiting code for it does NOT exist, therefore 
			// return false
			if (version == Constants.CurrentVolumeVersion)
				return false;
			
			// "special case" for 1.01
			if (version == "1.01")
				return true;

			if (Enum.IsDefined(typeof(VolumeVersions), version))
				return true;
			
			return false;

		}

	}
}
