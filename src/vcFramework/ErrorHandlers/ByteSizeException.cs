//////////////////////////////////////////////////////////////////////////////////////
// Author				: Shukri Adams												//
// Contact				: shukri.adams@gmail.com									//
//																					//
// vcFramework : A reuseable library of utility classes                             //
// Copyright (C)																	//
//////////////////////////////////////////////////////////////////////////////////////

namespace vcFramework.ErrorHandlers
{
	/// <summary> Summary description for ByteSizeException. </summary>
	public class ByteSizeException : System.Exception
	{

		#region MEMBERS

		private ExceptionTypes m_ExceptionType;
		
		private string m_strExceptionMessage;
		
		public enum ExceptionTypes : int
		{
			InvalidByteSize
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

		public ByteSizeException(ExceptionTypes ExceptionType, string Message)
		{
			m_ExceptionType = ExceptionType;
			m_strExceptionMessage = Message;
		}

		#endregion

	}
}
