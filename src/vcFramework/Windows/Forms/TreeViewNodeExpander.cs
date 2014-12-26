//////////////////////////////////////////////////////////////////////////////////////
// Author				: Shukri Adams												//
// Contact				: shukri.adams@gmail.com										//
// Compiler requirement : .Net 4.0													//
//																					//
// vcFramework : A reuseable library of utility classes                             //
// Copyright (C)																	//
//																					//
// This program is free software; you can redistribute it and/or modify it under	//
// the terms of the GNU General Public License as published by the Free Software	//
// Foundation; either version 2 of the License, or (at your option) any later		//
// version.																			//
//																					//
// This program is distributed in the hope that it will be useful, but WITHOUT ANY	//
// WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A	//
// PARTICULAR PURPOSE. See the GNU General Public License for more details.			//
//																					//
// You should have received a copy of the GNU General Public License along with		//
// this program; if not, write to the Free Software Foundation, Inc., 59 Temple		//
// Place, Suite 330, Boston, MA 02111-1307 USA										//
//////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Windows.Forms;
using vcFramework.Parsers;

namespace vcFramework.Windows.Forms
{
	/// <summary> Static class containing methods for expanding treeview objects </summary>
	public class TreeViewNodeExpander
	{

		
		/// <summary> Expands nodes on a tree from a given 
		/// treepath string.  </summary>
		/// <param name="strTreePath"></param>
		/// <param name="strDelimiter"></param>
		/// <param name="objTreeView"></param>
		/// <param name="objCallBack"></param>
		public static void ExpandToNodePath(
			string strTreePath, 
			string strDelimiter, 
			TreeView objTreeView
			)
		{
			ExpandToNodePath(
				strTreePath, 
				strDelimiter, 
				objTreeView.Nodes);
		}


		
		/// <summary> Expands nodes on a tree from a given 
		/// treepath string. This overload takes a 
		/// treenodecollection,making it the starting point 
		/// for a recursive loop for a treeview object. 
		/// Recursing.</summary>
		/// <param name="strTreePath"></param>
		/// <param name="strDelimiter"></param>
		/// <param name="objNode"></param>
		private static void ExpandToNodePath(
			string strTreePath, 
			string strDelimiter, 
			TreeNodeCollection objNode
			)
		{

			// gets the node name for this level
			string strThisNodeName = "";
			bool blnEndOfPathReached = false;

			if (strTreePath.IndexOf(strDelimiter) != -1)
				strThisNodeName = ParserLib.ReturnUpto(strTreePath, strDelimiter);
			else
			{	
				blnEndOfPathReached = true;
				strThisNodeName = strTreePath;
			}
			 

			// sets start node
			for (int i = 0 ; i < objNode.Count ; i ++)
				if (objNode[i].Text == strThisNodeName)
				{
					objNode[i].TreeView.SelectedNode = objNode[i];
					objNode[i].EnsureVisible();
					
					// calls this method again - recursion happens here
					ExpandToNodePath(
						ParserLib.ReturnAfter(strTreePath, strDelimiter),
						strDelimiter,
						objNode[i]);

					break;
				}


		}



		/// <summary> Expands nodes on a tree from a given treepath string. Supports TreeNodes, which means
		/// typically it has to be called from the ExpandToNodePath() overload that takes a treenodecollection as 
		/// an argument. Recursing. </summary>
		/// <param name="strTreePath"></param>
		/// <param name="strDelimiter"></param>
		/// <param name="objNode"></param>
		private static void ExpandToNodePath(
			string strTreePath, 
			string strDelimiter, 
			TreeNode objNode
			)
		{
			// gets the node name for this level
			string strThisNodeName = "";
			bool blnEndOfPathReached = false;

			if (strTreePath.IndexOf(strDelimiter) != -1)
				strThisNodeName = ParserLib.ReturnUpto(strTreePath, strDelimiter);
			else
			{
				blnEndOfPathReached = true;
				strThisNodeName = strTreePath;
			}

			// sets start node
			for (int i = 0 ; i < objNode.Nodes.Count ; i ++)
			{
				if (objNode.Nodes[i].Text == strThisNodeName)
				{
					objNode.Nodes[i].Expand();
					objNode.TreeView.SelectedNode = objNode.Nodes[i];
					objNode.Nodes[i].EnsureVisible();
					
					
					// calls this method again - recursion happens here
					ExpandToNodePath(
						ParserLib.ReturnAfter(strTreePath, strDelimiter),
						strDelimiter,
						objNode.Nodes[i]);

					break;
				}

			}
		}
		

	}
}
