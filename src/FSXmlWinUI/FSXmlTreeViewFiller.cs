///////////////////////////////////////////////////////////////
// FSXmlWinUI - A library of Windows controls specifically   //  
// made for working with FSXml data.                         // 
// Shukri Adams (shukri.adams@gmail.com)                     //
// https://github.com/shukriadams/browsemonkey               //
// MIT License (MIT) Copyright (c) 2014 Shukri Adams         //
///////////////////////////////////////////////////////////////

using System;
using System.Xml;
using System.Windows.Forms;
using vcFramework;
using vcFramework.Assemblies;
using vcFramework.Delegates;
using vcFramework.Xml;

namespace FSXmlWinUI
{
	/// <summary> 
	/// Populates a standard Windows Treeview with FSXml data
	/// </summary>
	public class FSXmlTreeViewFiller : IProgress
	{

		#region FIELDS

		/// <summary>
		/// Default is -1, which means "all levels", ie depth is ignored. Holds the depth in 
		/// the xml data tree view will be populated to. This is used to 
		/// control how much fsxml data is converted to treeview nodes. This aids in building
		/// up the treeview structure progressively as a user opens up the node structure. There
		/// is no need to build up the entire eventual treeview in one go, as this may take 
		/// too long for very large Fsxml data blocks. Instead we can "read ahead" using depth
		/// and current node to ensure that all the tree nodes X levels ahead of where the user
		/// currently is situated are populated
		/// </summary>
		private int _depth;

		/// <summary> 
		/// Invoked when treefill object finishes populating a tree 
		/// </summary>
		public event EventHandler OnNext;

		/// <summary> 
		/// Invoked for each step of treeview populating 
		/// </summary>
		public event EventHandler OnEnd;

		/// <summary>
		/// 
		/// </summary>
		private FillStyles _fillStyle;

		/// <summary>
		/// When starting with an empty treeview, this is the treeview object to append
		/// nodes to. If not working with an empty treeview, need to add nodes to an
		/// existing node, in which case nodes will be appended to the node _treeNode
		/// </summary>
		private TreeView _treeView;

		/// <summary>
		/// When starting with a treeview that already has nodes in it, we use a specific node
		/// in that treeview to append new nodes to - this node reference is that node.
		/// </summary>
		private FSXmlTreeNode _treeNode;

		/// <summary>
		/// FSXml data we will be using to populate treeview with
		/// </summary>
		private XmlNode _fsXMLData;

		/// <summary>
		/// Set to true if the running process should be stopped
		/// </summary>
		private bool _aborting;
		
		/// <summary>
		/// Set to true if the process in this object is still running
		/// </summary>
		private bool _running;

		/// <summary>
		/// Holds the current iteration out _steps (which is the total
		/// iterations to complete the running process)
		/// </summary>
		private long _currentStep;

		/// <summary>
		/// Holds the total number of steps needed to complete the 
		/// projected running process
		/// </summary>
		private long _steps;

		/// <summary> 
		/// </summary>
		public enum FillStyles : int
		{
			Folders,
			FoldersAndFiles
		}


		
		#endregion
			

		#region PROPERTIES

		/// <summary> 
		/// Gets number of folders to process during current populating  
		/// </summary>
		public long FoldersToProcess
		{
			get
			{
				return _steps;
			}
		}

		

		/// <summary>
		/// Gets the number of levels ahead of where the user currently is,
		/// to which the treeview structure will be built. 
		/// </summary>
		public int Depth
		{
			get
			{
				return _depth;
			}
		}

		

		/// <summary>
		/// Gets or sets the fillstyle for this object - fillstyle can be either
		/// folders, or folders + files. Controls what treeview will be filled with
		/// </summary>
		public FillStyles FillStyle
		{
			get
			{
				return _fillStyle;
			}
			set
			{
				_fillStyle = value;
			}
		}

		
		
		/// <summary>
		/// Gets the total number of iterations needed to finish the operation for this 
		/// object
		/// </summary>
		public long Steps
		{
			get
			{
				return _steps;
			}
		}

		
		/// <summary>
		/// Gets the current iteration out of the total number of iterations projected
		/// to complete the operation for this object
		/// </summary>
		public long CurrentStep
		{
			get
			{
				return _currentStep;
			}
		}

		
		/// <summary>
		/// Gets if this object is still busy processing
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
		/// Constructor for a FSXmlTreeViewFiller object which fills and empty treeview. 
		/// Nodes will be added to the treeview object
		/// </summary>
		/// <param name="treeview"></param>
		/// <param name="fsxmlData"></param>
		/// <param name="depth">Set to -1 to use all available layers</param>
		public FSXmlTreeViewFiller(
			TreeView treeview,
			XmlNode fsxmlData,
			int depth
			)
		{
		
			if (depth < -1)
				throw new Exception(
					"Depth cannot be less than -1");

			if (depth < 0)
				throw new Exception(
					"Depth cannot be 0. Set depth to -1 to use all levels, or a number greater than 1 to specify the number of levels to use");

			_treeView = treeview;
			_fsXMLData = fsxmlData;
			_depth = depth;

			// counts how many nodes (directory) must be processed
			if (depth == -1)
				XmlLib.NamedNodeCount(
					_fsXMLData, 
					"d",
					ref _steps);
			else
				XmlLib.NamedNodeCount(
					_fsXMLData, 
					"d",
					_depth,
					ref _steps);
			

		}



		/// <summary>
		/// Constructor for an FSXmlTreeViewFiller which appends new nodes to an existing
		/// node, ie, a treenode in a treeview which has already been populated, and to
		/// which further content will be added
		/// </summary>
		public FSXmlTreeViewFiller(
			FSXmlTreeNode treeNode,
			XmlNode fsxmlData,
			int depth
			)
		{

			if (depth < -1)
				throw new Exception(
					"Depth cannot be less than -1");

			if (depth < 0)
				throw new Exception(
					"Depth cannot be 0. Set depth to -1 to use all levels, or a number greater than 1 to specify the number of levels to use");

			
			_treeNode = treeNode;
			_fsXMLData = fsxmlData;
			_depth = depth;

			// counts how many nodes (directory) must be processed
			if (depth == -1)
				XmlLib.NamedNodeCount(
					_fsXMLData, 
					"d",
					ref _steps);
			else
				XmlLib.NamedNodeCount(
					_fsXMLData, 
					"d",
					_depth,
					ref _steps);

		}
		


		#endregion


		#region METHODS


		/// <summary> 
		/// Begins processing
		/// /// </summary>
		public void Start(
			)
		{
		
			_running = true;

			if (_treeView != null)
			{
				
				// ##############################################################
				// builds imagelist for treeview
				// --------------------------------------------------------------
				// loads default icons for treeview
				AssemblyAccessor assemblyAccessor = new AssemblyAccessor(
					System.Reflection.Assembly.GetAssembly(typeof(FSXmlTreeViewFiller)));

				ImageList imageList = new ImageList();
				_treeView.ImageList = imageList;

				// note that positions in which these items are loaded is importnat
				// 1 = unselected folder
				// 2 = selected folder
				// 3 = unselected file
				// 4 = selected file
				imageList.Images.Add(assemblyAccessor.GetIcon(assemblyAccessor.RootName + ".Resources.FolderUnselected.ico"));
				imageList.Images.Add(assemblyAccessor.GetIcon(assemblyAccessor.RootName + ".Resources.FolderSelected.ico"));
				imageList.Images.Add(assemblyAccessor.GetIcon(assemblyAccessor.RootName + ".Resources.FileUnselected.ico"));
				imageList.Images.Add(assemblyAccessor.GetIcon(assemblyAccessor.RootName + ".Resources.FileSelected.ico"));

			
			
				// ###############################################
				// manually add the first (root) node to the 
				// treeview
				// -----------------------------------------------
				// gets name of root node
				FSXmlTreeNode objNode		= new FSXmlTreeNode(_fsXMLData.SelectSingleNode(".//n").InnerText); // use first "name" node in fsxml as name for treeview node
				objNode.NodeType			= FSXmlTreeNodeTypes.Folder;
				objNode.ImageIndex			= 0;	// NOTE - these index positions are reserved - see notes
				objNode.SelectedImageIndex	= 1;	// NOTE - these index positions are reserved - see notes

				_treeView.Nodes.Add(objNode);

				
				// ###############################################
				// starts recursive process that assigns all child 
				// nodes to treeview. note that nodes are appended
				// to the root node which was just created
				// -----------------------------------------------
				_treeView.BeginUpdate();

				ProcessTreeLevel(
					_fsXMLData, 
					objNode, 
					0);

				_treeView.EndUpdate();

			}
			else
			{

				// ###############################################
				// begins adding data to a specific node
				// -----------------------------------------------
				_treeNode.TreeView.BeginUpdate();

				ProcessTreeLevel(
					_fsXMLData, 
					_treeNode, 
					0);


				_treeNode.TreeView.EndUpdate();

			}



			// ###############################################
			// 
			// -----------------------------------------------			
			DelegateLib.InvokeSubscribers(
				OnEnd, 
				this);


			_running = false;
			_aborting = false;

		}


		
		/// <summary>
		/// Aborts the tree filling process if it is still running
		///  </summary>
		public void Stop(
			)
		{
			_aborting = true;
		}



		/// <summary> 
		/// Recursive-compliant method : renders a level on treeview from xmlNode 
		/// </summary>
		private void ProcessTreeLevel(
			XmlNode nXmlTreeLevel, 
			TreeNode parentNode, 
			int currentDepth
			)
		{
			
			int i = 0;
			bool nodePopulated = false;
			string newNodeName = "";
			FSXmlTreeNode fileNode = null;
			FSXmlTreeNode folderNode = null;

				
			// fires advance step event hanlder
			DelegateLib.InvokeSubscribers(
				OnNext, 
				this);

			_currentStep ++;
			


			// ##################################################
			// handles aborting the running process
			// --------------------------------------------------
			if (_aborting)
				return;
			


			// ##################################################
			// sets depth limit
			// --------------------------------------------------
			if (currentDepth >= _depth)
				return;


			// ##################################################
			// --------------------------------------------------
			nodePopulated = false;
			if (parentNode.Nodes.Count > 0)
				nodePopulated = true;

			for (i = 0 ; i < nXmlTreeLevel.SelectSingleNode(".//ds").ChildNodes.Count ; i ++)
			{
						
				//creates a new node object
				if (!nodePopulated)
				{
					// creates new node
					newNodeName = nXmlTreeLevel.SelectSingleNode(".//ds").ChildNodes[i].SelectSingleNode(".//n").InnerText;
					folderNode = new FSXmlTreeNode(newNodeName);
					folderNode.NodeType = FSXmlTreeNodeTypes.Folder;
			
					// assigns icons to treenode if necessary
					folderNode.ImageIndex			= 0;	// NOTE - these index positions are reserved - see notes
					folderNode.SelectedImageIndex	= 1;	// NOTE - these index positions are reserved - see notes

					parentNode.Nodes.Add(folderNode);

				}
				else
					folderNode = (FSXmlTreeNode)parentNode.Nodes[i];


				
				// processes children in current node - recursion happens here
				ProcessTreeLevel(
					nXmlTreeLevel.SelectSingleNode(".//ds").ChildNodes[i], 
					folderNode, 
					currentDepth + 1);

			} //for



			// ##################################################
			// ADDS FILE ITEMS - processed only if fill style 
			// requires that files to be rendered as well
			// --------------------------------------------------
			if (this.FillStyle == FillStyles.FoldersAndFiles)
				for (i = 0 ; i < nXmlTreeLevel.SelectSingleNode(".//fs").ChildNodes.Count ; i ++)
				{

					fileNode						= new FSXmlTreeNode(nXmlTreeLevel.SelectSingleNode(".//fs").ChildNodes[i].SelectSingleNode(".//n").InnerText);
					fileNode.NodeType			= FSXmlTreeNodeTypes.File;
					fileNode.ImageIndex			= 2;	// NOTE - these index positions are reserved - see notes
					fileNode.SelectedImageIndex	= 3;	// NOTE - these index positions are reserved - see notes
					parentNode.Nodes.Add(fileNode);

				}


		}
		

		#endregion

	}
}
