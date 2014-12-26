///////////////////////////////////////////////////////////////
// FSXmlWinUI - A library of Windows controls specifically   //  
// made for working with FSXml data.                         // 
// Shukri Adams (shukri.adams@gmail.com)                     //
// https://github.com/shukriadams/browsemonkey               //
// MIT License (MIT) Copyright (c) 2014 Shukri Adams         //
///////////////////////////////////////////////////////////////

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using vcFramework.Assemblies;
using vcFramework.DataItems;
using vcFramework.Delegates;
using vcFramework.Parsers;
using vcFramework.Windows.Forms;
using vcFramework.Xml;
using FSXml;

namespace FSXmlWinUI
{
	/// <summary>
	/// Windows Explorer-style control for FSXml data.
	/// </summary>
	public class FSXmlExplorer : System.Windows.Forms.UserControl
	{

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.pnlLeftContent = new System.Windows.Forms.Panel();
			this.tvFolders = new System.Windows.Forms.TreeView();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.pnlRightContent = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lblWait = new System.Windows.Forms.Label();
			this.lvFiles = new vcFramework.Windows.Forms.ListViewSP();
			this.statusBar = new System.Windows.Forms.StatusBar();
			this.sbpFilesCount = new System.Windows.Forms.StatusBarPanel();
			this.sbpSize = new System.Windows.Forms.StatusBarPanel();
			this.lblCurrentPath = new System.Windows.Forms.Label();
			this.pnlLeftContent.SuspendLayout();
			this.pnlRightContent.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.sbpFilesCount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sbpSize)).BeginInit();
			this.SuspendLayout();
			// 
			// pnlLeftContent
			// 
			this.pnlLeftContent.Controls.Add(this.tvFolders);
			this.pnlLeftContent.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnlLeftContent.Location = new System.Drawing.Point(0, 0);
			this.pnlLeftContent.Name = "pnlLeftContent";
			this.pnlLeftContent.Size = new System.Drawing.Size(152, 296);
			this.pnlLeftContent.TabIndex = 0;
			// 
			// tvFolders
			// 
			this.tvFolders.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tvFolders.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvFolders.ImageIndex = -1;
			this.tvFolders.Location = new System.Drawing.Point(0, 0);
			this.tvFolders.Name = "tvFolders";
			this.tvFolders.SelectedImageIndex = -1;
			this.tvFolders.Size = new System.Drawing.Size(152, 296);
			this.tvFolders.TabIndex = 0;
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(152, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 296);
			this.splitter1.TabIndex = 1;
			this.splitter1.TabStop = false;
			// 
			// pnlRightContent
			// 
			this.pnlRightContent.Controls.Add(this.panel1);
			this.pnlRightContent.Controls.Add(this.statusBar);
			this.pnlRightContent.Controls.Add(this.lblCurrentPath);
			this.pnlRightContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlRightContent.Location = new System.Drawing.Point(155, 0);
			this.pnlRightContent.Name = "pnlRightContent";
			this.pnlRightContent.Size = new System.Drawing.Size(685, 296);
			this.pnlRightContent.TabIndex = 2;
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Controls.Add(this.lblWait);
			this.panel1.Controls.Add(this.lvFiles);
			this.panel1.Location = new System.Drawing.Point(0, 16);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(684, 260);
			this.panel1.TabIndex = 3;
			// 
			// lblWait
			// 
			this.lblWait.Location = new System.Drawing.Point(168, 200);
			this.lblWait.Name = "lblWait";
			this.lblWait.TabIndex = 2;
			this.lblWait.Text = "Generating file list ...";
			this.lblWait.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// listview
			// 
			this.lvFiles.AllowKeyboardDeleteKeyDeletion = false;
			this.lvFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lvFiles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lvFiles.FocusNewItemOnInsert = true;
			this.lvFiles.FullRowSelect = true;
			this.lvFiles.HideSelection = false;
			this.lvFiles.InsertPosition = vcFramework.Windows.Forms.ListViewSP.InsertPositions.Top;
			this.lvFiles.Location = new System.Drawing.Point(80, 32);
			this.lvFiles.LockWidthOnZeroWidthColumns = false;
			this.lvFiles.Name = "listview";
			this.lvFiles.Size = new System.Drawing.Size(524, 140);
			this.lvFiles.TabIndex = 1;
			this.lvFiles.View = System.Windows.Forms.View.Details;
			// 
			// statusBar
			// 
			this.statusBar.Location = new System.Drawing.Point(0, 274);
			this.statusBar.Name = "statusBar";
			this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																						 this.sbpFilesCount,
																						 this.sbpSize});
			this.statusBar.ShowPanels = true;
			this.statusBar.Size = new System.Drawing.Size(685, 22);
			this.statusBar.SizingGrip = false;
			this.statusBar.TabIndex = 2;
			// 
			// sbpFilesCount
			// 
			this.sbpFilesCount.Text = "[Files]";
			this.sbpFilesCount.Width = 110;
			// 
			// sbpSize
			// 
			this.sbpSize.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.sbpSize.Text = "[Size]";
			this.sbpSize.Width = 575;
			// 
			// lblCurrentPath
			// 
			this.lblCurrentPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblCurrentPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblCurrentPath.Location = new System.Drawing.Point(0, 0);
			this.lblCurrentPath.Name = "lblCurrentPath";
			this.lblCurrentPath.Size = new System.Drawing.Size(684, 16);
			this.lblCurrentPath.TabIndex = 0;
			this.lblCurrentPath.Text = "CURRENTPATH";
			// 
			// FSXmlExplorer
			// 
			this.Controls.Add(this.pnlRightContent);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.pnlLeftContent);
			this.Name = "FSXmlExplorer";
			this.Size = new System.Drawing.Size(840, 296);
			this.pnlLeftContent.ResumeLayout(false);
			this.pnlRightContent.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.sbpFilesCount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.sbpSize)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion


		#region FIELDS

		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel pnlLeftContent;
		private System.Windows.Forms.Panel pnlRightContent;
		private System.Windows.Forms.StatusBar statusBar;
		private System.Windows.Forms.TreeView tvFolders;
		private System.Windows.Forms.Label lblCurrentPath;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.StatusBarPanel sbpFilesCount;
		private System.Windows.Forms.StatusBarPanel sbpSize;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label lblWait;

		/// <summary>
		/// 
		/// </summary>
		private ListViewSP lvFiles;
		
		/// <summary>
		/// The root node of the FSXml data structure this control will be used to "explore"
		/// </summary>
		private XmlNode _nFSXmlData;

		/// <summary>
		/// The number of node levels the treeview populator will "write ahead" when populating 
		/// tree. Ensures that the tree is populated X number of levels ahead of where the user
		/// currently is
		/// </summary>
		private const int _TREE_POPULATION_DEPTH = 2;

		/// <summary>
		/// Used when populating listview across threads
		/// </summary>
		private ListViewItem[] _rows;
		
		/// <summary>
		/// Treenode which triggered OnExpand event. Stored as class member
		/// because it needs to be passed to background thread
		/// </summary>
		private TreeNode _expandedNode;

		#endregion


		#region CONSTRUCTORS

		/// <summary>
		/// 
		/// </summary>
		public FSXmlExplorer(
			)
		{

			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();


			// ##############################################
			// sets up column structure of listview
			// ----------------------------------------------
			AssemblyAccessor assemblyAccessor = new AssemblyAccessor(
				System.Reflection.Assembly.GetAssembly(typeof(FSXmlExplorer)));

			XmlDocument dXmlListviewStructure = assemblyAccessor.GetXmlDocument(
				assemblyAccessor.RootName + ".Resources.ListviewConfig.xml");

			lvFiles.ColumnsSet(
				dXmlListviewStructure.DocumentElement.ChildNodes);


			// ##############################################
			// connects up event handlers
			// ----------------------------------------------
			this.Load += new EventHandler(FSXmlExplorer_Load);
			tvFolders.AfterSelect += new TreeViewEventHandler(tvFolders_AfterSelect);
			tvFolders.AfterExpand += new TreeViewEventHandler(tvFolders_AfterExpand);
			lvFiles.MultiSelect = false;
			lvFiles.SelectedIndexChanged += new EventHandler(listview_SelectedIndexChanged);
		}


		#endregion


		#region DESTRUCTORS

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
					components.Dispose();
			}
			base.Dispose( disposing );
		}


		#endregion

		
		#region METHODS


		/// <summary>
		/// Focuses a path in the FSXml data which was used to populate this control.
		/// The treeview will be opened to the selection, and if the item is available
		/// in the listview, it will be highlighted
		/// </summary>
		/// <param name="path"></param>
		public void FocusPath(
			string path
			)
		{
			// strPath is the path, delimited by "\\" ... however, in some cases the folder name itself can contain an "\\", such as 
			// drive names ("c:\") - this will mess up pathfinding in the tree, because the code looks for the first instance of the delimiter
			// , and in the case of a folder name the SECOND "\\" is the delimiter, and the first is actually part of the node name
			//strPath = strPath.Replace("\\\\", "\\" + strDelimiter);

			string strDelimiter = "¤";
			path = path.Replace("\\", strDelimiter);
			path = path.Replace(strDelimiter + strDelimiter, "\\" + strDelimiter);
			
			// opens tree view up to path			
			TreeViewNodeExpander.ExpandToNodePath(
				path,
				strDelimiter, 
				tvFolders);
			
			// focuses the file or folder in the listview
			string listviewValue = ParserLib.ReturnAfterLast(path, strDelimiter);
			if (listviewValue.Length > 0)
				foreach(ListViewItem item in lvFiles.Items)
					if (item.SubItems[0].Text == listviewValue)
					{
						item.Selected = true;
						lvFiles.Focus();
						lvFiles.EnsureVisible(item.Index);
						break;
					}


		}



		/// <summary> 
		// Fills the "files" listview to contain the children of whatever fsXML node is
		// currently selected in treeview. This method is private for a reason - it should
		// be called only by the event handler for the treeviews's "afterclick" event
		/// </summary>
		/// <param name="objFocusNode"></param>
		private void FillFilesListView(
			)
		{

			string strFullPath				= "";
			XmlNode nXmlCurrentNodeFromTree = null;
			TreeNode objFocusNode			= null;


			
			// ###########################################################
			// If can't get a node, no need to continue
			// -----------------------------------------------------------
			objFocusNode = tvFolders.SelectedNode;
			if (objFocusNode == null)
				return;


			// ###########################################################
			// hide listview, show "populating" button
			// -----------------------------------------------------------
			lvFiles.Visible = false;
			lblWait.Visible = true;


			// ###########################################################
			// first get of current treenode - path can be used to get
			// the Xmlnode in the XmlFileData which corresponds to the
			// current treenode
			// -----------------------------------------------------------
			strFullPath = objFocusNode.FullPath;

			nXmlCurrentNodeFromTree = FSXmlLib.GetNode(
				strFullPath, 
				_nFSXmlData, 
				NodeTypes.Folder);



			// ###########################################################
			// Now that we have the Xml node corresponding to the current
			// treenode, we can build a list of all folders and files 
			// under that node, and display that list in the listview
			// -----------------------------------------------------------
			XmlNode nXmlCurrentNode	= null;
			string strItemName		= "";
			ByteSizeItem strcSize;


			XmlNodeList files = nXmlCurrentNodeFromTree.SelectSingleNode(".//fs").ChildNodes;

			_rows = new ListViewItem[files.Count];

			for (int i = 0 ; i < files.Count ; i ++)
			{

				string[] rowContents = new string[5];
				nXmlCurrentNode = files[i];
				
				strItemName = XmlLib.RestoreReservedCharacters(
					nXmlCurrentNode.SelectSingleNode(".//n").InnerText);
					
				strcSize = new ByteSizeItem(
					Convert.ToInt64(nXmlCurrentNode.SelectSingleNode(".//s").InnerText));
	
				string path = XmlLib.RestoreReservedCharacters(FSXmlLib.GetFullPathForFile(nXmlCurrentNode, "\\"));
				
				rowContents[0] = strItemName;
				rowContents[1] = nXmlCurrentNode.SelectSingleNode(".//dc").InnerText;
				rowContents[2] = nXmlCurrentNode.SelectSingleNode(".//dm").InnerText;
				rowContents[3] = strcSize.BytesParsed + " " + strcSize.SizeUnit;
				rowContents[4] = Path.GetExtension(strItemName);
				
				_rows[i] = new ListViewItem(rowContents);
				_rows[i].Tag = path;

			}

				
			lvFiles.Items.Clear();


			// inserts all data as a background threaded process
			if (_rows.Length > 0)
				lvFiles.Items.AddRange(
					_rows);


			// set row colors on listview
			lvFiles.SetRowColors(
				Color.FromArgb(242,242,242),
				Color.FromArgb(253,253,253));


			// ###########################################################
			// updates files size and file count info and other labels
			// displaying info for current explorer contents
			// -----------------------------------------------------------
			sbpFilesCount.Text = "";
			if (lvFiles.Items.Count > 0)
				sbpFilesCount.Text = "Files : " + lvFiles.Items.Count;

			ByteSizeItem strByteSize = new ByteSizeItem(
				FSXmlLib.DirectoryNodeBytes(nXmlCurrentNodeFromTree, false));
			
			sbpSize.Text = "";
			if (strByteSize.Bytes > 0)
				sbpSize.Text = strByteSize.BytesParsed + " " + strByteSize.SizeUnit;


			// ###########################################################
			// updates various UI stuff
			// -----------------------------------------------------------
			//lblCurrentPath.Text = strFullPath;
			lvFiles.Visible = true;
			lblWait.Visible = false;
			
			// autoselect first item in listview
			if (lvFiles.Items.Count > 0)
				lvFiles.Items[0].Selected = true;

		}
		


		/// <summary>
		/// "Pseudo-constructor" for this user control. Sets the FSXml data the contrl will be
		/// used to explore, and automatically causes that xml data to be loaded into the 
		/// treeview and listview controls on explorer. This logic could not be embedded in the
		/// constructor of this control because it would break it's designer support
		/// </summary>
		public void Initialize(
			XmlNode nFSXmlData
			)
		{

			// set xmlnode member
			_nFSXmlData = nFSXmlData;
		
			// refills treeview, calling multithread-friendly refill method
			FillTree();

			// focuses on root node
			if (tvFolders.Nodes.Count > 0)
				tvFolders.SelectedNode = tvFolders.Nodes[0];

		}



		/// <summary>
		/// Multithread-friendly method for refilling treeview. This method can be called
		/// from any thread without it locking up the UI
		/// </summary>
		public void FillTree(
			)
		{
			
			// invoke update on tree - has to be done via a delegate to make this 
			// usercontrol multi-thread compliant
			WinFormActionDelegate dlgtProcess = new WinFormActionDelegate(FillTree_ThreadedProcess);
			tvFolders.Invoke(dlgtProcess);
		
		}



		/// <summary> 
		/// Populates treeview. Call this method only from the public RefillTree() method.
		/// Treeview is always populated "downstream" from the selected treenode. If there
		/// are no nodes in treeview, filling begins from the root of the fsxml data in
		/// in this control
		/// </summary>
		private void FillTree_ThreadedProcess(
			)
		{
			
			FSXmlTreeViewFiller treeFiller = null;

			// ######################################################
			// populates tree - creates a FSXml Treeview populator 
			// object which "spools" the FSXml out into a treeview
			// structure
			// ------------------------------------------------------
			if (tvFolders.Nodes.Count == 0)
				treeFiller = new FSXmlTreeViewFiller(
					tvFolders,
					_nFSXmlData,
					_TREE_POPULATION_DEPTH);
			else
			{
				FSXmlTreeNode node = null;

				if (_expandedNode != null)
					node = (FSXmlTreeNode)_expandedNode;
				else
					node = (FSXmlTreeNode)tvFolders.SelectedNode;

				string strFullPath = node.FullPath;

				XmlNode nXmlCurrentNodeFromTree = FSXmlLib.GetNode(
					strFullPath, 
					_nFSXmlData, 
					NodeTypes.Folder);

				treeFiller = new FSXmlTreeViewFiller(
					node,
					nXmlCurrentNodeFromTree,
					_TREE_POPULATION_DEPTH);	
			}

			treeFiller.FillStyle = FSXmlTreeViewFiller.FillStyles.Folders;

			treeFiller.Start();

			// note that we dont default focus the tree node because doing so
			// throws a null ref exception, possibly because treeview is usualy
			// not yet visible when we try to auto select a node

		}



		#endregion


		#region EVENTS

		/// <summary>
		/// 
		/// </summary>
		private void FSXmlExplorer_Load(
			object sender,
			EventArgs e
			)
		{
		
			lblWait.Dock = DockStyle.Fill;
			lvFiles.Dock = DockStyle.Fill;
			lblWait.Visible = false;

			// ###########################################
			// workaround to ensure that the first list
			// view column is shown ...
			// -------------------------------------------
			int width = 0;
			foreach(ColumnHeader header in lvFiles.Columns)
				if (header.Index != 0)
					width += header.Width;

			lvFiles.Columns[0].Width = lvFiles.Width - width;

		}


		/// <summary> 
		/// Invoked after a tree node is clicked - causes the listview to display the files in the 
		/// clicked treenode 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tvFolders_AfterSelect(
			object sender, 
			TreeViewEventArgs e
			)
		{

			try
			{

				Cursor.Current = Cursors.WaitCursor;

				if (tvFolders.SelectedNode != null)
				{	
					FillTree();
					FillFilesListView();
				}
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}		

		
		
		/// <summary> 
		/// Invoked after a treenode is expanded. Causes the treeview to "read ahead" in the 
		/// treeview structure, ie, build up nodes a few levels below the expanded node, 
		/// ensuring that if the user opens up a node below the seleced node, there will
		/// already be treeview stucture there
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tvFolders_AfterExpand(
			object sender, 
			TreeViewEventArgs e
			)
		{

			try
			{
				Cursor.Current = Cursors.WaitCursor;

				if (e.Node != null)
				{
					_expandedNode = e.Node;
					FillTree();
					_expandedNode = null;
				}
			}
			finally
			{
				Cursor.Current = Cursors.Default;
			}
		}
		
		

		/// <summary>
		/// Invoked when a treeview item is selected
		/// </summary>
		public void listview_SelectedIndexChanged(
			object sender,
			EventArgs e
			)
		{


			// ########################################
			// sets current path label contents
			// ----------------------------------------
			string path = "";



			if (lvFiles.SelectedItems.Count == 1)
			{
				if (tvFolders.SelectedNode != null)
					path = tvFolders.SelectedNode.FullPath;

				path += "\\" + lvFiles.SelectedItems[0].SubItems[0].Text;
			}

			lblCurrentPath.Text = path;


		}


		#endregion

	}
}
