//////////////////////////////////////////////////////////////////////////////////////
// Author				: Shukri Adams												//
// Contact				: shukri.adams@gmail.com									//
//																					//
// vcFramework : A reuseable library of utility classes                             //
// Copyright (C)																	//
//////////////////////////////////////////////////////////////////////////////////////

using System;
using System.IO;
using System.Reflection;
using vcFramework.Parsers;

namespace vcFramework.Assemblies
{
	/// <summary>
	/// Summary description for AssemblyLib.
	/// </summary>
	public class AssemblyLib
	{

		/// <summary> Gets the last writedate of a given assembly </summary>
		/// <param name="assembly"></param>
		/// <returns></returns>
		public static DateTime AssemblyLastWriteDate(
			Assembly assembly
			)
		{

			return File.GetLastWriteTime(
				assembly.CodeBase.Substring("file:///".Length)
				);
		
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="assembly"></param>
		/// <returns></returns>
		public static string GetAssemblyRootName(
			Assembly assembly
			)
		{

				return ParserLib.ReturnUpto(
					assembly.FullName,
					","
					);

		}
	}
}
