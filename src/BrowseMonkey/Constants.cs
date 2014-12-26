///////////////////////////////////////////////////////////////
// BrowseMonkey - An offline filesystem browser              //
// Shukri Adams (shukri.adams@gmail.com)                     //
// https://github.com/shukriadams/browsemonkey               //
// MIT License (MIT) Copyright (c) 2014 Shukri Adams         //
///////////////////////////////////////////////////////////////

namespace BrowseMonkey
{
	/// <summary>
	/// Application-wide constants
	/// </summary>
	public class Constants
	{

		/// <summary> 
		/// Default extension of BrowseMonkey files. 
		/// </summary>
		public const string VOLUME_FILE_EXTENSION = "bmi";

		/// <summary> 
		/// Name of folder in application path where all config , state and other internal-use 
		/// Xml files are kept 
		/// </summary>
		public const string INTERNAL_DATA_FOLDER = "cfg";

		/// <summary>
		/// 
		/// </summary>
		public const string FILE_DIALOGUE_FILTER = "BrowseMonkey files (*." + Constants.VOLUME_FILE_EXTENSION + ")|*." + Constants.VOLUME_FILE_EXTENSION + "|All files (*.*)|*.*";
	
		/// <summary> 
		/// The string name of file where docking pesistence 
		/// info will be stored.
		/// </summary>
		public const string DOCK_MANAGER_PERSISTENCE_FILE = "dockingPers.dat";

		/// <summary>
		/// The maximum number of recent files show in "Recent Files" menu
		/// </summary>
		public const int MAX_RECENT_FILES = 20;
		
		/// <summary>
		/// The maximum number of recently searched folders
		/// </summary>
		public const int MAX_RECENT_SEARCH_FOLDERS = 10;

	}
}
