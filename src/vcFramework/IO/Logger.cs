//////////////////////////////////////////////////////////////////////////////////////
// Author				: Shukri Adams												//
// Contact				: shukri.adams@gmail.com									//
//																					//
// vcFramework : A reuseable library of utility classes                             //
// Copyright (C)																	//
//////////////////////////////////////////////////////////////////////////////////////

using System;
using System.IO;
//using System.Windows.Forms;

using vcFramework.ErrorHandlers;
using vcFramework.Time;

namespace vcFramework.IO
{

	public class Logger : IDisposable
	{
		
		#region MEMBERS

		//private bool m_blnDefaultCheckComplete = false;
		//private bool m_blnLogUnwritable = false;
		//private bool m_blnCreateFolderIfNotExists = true;
		//private int m_intMaxLogFileSize = 100000;							//IN Kilobytes
		//private string m_strLogFileName;
		//private string m_strLogFilePath;
		
		/// <summary> </summary>
		private string m_strLogFilePathAndName;
		
		/// <summary> Set to true if object's log is ready to write to  </summary>
		private bool m_blnLogWriteable = false;
		
		private long m_lngMaxLogFileByteSize = 0;
		
		private bool _compressLogOnClose;

		#endregion

		
		#region PROPERTIES

			
		/// <summary>
		/// Gets or sets if log should be compressed when it is closed off
		/// </summary>
		public bool CompressLogOnClose
		{
			get
			{
				return _compressLogOnClose;
			}
			set
			{
				_compressLogOnClose = value;
			}
		}
		

		/// <summary>Gets log path+file name</summary>
		public string LogFilePathAndName
		{
			get{return m_strLogFilePathAndName;}
		}


		/// <summary>Gets maximum log file size in kilobytes. </summary>
		public long MaxLogFileByteSize	
		{
			get{return m_lngMaxLogFileByteSize;}
		}



		#endregion

		
		#region CONSTRUCTORS
	

		/// <summary>  </summary>
		/// <param name="strLogFilePathAndName"></param>
		/// <param name="lngMaxLogFileByteSize"></param>
		/// <param name="blnCreatePathIfNotExists"></param>
		/// <param name="blnResumeExistingLogIfFound"></param>
		public Logger(
			string strLogFilePathAndName, 
			long lngMaxLogFileByteSize, 
			bool blnCreatePathIfNotExists, 
			bool blnResumeExistingLogIfFound
			)
		{

			m_strLogFilePathAndName = strLogFilePathAndName;
			m_lngMaxLogFileByteSize = lngMaxLogFileByteSize;
			m_blnLogWriteable = false;
			this.CompressLogOnClose	= false;

			// gets path only
			string strPath = Path.GetDirectoryName(strLogFilePathAndName);
            string strFile = Path.GetFileName(strLogFilePathAndName);


			// 1. checks if path is valid
            if (!Directory.Exists(strPath) && blnCreatePathIfNotExists)
			{
				Directory.CreateDirectory(strPath);
			}

			// 2. checks again if path is valid
			if (!Directory.Exists(strPath))
			{
				// cannot write log - throw exception or something and destroy object
				throw new LogFileException(LogFileException.ExceptionTypes.LogInitializationException, "Cannot access log directory");
				
			}
			else
			{
				//path is valid - check if file exists
				if (File.Exists(strLogFilePathAndName))
				{
					// file exists
					if (blnResumeExistingLogIfFound)
					{
						// can resume use of file - check if its size is legal
						if (LogSizeExceeded(strLogFilePathAndName, lngMaxLogFileByteSize))
						{
							// size exceeded - close existing log off and create new one
							CloseLog(
								strLogFilePathAndName
								);

							CreateLog(
								strLogFilePathAndName
								);

							m_blnLogWriteable = true;
						}
						else
						{
							// existing log is ready for use
							m_blnLogWriteable = true;
						}
					}
					else
					{
						// cannot resume existing file - close it off and create a new log file
						CloseLog(
							strLogFilePathAndName
							);

						CreateLog(
							strLogFilePathAndName
							);

						m_blnLogWriteable = true;

					}
				}
				else
				{
					// file doesn't exist - create it
					CreateLog(
						strLogFilePathAndName
						);

					m_blnLogWriteable = true;
				}
			}
		}
			


		public Logger(
			string strLogFilePathAndName, 
			long lngMaxLogFileByteSize, 
			bool blnCreatePathIfNotExists, 
			bool blnResumeExistingLogIfFound,
			bool compressLogOnClose
			)
		{

			m_strLogFilePathAndName = strLogFilePathAndName;
			m_lngMaxLogFileByteSize = lngMaxLogFileByteSize;
			m_blnLogWriteable = false;
			this.CompressLogOnClose	= compressLogOnClose;

			// gets path only
			string strPath = Path.GetDirectoryName(strLogFilePathAndName);
			string strFile = Path.GetFileName(strLogFilePathAndName);


			// 1. checks if path is valid
			if (!Directory.Exists(strPath) && blnCreatePathIfNotExists)
			{
                Directory.CreateDirectory(strPath);
			}

			// 2. checks again if path is valid
			if (!Directory.Exists(strPath))
			{
				// cannot write log - throw exception or something and destroy object
				throw new LogFileException(LogFileException.ExceptionTypes.LogInitializationException, "Cannot access log directory");
				
			}
			else
			{
				//path is valid - check if file exists
				if (File.Exists(strLogFilePathAndName))
				{
					// file exists
					if (blnResumeExistingLogIfFound)
					{
						// can resume use of file - check if its size is legal
						if (LogSizeExceeded(strLogFilePathAndName, lngMaxLogFileByteSize))
						{
							// size exceeded - close existing log off and create new one
							CloseLog(
								strLogFilePathAndName
								);

							CreateLog(
								strLogFilePathAndName
								);

							m_blnLogWriteable = true;
						}
						else
						{
							// existing log is ready for use
							m_blnLogWriteable = true;
						}
					}
					else
					{
						// cannot resume existing file - close it off and create a new log file
						CloseLog(
							strLogFilePathAndName
							);

						CreateLog(
							strLogFilePathAndName
							);

						m_blnLogWriteable = true;

					}
				}
				else
				{
					// file doesn't exist - create it
					CreateLog(
						strLogFilePathAndName
						);

					m_blnLogWriteable = true;
				}
			}
		}
			

		#endregion


		#region METHODS

		/// <summary> Writes text to log </summary>
		/// <param name="strTextToAppend"></param>
		public void WriteToLog(
			string strTextToAppend
			)
		{

			bool blnProceed		= true;
			string strTextToLog = "";
		

			// ###########################################
			// checks if log file available
			// -------------------------------------------
			if (!m_blnLogWriteable)
			{
				// should never reach here - this object should not exist if the m_blnLogWriteable = false - 
				// constructor should terminate it if the log cannot be created by the constructor
				blnProceed = false;
					
				throw new LogFileException(
					LogFileException.ExceptionTypes.LogWriteException, 
					"LogToFile object bound to invalid log");

			}
			else if(!File.Exists(m_strLogFilePathAndName))
				// paranoia check - true to determine if log file has been tampered with after constructor set it up
				CreateLog(m_strLogFilePathAndName);

				


			// ###########################################
			// does a size check before writing to log
			// -------------------------------------------
			if (LogSizeExceeded(m_strLogFilePathAndName, m_lngMaxLogFileByteSize))
			{
				// log file is too large - close and create a new one
				CloseLog(m_strLogFilePathAndName);
				CreateLog(m_strLogFilePathAndName);
			}



			// ###########################################
			// checks passed- write to log
			// -------------------------------------------
			if (blnProceed)
			{

				strTextToLog = DateTimeLib.DateNow() + "\t" +  strTextToAppend + "\r\n";
                File.AppendAllText(
					m_strLogFilePathAndName, 
					strTextToLog);
			}
		}


		
		/// <summary> Closes off an existing log file </summary>
		private void CloseLog(
			string strFileAndPath
			)
		{
			string strNewFileName = "";
	
			FileStream objFileStream = new FileStream	(
				strFileAndPath,
				FileMode.Append,
				FileAccess.Write
				);

			BufferedStream objBufStream = new BufferedStream(objFileStream);
			StreamWriter objStreamWriter = new StreamWriter(objBufStream);

			objStreamWriter.Write("\r\n\r\nLog closed off. Date : " + DateTimeLib.DateNow());
			objStreamWriter.Flush();
			objStreamWriter.Close();

			objStreamWriter.Close();	
			objBufStream.Close();
			objFileStream.Close();
			
			
			FileInfo objFileMove = new FileInfo(strFileAndPath);
			
			// creates new file name based on existing one
			strNewFileName = "CLOSED ON -- " + DateTimeLib.DateNow().ToString().Replace(":","-") +  "--" + Path.GetFileName(strFileAndPath);
			
			string test = Path.GetDirectoryName(strFileAndPath) + "\\" + strNewFileName;
			
			// uses "moveTo" to rename file - location stays the same, but name changes
			objFileMove.MoveTo(test);


			// compress log if necessary
            /*
			if (this.CompressLogOnClose)
			{
				SharpZipWrapperLib.ZipFile(
					test,
					test + ".zip",
					6,		// hardcoded magic number - can be anything really
					128		// hardcoded magic number - can be anything really
					);

				// note -- the File.Exists() is not logically necessary, but i have noticed
				// that if not using it, sometimes the file to delete is not detected, and
				// the delete process fails
				if (File.Exists(test))
					File.Delete(
						test
						);
			}
            */ 
		}
		


		/// <summary> Creates a new log file </summary>
		private void CreateLog(
			string strFileAndPath
			)
		{
			string strLogFileHeader = "";
			BufferedStream objBufStream = null;
			StreamWriter objStreamWriter = null;
			FileStream objFileStream = null;


			objFileStream = new FileStream (
				strFileAndPath,
				FileMode.OpenOrCreate,
				FileAccess.Write
				);

			objBufStream = new BufferedStream(objFileStream);
			objStreamWriter = new StreamWriter(objBufStream);

			strLogFileHeader = "Log file created " + System.DateTime.Now.ToLongTimeString() + "." + "\r\n" + "\r\n" + "\r\n";

				
			objStreamWriter.Write(
				strLogFileHeader
				);

			objStreamWriter.Flush();
			objStreamWriter.Close();

			objFileStream = null;
			objBufStream = null;
			objStreamWriter = null;


		}



		/// <summary> Determines if log file max size has been reached / exceeded</summary>
		/// <returns></returns>
		private bool LogSizeExceeded(
			string strFileAndPath,
			long lngtByteSize
			)
		{

			bool blnOutput			= false;
			FileInfo objFileCheck	= null;

			objFileCheck = new FileInfo(
				strFileAndPath
				);
				
			if (objFileCheck.Length > lngtByteSize)
				blnOutput = true;

			objFileCheck = null;

			return blnOutput;

		}



		/// <summary>
		/// Returns the contents of the current log
		/// </summary>
		/// <returns></returns>
		public string Read(
			)
		{
			try
			{
				return File.ReadAllText(m_strLogFilePathAndName);
			}
			catch(IOException)
			{
				// suppress io exceptions
			}

			return string.Empty;
		}


		/// <summary> Destroys this object </summary>
		public void Dispose(
			)
		{
		}



		#endregion
	
	}

}
