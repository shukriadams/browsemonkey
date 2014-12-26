//////////////////////////////////////////////////////////////////////////////////////
// Author				: Shukri Adams												//
// Contact				: shukri.adams@gmail.com									//
//																					//
// vcFramework : A reuseable library of utility classes                             //
// Copyright (C)																	//
//////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace vcFramework.Xml
{
	/// <summary> 
	/// Static class containing Xml handling methods 
	/// </summary>
	public class XmlLib
	{

		
		/// <summary> 
		/// Converts reserved charactes into  Xml-compliant strings 
		/// </summary>
		/// <param name="strSource"></param>
		/// <returns></returns>
		public static string FilterForReservedXmlCharacters(
			string strSource
			)
		{
			strSource = strSource.Replace("&", "&amp;");	// &
			strSource = strSource.Replace("'", "&apos;");	// '
			strSource = strSource.Replace(">", "&gt;");		// >
			strSource = strSource.Replace("<", "&lt;");		// <
			strSource = strSource.Replace("\"", "&quot;");	// "

			return strSource;		

		}
		


		/// <summary> Converts XML-safe strings back to 
		/// their original form </summary>
		/// <param name="strSource"></param>
		/// <returns></returns>
		public static string RestoreReservedCharacters(
			string strSource
			)
		{
			
			strSource = strSource.Replace("&amp;", "&");	// &
			strSource = strSource.Replace("&apos;", "'");	// '
			strSource = strSource.Replace("&gt;", ">");		// >
			strSource = strSource.Replace("&lt;", "<");		// <
			strSource = strSource.Replace("&quot;", "\"");	// "
				

			return strSource;				
		}



		/// <summary> Converts a dataset to Xml document.
		///  </summary>
		/// <param name="ds"></param>
		/// <returns></returns>
		public XmlDocument ReadDataSetToXmlDom(
			DataSet ds
			)
		{
			XmlDocument xmlData = new XmlDocument();
			System.IO.Stream msData = new System.IO.MemoryStream();

			try
			{
				//THIS METHOD WAS WRITTEN BY BRIAN COWAN
				ds.WriteXml(msData);
				msData.Flush();

				System.IO.StreamReader srReadToEnd = new System.IO.StreamReader(msData,System.Text.Encoding.UTF8);
				srReadToEnd.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);

				xmlData.LoadXml(srReadToEnd.ReadToEnd());			
			}
			finally
			{
				msData.Close();
				msData = null;
			}
			return xmlData;
		}
				


		/// <summary> Converts a recordset to an xmldocument 
		/// </summary>
		/// <param name="dsSource"></param>
		/// <returns></returns>
		public static XmlDocument DataSetToXml(
			DataSet dsSource
			)
		{
			string strBlankNodeRecord = "";
			XmlDocumentFragment fXmlRecord = null;

			XmlDocument dXmlDataSet = new XmlDocument();
			dXmlDataSet.InnerXml = "<records/>";

			for (int i = 0 ; i < dsSource.Tables[0].Columns.Count ; i ++)
				strBlankNodeRecord += "<" + dsSource.Tables[0].Columns[i].ColumnName + "/>" ;

			strBlankNodeRecord = "<record>" + strBlankNodeRecord + "</record>";
		
			for (int i = 0 ; i < dsSource.Tables[0].Rows.Count ; i ++)
			{

				fXmlRecord = dXmlDataSet.CreateDocumentFragment();
				fXmlRecord.InnerXml = strBlankNodeRecord;

				for (int j = 0 ; j < dsSource.Tables[0].Columns.Count ; j ++)
					fXmlRecord.SelectSingleNode(".//" + dsSource.Tables[0].Columns[j].ColumnName).InnerText = dsSource.Tables[0].Rows[i][j].ToString();
				
				dXmlDataSet.DocumentElement.AppendChild(fXmlRecord);
			}
			
			return dXmlDataSet;
		}


		
		/// <summary>
		/// Returns true of the given string contains valid xml
		/// </summary>
		/// <returns></returns>
		public static bool IsValidXml(
			string text
			)
		{
			try
			{
				XmlDocument d = new XmlDocument();
				d.LoadXml(text);
				d.InnerXml = "<d/>";
				return true;

				
			}
			catch
			{
				return false;
			}
		}



		/// <summary> Counts how many nodes there are under 
		/// a given node - recursive method </summary>
		/// <param name="nXmlCountNode"></param>
		/// <param name="?"></param>
		public static void NodesCount(
			XmlNode nXmlCountNode, 
			ref int intResult
			)
		{
			// tallies up for "this" node
            intResult ++;

			// invokes this method for each child node of "this" node
			for (int i = 0 ; i < nXmlCountNode.ChildNodes.Count ; i ++)
			{
				NodesCount(nXmlCountNode.ChildNodes[i], ref intResult);
			}
		}



		/// <summary> Counts how many nodes there are under 
		/// a given node - recursive method </summary>
		/// <param name="nXmlCountNode"></param>
		/// <param name="?"></param>
		public static void NodesCount(
			XmlNode nXmlCountNode, 
			ref long lngResult
			)
		{
			// tallies up for "this" node
			lngResult ++;

			// invokes this method for each child node of "this" node
			for (int i = 0 ; i < nXmlCountNode.ChildNodes.Count ; i ++)
			{
				NodesCount(nXmlCountNode.ChildNodes[i], ref lngResult);
			}
		}



		/// <summary> 
		/// Counts how many nodes of a given name 
		/// there are under a given node - recursive method 
		/// </summary>
		/// <param name="nXmlCountNode"></param>
		/// <param name="?"></param>
		public static void NamedNodeCount(
			XmlNode nXmlCountNode, 
			string strNodeName, 
			ref long lngResult
			)
		{
			// tallies up for "this" node
			if (nXmlCountNode.Name == strNodeName)
				lngResult ++;

			// invokes this method for each child node of "this" node
			for (int i = 0 ; i < nXmlCountNode.ChildNodes.Count ; i ++)
				NamedNodeCount(
					nXmlCountNode.ChildNodes[i], 
					strNodeName, 
					ref lngResult
					);
		}


		/// <summary> 
		/// Counts how many nodes of a given name there are under a given node. This is a 
		/// recursing method - the "depth" of the recursion can be set with the maxDepth
		/// value
		/// </summary>
		/// <param name="nXmlCountNode"></param>
		/// <param name="?"></param>
		public static void NamedNodeCount(
			XmlNode nXmlCountNode, 
			string strNodeName,
			int maxDepth,
			ref long lngResult
			)
		{

				NamedNodeCount_internal(
					nXmlCountNode, 
					strNodeName,
					maxDepth,
					0,
					ref lngResult);
		}

		
		/// <summary> 
		/// Counts how many nodes of a given name there are under a given node. This is a 
		/// recursing method - the "depth" of the recursion can be set with the maxDepth
		/// value
		/// </summary>
		private static void NamedNodeCount_internal(
			XmlNode nXmlCountNode, 
			string strNodeName,
			int maxDepth,
			int currentDepth,
			ref long lngResult
			)
		{

			// tallies up for "this" node
			if (currentDepth > maxDepth)
				return;
			else
				currentDepth ++;

			if (nXmlCountNode.Name == strNodeName)
				lngResult ++;

			// invokes this method for each child node of "this" node
			for (int i = 0 ; i < nXmlCountNode.ChildNodes.Count ; i ++)
				NamedNodeCount(
					nXmlCountNode.ChildNodes[i], 
					strNodeName,
					currentDepth,
					ref lngResult);
		}



		/// <summary> Counts how many nodes of a given 
		/// name have a given value </summary>
		public static void NamedAndValuedNodeCount(
			XmlNode nXmlCountNode, 
			string strNodeName, 
			string strNodeValue,
			ref long lngResult			
			)
		{
			// tallies up for "this" node - REDFLAG - innerxml not best property to use
			// here, because if there is a mixture of node value text + sub nodes on the 
			// current node, there will be a false negative
			if (nXmlCountNode.Name == strNodeName && nXmlCountNode.InnerXml == strNodeValue)
				lngResult ++;

			// invokes this method for each child node of "this" node
			for (int i = 0 ; i < nXmlCountNode.ChildNodes.Count ; i ++)
				NamedAndValuedNodeCount(
					nXmlCountNode.ChildNodes[i], 
					strNodeName,
					strNodeValue,
					ref lngResult);

		}

		

		/// <summary> Returns the summed value of the 
		/// specified node as a long </summary>
		/// <param name="nXmlDataSource"></param>
		/// <param name="strNodeName"></param>
		/// <param name="blnCheckIntegrity"></param>
		/// <param name="lngResult"></param>
		public static void NamedNodeValueTotal(
			XmlNode nXmlDataSource, 
			string strNodeName, 
			bool blnCheckIntegrity, 
			ref long lngResult
			)
		{
			
			// 1. gets byte total for this node
			if (nXmlDataSource.Name == strNodeName)
			{
				if (blnCheckIntegrity)
				{}

				lngResult += Convert.ToInt64(nXmlDataSource.InnerText);

				try
				{
				}
				catch
				{
				
				}
			}

			// 2. processes child nodes if necessary
			for (int i = 0 ; i < nXmlDataSource.ChildNodes.Count ; i ++)
				NamedNodeValueTotal(nXmlDataSource.ChildNodes[i], strNodeName, blnCheckIntegrity, ref lngResult);


		}
		
		

		/// <summary>
		/// Returns the contents of the given Xml document as indent-formatted text.
		/// Returned text is well-formed Xml
		/// </summary>
		/// <param name="doc"></param>
		/// <returns></returns>
		public static string IndentFormatXml(
			XmlDocument doc
			)
		{

			StringBuilder x = new StringBuilder();

			IndentFormatXml_internal(
				doc.DocumentElement,
				0,
				x);

			return x.ToString();

		}



		/// <summary>
		/// recursive method called by IndentFormatXml()
		/// </summary>
		/// <param name="node"></param>
		/// <param name="depth"></param>
		/// <param name="x"></param>
		private static void IndentFormatXml_internal(
			XmlNode node,
			int depth,
			StringBuilder x
			)
		{
			
			string attributes = "";
			string indent = " ";



				
			// ##############################################################################
			// builds up a string containing all attributes
			// ------------------------------------------------------------------------------
			for (int i = 0 ; i < node.Attributes.Count ; i ++)
				attributes += " "+  node.Attributes[i].Name + "='" + XmlLib.FilterForReservedXmlCharacters(node.Attributes[i].Value) +  "'";
														   

				
			// ##############################################################################
			// opening tag + attributes
			// ------------------------------------------------------------------------------
			x.Append(
				vcFramework.Parsers.StringFormatLib.CharLine(indent, depth) +
				"<" + node.Name + attributes + ">");
				

			// ##############################################################################
			// if this node has any entity chilren, those entities will need to appear on a
			// new line, and indented
			// ------------------------------------------------------------------------------
			bool hasAnElementChild = false;
				
			if (node.ChildNodes.Count > 0)
				foreach(XmlNode child in node.ChildNodes)
					if (child.NodeType == XmlNodeType.Element)
					{
						hasAnElementChild = true;
						break;
					}
			if (hasAnElementChild)
				x.Append("\r\n");
						


			// ##############################################################################
			// children - recursion happens here
			// ------------------------------------------------------------------------------
			foreach(XmlNode child in node.ChildNodes)
			{
				if (child.NodeType == XmlNodeType.Element)
					IndentFormatXml_internal(
						child,
						depth + 1,
						x);
				else
					x.Append(
						child.OuterXml);

			}


			// ##############################################################################
			// closing tag
			// ------------------------------------------------------------------------------
			// indent closing tag only if it there is not child node text infront of it
			if (hasAnElementChild)
				x.Append(vcFramework.Parsers.StringFormatLib.CharLine(indent, depth));

			// closing tags
			x.Append(
				"</" + node.Name + ">\r\n");

		}


		/// <summary>
		/// Transforms the given xml text using the given schema
		/// </summary>
		/// <param name="xml"></param>
		/// <param name="schema"></param>
		/// <returns></returns>
		public static string Transform(
			string xml, 
			string schema
			)
		{
			//read XML
			TextReader tr1=new StringReader(xml);
			XmlTextReader tr11=new XmlTextReader(tr1);
			XPathDocument xPathDocument=new XPathDocument(tr11);
    
			//read XSLT
			TextReader tr2=new StringReader(schema);
			XmlTextReader tr22=new XmlTextReader(tr2);
			XslTransform xslt = new XslTransform ();
			xslt.Load(tr22);
    
			//create the output stream
			StringBuilder sb=new StringBuilder();
			TextWriter tw=new StringWriter(sb);

			//xsl.Transform (doc, null, Console.Out);
			xslt.Transform(xPathDocument,null,tw);
    
			//get result
			return sb.ToString();
		}



	}
}
