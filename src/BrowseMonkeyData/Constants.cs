///////////////////////////////////////////////////////////////
// BrowseMonkeyData - A class library for the data file      // 
// format of BrowseMonkey.                                   //
// Shukri Adams (shukri.adams@gmail.com)                     //
// https://github.com/shukriadams/browsemonkey               //
// MIT License (MIT) Copyright (c) 2014 Shukri Adams         //
///////////////////////////////////////////////////////////////

namespace BrowseMonkeyData
{
	/// <summary>
	/// Summary description for Constants.
	/// </summary>
	public class Constants
	{

		/// <summary>
		/// Single point reference for the latest volume version. This is the version all currently generated
		/// volumes will acquire. This reference MUST be updated each time a new volume version is created
		/// </summary>
		public static string CurrentVolumeVersion = VolumeVersions.v102.ToString();
		
		/// <summary>
		/// NEVER change this value. This GUID is written to all volumes from version 1.02 on, as a way
		/// to use the raw binary contents of the file to determine if it is a volume or not.
		/// </summary>
		public static string UniversalVolumeIdenfifier = "{6096a1f8-f0fa-499f-b6f4-46dd36c70b28}";

		/// <summary>
		/// This is a constant, and should never be changed. Changing it will render
		/// previous Volume versions inuseable. Note that the choice of char for this
		/// flag is based on its Xml incompatibility. The hash symbol cannot be used
		/// raw in Xml. Therefore, there is no chance of mistakenly reading a char in
		/// the Index section of of the Volume file as the divider flag. The divider
		/// flag is thus the first occurence of the char below in the Volume file.
		/// </summary>
		public static string DIVIDER_FLAG = "#";
	}
}
