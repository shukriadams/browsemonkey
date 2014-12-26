///////////////////////////////////////////////////////////////
// BrowseMonkey - An offline filesystem browser              //
// Shukri Adams (shukri.adams@gmail.com)                     //
// https://github.com/shukriadams/browsemonkey               //
// MIT License (MIT) Copyright (c) 2014 Shukri Adams         //
///////////////////////////////////////////////////////////////

namespace BrowseMonkey
{
	/// <summary>
	/// Empty interface used to mark certain form types in the application. A "spawned"
	/// form is any form of which there can be numerous concurrent instances, usually
	/// with each instance containing unique information. This will be forms such as 
	/// volumebrowsers, search results, text dumps etc. The "opposite" of an ISpawned
	/// form is an IToggled form, of which there is only ever one instance at a time.
	/// 
	/// Forms are marked to make it easier for the application to manage them via 
	/// generic methods which group forms together into categories.
	/// </summary>
	public interface ISpawned
	{

	}
}
