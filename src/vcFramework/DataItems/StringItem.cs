//////////////////////////////////////////////////////////////////////////////////////
// Author				: Shukri Adams												//
// Contact				: shukri.adams@gmail.com									//
//																					//
// vcFramework : A reuseable library of utility classes                             //
// Copyright (C)																	//
//////////////////////////////////////////////////////////////////////////////////////

using System;

namespace vcFramework.DataItems
{
	/// <summary> 
    /// Struct for making simple data items. A data item has a name and a 
	/// value, and can be used to populate controls like listviews 
    /// </summary>
	[Serializable]
    public class StringItem
	{
        public string Name {get;set;}
        public string Value {get;set;}

        public StringItem()
        {
            this.Name = string.Empty;
            this.Value = string.Empty;
        }

        public StringItem(string value, string name)
        {
            this.Name = name;
            this.Value = value;
        }

	}
}
