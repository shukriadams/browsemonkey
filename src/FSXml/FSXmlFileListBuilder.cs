///////////////////////////////////////////////////////////////
// FSXml - A library for representing file system data as    //
// Xml.                                                      //
// Shukri Adams (shukri.adams@gmail.com)                     //
// https://github.com/shukriadams/browsemonkey               //
// MIT License (MIT) Copyright (c) 2014 Shukri Adams         //
///////////////////////////////////////////////////////////////

using System;
using System.IO;
using System.Text;
using System.Xml;
using vcFramework;
using vcFramework.Arrays;
using vcFramework.DataItems;
using vcFramework.Delegates;
using vcFramework.IO.Streams;
using vcFramework.Xml;

namespace FSXml
{
	/// <summary> 
	/// "Flattens" a node of FSXml into a single node of files. The resultant data is still valid FSXml. 
	/// </summary>
	public class FSXmlFileListBuilder : IProgress, IDisposable
	{
		
		#region FIELDS
		
		/// <summary> 
		/// References to XmlFileStructure data which will be searched 
		/// </summary>
		private XmlNode _fsXmlData;
		
		/// <summary> 
		/// String array of include file extensions 
		/// </summary>
		private string[] _includeFiles;
		
		/// <summary> 
		/// String array of exclude file extensions 
		/// </summary>
		private string[] _excludeFiles;
		
		/// <summary> 
		/// 
		/// </summary>
		public event EventHandler OnNext;

		/// <summary> 
		/// 
		/// </summary>
		public event EventHandler OnEnd;
		
		/// <summary> 
		/// 
		/// </summary>
		private long _currentStep;
		
		/// <summary> 
		/// 
		/// </summary>
		private long _steps;
		
		/// <summary> 
		/// 
		/// </summary>
		private bool _running;
		
		/// <summary> 
		/// 
		/// </summary>
		private bool _aborting;
		
		/// <summary> 
		/// 
		/// </summary>
		private MemoryStream _memStream;

		/// <summary> 
		/// 
		/// </summary>
		private XmlTextWriter _xmlWriter;

		/// <summary> 
		/// 
		/// </summary>
		private XmlDocument _outputAsXml;

		/// <summary> 
		/// 
		/// </summary>
		private Encoding _encoding;

		#endregion
		

		#region PROPERTIES
		
		/// <summary> 
		/// 
		/// </summary>
		public Encoding Encoding
		{
			get
			{
				return _encoding;
			}
			set
			{
				_encoding = value;
			}
		}
		

		/// <summary> 
		/// 
		/// </summary>
		public Stream Output
		{
			get
			{
				return _memStream;
			}
		}

		
		/// <summary> 
		/// 
		/// </summary>
		public XmlDocument OutputXml
		{
			get
			{
				// creates internally cached object
				if (_outputAsXml == null)
				{
					_outputAsXml = new XmlDocument();

					_outputAsXml.LoadXml(
						StreamsLib.BinaryStreamToString(
						_memStream,
						64));	//arb block size
						
				}

				return _outputAsXml;

			}
		}


		/// <summary>
		/// 
		///  </summary>
		public bool Running
		{
			get
			{
				return _running;
			}
		}

		
		/// <summary>
		/// 
		/// </summary>
		public string[] ExcludedFileTypes
		{
			get
			{
				return _excludeFiles;
			}
			set
			{
				
				// need to standardize input. extensions must not start
				// with ".", and must not be zero length
				for (int i = 0 ; i < value.Length ; i ++)
					while (true)
					{
						if (value[i].Length == 0 || !value[i].StartsWith("."))
							break;

						value[i] = value[i].Substring(1, value[i].Length - 1); 
					}

				value = StringArrayLib.RemoveEmptyStrings(value);

				_excludeFiles = value;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		public string[] IncludedFileTyYpes
		{
			get
			{
				return _includeFiles;
			}
			set
			{

				// need to standardize input. extensions must not start
				// with ".", and must not be zero length
				for (int i = 0 ; i < value.Length ; i ++)
					while (true)
					{
						if (value[i].Length == 0 || !value[i].StartsWith("."))
							break;

						value[i] = value[i].Substring(1, value[i].Length - 1); 
					}


				value = StringArrayLib.RemoveEmptyStrings(value);

				_includeFiles = value;
			}
		}


		/// <summary> 
		/// 
		/// </summary>
		public long CurrentStep
		{
			get
			{
				return _currentStep;	
			}
		}


		/// <summary> 
		///  
		/// </summary>
		public long Steps
		{
			get
			{
				return _steps;
			}
		}


		#endregion


		#region CONSTRUCTORS

		/// <summary> 
		/// 
		///  </summary>
		/// <param name="nXmlLevelData"></param>
		/// <param name="arrStrIncludeTypes"></param>
		/// <param name="arrStrExcludeTypes"></param>
		/// <param name="blnIgnoreCase"></param>
		public FSXmlFileListBuilder(
			XmlNode nXmlData
			)
		{

			_fsXmlData					= nXmlData;
			_aborting					= false;
			_running					= false;
			_includeFiles	= new string[0];
			_excludeFiles	= new string[0];
			
			// sets default encoding
			this.Encoding = Encoding.ASCII;

			// calculate how many steps are involved
			XmlLib.NamedNodeCount(
				_fsXmlData,
				"d",
				ref _steps);

		}


		#endregion


		#region METHODS

		/// <summary>  
		/// 
		/// </summary>
		/// <returns></returns>
		public void Start(
			)
		{

			_running = true; 

			_memStream = new MemoryStream();
			
			_xmlWriter = new XmlTextWriter(
				_memStream,
				this.Encoding);

			// transforms arrays of file enxtensions to ensure case insensitivity
			for (int i = 0 ; i < _includeFiles.Length ; i ++)
				_includeFiles[i] = _includeFiles[i].ToLower();
		
			for (int i = 0 ; i < _excludeFiles.Length ; i ++)
				_excludeFiles[i] = _excludeFiles[i].ToLower();


			_xmlWriter.WriteRaw("<fs>");

			GetFilesList_Internal(
				_fsXmlData);

			_xmlWriter.WriteRaw("</fs>");
			
			_xmlWriter.Flush();

			if (_aborting)
				this.Dispose();

			// fires "onend" events
			DelegateLib.InvokeSubscribers(
				OnEnd,
				this);

			_running = false; 

			
		}



		/// <summary>
		/// 
		/// </summary>
		public void Stop(
			)
		{
			_aborting = true;
		}



		/// <summary> 
		/// 
		/// </summary>
		/// <param name="nXmlCurrentNode"></param>
		/// <param name="dXmlListResults"></param>
		/// <param name="arrStrIncludeTypes"></param>
		/// <param name="arrStrExcludeTypes"></param>
		/// <param name="blnIgnoreCase"></param>
		private void GetFilesList_Internal(
			XmlNode nXmlCurrentNode
			)
		{

			XmlNode nXmlCurrentFile			= null;
			string strCurrentItemFullPath	= "";
			ByteSizeItem fileSize;			

		
			// do event stuff etc
			// fires "onnext" events
			DelegateLib.InvokeSubscribers(
				OnNext,
				this);


			// #############################################
			// handles aborting
			// ---------------------------------------------
			if (_aborting)
				return;
			

			_currentStep ++;


			// process files in this node
			for (int i = 0 ; i < nXmlCurrentNode.SelectSingleNode(".//fs").ChildNodes.Count ; i ++)
			{
				nXmlCurrentFile = nXmlCurrentNode.SelectSingleNode(".//fs").ChildNodes[i];
				
				// determines if current file should be included or not
				if (!IsItemRequired(Path.GetExtension(nXmlCurrentFile.SelectSingleNode("n").InnerText)))
					continue;

				strCurrentItemFullPath = FSXmlLib.GetFullPathForFile(
					nXmlCurrentFile,
					"\\");

				// converts raw file size to parsed file size object
				fileSize = new ByteSizeItem(
					Convert.ToInt64(nXmlCurrentFile.SelectSingleNode("s").InnerText));

				_xmlWriter.WriteRaw( 
					"<f>" +
					"<n>" + XmlLib.FilterForReservedXmlCharacters(nXmlCurrentFile.SelectSingleNode("n").InnerText) + "</n>"+
					"<dc>" + nXmlCurrentFile.SelectSingleNode("dc").InnerText + "</dc>" +
					"<dm>" + nXmlCurrentFile.SelectSingleNode("dm").InnerText + "</dm>" +
					"<s>" + fileSize.BytesParsed + " " + fileSize.SizeUnit + "</s>" +
					"<o>"+ 
					"<itemPath>" + XmlLib.FilterForReservedXmlCharacters(strCurrentItemFullPath) + "</itemPath>" + 
					"</o>"+ 
					"</f>");

			} // for


			 // process children directories under current node - recursion happens here
			for (int i = 0 ; i < nXmlCurrentNode.SelectSingleNode(".//ds").ChildNodes.Count ; i ++)
				GetFilesList_Internal(
					nXmlCurrentNode.SelectSingleNode(".//ds").ChildNodes[i]);
		}


		
		/// <summary> 
		/// Contains the "search" logic for this class - returns true if a given file should be included. 
		/// </summary>
		/// <param name="strItemName"></param>
		/// <param name="arrStrIncludeTypes"></param>
		/// <param name="arrStrExcludeTypes"></param>
		/// <param name="blnIgnoreCase"></param>
		/// <returns></returns>
		private bool IsItemRequired(
			string strFileExtension
			)
		{

			bool blnAddThisItem					= false;
			
			// ensures case-insensitivity
			strFileExtension = strFileExtension.ToLower();

			
			// removes leading "." on extensions
			while (true)
			{
				if (strFileExtension.Length == 0 || !strFileExtension.StartsWith("."))
					break;

				strFileExtension = strFileExtension.Substring(1 , strFileExtension.Length - 1);
			}

			if (strFileExtension.Length == 0)
				return false;



			// loops through array of file extensions - checks if current file's
			// extension matches any in array
			for (int j = 0 ; j < _includeFiles.Length ; j ++)
			{
					
				// implements wildcard
				if (_includeFiles[j] == "*")
				{
					blnAddThisItem = true;
					break;	
				}
				else if (strFileExtension == _includeFiles[j])
				{
					blnAddThisItem = true;
					break;
				}

			} //for

		

			// does exclusion test
			for (int j = 0 ; j < _excludeFiles.Length ; j ++)
			{
				// note - there is no wild card for exclusion listing

				// implements wildcard
				if (strFileExtension == _excludeFiles[j])
				{
					blnAddThisItem = false;
					break;	
				}

			} // for
			
			return blnAddThisItem;

		}

	
		/// <summary> 
		/// 
		/// </summary>
		public void Dispose(
			)
		{

			if (_xmlWriter != null)
				_xmlWriter.Flush();

			if (_outputAsXml != null)
				_outputAsXml.InnerXml = "<empty/>";

		}


		#endregion

	}
}
