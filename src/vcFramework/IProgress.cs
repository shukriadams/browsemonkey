//////////////////////////////////////////////////////////////////////////////////////
// Author				: Shukri Adams												//
// Contact				: shukri.adams@gmail.com									//
//																					//
// vcFramework : A reuseable library of utility classes                             //
// Copyright (C)																	//
////////////////////////////////////////////////////////////////////////////////////// 

using System;

namespace vcFramework
{
	/// <summary> Interface for objects which with trackable progress. These are 
	/// typically objects which may be used to process a large amount of data, and 
	/// require an external progress bar indicator. The simple events and property 
	/// listed below make it easy to track progress. </summary>
	public interface IProgress
	{

		/// <summary> 
		/// Gets the number of steps this object will take to complete its 
		/// task 
		/// </summary>
		long Steps { get ;}
		
		/// <summary> 
		/// Gets teh current step 
		/// </summary>
		long CurrentStep { get ;}

		/// <summary> 
		/// Gets if progressible process is running 
		/// </summary>
		bool Running { get ; }

		/// <summary> 
		/// Fires when the object "steps forward" one Step 
		/// </summary>
		event EventHandler OnNext;

		/// <summary> 
		/// Fires when the object finishes doing what it's supposed to be 
		/// doing. Should also be fired if something goes wrong and the object 
		/// terminates whatever its doing 
		/// </summary>
		event EventHandler OnEnd;

		/// <summary> 
		/// Starts progressible process 
		/// </summary>
	//	void Start();

		/// <summary> 
		/// Stops progressible process regardless of where it is
		/// </summary>
	//	void Stop();


	}
}
