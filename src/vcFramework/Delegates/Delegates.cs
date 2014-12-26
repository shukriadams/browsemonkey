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
    /// Used to pass messages across threads etc
    /// </summary>
    /// <param name="message"></param>
    public delegate void WriteMessageDelegate(string message);		

	/// <summary> 
	/// Delegate used to invoke download managing process  
	/// </summary>
	public delegate void DownloadActionDelegate();		

	/// <summary> 
	/// Delegate used to invoke void methods with no arugments. this is mainly used for manipulating
	/// Windform Control objects that do not behave will when called from foreign threads 
	/// </summary>
	public delegate void WinFormActionDelegate();

	/// <summary> 
	/// 
	/// </summary>
	public delegate void ProgressDialogueInvokeDelegate(int intSteps);

	/// <summary> 
	/// 
	/// </summary>
	public delegate void ErrorHandlerDelegate(
		string strClass, 
		string strMethod, 
		Exception e);

	/// <summary> 
	/// Delegate used to invoke upload managing process  
	/// </summary>
	public delegate void UploadActionDelegate(
		string strClientHost, 
		string strJobCode, 
		string strFullPath);

}
