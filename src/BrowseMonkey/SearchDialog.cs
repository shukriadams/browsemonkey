///////////////////////////////////////////////////////////////
// BrowseMonkey - An offline filesystem browser              //
// Shukri Adams (shukri.adams@gmail.com)                     //
// https://github.com/shukriadams/browsemonkey               //
// MIT License (MIT) Copyright (c) 2014 Shukri Adams         //
///////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using vcFramework.Delegates;
using vcFramework.Threads;
using vcFramework.Windows.Forms;
using vcFramework.Xml;
using BrowseMonkeyData;
using FSXml;

namespace BrowseMonkey
{
	/// <summary>
	/// Summary description for SearchDialog.
	/// </summary>
	public class SearchDialog : Form
	{

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.pgCurrentFileProgress = new System.Windows.Forms.ProgressBar();
			this.pgTotalProgress = new System.Windows.Forms.ProgressBar();
			this.lblCurerntFileName = new System.Windows.Forms.Label();
			this.lblTotalProgress = new System.Windows.Forms.Label();
			this.btnCancelSearch = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// pgCurrentFileProgress
			// 
			this.pgCurrentFileProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.pgCurrentFileProgress.Location = new System.Drawing.Point(8, 40);
			this.pgCurrentFileProgress.Name = "pgCurrentFileProgress";
			this.pgCurrentFileProgress.Size = new System.Drawing.Size(320, 8);
			this.pgCurrentFileProgress.TabIndex = 7;
			// 
			// pgTotalProgress
			// 
			this.pgTotalProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.pgTotalProgress.Location = new System.Drawing.Point(8, 72);
			this.pgTotalProgress.Name = "pgTotalProgress";
			this.pgTotalProgress.Size = new System.Drawing.Size(320, 8);
			this.pgTotalProgress.TabIndex = 10;
			// 
			// lblCurerntFileName
			// 
			this.lblCurerntFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblCurerntFileName.Location = new System.Drawing.Point(8, 8);
			this.lblCurerntFileName.Name = "lblCurerntFileName";
			this.lblCurerntFileName.Size = new System.Drawing.Size(320, 32);
			this.lblCurerntFileName.TabIndex = 11;
			this.lblCurerntFileName.Text = "File : [file name set in code]";
			// 
			// lblTotalProgress
			// 
			this.lblTotalProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblTotalProgress.Location = new System.Drawing.Point(8, 56);
			this.lblTotalProgress.Name = "lblTotalProgress";
			this.lblTotalProgress.Size = new System.Drawing.Size(320, 16);
			this.lblTotalProgress.TabIndex = 12;
			this.lblTotalProgress.Text = "Total progress : [set in code]";
			// 
			// btnCancelSearch
			// 
			this.btnCancelSearch.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnCancelSearch.Location = new System.Drawing.Point(132, 88);
			this.btnCancelSearch.Name = "btnCancelSearch";
			this.btnCancelSearch.TabIndex = 1;
			this.btnCancelSearch.Text = "Cancel";
			this.btnCancelSearch.Click += new System.EventHandler(this.btnCancelSearch_Click);
			// 
			// SearchDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(338, 120);
			this.Controls.Add(this.lblTotalProgress);
			this.Controls.Add(this.lblCurerntFileName);
			this.Controls.Add(this.pgTotalProgress);
			this.Controls.Add(this.pgCurrentFileProgress);
			this.Controls.Add(this.btnCancelSearch);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximumSize = new System.Drawing.Size(344, 144);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(344, 144);
			this.Name = "SearchDialog";
			this.Text = "Searching ...";
			this.Load += new System.EventHandler(this.SearchDialog_Load);
			this.ResumeLayout(false);

		}
		#endregion


		#region FIELDS

		private System.Windows.Forms.ProgressBar pgCurrentFileProgress;
		private System.Windows.Forms.ProgressBar pgTotalProgress;
		private System.Windows.Forms.Label lblCurerntFileName;
		private System.Windows.Forms.Label lblTotalProgress;
		private System.Windows.Forms.Button btnCancelSearch;
		private System.ComponentModel.Container components = null;

		
		/// <summary>
		/// 
		/// </summary>
		private ThreadCollection _threadCollection;

		/// <summary>
		/// 
		/// </summary>
		private string[] _searchArgs;
		
		/// <summary>
		/// 
		/// </summary>
		private string[] _searchFiles;

		/// <summary>
		/// 
		/// </summary>
		private bool _allowPartialMatches;
		
		/// <summary>
		/// Used to populate listview of test results
		/// </summary>
		private ArrayList _rows;
		
		/// <summary>
		/// Searching in Xml data done by this object
		/// </summary>
		private XmlSearch _searcher;
		
		/// <summary>
		/// wraps progress bar for total progress (all files) 
		/// </summary>
		private ProgressBarHelper _totalProgressHelper;

		/// <summary>
		/// wraps progress bar for current file progress 
		/// </summary>
		private ProgressBarHelper _currentProgressHelper;
		
		/// <summary> 
		/// Holds the name of the file currently being searched - used for thread-safe setting of 
		/// lblCurerntFileName.Text 
		/// </summary>
		private string _currentFile;

		/// <summary>
		/// Time that seach begins. used to calculate search duration
		/// </summary>
		private DateTime _searchStart;

		/// <summary>
		/// The curent file out of X files in the search - used to update the UI with
		/// progress information
		/// </summary>
		private int _currentFileIndex;

		
		/// <summary>
		/// Set to true if attempting to cancel the search
		/// </summary>
		private bool _cancelling;
		
		/// <summary>
		/// length of search 
		/// </summary>
		private TimeSpan _duration;
		
		/// <summary>
		/// 
		/// </summary>
		private string _searchFor;

		#endregion


		#region PROPERTIES

		/// <summary>
		/// Gets arraylist of listview rows representing search results
		/// </summary>
		public ArrayList ListViewRows
		{
			get
			{
				return _rows;
			}
		}


		/// <summary>
		/// Gets the text searched for
		/// </summary>
		public string SearchFor
		{
			get
			{
				return _searchFor;
			}
		}


		/// <summary>
		/// Gets the time taken to perform search
		/// </summary>
		public TimeSpan Duration
		{
			get
			{
				return _duration;
			}
		}

	
		/// <summary>
		/// 
		/// </summary>
		public bool Cancelled
		{
			get
			{
				return _cancelling;
			}
		}

		#endregion


		#region CONSTRUCTORS

		/// <summary>
		/// 
		/// </summary>
		/// <param name="searchArgs"></param>
		/// <param name="searchFiles"></param>
		/// <param name="allowPartialMatches"></param>
		public SearchDialog(
			string[] searchArgs,
			string[] searchFiles,
			bool allowPartialMatches
			)
		{

			InitializeComponent();
			this.ShowInTaskbar = false;

			_searchArgs = searchArgs;
			_allowPartialMatches = allowPartialMatches;
			_searchFiles = searchFiles;
			_threadCollection = new ThreadCollection();


		}


		#endregion

		
		#region METHODS

		/// <summary>
		/// Background process, called from Search(). Actual searching within volumes
		/// are done in this method. For obvious reasons this can be a slow process, which
		/// is why this method is meant to be invoked in its own thread space
		/// </summary>
		private void Search_BackgroundProcess(
			)
		{

			/* Quick description of how Xml search engine works : It is an object when 
			 * takes object references to exisint Xml data in memory, and returns an
			 * array of Xmlnode objects - these nodes are references to the specific
			 * nodes in the in-memory Xml where the search hits were made
			 * */

			string[] searchNodeNames			= {"n"};			// holds names of nodes to be searched,"n" = "name" fields.
			XmlNode[] searchNodes				= new XmlNode[1];	// holds only one node at a time
			Volume volume						= null;
			WinFormActionDelegate guiInvoker	= null;
			ArrayList results					= new ArrayList();
			XmlSearchResult[] rawResults		= null;
			ArrayList finalSearchFiles			= new ArrayList();

			// instantiate now - needs to be incase search is cancelled
			_rows = new ArrayList();

			// searches each file in given file list
			for (int i = 0 ; i < _searchFiles.Length; i ++)
			{

				// ##################################################
				// cancelling is implemented here, merely as stopping
				// the processing of additional volumes, and 
				// presenting results already generated
				// --------------------------------------------------
				if (_cancelling)
					break;


				// ##################################################
				// set field with file name - used to update
				// lblCurrentfilename.text
				// --------------------------------------------------
				_currentFile = _searchFiles[i];
				guiInvoker = new WinFormActionDelegate(UpdateCurrentFileLabel);
				lblCurerntFileName.Invoke(guiInvoker);



				// ##################################################
				// Tries to open a the current volume file for
				// searching. Note that is is the first place in the 
				// test process where validity of the file for
				// searching is tested - it is more economical to 
				// test once only, at this spot.
				// --------------------------------------------------
				try
				{
					volume = new Volume(
						_searchFiles[i]);
				}
				catch
				{
					if (volume != null)
						volume.Dispose();

					// if volume file opening failed it could because the volume
					// is not a volume at all, or just an incorrect version
					if (VolumeIdentificationLib.FileIsVolume(_searchFiles[i]))
					{
						// if reach here, file IS a valid volume, but the version is invalid.
						if (VolumeIdentificationLib.FileIsConvertable(_searchFiles[i]))
							MainForm.ConsoleAdd(
								Path.GetFileName(_searchFiles[i] + " is a valid volume, but is an older version which needs to be updated to make it readable. Opening the volume will convert it"));
						else
							MainForm.ConsoleAdd(
								Path.GetFileName(_searchFiles[i] + " is a valid volume, but requires a newer version of BrowseMonkey to open."));

					}
					
					// advance main progress bar 
					// (must done per  each file processed)
					_totalProgressHelper.PerformStep();

					// moves to next volume file in array
					continue;
				}


				


				// ##################################################
				// xml search engine searches in an array of xmlnodes.
				// for our needs though, we only want to search in
				// one node, the current Xml document. we do this 
				// to avoid having to load all the eventual search 
				// volumes into memory  at the same time
				// --------------------------------------------------
				searchNodes[0] = volume.VolumeData;

                
				_searcher = new XmlSearch(
					searchNodes,
					_searchArgs,			// array of text to search for
					_allowPartialMatches,	
					true,						// always ignore cases
					searchNodeNames);		// array of node names to search in
					


				// sets up progress bar helper object for currently processed file
				_currentProgressHelper = new ProgressBarHelper(
					pgCurrentFileProgress,
					_searcher.Steps);
				_searcher.OnNext += new EventHandler(_currentProgressHelper.PerformStep);


				// gets results back as xml node array
				// a null is returned if nothing is found
				rawResults =  _searcher.Search();
				
				foreach (XmlSearchResult result in rawResults)
				{
					results.Add(result);
					finalSearchFiles.Add(_searchFiles[i]);
				}

				// properly close volume
				volume.Dispose();

				// advance main progress bar 
				// (must done per  each file processed)
				_totalProgressHelper.PerformStep();

				_currentFileIndex ++;

			} // for			



			// ##################################################
			// builds up listview data, but these are added to
			// the lisview in the next method, ie, from the 
			// GUI thread. note that this process can still be
			// reached if search is cancelled
			// --------------------------------------------------
			ListViewItem[] items = new ListViewItem[results.Count];
			int j = 0;
			foreach (XmlSearchResult result in results)
			{
				
				if (_cancelling)
					break;

				string[] row = new string[4];
				string tag  = "";
				
				row[0] = XmlLib.RestoreReservedCharacters(result.Node.InnerText);
				row[1] = finalSearchFiles[j].ToString();
				row[3] = Math.Round(result.Score,0).ToString()+ "%";
					
				if (result.Node.ParentNode.Name == "d")
				{
					row[2] = "Folder";
					tag = FSXmlLib.GetFullPathForFolder(
						result.Node.ParentNode,
						@"\");
				}
				else
				{
					row[2] = "File";
					tag = FSXmlLib.GetFullPathForFile(
						result.Node.ParentNode,
						@"\");
				}


				ListViewItem item = new ListViewItem(row);
				item.Tag = tag;
				_rows.Add(item);
				
				j ++;
			}


			_duration = DateTime.Now - _searchStart;
			
			foreach(string arg in _searchArgs)
				_searchFor += arg + " ";


			// close up
			WinFormActionDelegate dlgt = new WinFormActionDelegate(Search_GUIProcess);
			this.Invoke(dlgt);

		}



		/// <summary>
		/// 
		/// </summary>
		private void Search_GUIProcess(
			)
		{
			this.Close();
		}



		/// <summary> 
		/// Updates GUI to show test progress. This method is meant to be invoked
		/// from background thread, and must therefore be called using a delegate
		/// </summary>
		private void UpdateCurrentFileLabel(
			)
		{
			lblCurerntFileName.Text = "Searching : " +  _currentFile;
			lblTotalProgress.Text = "Total progress : " +  "("+ (_currentFileIndex+1) + "/" + _searchFiles.Length + ")";
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
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion


		#region EVENTS

		/// <summary>
		/// Search process starts here. All search arguments need to have already been set via the 
		/// properties of this firm. This method invokes search process as seperate thread. 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SearchDialog_Load(
			object sender, 
			System.EventArgs e
			)
		{

			Thread th = null;

			// ########################################################################
			// note that no checks are done here for validity of search arguments - all 
			// testing is done in the Search form, and it assumed once the args get 
			// to here, they are valid.
			// ------------------------------------------------------------------------

			_searchStart = DateTime.Now;
			
			// sets up "total" progress bar, based on how many
			// files must be progressed
			_totalProgressHelper = new ProgressBarHelper(
				pgTotalProgress,
				_searchFiles.Length);


			lblCurerntFileName.Text = "Searching : ";
			lblTotalProgress.Text = "Total progress : ";

			
			
			th				= new Thread(new ThreadStart(Search_BackgroundProcess));
			th.Name			= "BackgroundSearchProcess";	
			th.IsBackground = true;
			th.Priority		= System.Threading.ThreadPriority.Normal;
			
			_threadCollection.AddThread(th);


			// ########################################################################
			// search process is wrapped in this try-catch block to trap exceptions
			// thrown during cancels. Canceling is somewhat "brutal", killing the 
			// thread on which the search is running. This has been known to throw
			// exceptions, such as the volume exception when cancelling occurs while
			// instantiationg a volume object. To avoid having to write a lot code to
			// handle exceptions during cancellation, we simply trap and suppress all
			// exceptions thrown during cancellation
			// ------------------------------------------------------------------------
			try
			{
				th.Start();
			}
			catch(Exception ex)
			{
				if (!_cancelling)
					throw ex;
			}
		}



		/// <summary> 
		/// Cancels the current search, if it can be cancelled 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCancelSearch_Click(
			object sender, 
			EventArgs e
			)
		{
			try
			{
				
				// sets class member. When true, no additional
				// volumes are searched, and results already
				// generated are presented
				btnCancelSearch.Enabled = false;
				_cancelling = true;


			}
			catch(Exception ex)
			{
				MainForm.ConsoleAdd(ex);			
			}

		}


		#endregion



	}
}
