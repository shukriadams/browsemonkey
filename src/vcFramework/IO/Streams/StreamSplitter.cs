//////////////////////////////////////////////////////////////////////////////////////
// Author				: Shukri Adams												//
// Contact				: shukri.adams@gmail.com									//
//																					//
// vcFramework : A reuseable library of utility classes                             //
// Copyright (C)																	//
//////////////////////////////////////////////////////////////////////////////////////

using System;
using System.IO;
using vcFramework.Delegates;

namespace vcFramework.IO.Streams
{
	/// <summary>  
	/// Object that wraps the same StreamSplit() method found in StreamsLib. 
	/// Wrapping the method in an object allows progress to be tracked in a convenient
	/// manner 
	/// </summary>
	public class StreamSplitter : IProgress
	{
		
		#region FIELDS
		
		/// <summary> 
		/// Invoked each time the StreamSplit method advances one block in splitting 
		/// </summary>
		public event EventHandler OnNext;
		
		/// <summary> 
		/// Invoked when streamsplitting ends 
		/// </summary>
		public event EventHandler OnEnd;
		
		/// <summary> 
		/// Holds the number of blocks to be processed in stream splitting 
		/// </summary>
		private long m_lngSteps;
		
		/// <summary> 
		/// Holds the current step 
		/// </summary>
		private long m_lngCurrentStep;
		
		/// <summary>
		/// 
		/// </summary>
		private bool _running;

		private Stream m_inStream;
		private long m_lngStartPosition;
		private long m_lngEndPosition;
		private int m_intBlockSize;
		private bool m_blnObjectInstantiationFailed;

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
		/// Gets the number of blocks to be processed for stream splitting. 
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


		#region CONSTRUCTOR
		
		/// <summary> 
		/// 
		/// </summary>
		/// <param name="inStream"></param>
		/// <param name="lngStartPosition"></param>
		/// <param name="lngEndPosition"></param>
		/// <param name="intBlockSize"></param>
		public StreamSplitter(
			Stream inStream,
			long lngStartPosition,
			long lngEndPosition,
			int intBlockSize
			)
		{
			// stores down to member fields
			m_inStream						= inStream;
			m_lngStartPosition				= lngStartPosition;
			m_lngEndPosition				= lngEndPosition;
			m_intBlockSize					= intBlockSize;
			

			// checks if invalid input
			m_blnObjectInstantiationFailed	= false;
			
			// ERROR ! -invalid input
			if (lngStartPosition >= lngEndPosition)
			{
				m_blnObjectInstantiationFailed = true;
				throw new Exception("Stream start position is after stream end postition.");
			}

			// error - endposition lies beyond end of instream
			if (lngEndPosition > inStream.Length)
			{
				m_blnObjectInstantiationFailed = true;
				throw new Exception("Stream end position lies beyond stream length.");
			}



			// calculates steps
			m_lngSteps = (m_lngEndPosition - m_lngStartPosition) / m_intBlockSize;
			if ((m_lngEndPosition - m_lngStartPosition) % m_intBlockSize != 0)
				m_lngSteps ++;


		}
		

		#endregion


		#region METHODS

		/// <summary> 
		/// Creates a new stream from an existing one, using a start and end
		/// position in the main stream 
		/// </summary>
		/// <returns></returns>
		public Stream Split(
			)
		{

			Stream objOutStream = new MemoryStream();
			byte[] arrBytTransferBlock = new byte[m_intBlockSize];
			int intRealBlockSize = 0;
			bool blnDo = true;

			
			try
			{

				_running = true;

				// immediately aborts if this object was not properly
				// instantiated.
				if (m_blnObjectInstantiationFailed)
					throw new Exception("Cannot split stream - this object was not properly instantiated.");	


				// move to position in main string from which reading will begin
				m_inStream.Seek(
					m_lngStartPosition,
					SeekOrigin.Begin
					);
			
				while (blnDo)
				{
					// calcs actual block size
					intRealBlockSize = m_intBlockSize;
					// gets block size for final transfer
					if (m_lngEndPosition - m_inStream.Position < m_intBlockSize)
					{	
						intRealBlockSize = Convert.ToInt32(m_lngEndPosition - m_inStream.Position);
						arrBytTransferBlock = new byte[intRealBlockSize];
						blnDo = false;	// loop will finish current run then break
					}

					// reads data into transfer byte array
					m_inStream.Read(
						arrBytTransferBlock,
						0,
						intRealBlockSize
						);

					// writes data into new stream
					objOutStream.Write(arrBytTransferBlock,
						0,
						intRealBlockSize
						);

					// fires "next" event
					m_lngCurrentStep ++;
					DelegateLib.InvokeSubscribers(
						OnNext,
						this);
				}

				// rewind output stream (becase thats what courteous ppl do)
				objOutStream.Seek(0, SeekOrigin.Begin);

				return objOutStream;

			}
			finally
			{
				// fires "next" event
				DelegateLib.InvokeSubscribers(
					OnEnd,
					this);

				_running = false;
			
			}

			
		}


		#endregion

	}
}
