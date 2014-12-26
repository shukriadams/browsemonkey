///////////////////////////////////////////////////////////////
// FSXml - A library for representing file system data as    //
// Xml.                                                      //
// Shukri Adams (shukri.adams@gmail.com)                     //
// https://github.com/shukriadams/browsemonkey               //
// MIT License (MIT) Copyright (c) 2014 Shukri Adams         //
///////////////////////////////////////////////////////////////

using System.Text;
using System.Xml;
using vcFramework;
using vcFramework.Arrays;
using vcFramework.Delegates;
using vcFramework.Parsers;
using vcFramework.Maths;
using vcFramework.Xml;

namespace FSXml
{
	/// <summary> 
	/// Exports nestes FSXml data to flatteted text
	/// </summary>
	public class FSXmlTextExporter : IProgress
	{
		
		#region FIELDS
		
		/// <summary>
		/// 
		/// </summary>
		private string[] _folderItemsToShow;
		
		/// <summary>
		/// 
		/// </summary>
		private string[] _fileItemsToShow;
		
		/// <summary>
		/// 
		/// </summary>
		private string _indentWith;
		
		/// <summary>
		/// 
		/// </summary>
		private string _spaceWith;
		
		/// <summary>
		/// 
		/// </summary>
		private bool _alignText;

		/// <summary>
		/// 
		/// </summary>
		private bool _decorateWithLines;

		/// <summary>
		/// 
		/// </summary>
		private bool _showFolderHeaders;

		/// <summary>
		/// 
		/// </summary>
		private bool _showFileHeaders;

		/// <summary>
		/// 
		/// </summary>
		private XmlNode _dataToDump;

		/// <summary> 
		/// </summary>
		public event System.EventHandler OnNext;

		/// <summary>  
		/// 
		/// </summary>
		public event System.EventHandler OnEnd;

		/// <summary>
		/// 
		/// </summary>
		private StringBuilder _x;
		
		/// <summary>
		/// 
		/// </summary>
		private long _steps;
		
		/// <summary>
		/// 
		/// </summary>
		private long  _currentStep;
		
		/// <summary>
		/// Set to true if object is busy processing
		/// </summary>
		private bool _running;
		
		/// <summary>
		/// Set to true if object is to stop processing
		/// </summary>
		private bool _stop;

		#endregion
		

		#region PROPERTIES

		/// <summary> 
		/// Gets the number of steps this object will take to complete its task 
		/// </summary>
		public long Steps
		{ 
			get
			{
				return _steps;
			}
		}
		


		/// <summary> 
		/// Gets teh current step 
		/// </summary>
		public long CurrentStep
		{ 
			get 
			{
				return _currentStep;
			}
		}



		/// <summary>
		/// Gets if object is busy with progressible processing
		/// </summary>
		public bool Running
		{
			get
			{
				return _running;
			}
		}
		


		/// <summary>
		/// Gets the exported text produced by this object
		/// </summary>
		public string Output
		{
			get
			{
				return _x.ToString();
			}
		}

		#endregion

	
		#region CONSTRUCTORS

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dXmlDumpData"></param>
		/// <param name="arrStrFolderItemsToShow"></param>
		/// <param name="arrStrFileItemsToShow"></param>
		/// <param name="strIndentWith"></param>
		/// <param name="strSpaceWith"></param>
		/// <param name="blnAlign"></param>
		/// <param name="blnDecorateWithLines"></param>
		/// <param name="blnShowFolderHeaders"></param>
		/// <param name="blnShowFileHeaders"></param>
		public FSXmlTextExporter(
			XmlNode dXmlDumpData,
			string[] folderItemsToShow,
			string[] fileItemsToShow,
			string indentWith,
			string spaceWith,
			bool align,
			bool decorateWithLines,
			bool showFolderHeaders,
			bool showFileHeaders
			)
		{
		
			_x = new StringBuilder();
			
			_dataToDump = dXmlDumpData;
			_folderItemsToShow = folderItemsToShow;
			_fileItemsToShow = fileItemsToShow;
			_indentWith = indentWith;
			_spaceWith = spaceWith;
			_alignText = align;
			_decorateWithLines = decorateWithLines;
			_showFolderHeaders = showFolderHeaders;
			_showFileHeaders = showFileHeaders;

			_stop = false;

			// calculates how many nodes must be processed to carry out dump
			// this is done by counting the number of directory nodes in
			// data
			XmlLib.NamedNodeCount(
				_dataToDump,
				"d", 
				ref _steps);

		}



		#endregion


		#region METHODS
		

		/// <summary> </summary>
		/// <param name="dXmlData"></param>
		/// <param name="arrStrFolderItemsToShow"></param>
		/// <param name="arrStrFileItemsToShow"></param>
		/// <param name="strIndentWith"></param>
		/// <param name="strSpaceWith"></param>
		/// <param name="blnAlign"></param>
		/// <param name="blnDecorateWithLines"></param>
		/// <param name="blnShowFolderHeaders"></param>
		/// <param name="blnShowFileHeaders"></param>
		/// <returns></returns>
		public void Start(
			)
		{
	
			_running = true;

			// call text dumper
			DumpListViewDataToText(
				_dataToDump,
				0);
			
			// fires OnFinish event
			DelegateLib.InvokeSubscribers(
				OnEnd, 
				this);

			_running = false;

		}


		
		/// <summary>
		/// Stops the progressible process in this object
		/// </summary>
		public void Stop(
			)
		{
			_stop = true;
		}



		/// <summary> </summary>
		/// <param name="nXmlLevelData"></param>
		/// <param name="intLevelDepth"></param>
		private void DumpListViewDataToText(
			XmlNode nXmlLevelData, 
			int depth
			)
		{
			string folderInfoText = "";
			string folderText = "";
			string fileInfoText = "";
			string fileText = "";
			string charLine = "";
			int[] maxLengths = new int[6];	//int array used to store the widths of items in a node, used for finding the widest items, needed for proper formatting
			int fullPathLength = 0;
			int itemCount = 0;
			


			// #############################################################
			// fires on advance dump event
			// -------------------------------------------------------------
			DelegateLib.InvokeSubscribers(
				OnNext, 
				this);
			_currentStep ++;

			

			// #############################################################
			// implements "abortion" of processing
			// -------------------------------------------------------------
			if (_stop)
				return;
		


			// #############################################################
			// generates info text and associated decoration for folders
			// -------------------------------------------------------------
			if (_showFolderHeaders)
			{
				
				// builds up folder info. This will look like :
				// Folder.......Path......DateCreated..........DateModified, except the "." padding
				// is " "
				// NAME
				if (StringArrayLib.Contains(_folderItemsToShow, "n"))
					folderInfoText += StringFormatLib.PadText("Folder", " ", nXmlLevelData.SelectSingleNode(".//n").InnerText.Length + _spaceWith.Length);
				// PATH
				if (StringArrayLib.Contains(_folderItemsToShow, "p"))
					folderInfoText += StringFormatLib.PadText("Path", " ", FSXmlLib.GetFullPathForFolder(nXmlLevelData, "\\").Length + _spaceWith.Length);
				// DATE CREATED
				if (StringArrayLib.Contains(_folderItemsToShow, "dc"))
					folderInfoText += StringFormatLib.PadText("Date Created", " ", nXmlLevelData.SelectSingleNode(".//dc").InnerText.Length + _spaceWith.Length);
				// DATE MODIFIED
				if (StringArrayLib.Contains(_folderItemsToShow, "dm"))
					folderInfoText += StringFormatLib.PadText("Date Modified", " ", nXmlLevelData.SelectSingleNode(".//dm").InnerText.Length + _spaceWith.Length);

				
				// if there is any folder header text, that text will require indentation
				// and decoration
				if (folderInfoText.Length > 0)
				{
					
					// first indent the line - the amount of indentation is a product of the length
					// of the indenting text, and the nesting depth of the current item being 
					// processed
					charLine = StringFormatLib.CharLine(_indentWith, _indentWith.Length*depth);
					
					// builds up the actual visible line which will be drawn under folder header text
					charLine += StringFormatLib.CharLine("_", folderInfoText.Length) + "\r\n";
						
					// indents folder header text in the same way the char line decoration text
					// was indented.
					folderInfoText = StringFormatLib.CharLine(_indentWith, _indentWith.Length*depth) + folderInfoText;	
					
					// adds charLine UNDERNEATH header text (using the linebreak)
					folderInfoText += "\r\n" + charLine;

				} //if


			}// if




			// #############################################################
			// 1- generates FOLDER text for this node. note that STR_SPACER 
			// is added to each property's value to ensure that, for the 
			// longest strings, there is still some space before the next 
			// property
			// -------------------------------------------------------------
			// NAME
			if (StringArrayLib.Contains(_folderItemsToShow, "n"))
				folderText += nXmlLevelData.SelectSingleNode(".//n").InnerText + _spaceWith;
			// PATH
			if (StringArrayLib.Contains(_folderItemsToShow, "p"))
				folderText += FSXmlLib.GetFullPathForFolder(nXmlLevelData, "\\") + _spaceWith;
			// DATE CREATED
			if (StringArrayLib.Contains(_folderItemsToShow, "dc"))
				folderText += nXmlLevelData.SelectSingleNode(".//dc").InnerText + _spaceWith;
			// DATE MODIFIED
			if (StringArrayLib.Contains(_folderItemsToShow, "dm"))
				folderText += nXmlLevelData.SelectSingleNode(".//dm").InnerText + _spaceWith;


			if (folderText.Length > 0)
			{
				charLine = "";
					
				// makes decoration lines if necessary - if not using decoration, strCharLine remains empty and can just
				// be added to the final output
				if (_decorateWithLines)
				{
					// makes charlines - must do this before strFolderText is indented, as needs the unindented length of strFolderText
					charLine = StringFormatLib.CharLine("*", folderText.Length);
					
					// indents charline, so char lines appear flush with the string's they wrap
					charLine = StringFormatLib.CharLine(_indentWith, _indentWith.Length*depth) + charLine + "\r\n";
				}

				// indents folder text
				folderText = StringFormatLib.CharLine(_indentWith, _indentWith.Length*depth) + folderText;
				folderText += "\r\n";
					
				// puts it all together
				if (_showFolderHeaders)
				{
					if (_decorateWithLines)
						_x.Append ( charLine +  folderInfoText + folderText + charLine ) ;
					else
						_x.Append ("\r\n" +  folderInfoText + folderText + charLine);		// add an extra linebreak if displaying header but not displaying decoration lines - the extra space make the resulting text more readable
				}
				else
					_x.Append (charLine + folderText + charLine);
			

			}//if





			// 2	- generates FILES text for this node
			// enters "file" processing section only if there are files on this node
			if (nXmlLevelData.SelectSingleNode(".//fs").ChildNodes.Count > 0)
			{
				
				XmlNode nXmlFiles = nXmlLevelData.SelectSingleNode(".//fs");
					
				// 2.1	- finds max width of file properties - needed for text align. this operation builds up
				//		  the int array only, nothing else. max width is required for neat aligning of text
				if (_alignText)
				{
					for (int i = 0 ; i < nXmlFiles.ChildNodes.Count ; i ++)
					{
						if (StringArrayLib.Contains(_fileItemsToShow, "n"))
						{
							if (nXmlFiles.ChildNodes[i].SelectSingleNode(".//n").InnerText.Length > maxLengths[0])
								maxLengths[0] = nXmlFiles.ChildNodes[i].SelectSingleNode(".//n").InnerText.Length;
						}

						if (StringArrayLib.Contains(_fileItemsToShow, "p"))
						{
							fullPathLength = FSXmlLib.GetFullPathForFile(nXmlFiles.ChildNodes[i], "\\").Length;
							if (fullPathLength > maxLengths[1]){maxLengths[1] = fullPathLength;}
						}

						if (StringArrayLib.Contains(_fileItemsToShow, "dc"))
						{
							if (nXmlFiles.ChildNodes[i].SelectSingleNode(".//dc").InnerText.Length > maxLengths[2])
								maxLengths[2] = nXmlFiles.ChildNodes[i].SelectSingleNode(".//dc").InnerText.Length;
						}

						if (StringArrayLib.Contains(_fileItemsToShow, "dm"))
						{
							if (nXmlFiles.ChildNodes[i].SelectSingleNode(".//dm").InnerText.Length > maxLengths[3])
								maxLengths[3] = nXmlFiles.ChildNodes[i].SelectSingleNode(".//dm").InnerText.Length;
						}

						if (StringArrayLib.Contains(_fileItemsToShow, "s"))
						{
							if (nXmlFiles.ChildNodes[i].SelectSingleNode(".//s").InnerText.Length > maxLengths[4])
								maxLengths[4] = nXmlFiles.ChildNodes[i].SelectSingleNode(".//s").InnerText.Length;
						}

						if (StringArrayLib.Contains(_fileItemsToShow, "e"))
						{
							if (ParserLib.ReturnAfterLast(nXmlFiles.ChildNodes[i].SelectSingleNode(".//n").InnerText, ".").Length > maxLengths[5])
								maxLengths[5] = ParserLib.ReturnAfterLast(nXmlFiles.ChildNodes[i].SelectSingleNode(".//n").InnerText, ".").Length;
						}

					}// for
				}// if
				

				// 2.2	- makes info text for file sections
				fileInfoText = "";
				if (StringArrayLib.Contains(_fileItemsToShow, "n") ||
					StringArrayLib.Contains(_fileItemsToShow, "p") ||
					StringArrayLib.Contains(_fileItemsToShow, "dc") ||
					StringArrayLib.Contains(_fileItemsToShow, "dm") ||
					StringArrayLib.Contains(_fileItemsToShow, "s") ||
					StringArrayLib.Contains(_fileItemsToShow, "e"))
				{
					if (StringArrayLib.Contains(_fileItemsToShow, "n"))
					{
						fileInfoText += StringFormatLib.PadText("File", " ", maxLengths[0]) + _spaceWith;
						itemCount++;
					}

					if (StringArrayLib.Contains(_fileItemsToShow, "p"))
					{
						fileInfoText += StringFormatLib.PadText("Path", " ", maxLengths[1]) + _spaceWith; 
						itemCount++;
					}

					if (StringArrayLib.Contains(_fileItemsToShow, "dc"))
					{
						fileInfoText += StringFormatLib.PadText("Date Created", " ", maxLengths[2]) + _spaceWith; 
						itemCount++;
					}

					if (StringArrayLib.Contains(_fileItemsToShow, "dm"))
					{
						fileInfoText += StringFormatLib.PadText("Date Modified", " ", maxLengths[3]) + _spaceWith; 
						itemCount++;
					}

					if (StringArrayLib.Contains(_fileItemsToShow, "s"))
					{
						fileInfoText += StringFormatLib.PadText("Size", " ", maxLengths[4]) + _spaceWith; 
						itemCount++;
					}
					
					if (StringArrayLib.Contains(_fileItemsToShow, "e"))
					{
						fileInfoText += StringFormatLib.PadText("Type", " ", maxLengths[5]) + _spaceWith; 
						itemCount++;
					}

					// indents file item - add 1 to file indentation counter so files are pushed in further than folders on the same level
					fileInfoText = StringFormatLib.CharLine(_indentWith, _indentWith.Length*(depth + 1)) + fileInfoText;
					fileInfoText += "\r\n";

					// makes _ charline and indents it, adds it to header
					charLine = StringFormatLib.CharLine(_indentWith, _indentWith.Length*(depth + 1));
					charLine += StringFormatLib.CharLine("_", AggregationLib.Sum(maxLengths) + _spaceWith.Length*itemCount);
					charLine += "\r\n";
					fileInfoText += charLine;

				}//if


				// put it together !
				charLine = "";
				if (_decorateWithLines)
				{
					// creates text decoration line
						
					charLine = StringFormatLib.CharLine(_indentWith, _indentWith.Length*(depth + 1));
					charLine += StringFormatLib.CharLine(".", AggregationLib.Sum(maxLengths) + _spaceWith.Length*itemCount);
					charLine += "\r\n";

				}
					
				if (_decorateWithLines)
				{
					if (_decorateWithLines)
						_x.Append ( charLine + fileInfoText );
					else
						_x.Append ("\r\n" + fileInfoText);		// add an extra linebreak if displaying header but not displaying decoration lines - the extra space make the resulting text more readable
				}
				else
					_x.Append ( charLine );


				// 2.3	- generates text for each  file item on this node
				for (int i = 0 ; i < nXmlFiles.ChildNodes.Count ; i ++)
				{
					fileText = "";
					
					// NAME
					if (StringArrayLib.Contains(_fileItemsToShow, "n"))
						fileText += StringFormatLib.PadText(nXmlFiles.ChildNodes[i].SelectSingleNode(".//n").InnerText, " ", maxLengths[0]) + _spaceWith;
					
					// PATH
					if (StringArrayLib.Contains(_fileItemsToShow, "p"))
						fileText += StringFormatLib.PadText(FSXmlLib.GetFullPathForFile(nXmlFiles.ChildNodes[i], "\\"), " ", maxLengths[1]) + _spaceWith;
				
					// DATE CREATED
					if (StringArrayLib.Contains(_fileItemsToShow, "dc"))
						fileText += StringFormatLib.PadText(nXmlFiles.ChildNodes[i].SelectSingleNode(".//dc").InnerText, " ", maxLengths[2]) + _spaceWith;
			
					// DATE MODIFIED
					if (StringArrayLib.Contains(_fileItemsToShow, "dm"))
						fileText += StringFormatLib.PadText(nXmlFiles.ChildNodes[i].SelectSingleNode(".//dm").InnerText, " ", maxLengths[3]) + _spaceWith;
						
					// SIZE
					if (StringArrayLib.Contains(_fileItemsToShow, "s"))
						fileText += StringFormatLib.PadText(nXmlFiles.ChildNodes[i].SelectSingleNode(".//s").InnerText, " ", maxLengths[4]) + _spaceWith;
						
					// EXTENSION (FILE TYPE)
					if (StringArrayLib.Contains(_fileItemsToShow, "e"))
						fileText += StringFormatLib.PadText(ParserLib.ReturnAfterLast(nXmlFiles.ChildNodes[i].SelectSingleNode(".//n").InnerText, "."), " ", maxLengths[5]) + _spaceWith;
					
					if (fileText.Length > 0)
					{
						// indents file item - add 1 to file indentation counter so files are pushed in further than folders on the same level
						fileText = StringFormatLib.CharLine(_indentWith, _indentWith.Length*(depth + 1)) + fileText;
							
						// adds file item to mainoutput string
						_x.Append ( fileText + "\r\n" );
					}

				}//for

				// entered on final run of loop - adds the char line to close off file block
				// THIS IS NOT SAFE ! ASSUMES THAT strCharLine STILL CONTAINS THE CORRENT TEXT NEEDED FOR A LINE
				if (_decorateWithLines)
					_x.Append ( charLine + "\r\n" );

			}// if - file processing section  - only entered if there are files on this node


			// 3. reinvokes this method if child elements exist //HANDLES CHILD NODES - RECURSION HAPPENS HERE
			for (int i = 0 ; i < nXmlLevelData.SelectSingleNode(".//ds").ChildNodes.Count ; i ++)
				DumpListViewDataToText(nXmlLevelData.SelectSingleNode(".//ds").ChildNodes[i], depth + 1);

		}


		
		#endregion

	}
}
