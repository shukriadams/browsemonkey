//////////////////////////////////////////////////////////////////////////////////////
// Author				: Shukri Adams												//
// Contact				: shukri.adams@gmail.com									//
//																					//
// vcFramework : A reuseable library of utility classes                             //
// Copyright (C)																	//
//////////////////////////////////////////////////////////////////////////////////////

using System;

namespace vcFramework.Delegates
{
	/// <summary> 
	/// Static class containing delegate "helper" methods etc
	/// </summary>
	public class DelegateLib
	{

		/// <summary> 
		/// Control onSendProgress event handling 
		/// </summary>
		static public void InvokeSubscribers(
			EventHandler objEvent, 
			object objCaller
			)
		{
			if (objEvent != null) // Make sure there are any subscribers
			{
				// Get the list of methods to call
				System.Delegate[] subscribers = objEvent.GetInvocationList();

				// Loop through the methods
				foreach (System.EventHandler target in subscribers)
					target(
						objCaller, 
						new EventArgs()); // Call the method
			}
		}


		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="objEvent"></param>
		/// <param name="objCaller"></param>
		/// <param name="e"></param>
		static public void InvokeSubscribers(
			EventHandler objEvent, 
			object objCaller,
			EventArgs e
			)
		{
			if (objEvent != null) // Make sure there are any subscribers
			{

				// Get the list of methods to call
				System.Delegate[] subscribers = objEvent.GetInvocationList();

				// Loop through the methods
				foreach (System.EventHandler target in subscribers)
					target(
						objCaller, 
						e); // Call the method

			}
		}
	

	}
}
