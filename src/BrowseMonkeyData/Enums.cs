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
	/// Exceptions assocaited with Volume opening/saving etc 
	/// </summary>
	public enum VolumeExceptionTypes : int	
	{
		FileCannotBeOpened,
		BadlyFormedXml,
		XmlSchemaError,
		UnknownError,
		SaveError
	}


	/// <summary>
	/// List of all volume versions past and present
	/// </summary>
	public enum VolumeVersions
	{
		Invalid,
		v101,
		v102
	}

}
