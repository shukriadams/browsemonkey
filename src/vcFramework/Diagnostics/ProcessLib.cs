//////////////////////////////////////////////////////////////////////////////////////
// Author				: Shukri Adams												//
// Contact				: shukri.adams@gmail.com									//
//																					//
// vcFramework : A reuseable library of utility classes                             //
// Copyright (C)																	//
//////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Reflection;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace vcFramework.Diagnostics
{
	/// <summary> Static class for diagnostics methods </summary>
	public class ProcessLib
	{

		/// <summary> </summary>
		/// <returns></returns>
		public static Process GetOtherRunningProcessOfCurrentApplication(
			Process current
			)
		{ 
			//Process current = Process.GetCurrentProcess(); 
			Process[] processes = Process.GetProcessesByName (current.ProcessName); 

			//Loop through the running processes in with the same name 
			foreach (Process process in processes) 
			{ 
				//Ignore the current process 
				if (process.Id != current.Id) 
				{ 

					//Make sure that the process is running from the exe file. 
					
					//Return the other process instance.  
					if (Assembly.GetCallingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName.Replace("/", "\\")) 
						return process; 
 
				}  
			}
 
			//No other instance was found, return null.  
			return null;  
		}

		

		/// <summary> Returns true if another instance
		/// of application is running </summary>
		/// <returns></returns>
		public static bool AnotherInstanceOfCurrentApplicationIsRunning(
			)
		{
			Mutex objMutex = null;

			try
			{
                objMutex = new Mutex(true, Application.ProductName);

				// if true, another app instance is running
				if (!objMutex.WaitOne(0,false))
					return true;
				return false;
			}
			finally
			{
				// this is apparently important (http://www.ai.uga.edu/~mc/SingleInstance.html)
				// so dont omit it
				GC.KeepAlive(objMutex);
			}
		}



	}
}
