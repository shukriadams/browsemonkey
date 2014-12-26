//////////////////////////////////////////////////////////////////////////////////////
// Author				: Shukri Adams												//
// Contact				: shukri.adams@gmail.com									//
//																					//
// vcFramework : A reuseable library of utility classes                             //
// Copyright (C)																	//
//////////////////////////////////////////////////////////////////////////////////////

namespace vcFramework.ErrorHandlers
{
	/// <summary> Exception class associated with LogToFile class - all custom exceptions throw from  </summary>
	public class LogFileException : System.Exception
	{


		#region FIELDS

		private ExceptionTypes m_ExceptionType;
		private string m_strExceptionMessage;
		
		public enum ExceptionTypes : int
		{
			LogInitializationException,
			LogWriteException
		}

		#endregion


		#region PROPERTIES

		public ExceptionTypes ExceptionType
		{
			get{return m_ExceptionType;}
		}

		public string Message
		{
			get{return m_strExceptionMessage;}
		}
			
		#endregion


		#region CONSTRUCTORS

		public LogFileException(ExceptionTypes ExceptionType, string Message)
		{
			m_ExceptionType =  ExceptionType;
			m_strExceptionMessage = Message;
		}

		#endregion

	}
}
