///////////////////////////////////////////////////////////////
// FSXml - A library for representing file system data as    //
// Xml.                                                      //
// Shukri Adams (shukri.adams@gmail.com)                     //
// https://github.com/shukriadams/browsemonkey               //
// MIT License (MIT) Copyright (c) 2014 Shukri Adams         //
///////////////////////////////////////////////////////////////

using System;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using vcFramework;
using vcFramework.Delegates;

namespace FSXml
{
	/// <summary> 
	/// Summary description for StreamUncompressor. 
	/// </summary>
	public class StreamUncompressor : IProgress
	{
		#region FIELDS
		
		/// <summary>
		/// 
		/// </summary>
		private long m_lngCurrentStep;

		/// <summary>
		/// 
		/// </summary>
		private long m_lngSteps;

		/// <summary>
		/// 
		/// </summary>
		private Stream m_inStream;

		/// <summary>
		/// 
		/// </summary>
		private int m_intBlockSize;
		
		/// <summary>
		/// 
		/// </summary>
		private bool _running;

		/// <summary>
		/// 
		/// </summary>
		public event EventHandler OnNext;
		
		/// <summary>
		/// 
		/// </summary>
		public event EventHandler OnEnd;	

		#endregion


		#region PROPERTIES

		/// <summary> 
		/// Returns current step 
		/// </summary>
		public long CurrentStep
		{
			get
			{
				return m_lngCurrentStep;
			}
		}
	

		/// <summary> 
		/// Gets the number of steps to complete this object's operation 
		/// </summary>
		public long Steps
		{
			get
			{
				return m_lngSteps;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		public bool Running
		{
			get
			{
				return _running;
			}
		}


		#endregion


		#region CONSTRUCTORS

		/// <summary> 
		/// </summary>
		/// <param name="inStream"></param>
		/// <param name="BlockSize"></param>
		public StreamUncompressor(
			Stream inStream,
			int BlockSize
			)
		{
			// save values to member fields
			m_inStream = inStream;
			m_intBlockSize = BlockSize;

			// "rewind" stream
			m_inStream.Seek(0,SeekOrigin.Begin);

			// gets size of data compressed in stream
			ZipInputStream objZipStream = new ZipInputStream(m_inStream);
			ZipEntry theEntry = objZipStream.GetNextEntry();
			m_lngSteps = theEntry.Size;

			// DO NOT close objZipStream - because of the way it binds
			// to m_inStream, it will close the latter if itself is 
			// closed

			theEntry = null;
		}


		#endregion
	

		#region METHODS

		/// <summary> </summary>
		/// <param name="inStream"></param>
		/// <returns></returns>
		public Stream GetUnzippedStream(
			)
		{

			try
			{
				_running = true;

				// "rewind" stream
				m_inStream.Seek(
					0,
					SeekOrigin.Begin);

				MemoryStream outStream = new MemoryStream();
				ZipInputStream objZipStream = new ZipInputStream(m_inStream);
			
				ZipEntry theEntry = null;
				theEntry = objZipStream.GetNextEntry();

				if (theEntry == null)
					return null;
				
				int size = m_intBlockSize;
				byte[] data = new byte[m_intBlockSize];


				while (true) 
				{
				
					size = objZipStream.Read(data, 0, data.Length);
						
					if (size > 0) 
						outStream.Write(data, 0, size);
					else
						break;

					// steps ahead
					m_lngCurrentStep += size;
					DelegateLib.InvokeSubscribers(OnNext, this);

				}
				
				// fires "end" event
				DelegateLib.InvokeSubscribers(OnEnd, this);

				return (Stream)outStream;
			}
			finally
			{
				_running = false;
			}
		}


		#endregion

	}
}
