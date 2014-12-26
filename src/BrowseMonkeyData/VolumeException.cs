///////////////////////////////////////////////////////////////
// BrowseMonkeyData - A class library for the data file      // 
// format of BrowseMonkey.                                   //
// Shukri Adams (shukri.adams@gmail.com)                     //
// https://github.com/shukriadams/browsemonkey               //
// MIT License (MIT) Copyright (c) 2014 Shukri Adams         //
///////////////////////////////////////////////////////////////

using System;

namespace BrowseMonkeyData
{
	/// <summary> </summary>
	public class VolumeException : System.Exception
	{

		#region FIELDS
		
		/// <summary>
		/// 
		/// </summary>
		private VolumeExceptionTypes _volumeExceptionType;
		
		/// <summary>
		/// 
		/// </summary>
		private string _message;

		/// <summary>
		/// The original exception raised, which this volume exception
		/// is then explicityl thrown in response to. Included to make
		/// exception reporting in debug mode easier.
		/// </summary>
		private Exception _underlyingException;

		#endregion


		#region PROPERTIES
			
		/// <summary>
		/// 
		/// </summary>
		public string Message
		{
			get
			{
				return _message;
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		public VolumeExceptionTypes VolumeExceptionType
		{
			get
			{
				return _volumeExceptionType;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		public Exception UnderlingException
		{
			get
			{
				return _underlyingException;
			}
		}

		#endregion


		#region CONSTRUCTORS

		/// <summary>
		/// 
		/// </summary>
		/// <param name="volumeExceptionType"></param>
		/// <param name="message"></param>
		public VolumeException(
			VolumeExceptionTypes volumeExceptionType,
			Exception underlyingException,
			string message
			)
		{
			_underlyingException = underlyingException;
			_volumeExceptionType = volumeExceptionType;
			_message = message;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="volumeExceptionType"></param>
		/// <param name="message"></param>
		public VolumeException(
			VolumeExceptionTypes volumeExceptionType,
			string message
			)
		{
			_volumeExceptionType = volumeExceptionType;
			_message = message;
		}

		#endregion

	}
}
