///////////////////////////////////////////////////////////////
// BrowseMonkey - An offline filesystem browser              //
// Shukri Adams (shukri.adams@gmail.com)                     //
// https://github.com/shukriadams/browsemonkey               //
// MIT License (MIT) Copyright (c) 2014 Shukri Adams         //
///////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Windows.Forms;
using vcFramework.Threads;
using vcFramework.Windows.Forms;

namespace BrowseMonkey
{
	/// <summary> 
	/// Searches are conducted and results are displayed on this form
	/// </summary>
    public class SearchResults : Form
	{

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lvSearchResults = new vcFramework.Windows.Forms.ListViewSP();
			this.panel2 = new System.Windows.Forms.Panel();
			this.lblNoResults = new System.Windows.Forms.Label();
			this.sbResults = new System.Windows.Forms.StatusBar();
			this.sbpFoldersFound = new System.Windows.Forms.StatusBarPanel();
			this.sbpFilesFound = new System.Windows.Forms.StatusBarPanel();
			this.sbpTimeTaken = new System.Windows.Forms.StatusBarPanel();
			this.lblPopulating = new System.Windows.Forms.Label();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.sbpFoldersFound)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sbpFilesFound)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.sbpTimeTaken)).BeginInit();
			this.SuspendLayout();
			// 
			// lvSearchResults
			// 
			this.lvSearchResults.AllowKeyboardDeleteKeyDeletion = false;
			this.lvSearchResults.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.lvSearchResults.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lvSearchResults.FocusNewItemOnInsert = true;
			this.lvSearchResults.FullRowSelect = true;
			this.lvSearchResults.HideSelection = false;
			this.lvSearchResults.InsertPosition = vcFramework.Windows.Forms.ListViewSP.InsertPositions.Top;
			this.lvSearchResults.Location = new System.Drawing.Point(20, 116);
			this.lvSearchResults.LockWidthOnZeroWidthColumns = false;
			this.lvSearchResults.MultiSelect = false;
			this.lvSearchResults.Name = "lvSearchResults";
			this.lvSearchResults.Size = new System.Drawing.Size(136, 60);
			this.lvSearchResults.TabIndex = 1;
			this.lvSearchResults.View = System.Windows.Forms.View.Details;
			this.lvSearchResults.DoubleClick += new System.EventHandler(this.lvSearchResults_DoubleClick);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.lblPopulating);
			this.panel2.Controls.Add(this.lblNoResults);
			this.panel2.Controls.Add(this.lvSearchResults);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(176, 184);
			this.panel2.TabIndex = 3;
			// 
			// lblNoResults
			// 
			this.lblNoResults.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.lblNoResults.Location = new System.Drawing.Point(20, 60);
			this.lblNoResults.Name = "lblNoResults";
			this.lblNoResults.Size = new System.Drawing.Size(136, 48);
			this.lblNoResults.TabIndex = 3;
			this.lblNoResults.Text = "Search complete. There are no results to display.";
			this.lblNoResults.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// sbResults
			// 
			this.sbResults.Location = new System.Drawing.Point(0, 184);
			this.sbResults.Name = "sbResults";
			this.sbResults.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																						 this.sbpFoldersFound,
																						 this.sbpFilesFound,
																						 this.sbpTimeTaken});
			this.sbResults.ShowPanels = true;
			this.sbResults.Size = new System.Drawing.Size(176, 22);
			this.sbResults.SizingGrip = false;
			this.sbResults.TabIndex = 2;
			// 
			// sbpFoldersFound
			// 
			this.sbpFoldersFound.Text = "[folders]";
			this.sbpFoldersFound.Width = 80;
			// 
			// sbpFilesFound
			// 
			this.sbpFilesFound.Text = "[files]";
			this.sbpFilesFound.Width = 70;
			// 
			// sbpTimeTaken
			// 
			this.sbpTimeTaken.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.sbpTimeTaken.Text = "[time taken]";
			this.sbpTimeTaken.Width = 26;
			// 
			// lblPopulating
			// 
			this.lblPopulating.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.lblPopulating.Location = new System.Drawing.Point(36, 12);
			this.lblPopulating.Name = "lblPopulating";
			this.lblPopulating.Size = new System.Drawing.Size(100, 40);
			this.lblPopulating.TabIndex = 4;
			this.lblPopulating.Text = "Displaying search results ...";
			// 
			// SearchResults
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(176, 206);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.sbResults);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "SearchResults";
			this.Text = "Search Results :";
			this.Load += new System.EventHandler(this.SearchResults_Load);
			this.panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.sbpFoldersFound)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.sbpFilesFound)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.sbpTimeTaken)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		
		#region FIELDS

		private System.ComponentModel.Container components = null;
		private ListViewSP lvSearchResults;
		private System.Windows.Forms.StatusBar sbResults;
		private System.Windows.Forms.StatusBarPanel sbpFoldersFound;
		private System.Windows.Forms.StatusBarPanel sbpFilesFound;
		private System.Windows.Forms.StatusBarPanel sbpTimeTaken;		
		private System.Windows.Forms.Label lblNoResults;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label lblPopulating;

		/// <summary>
		/// 
		/// </summary>
		private ThreadCollection _threadCollection;

		/// <summary>
		/// 
		/// </summary>
		private string _searchArgs;
		

		/// <summary>
		/// Used to populate listview of test results
		/// </summary>
		private ArrayList _rows;

		/// <summary>
		/// 
		/// </summary>
		private TimeSpan _duration;

		#endregion
		

		#region CONSTRUCTORS
		
		/// <summary> 
		/// This object conducts searches - it therefore needs to be instantiated with all 
		/// the information required to conduct a search 
		/// </summary>
		public SearchResults(
			ArrayList rows,
			string searchArgs,
			TimeSpan duration
			)
		{

			InitializeComponent();

			_threadCollection = new ThreadCollection();
			_duration = duration;
			_rows = rows;
			_searchArgs = searchArgs;
			lvSearchResults.MultiSelect = false;

		}
		
		
		#endregion


		#region DESTRUCTORS
		
		/// <summary> </summary>
		/// <param name="disposing"></param>
		protected override void Dispose(
			bool disposing
			)
		{
			try
			{
				if( disposing )
				{
					// terminates all threads
					_threadCollection.AbortAndRemoveAllThreads();

					if(components != null)
						components.Dispose();

				}
				base.Dispose( disposing );
			}
			catch(Exception ex)
			{
				MainForm.ConsoleAdd(ex);			
			}

		}
		

		#endregion


		#region METHODS - Search

		
		/// <summary> 
		/// </summary>
		private void Search_GUIProcess(
			)
		{
			
			// ################################################
			// fill results listview
			// ------------------------------------------------
			lvSearchResults.Items.Clear();
			if (_rows.Count > 0)
				lvSearchResults.Items.AddRange(
                    (ListViewItem[])_rows.ToArray(typeof(ListViewItem)));
			
			lvSearchResults.SortByColumn("score");
			

			// ################################################
			// calculates various thing for status bar
			// ------------------------------------------------
			int files		= 0;
			int folders		= 0;

			foreach(ListViewItem item in lvSearchResults.Items)
				if (item.SubItems[2].Text == "Folder")
					folders ++;
				else if (item.SubItems[2].Text == "File")
					files ++;
			
			sbpFilesFound.Text = "Files : " + files;
			sbpFoldersFound.Text = "Folders : " + folders;	

			
			// search duration
			sbpTimeTaken.Text = "Time taken : ";
			
			if (_duration.Minutes > 0)
				sbpTimeTaken.Text += _duration.Minutes + " (m)";
			else if (_duration.Seconds > 0)
				sbpTimeTaken.Text += _duration.Seconds + " (s)";
			else
				sbpTimeTaken.Text += _duration.Milliseconds + " (ms)";;
			
					


			// ################################################
			// sets form contents visibility
			// ------------------------------------------------
			lblPopulating.Visible = false;
			if (lvSearchResults.Items.Count == 0)
				lblNoResults.Visible = true;
			else
				lvSearchResults.Visible = true;

		}




		#endregion


		#region METHODS - Misc

		/// <summary> 
		/// Launches a browser for the volume in which the selected search 
		/// result is found
		/// </summary>
		private void OpenVolumeForSelectedSearchResult(
			)
		{	

			string strVolumeFile		= "";
			string strItemToFocusPath	= "";
			bool blnVolumeAlreadyOpen	= true;

			// cannot open a volume for search result if more than
			// one result is selected
			if (lvSearchResults.SelectedItems.Count != 1)
				return;

			// gets name of volume in which search result was found
			strVolumeFile = lvSearchResults.SelectedItems[0].SubItems[1].Text;
				
			// gets array of all open browsers
			VolumeBrowser[] volumeBrowsers = FormFinderLib.GetVolumeBrowsers(
				strVolumeFile);

			// checks if that volume is already open
			if (volumeBrowsers.Length == 0)
				blnVolumeAlreadyOpen = false;
				
			// gets path of search result in volume data structure - this is needed to focus the correct result in the treeview
			strItemToFocusPath = lvSearchResults.SelectedItems[0].Tag.ToString();
				
			// spawns browser containing search result if it is not already open - note that the focus path is
			// passed to the form spawner. This path will be focused after the volumeExplorer user control
			// has finished filling the treeview. This step is necessary because the focus path cannot be passed to the 
			// usercontrol AFTER form instantiation, because form instantiation runs in its own thread, and there is no easy
			// way of waiting on this end for that thread to finish so the path can be passed.
			if (!blnVolumeAlreadyOpen)
			{
				FormFactory.SpawnVolumeBrowser(
					strVolumeFile, 
					strItemToFocusPath);

				// find the newly instantiated volumebrowser
				volumeBrowsers = FormFinderLib.GetVolumeBrowsers(
					strVolumeFile);

			}

			// focus tree node if - fínd the correct volumeBrowser first
			if (volumeBrowsers.Length == 1)
			{
				volumeBrowsers[0].Show();
				volumeBrowsers[0].FocusTreeNode(
					strItemToFocusPath);
			}

		}


		#endregion


		#region EVENTS


		/// <summary> 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SearchResults_Load(
			object sender, 
			EventArgs e
			)
		{
			
			try
			{

				// ################################################
				// set up listview
				// ------------------------------------------------
				lvSearchResults.ColumnsSet(
					MainForm.XmlListViewConfig.SelectSingleNode(".//listview [@name='searchResults']").ChildNodes);

			
				// ################################################
				// sets initial form appearance
				// ------------------------------------------------
				this.Text = "Search result : " + _searchArgs;

				lblNoResults.Dock = DockStyle.Fill;
				lvSearchResults.Dock = DockStyle.Fill;
				lblPopulating.Dock = DockStyle.Fill;
				lblNoResults.Visible = false;
				lvSearchResults.Visible = false;

				/*
				Thread th		= new Thread(new ThreadStart(Search_GUIProcess));
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
				*/
				Search_GUIProcess();

			}
			catch(Exception ex)
			{
				MainForm.ConsoleAdd(ex);		
			}

		}



		/// <summary> 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lvSearchResults_DoubleClick(
			object sender, 
			EventArgs e
			)
		{
			try
			{
				OpenVolumeForSelectedSearchResult();
			}
			catch(Exception ex)
			{
				MainForm.ConsoleAdd(ex);			
			}
		}

		#endregion


	}
}
