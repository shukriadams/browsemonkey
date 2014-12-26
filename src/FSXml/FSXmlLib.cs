///////////////////////////////////////////////////////////////
// FSXml - A library for representing file system data as    //
// Xml.                                                      //
// Shukri Adams (shukri.adams@gmail.com)                     //
// https://github.com/shukriadams/browsemonkey               //
// MIT License (MIT) Copyright (c) 2014 Shukri Adams         //
///////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Xml;
using vcFramework.Parsers;

namespace FSXml
{
	/// <summary> 
	/// Static class contain methods for working with directory and file structures using XML. 
	/// </summary>
	public class FSXmlLib
	{

		#region METHODS

		/// <summary>
		/// Gets a list of unique, distinct, alphabetically sorted file extensions in the given
		/// FSXml data. File extensions are are returned in lower case
		/// </summary>
		/// <returns></returns>
		static public string[] GetFileExtensions(
			XmlNode nFSXmlData
			)
		{
			
			ArrayList fileTypes = new ArrayList();
			XmlNode nXmlStartNode = null;

			// determines start node - we dont know for sure where in fsxml structure
			// the passed Xml data is, and we want the "folders" node that's as close
			// to the start node as possible
			if (nFSXmlData.Name == "d")
				nXmlStartNode = nFSXmlData;
			else if (nFSXmlData.SelectSingleNode("d") != null)
				nXmlStartNode = nFSXmlData.SelectSingleNode("d");
			else if (nFSXmlData.SelectSingleNode(".//d") != null)
				nXmlStartNode = nFSXmlData.SelectSingleNode(".//d");

				

			// starts recursive process
			GetFileExtensions_Internal(
				nXmlStartNode,
				fileTypes);

			fileTypes.Sort();

			// transfers arraylist contents to string array
			string[] returnedFileTypes = new string[fileTypes.Count];
			
			for (int i = 0 ; i < fileTypes.Count ; i ++)
				returnedFileTypes[i] = fileTypes[i].ToString();

			
			return returnedFileTypes;

		}
		


		/// <summary>
		/// "behind the scenes" recursive loop for public GetFileExtensions() method
		/// </summary>
		static private void GetFileExtensions_Internal(
			XmlNode nXmlCurrentNode,
			ArrayList fileTypes
			)
		{

			string filename = "";

			// process files
			for (int i = 0 ; i < nXmlCurrentNode.SelectSingleNode(".//fs").ChildNodes.Count ; i ++)
			{
				filename = nXmlCurrentNode.SelectSingleNode(".//fs").ChildNodes[i].SelectSingleNode(".//n").InnerText;
				filename = ParserLib.ReturnAfterLast(filename, ".").ToLower();
				
				if (filename.Length > 0 && !fileTypes.Contains(filename))
					fileTypes.Add(filename);
			}


			// process folders - recursion happens here
			for (int i = 0 ; i < nXmlCurrentNode.SelectSingleNode(".//ds").ChildNodes.Count ; i ++)
				GetFileExtensions_Internal(
					nXmlCurrentNode.SelectSingleNode(".//ds").ChildNodes[i],
					fileTypes);
		}



		/// <summary> 
		/// Returns the node from Xmlnode structure (containing valid XmlFileStructure) using 
		/// the specified filesystem-style path, example "c\folder\filename.extension"
		/// </summary>
		/// <param name="strFolderPath"></param>
		static public XmlNode GetNode(
			string folderPath, 
			XmlNode nXmlFileStructure, 
			NodeTypes nodeType
			)
		{

			XmlNode nXmlCurrentNode		= null;
			string currentPathSection	= "";
			bool onFinalNode			= false;
			bool folderNodeFound		= false;



			// why is NodeType required? to prevent possible ambiguous 
			// references. it is possible to have identically-named file and 
			// folder at the same point in a file structure heirarchy. by 
			// specifying nodetype, we can assume that all parents to the node 
			// are folders, and we can be sure of the final node too


			// strFolderPath Typically begins with a @ char, and the delimiter 
			// between path objects is \ .There is no \ at the end of the string, 
			// ie, the string is "unterminated"


				
			// "terminates" strFolderPath, making it easier to parse. 
			// Terminator will be removed later note - termination occurs only 
			// if the string isn't already terminated, as happens with some 
			// folder references . eg - "d:\"
			if (!folderPath.EndsWith("\\"))
				folderPath += "\\";
				

			// gets the "start" node. there can be several seperate xml directory 
			// structures in the document being scanned each stucture must have a 
			// unique root folder though. this assumption is used below to 
			// determine which dir. structure to being searching in (ie, the 
			// "start" node.
			nXmlCurrentNode = nXmlFileStructure;
				

			// ok, here comes a workaround ! sometimes, there might be a double 
			// "\\" in the string, which makes parsing it a nightmare - the 
			//	followin removes all double "\"'s ... 
			folderPath = folderPath.Replace(@"\\", @"\");
				
				
			// all Xml file structures begin with a folder so the first part 
			// of strFolderPath can now be discarded as its node has just been 
			// selected as the "start" node
			if (folderPath.Length > folderPath.IndexOf("\\"))
				folderPath = folderPath.Substring(
					folderPath.IndexOf("\\") + 1, folderPath.Length -  folderPath.IndexOf("\\") - 1);
			else
				folderPath = "";
				

			// if strFolderPath is empty, there is no need to continue - the node 
			// selected up to here is all that's needed this only happens if the 
			// share is one folder level deep
			if (folderPath.Length == 0)
				return nXmlCurrentNode;


			while (folderPath.Length > 0)
			{
					
				// returns everything upto and including the first \ encountered 
				// in string
				currentPathSection = folderPath.Substring(
					0, folderPath.IndexOf("\\") + 1);

				// removes the currently process pathsubsection from strFolderPath
				if (folderPath.Length > folderPath.IndexOf("\\"))
					folderPath = folderPath.Substring(
						folderPath.IndexOf("\\") + 1, folderPath.Length -  folderPath.IndexOf("\\") - 1);
				else
					folderPath = "";
					

				if(folderPath.Length == 0)
					onFinalNode = true;

				// gets the node. assumptions : if not blnOnFinalNode, assume the 
				// currently processed node is a folder if blnOnFinalNode, then use 
				// NodeType to determine what current node is
				if (!onFinalNode)
				{

					// if reached here, then we are still looking for a folder
						
					// must first remove the \ at end of string - TODO : perhaps 
					// the string should not be built up with the extra \ in the 
					// first place
					currentPathSection = currentPathSection.Substring(
						0, currentPathSection.Length - 1);
						
						
					folderNodeFound = false;

					for (int i = 0 ; i < nXmlCurrentNode.SelectSingleNode(".//ds").ChildNodes.Count ; i ++)
						if (nXmlCurrentNode.SelectSingleNode(".//ds").ChildNodes[i].SelectSingleNode(".//n").InnerText == currentPathSection)
						{
							nXmlCurrentNode = nXmlCurrentNode.SelectSingleNode(".//ds").ChildNodes[i];
							folderNodeFound = true;
							break;
						}


					if (!folderNodeFound)
						return null;	// if reached here, the strCurrentlyPathSubSection value does not exists, so the path is invalid



				}
				else
				{
					// if here, then we are now looking for a either a file or a folder
						
					// must first remove that / that was added to aid parsing
					currentPathSection = currentPathSection.Substring(0, currentPathSection.Length - 1);
					

					if (nodeType == NodeTypes.File)
						for (int i = 0 ; i < nXmlCurrentNode.SelectSingleNode(".//fs").ChildNodes.Count ; i ++)
						{
							if (nXmlCurrentNode.SelectSingleNode(".//fs").ChildNodes[i].SelectSingleNode(".//n").InnerText == currentPathSection)
							{
								nXmlCurrentNode = nXmlCurrentNode.SelectSingleNode(".//fs").ChildNodes[i];
								return nXmlCurrentNode;
							}
						}
					else if (nodeType == NodeTypes.Folder)
						for (int i = 0 ; i < nXmlCurrentNode.SelectSingleNode(".//ds").ChildNodes.Count ; i ++)
						{
							if (nXmlCurrentNode.SelectSingleNode(".//ds").ChildNodes[i].SelectSingleNode(".//n").InnerText == currentPathSection)
							{
								nXmlCurrentNode = nXmlCurrentNode.SelectSingleNode(".//ds").ChildNodes[i];
								return nXmlCurrentNode;
							}
						}

				}// if
			}// while

			return null;
		}



		/// <summary> 
		/// Returns the number of bytes contained in a directory
		/// </summary>
		/// <param name="nXmlFileStructure"></param>
		/// <param name="blnRecurse">True if all sub-items size must be included</param>
		/// <returns></returns>
		static public long DirectoryNodeBytes(
			XmlNode nXmlFileStructure, 
			bool recurse
			)
		{

			long bytes = 0;
			
			DirectoryNodeBytes_internal(
				nXmlFileStructure, 
				recurse, 
				ref bytes);

			return bytes;

		}
		


		/// <summary> 
		/// Internal method - used by DirectoryNodeBytes 
		/// </summary>
		/// <param name="nXmlFileStructure"></param>
		/// <param name="blnRecurse"></param>
		/// <param name="lngBytes"></param>
		/// <returns></returns>
		static private void DirectoryNodeBytes_internal(
			XmlNode nXmlFileStructure, 
			bool recurse, 
			ref long bytes
			)
		{
			
			string strSize = "";
			
			// 1. gets byte total for this node
			for (int i = 0 ; i < nXmlFileStructure.SelectSingleNode(".//fs").ChildNodes.Count ; i ++)
			{
				strSize = nXmlFileStructure.SelectSingleNode(".//fs").ChildNodes[i].SelectSingleNode(".//s").InnerText;
				if (strSize.Length > 0)
					bytes += Convert.ToInt64(strSize);
			}

			// 2. processes child nodes if necessary
			if (recurse)
				for (int i = 0 ; i < nXmlFileStructure.SelectSingleNode(".//ds").ChildNodes.Count ; i ++)
					DirectoryNodeBytes_internal(
						nXmlFileStructure.SelectSingleNode(".//ds").ChildNodes[i], 
						true, 
						ref bytes);

		}



		/// <summary> 
		/// Returns the full path of the given XmlNode's filestructure item. The file name 
		/// of the file itself IS attached. Requires that the root node must be specified.
		/// </summary>
		/// <param name="dXmlItem"></param>
		/// <returns></returns>
		static public string GetFullPathForFile(
			XmlNode dXmlItem, 
			XmlNode dXmlRootNode, 
			string delimiter
			)
		{

			string fullPath		= "";
			XmlNode currentNode	= null;
			bool rootReached	= false;


			// gets file name
			fullPath = delimiter + dXmlItem.SelectSingleNode(".//n").InnerText;

			// checks if the xmlnode is a file under the topmost node - note, there is no need to worry about
			// overshooting the documentElement with the parentnode.parentnode check, because a file item is 
			// always at least two nodes from the document element
			if (dXmlItem.ParentNode.ParentNode == dXmlRootNode)
			{
				// gets parent folder name
				fullPath = dXmlItem.ParentNode.ParentNode.SelectSingleNode(".//n").InnerText + fullPath;

				return fullPath;
			}

			// "moves up" to the folder that holds the file node - the loop below works on folder nodes, not file
			// nodes, so need to shift to folder node to enter loop
			currentNode = dXmlItem.ParentNode.ParentNode;
			fullPath = delimiter + currentNode.SelectSingleNode(".//n").InnerText + fullPath;

			// typically, the supplied node, if it has a filestructure parent, will have it's parent two nodes above it
			// if the node is the very first node in the filestructure hierarchy, it will be the document element
			// either way, there should always be two nodes above it.
			while (!rootReached)
			{
				// gets name of current folder node
					
				currentNode = currentNode.ParentNode.ParentNode;
				fullPath = delimiter + currentNode.SelectSingleNode(".//n").InnerText + fullPath;
					
				if (currentNode == dXmlRootNode)
					rootReached = true;
			}

			// removes unnecessary strDelimiter at start path string
			fullPath = fullPath.Substring(delimiter.Length, fullPath.Length - delimiter.Length);


			return fullPath;
		}
	



		/// <summary>  
		/// Returns the full path of the given XmlNode's filestructure item. The file name 
		/// of the file itself IS attached. Doesnt require the root node of the node 
		/// structure - works its way up along xml structure until runs out of valid 
		/// XmlFileStructure data to work with, then returns its value
		/// </summary>
		/// <param name="dXmlItem"></param>
		/// <param name="strDelimiter"></param>
		/// <returns></returns>
		static public string GetFullPathForFile(
			XmlNode dXmlItem, 
			string delimiter
			)
		{

			string fullPath					= "";
			XmlNode nXmlCurrentProcessedNode	= null;
			bool rootReached			= false;

			// gets file name
			fullPath = delimiter + dXmlItem.SelectSingleNode(".//n").InnerText;

			// checks if the xmlnode is a file under the topmost node - note, there is no need to worry about
			// overshooting the documentElement with the parentnode.parentnode check, because a file item is 
			// always at least two nodes from the document element
			rootReached = true;
			if (dXmlItem.ParentNode != null && 
				dXmlItem.ParentNode.ParentNode != null && 
				dXmlItem.ParentNode.ParentNode.Name == "d")
					rootReached = false;
				
			if (rootReached)
			{
				// gets parent folder name
				fullPath = dXmlItem.ParentNode.ParentNode.SelectSingleNode(".//n").InnerText + fullPath;

				return fullPath;
			}

			// "moves up" to the folder that holds the file node - the loop below works on folder nodes, not file
			// nodes, so need to shift to folder node to enter loop
			nXmlCurrentProcessedNode = dXmlItem.ParentNode.ParentNode;
			fullPath = delimiter + nXmlCurrentProcessedNode.SelectSingleNode(".//n").InnerText + fullPath;

			// typically, the supplied node, if it has a filestructure parent, will have it's parent two nodes above it
			// if the node is the very first node in the filestructure hierarchy, it will be the document element
			// either way, there should always be two nodes above it.
			rootReached = false;
			while (!rootReached)
			{
				// gets name of current folder node
					
					
				rootReached = true;
				if (nXmlCurrentProcessedNode.ParentNode != null && 
					nXmlCurrentProcessedNode.ParentNode.ParentNode != null && 
					nXmlCurrentProcessedNode.ParentNode.ParentNode.Name == "d")
						rootReached = false;
					
				if (!rootReached)
				{
					nXmlCurrentProcessedNode = nXmlCurrentProcessedNode.ParentNode.ParentNode;
					fullPath = delimiter + nXmlCurrentProcessedNode.SelectSingleNode(".//n").InnerText + fullPath;
				}

			}

			// removes unnecessary strDelimiter at start path string
			fullPath = fullPath.Substring(delimiter.Length, fullPath.Length - delimiter.Length);

			return fullPath;
		}


	

		/// <summary> 
		/// Returns the full path of the given XmlNode's filestructure item. The file 
		/// name of the folder itself IS attached. Note that this method will only work 
		/// with PURE XmlFileStructure data - if the the xml data is appended to any 
		/// accessory data, as is normal with most applications that use the Xmlflestructure 
		/// system, this method will NOT work, as it assumes that the root node of the file 
		/// structure is also the xml document element 
		/// </summary>
		/// <param name="dXmlItem"></param>
		/// <returns></returns>
		static public string GetFullPathForFolder(
			XmlNode dXmlItem, 
			XmlNode dXmlRootNode, 
			string delimiter
			)
		{

			string fullPath					= "";
			XmlNode nXmlCurrentProcessedNode	= null;
			bool rootReached			= false;


			// handles if the node is the document element
			if (dXmlItem == dXmlRootNode)
			{
				// gets file name
				fullPath = dXmlItem.SelectSingleNode(".//n").InnerText;
				return fullPath;
			}
				
			nXmlCurrentProcessedNode = dXmlItem;
			fullPath = delimiter + nXmlCurrentProcessedNode.SelectSingleNode(".//n").InnerText + fullPath;

			// typically, the supplied node, if it has a filestructure parent, will have it's parent two nodes above it
			// if the node is the very first node in the filestructure hierarchy, it will be the document element
			// either way, there should always be two nodes above it.
			while (!rootReached)
			{
				// gets name of current folder node
				nXmlCurrentProcessedNode = nXmlCurrentProcessedNode.ParentNode.ParentNode;
				fullPath = delimiter + nXmlCurrentProcessedNode.SelectSingleNode(".//n").InnerText + fullPath;

				if (nXmlCurrentProcessedNode == dXmlRootNode)
					rootReached = true;
			}

			// removes unnecessary strDelimiter at start path string
			fullPath = fullPath.Substring(delimiter.Length, fullPath.Length - delimiter.Length);

			return fullPath;
		}



		/// <summary>  
		/// Returns the full path of the given XmlNode's filestructure item. The file name 
		/// of the file itself IS attached. Doesnt require the root node of the node 
		/// structure - works its way up along xml structure until runs out of valid 
		/// XmlFileStructure data to work with, then returns its value
		/// </summary>
		/// <param name="dXmlItem"></param>
		/// <param name="strDelimiter"></param>
		/// <returns></returns>		
		static public string GetFullPathForFolder(
			XmlNode dXmlItem, 
			string delimiter
			)
		{

			string fullPath					= "";
			XmlNode nXmlCurrentProcessedNode	= null;
			bool rootReached			= false;


			rootReached = true;
			if (dXmlItem.ParentNode != null && 
				dXmlItem.ParentNode.ParentNode != null && 
				dXmlItem.ParentNode.ParentNode.Name == "d")
					rootReached = false;

			// handles if the node is the document element
			if (rootReached)
			{
				// gets file name
				fullPath = dXmlItem.SelectSingleNode(".//n").InnerText;
				return fullPath;
			}
				
			nXmlCurrentProcessedNode = dXmlItem;
			fullPath = delimiter + nXmlCurrentProcessedNode.SelectSingleNode(".//n").InnerText + fullPath;

			// typically, the supplied node, if it has a filestructure parent, will have it's parent two nodes above it
			// if the node is the very first node in the filestructure hierarchy, it will be the document element
			// either way, there should always be two nodes above it.
			rootReached = false;
			while (!rootReached)
			{

				rootReached = true;
				if (nXmlCurrentProcessedNode.ParentNode != null && 
					nXmlCurrentProcessedNode.ParentNode.ParentNode != null && 
					nXmlCurrentProcessedNode.ParentNode.ParentNode.Name == "d")
						rootReached = false;

				if (!rootReached)
				{
					// gets name of current folder node
					nXmlCurrentProcessedNode = nXmlCurrentProcessedNode.ParentNode.ParentNode;
					fullPath = delimiter + nXmlCurrentProcessedNode.SelectSingleNode(".//n").InnerText + fullPath;
					
				}
			}

			// removes unnecessary strDelimiter at start path string
			fullPath = fullPath.Substring(delimiter.Length, fullPath.Length - delimiter.Length);

			return fullPath;
		}



		#endregion

	}
}
