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
using vcFramework.IO.Streams;

namespace FSXml

{
	/// <summary>  Summary description for SharpZipWrap. </summary>
	public class SharpZipWrapperLib
	{


		/// <summary> Zips an existing file </summary>
		/// <param name="FileToZip"></param>
		/// <param name="ZipedFile"></param>
		/// <param name="CompressionLevel"></param>
		/// <param name="BlockSize"></param>
		public static void ZipFile(
			string FileToZip, 
			string ZipedFile,
			int CompressionLevel, 
			int BlockSize
			)
		{
		
			FileStream StreamToZip = new FileStream(FileToZip,System.IO.FileMode.Open , System.IO.FileAccess.Read);
			System.IO.FileStream ZipFile = System.IO.File.Create(ZipedFile);
			ZipOutputStream ZipStream = new ZipOutputStream(ZipFile);
			ZipEntry ZipEntry = new ZipEntry(Path.GetFileName(FileToZip));
			ZipStream.PutNextEntry(ZipEntry);
			ZipStream.SetLevel(CompressionLevel);
			byte[] buffer = new byte[BlockSize];
			System.Int32 size =StreamToZip.Read(buffer,0,buffer.Length);
			ZipStream.Write(buffer,0,size);

			try 
			{
				while (size < StreamToZip.Length) 
				{
					int sizeRead =StreamToZip.Read(buffer,0,buffer.Length);
					ZipStream.Write(buffer,0,sizeRead);
					size += sizeRead;
				}
			} 
			catch(System.Exception ex)
			{
				throw ex;
			}

			ZipStream.Finish();
			ZipStream.Close();
			StreamToZip.Close();
		}

		

		/// <summary>
		/// 
		/// </summary>
		/// <param name="inStream"></param>
		/// <param name="compressionLevel"></param>
		/// <param name="blockSize"></param>
		/// <returns></returns>
		public static Stream ZipStream(
			Stream inStream,
			int compressionLevel,
			int blockSize
			)
		{

			// "rewinds" instream
			inStream.Seek(
				0,
				SeekOrigin.Begin);


			// sets up streams to compress existing file
			MemoryStream tempFileReader = new MemoryStream();
			ZipOutputStream ZipStream = new ZipOutputStream(tempFileReader);
			ZipEntry ZipEntry = new ZipEntry("ZippedStream");
			ZipStream.PutNextEntry(ZipEntry);
			ZipStream.SetLevel(compressionLevel);
			
			// copies uncompressed stream into compressed stream
			StreamsLib.StreamCopyAll(
				(Stream)inStream,
				(Stream)ZipStream,
				blockSize);



			ZipStream.Flush();
			ZipStream.CloseEntry();
			ZipStream.Finish();
			tempFileReader.Flush();

			string test = StreamsLib.BinaryStreamToString(tempFileReader, 128);

			return tempFileReader;

		}



		/// <summary> </summary>
		/// <param name="strFileAndPath"></param>
		/// <returns></returns>
		public static Stream GetUnzippedStream(
			string strFileAndPath
			)
		{
			ZipInputStream objZipStream = new ZipInputStream(File.OpenRead(strFileAndPath));
			objZipStream.  GetNextEntry();
			return (Stream)objZipStream;
		}



		/// <summary> </summary>
		/// <param name="inStream"></param>
		/// <returns></returns>
		public static Stream GetUnzippedStream(
			Stream inStream,
			int BlockSize
			)
		{

			// "rewind" stream
			inStream.Seek(0,SeekOrigin.Begin);

			MemoryStream outStream = new MemoryStream();
			ZipInputStream objZipStream = new ZipInputStream(inStream);
			
			ZipEntry theEntry = null;
			theEntry = objZipStream.GetNextEntry();
			if (theEntry == null)
				return null;
			string strtest = theEntry.Name;
			//			objZipStream.GetNextEntry();
	
				
			int size = 2048;
			byte[] data = new byte[2048];

			long lngtest = 0;

			while (true) 
			{
				size = objZipStream.Read(data, 0, data.Length);
				if (size > 0) 
				{
					lngtest = objZipStream.Position;
					outStream.Write(data, 0, size);
				} 
				else 
				{
					break;
				}
			}
				

			
			long test1 = inStream.Length;
			long test2 = outStream.Length; 
			
			objZipStream.Flush();
			objZipStream.Close();
			


			return (Stream)outStream;
			
		}
		


		/// <summary> </summary>
		/// <param name="strFileAndPath"></param>
		public static void UnzipFile(
			string strFileAndPath
			)
		{
			ZipInputStream s = new ZipInputStream(File.OpenRead(strFileAndPath));
			UnzipFile(s);	
		}


		
		/// <summary> Unzips an existing zipped file </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public static void UnzipFile(
			ZipInputStream s
			)
		{

			//ZipInputStream s = new ZipInputStream(File.OpenRead(strFileAndPath));
		
			ZipEntry theEntry;
			while ((theEntry = s.GetNextEntry()) != null) 
			{
			
				//Console.WriteLine(theEntry.Name);
			
				string directoryName = Path.GetDirectoryName(theEntry.Name);
				string fileName      = Path.GetFileName(theEntry.Name);
			
				// create directory
				//			Directory.CreateDirectory(directoryName);
			
				if (fileName != String.Empty) 
				{
					FileStream streamWriter = File.Create(theEntry.Name);
				
					int size = 2048;
					byte[] data = new byte[2048];
					while (true) 
					{
						size = s.Read(data, 0, data.Length);
						if (size > 0) 
						{
							streamWriter.Write(data, 0, size);
						} 
						else 
						{
							break;
						}
					}
				
					streamWriter.Close();
				}
			}
			s.Close();		
		}



	}
}
