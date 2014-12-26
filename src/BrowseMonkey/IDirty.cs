///////////////////////////////////////////////////////////////
// BrowseMonkey - An offline filesystem browser              //
// Shukri Adams (shukri.adams@gmail.com)                     //
// https://github.com/shukriadams/browsemonkey               //
// MIT License (MIT) Copyright (c) 2014 Shukri Adams         //
///////////////////////////////////////////////////////////////

using System;

namespace vcFramework.Interfaces
{

	/// <summary>
	/// Defines interface requirements for a windows form which can holds any content 
	/// that can be saveable. The form is said to be "dirty" when containing unsaved data.
	/// Furthermore, the "Created" property, which is a standard Winform property, is 
	/// explicity explosed as it is convenient when trying to close Winforms which 
	/// implement this interface.
	/// </summary>
	public interface IDirty : IDisposable
	{
		
		/// <summary>
		/// 
		/// </summary>
		bool Dirty { get ; }


		/// <summary>
		/// 
		/// </summary>
		bool Created { get ;}

	}
}
