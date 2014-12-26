///////////////////////////////////////////////////////////////
// FSXml - A library for representing file system data as    //
// Xml.                                                      //
// Shukri Adams (shukri.adams@gmail.com)                     //
// https://github.com/shukriadams/browsemonkey               //
// MIT License (MIT) Copyright (c) 2014 Shukri Adams         //
///////////////////////////////////////////////////////////////

using System;

namespace FSXml
{
	/// <summary>
	/// Summary description for ReadError.
	/// </summary>
	public class ReadError
	{

		#region FIELDS
		
		/// <summary>
		/// 
		/// </summary>
		private Exception _exception;

		/// <summary>
		/// 
		/// </summary>
		private bool _totalReadFailure;

		/// <summary>
		/// 
		/// </summary>
		private string _item;

		#endregion


		#region PROPERTIES

		/// <summary>
		/// 
		/// </summary>
		public Exception Exception
		{
			get
			{
				return _exception;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		public bool TotalReadFailure
		{
			get
			{
				return _totalReadFailure;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		public string Item
		{
			get
			{
				return _item;
			}
		}


		#endregion


		#region CONSTRUCTORS

		/// <summary>
		/// 
		/// </summary>
		/// <param name="message"></param>
		/// <param name="item"></param>
		/// <param name="totalReadFailure"></param>
		public ReadError(
			Exception exception,
			string item,
			bool totalReadFailure
			)
		{
			_exception = exception;
			_item = item;
			_totalReadFailure = totalReadFailure;
		}

		
		#endregion

	}
}
