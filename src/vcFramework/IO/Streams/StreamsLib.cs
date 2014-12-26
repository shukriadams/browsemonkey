//////////////////////////////////////////////////////////////////////////////////////
// Author				: Shukri Adams												//
// Contact				: shukri.adams@gmail.com									//
//																					//
// vcFramework : A reuseable library of utility classes                             //
// Copyright (C)																	//
//////////////////////////////////////////////////////////////////////////////////////

using System;
using System.IO;
using System.Text;
using vcFramework.Arrays;

namespace vcFramework.IO.Streams
{
	/// <summary>  Static class for working with basic streams </summary>
	public class StreamsLib
	{
        public static byte[] StreamToByteArray(Stream s)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                s.CopyTo(ms);
                return ms.ToArray();
            }
        }

	    /// <summary> 
        /// Takes a string and converts to an binary stream 
        /// </summary>
	    /// <param name="input"></param>
	    /// <param name="blockSize"></param>
	    /// <returns></returns>
	    public static Stream StringToBinaryStream(string input,int blockSize)
		{
	        char[] charBuffer = new char[blockSize];
            byte[] byteBuffer = new byte[blockSize];
			StringReader reader	= null;
	        Encoder encoder = Encoding.Default.GetEncoder(); 
			
			try
			{

                Stream mem = new MemoryStream();
				// puts string into string reader stream
                reader = new StringReader(input);
			
				// stores total length of string
                long totalLength = input.Length;

                while (totalLength > 0)
				{
					// sets normal transfer block size
                    int bufferSize = blockSize;							// actual used block size - is usually = intBlockSize, but can be less on final block
					// if this is the the final block transfer, transfer size is the
					// remainder of the total original size divided by intBlockSize
                    if (totalLength < blockSize)
					{
                        bufferSize = Convert.ToInt32(totalLength);
						// recreate arrays to accurately fit input
                        charBuffer = new char[bufferSize];
                        byteBuffer = new byte[bufferSize];
					}
					


					// writes block of string to char array
                    reader.Read(charBuffer, 0, bufferSize);
				
					// converts char block to binary
                    encoder.GetBytes(charBuffer, 0, bufferSize, byteBuffer, 0, true);

					// writes binary array to file
                    mem.Write(byteBuffer, 0, bufferSize);

                    totalLength = totalLength - blockSize;
				}
				

				// resets stream start position
                mem.Seek(0, SeekOrigin.Begin);

                return mem;
				
			}
			finally
			{
				// clean up 
                if (reader != null)
                {
                    reader.Close();
                }
			}
		}

		

		/// <summary> 
        /// Returns contents of binary stream as string
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="blockSize"></param>
		/// <returns></returns>
		public static string BinaryStreamToString(Stream stream,int blockSize)
		{
		    char[] charBuffer = new char[blockSize];
            byte[] byteBuffer = new byte[blockSize];
			Decoder decoder = Encoding.Default.GetDecoder(); 
			StringBuilder s = new StringBuilder();
			
			// rewinds stream
            stream.Seek(0, SeekOrigin.Begin);

			// stores total length of stream
            long totalLength = stream.Length;

            while (totalLength > 0)
			{
				// sets normal transfer block size
                int bufferSize = blockSize;
				
				// if this is the the final block transfer, transfer size is the
				// remainder of the total original size divided by intBlockSize
                if (totalLength < blockSize)
				{
                    bufferSize = Convert.ToInt32(totalLength);
					// recreate arrays to accurately fit input
                    charBuffer = new char[bufferSize];
                    byteBuffer = new byte[bufferSize];
				}
			
				// writes block of to byte array
                stream.Read(byteBuffer, 0, bufferSize);
				
				// converts byte block to char
                decoder.GetChars(byteBuffer, 0, bufferSize, charBuffer, 0);

				// writes chars to stringbuilder
                s.Append(new string(charBuffer));

                totalLength = totalLength - bufferSize;

			}

			return s.ToString();
		}



		/// <summary>
		/// found at http://www.yoda.arachsys.com/csharp/readbinary.html
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="initialLength"></param>
		/// <returns></returns>
		public static byte[] BinaryStreamToByteArray (
			Stream stream, 
			int initialLength
			)
		{
			// If we've been passed an unhelpful initial length, just
			// use 32K.
			if (initialLength < 1)
			{
				initialLength = 32768;
			}
    
			byte[] buffer = new byte[initialLength];
			int read=0;
    
			int chunk;
			
			while ( (chunk = stream.Read(buffer, read, buffer.Length-read)) > 0)
			{
				read += chunk;
        
				// If we've reached the end of our buffer, check to see if there's
				// any more information
				if (read == buffer.Length)
				{
					int nextByte = stream.ReadByte();
            
					// End of stream? If so, we're done
					if (nextByte==-1)
						return buffer;
            
					// Nope. Resize the buffer, put in the byte we've just
					// read, and continue
					byte[] newBuffer = new byte[buffer.Length*2];
					Array.Copy(buffer, newBuffer, buffer.Length);
					newBuffer[read]=(byte)nextByte;
					buffer = newBuffer;
					read++;
				}
			}

			// Buffer is now too big. Shrink it.
			byte[] ret = new byte[read];
			Array.Copy(buffer, ret, read);
			return ret;
		}


		
		/// <summary> 
		/// Creates a new stream from an existing one, using a start and end
		/// position in the main stream 
		/// </summary>
		/// <returns></returns>
		private static Stream HIDEStreamSplit(
			Stream inStream,
			long lngStartPosition,
			long lngEndPosition,
			int intBlockSize
			)
		{
			Stream objOutStream = new MemoryStream();
			byte[] arrBytTransferBlock = new byte[intBlockSize];
			int intRealBlockSize = 0;
			bool blnDo = true;

			// ERROR ! -invalid input
			if (lngStartPosition >= lngEndPosition)
				return null;

			// error - endposition lies beyond end of instream
			if (lngEndPosition >= inStream.Length)
				return null;
			
			// move to position in main string from which reading will begin
			inStream.Seek(
				lngStartPosition,
				SeekOrigin.Begin
				);
			
			while (blnDo)
			{
				// calcs actual block size
				intRealBlockSize = intBlockSize;
				// gets block size for final transfer
                if (lngEndPosition - inStream.Position < intBlockSize)
				{	
					intRealBlockSize = Convert.ToInt32(lngEndPosition - inStream.Position);
					arrBytTransferBlock = new byte[intRealBlockSize];
					blnDo = false;	// loop will finish current run then break
				}

				// reads data into transfer byte array
				inStream.Read(
					arrBytTransferBlock,
					0,
					intRealBlockSize
					);

				// writes data into new stream
				objOutStream.Write(arrBytTransferBlock,
					0,
					intRealBlockSize
					);
			}

			// rewind output stream (becase thats what courteous ppl do)
			objOutStream.Seek(0, SeekOrigin.Begin);

			return objOutStream;
		}


	    /// <summary>
	    /// Inserts a given length of data from a point in one string, into another string,
	    /// also at a given position
	    /// </summary>
	    /// <param name="srcStream">Stream which data will be copied from</param>
	    /// <param name="targetStream"></param>
	    /// <param name="srcPosition">Position in source stream from which data will be read</param>
	    /// <param name="targetPosition">Position in target stream where data will be inserted</param>
	    /// <param name="insertLength">The length of data in source stream to be copied to target stream</param>
	    /// <param name="blockSize">Block size for buffering</param>
	    public static Stream InsertStream(
			Stream srcStream,
			Stream targetStream,
			long srcPosition,
			long targetPosition,
			long insertLength,
			int blockSize
			)
		{
	
			Stream finalStream = new MemoryStream();
			byte[] buffer = new byte[blockSize];
			bool blnDo = true;
			int realBlockSize = 0;					// actual size of transfer block  - usually = intBlockSize, but on final transfer can be less


			// move to position in main string where merging will begin
			srcStream.Seek( srcPosition, SeekOrigin.Begin );
			targetStream.Seek( 0, SeekOrigin.Begin );

			realBlockSize = blockSize; 
			
			// copy all data in target stream BEFORE the insert point in target stream.
			// data is copied to the final output stream
			while (blnDo)
			{

				// calcs actual block size
				// gets block size for final transfer
				if (targetPosition - targetStream.Position < blockSize)
				{	
					realBlockSize = Convert.ToInt32(targetPosition - targetStream.Position);

					if (realBlockSize == 0)
						break;

					buffer = new byte[realBlockSize];
					blnDo = false;	// loop will finish current run then break
				}

				// reads data into transfer byte array
				targetStream.Read(buffer, 0, realBlockSize);

				// writes data into new stream
				finalStream.Write(buffer, 0, realBlockSize);

			}

				
			buffer = new byte[blockSize];
			realBlockSize = blockSize; 
			blnDo = true;

			// copy src data
			while (blnDo)
			{

				// calcs actual block size
				// gets block size for final transfer
				if (insertLength - srcStream.Position < blockSize)
				{
					realBlockSize = Convert.ToInt32(insertLength- srcStream.Position);

					if (realBlockSize == 0)
						break;

					buffer = new byte[realBlockSize];
					blnDo = false;	// loop will finish current run then break
				}

				// reads data into transfer byte array
				srcStream.Read(buffer, 0, realBlockSize);

				// writes data into new stream
				finalStream.Write(buffer, 0, realBlockSize);

			}


			buffer = new byte[blockSize];
			realBlockSize = blockSize; 
			blnDo = true;

			// trailing target data
			while (blnDo)
			{

				// calcs actual block size
				// gets block size for final transfer
				if (targetStream.Length - targetStream.Position < blockSize)
				{
					realBlockSize = Convert.ToInt32(targetStream.Length - targetStream.Position);

					if (realBlockSize == 0)
						break;

					buffer = new byte[realBlockSize];
					blnDo = false;	// loop will finish current run then break
				}

				// reads data into transfer byte array
				targetStream.Read(buffer, 0, realBlockSize);

				// writes data into new stream
				finalStream.Write(buffer, 0, realBlockSize);

			}

			
			
			return finalStream;

		}

		

		/// <summary>
		/// Returns a stream minus a "clipped" section
		/// </summary>
		/// <param name="inStream"></param>
		/// <param name="position"></param>
		/// <param name="length"></param>
		/// <param name="bufferSize"></param>
		/// <returns></returns>
		public static Stream CutStream(
			Stream inStream,
			long position,
			long length,
			int bufferSize
			)
		{
			
			Stream outStream = new MemoryStream();
			byte[] buffer = new byte[bufferSize];

			inStream.Seek(0,SeekOrigin.Begin);
			
			// copies pre-clip section
			while (inStream.Position < position)
			{
				if (position - inStream.Position < bufferSize)
					buffer = new byte[position - inStream.Position];

				inStream.Read(buffer,0, buffer.Length);
				outStream.Write(buffer,	0, buffer.Length);
			}


			// skip over "clip" section
			inStream.Seek(position + length, SeekOrigin.Begin);


			// copies post-clip section
			while (inStream.Position < inStream.Length)
			{
				if (inStream.Length - inStream.Position < bufferSize)
					buffer = new byte[inStream.Length - inStream.Position];

				inStream.Read(buffer,0, buffer.Length);
				outStream.Write(buffer,	0, buffer.Length);
			}

			// "rewind" output stream before returning
			outStream.Seek(0,SeekOrigin.Begin);

			return outStream;

		}



		/// <summary> 
		/// Merges one stream into another at a given position 
		/// </summary>
		/// <param name="objMainStream"></param>
		/// <param name="objStreamToMerge"></param>
		/// <param name="lngMergePosition"></param>
		public static void MergeStream(
			Stream objMainStream,
			Stream objStreamToMerge,
			long lngMergePosition,
			int intBlockSize
			)
		{

			byte[] arrBytTransferBlock = new byte[intBlockSize];
			bool blnDo = true;
			int intRealBlockSize = 0;					// actual size of transfer block  - usually = intBlockSize, but on final transfer can be less
			
			//error - beyond range of main stream


			// move to position in main string where mergingr will begin
			objMainStream.Seek(
				lngMergePosition,
				SeekOrigin.Begin
				);

			// need to move merged streamt to its start
			objStreamToMerge.Seek(
				0,
				SeekOrigin.Begin
				);
			
			// sets default block size
			intRealBlockSize = intBlockSize;

			while (blnDo)
			{
				// calcs actual block size
				// gets block size for final transfer
				if (objStreamToMerge.Length - objStreamToMerge.Position < intBlockSize)
				{	
					intRealBlockSize = Convert.ToInt32(objStreamToMerge.Length - objStreamToMerge.Position);
					arrBytTransferBlock = new byte[intRealBlockSize];
					blnDo = false;	// loop will finish current run then break
				}


				// reads data into transfer byte array
				objStreamToMerge.Read(
					arrBytTransferBlock,
					0,
					intRealBlockSize);

				// writes data into new stream
				objMainStream.Write(
					arrBytTransferBlock,
					0,
					intRealBlockSize);

			}

		}
		

		
		/// <summary>
		/// Returns the position in a stream, of a sequence of bytes. Returns -1 
		/// if the sequence is not found
		/// </summary>
		/// <param name="objCheckStream"></param>
		/// <param name="bytCheckForByteBlock"></param>
		/// <returns></returns>
		public static long ByteArrayPositionInStream(
			Stream s,
			byte[] bytes
			)
		{

			byte[] buffer = new byte[bytes.Length];
			long lngPosition = 0;
			
			// "rewind" stream
			s.Seek(
				0,
				SeekOrigin.Begin);
	
			while (s.Position < s.Length - bytes.Length)
			{

				// reads _1_ byte into stream
				s.Read(
					buffer,
					0,
					buffer.Length);

				if (ByteArrayLib.AreEqual(
					buffer, 
					bytes, 
					false
					))
				{
					return lngPosition;
				}
				
				lngPosition ++;

				// advance stream forward 1 step
				s.Seek(
					lngPosition,
					SeekOrigin.Begin);
			}
						
			return -1;
		}
		


		/// <summary> Converts a string to byte array </summary>
		/// <param name="strInput"></param>
		/// <returns></returns>
		public static byte[] StringToByteArray(
			string input
			)
		{

			// note - returns byte array instead of byte because it's just 
			// easier to use byte arrays! I'm lazy!!
			byte[] bytes = new byte[input.Length];

			Encoder objEncoder = Encoding.Default.GetEncoder(); 
			
			objEncoder.GetBytes(
				input.ToCharArray(),
				0,
				input.Length,
				bytes,
				0,
				true);


			return bytes;
		}
		

		/// <summary>
		/// Converts byte array to string
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string ByteArrayToString(
			byte[] input)
		{
			Decoder decoder = Encoding.Default.GetDecoder(); 

			char[] output = new char[input.Length];

			decoder.GetChars(
				input,
				0,
				input.Length,
				output,
				0);

			return output.ToString();
		}

		
		/// <summary> Copies the entire contents of one binary stream to another binary stream</summary>
		public static void StreamCopyAll(
			Stream source,
			Stream target,
			int bufferSize
			)
		{
			
			// rewind src stream
			source.Seek(0, SeekOrigin.Begin);
	
			int size = 0;
			byte[] buffer = new byte[bufferSize];
			bool doloop = true;
			
			while (doloop)
			{
				if (source.Length - source.Position < bufferSize)
				{
					buffer = new byte[source.Length - source.Position];
					doloop = false;	// last loop run
				}

				size += source.Read(buffer, 0, buffer.Length);
				target.Write(buffer, 0, buffer.Length);

			}

		}

		

		public static string EncodeBase64(string content)
		{
			MemoryStream mOut = (MemoryStream)StringToBinaryStream(content, 128);
			return Convert.ToBase64String(mOut.GetBuffer(), 0, (int)mOut.Length);
		}
		

		public static string DecodeBase64(string content)
		{
			byte[] bPlain = new byte[content.Length];

			// this will throw an exception if the data is not base64 encoded!
			bPlain = Convert.FromBase64CharArray(content.ToCharArray(), 0, content.Length);
			
			return ByteArrayToString(bPlain);


		}
	}
}
