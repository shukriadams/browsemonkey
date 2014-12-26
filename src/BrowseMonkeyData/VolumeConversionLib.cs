///////////////////////////////////////////////////////////////
// BrowseMonkeyData - A class library for the data file      // 
// format of BrowseMonkey.                                   //
// Shukri Adams (shukri.adams@gmail.com)                     //
// https://github.com/shukriadams/browsemonkey               //
// MIT License (MIT) Copyright (c) 2014 Shukri Adams         //
///////////////////////////////////////////////////////////////

using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using FSXml;
using vcFramework.IO.Streams;

namespace BrowseMonkeyData
{
	/// <summary>
	/// Contains methods used to transfer the data from one volume version file to another. 
	/// </summary>
	public class VolumeConversionLib
	{

	
		/// <summary>
		/// 
		/// </summary>
		/// <param name="volume"></param>
		/// <param name="newVolume"></param>
		public static void Convert101_102(
			string volume,
			string newVolume,
			CultureInfo culture
			)
		{

			Volume101 volume101		= null;
			StringBuilder x			= new StringBuilder();
			string index			= "";
			
			volume101 = new Volume101(volume);

			// #################################################
			// generates a list of all filetypes listed in this 
			// volume, and writes that data to a stream
			// -------------------------------------------------
			string[] fileTypes = FSXmlLib.GetFileExtensions(
				volume101.VolumeData);
			
			foreach (string fileType in fileTypes)
				x.Append("<t>" + fileType + "</t>");



			index = 
			"<i>" + 
			"<!-- " + Constants.UniversalVolumeIdenfifier + " -->" + 
			"<!-- description --><de>" + volume101.Name + " - " + volume101.Description + "</de>" + 
			"<!-- date this volume created --><dc>" + volume101.DateCreated + "</dc>" + 
			"<!-- date this volume modifed --><dm>" + volume101.DateModified + "</dm>" + 
			"<!-- version --><v>" + VolumeVersions.v102.ToString() + "</v>" + 
			"<!-- type --><t></t>" + 
			"<!-- compressed (True or False) --><c>" + volume101.Compressed + "</c>" + 
			"<!-- directorycount --><d>" + volume101.DirectoryCount + "</d>" + 
			"<!-- filecount --><f>" + volume101.FileCount + "</f>" + 
			"<!-- volume contents size --><cs>" + volume101.VolumeContentsSize + "</cs>" + 
			"<!-- file types --><ft>" + x.ToString() + "</ft>" +
			"<!-- culture --><cu>" + culture.Name + "</cu>" +
			"</i>" + 
			Constants.DIVIDER_FLAG;
			
			// transforms xml
			MemoryStream transformedXml = new MemoryStream();
			XmlTextWriter writer = new XmlTextWriter(transformedXml, System.Text.Encoding.Default);
			XmlRestucture101_102(
				volume101.VolumeData, 
				writer);
			writer.Flush();


			// puts it all together
			FileStream newfile = new FileStream(
				newVolume,
				FileMode.Create);

			// converts index string to stream
			Stream newContent = StreamsLib.StringToBinaryStream(
				index,
				128);
			// writes index stream to file
			StreamsLib.MergeStream(
				newfile,
				newContent,
				0,
				128);
			
			// writes xml data to compressed stream
			newContent = (Stream)SharpZipWrapperLib.ZipStream(
				transformedXml,		// stream to be compressed
				5,					// compression level (hardcoded)
				128);				// buffer size - arbitrary

			// write memstream down to volume file 
			StreamsLib.MergeStream(
				newfile,			// stream to merge to
				newContent,			// stream to merge
				newfile.Length,		// position in merge-to stream where data will be written - in this case, the END of the file
				128);				// buffer size - kinda arbitrary

			// clean up
			newfile.Close();
			volume101.Dispose();

		}
	
		

		/// <summary>
		/// 
		/// </summary>
		/// <param name="currentNode"></param>
		/// <param name="writer"></param>
		private static void XmlRestucture101_102(
			XmlNode currentNode,
			XmlTextWriter writer
			)
		{
		
			// handles this node
			writer.WriteRaw("<d>");
			writer.WriteRaw(currentNode.SelectSingleNode(".//n").OuterXml);
			writer.WriteRaw(currentNode.SelectSingleNode(".//dc").OuterXml);
			writer.WriteRaw(currentNode.SelectSingleNode(".//dm").OuterXml);
			//writer.WriteRaw(currentNode.SelectSingleNode("d/o").OuterXml);
			writer.WriteRaw(currentNode.SelectSingleNode(".//fs").OuterXml);
			

			// handles child directories
			writer.WriteRaw("<ds>");
			XmlNode dirs = currentNode.SelectSingleNode(".//ds");
			
			foreach(XmlNode dir in dirs.ChildNodes)
				XmlRestucture101_102(
					dir,
					writer);

			writer.WriteRaw("</ds>");


			writer.WriteRaw("</d>");

		}

	}
}
