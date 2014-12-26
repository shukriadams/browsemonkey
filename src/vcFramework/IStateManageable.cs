//////////////////////////////////////////////////////////////////////////////////////
// Author				: Shukri Adams												//
// Contact				: shukri.adams@gmail.com									//
//																					//
// vcFramework : A reuseable library of utility classes                             //
// Copyright (C)																	//
//////////////////////////////////////////////////////////////////////////////////////

using System.Xml;

namespace vcFramework
{
	/// <summary> Summary description for IStateManageable. </summary>
	public interface IStateManageable
	{
		/// <summary> Returns an xml node containing the state of the object</summary>
		/// <returns></returns>
		XmlNode GetState();

		/// <summary> Sets the state of the object using the data in teh xmlnode</summary>
		/// <param name="nXmlState"></param>
		void SetState(XmlNode nXmlState);
	}
}
