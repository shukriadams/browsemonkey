///////////////////////////////////////////////////////////////
// BrowseMonkey - An offline filesystem browser              //
// Shukri Adams (shukri.adams@gmail.com)                     //
// https://github.com/shukriadams/browsemonkey               //
// MIT License (MIT) Copyright (c) 2014 Shukri Adams         //
///////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading; 
using System.Windows.Forms;
using System.Xml;
using vcFramework.Assemblies;
using vcFramework.Collections;
using vcFramework.Delegates;
using vcFramework.Diagnostics;
using vcFramework.Interfaces;
using vcFramework.Interop;
using vcFramework.Windows.Forms;
using vcFramework.Xml;
using WeifenLuo.WinFormsUI.Docking;
using BrowseMonkeyData;

namespace BrowseMonkey
{

	/// <summary> 
	/// Main form of application - doesn't contain a lot of code specific to the 
	/// application. Mostly holds everything together, handles app start and app 
	/// close, etc etc. 
	/// </summary>
	public class MainForm : Form
	{

		#region Windows Form Designer generated code
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(256, 222);
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.Text = "[Set in code]";
            this.Load += new System.EventHandler(this.MainForm_Load);

            // set up menus
            _topMenu = new MenuStrip();
            _topMenu.Dock = DockStyle.Top;
            _dividerMenu = new ToolStripMenuItem("-");

            // top level Menu items
            _fileMenu = new ToolStripMenuItem("&File");
            _viewMenu = new ToolStripMenuItem("&View");
            _helpMenu = new ToolStripMenuItem("&Help");

            _topMenu.Items.AddRange(new ToolStripMenuItem[]{
																	 _fileMenu, 
																	 _viewMenu, 
																	 _helpMenu
																 });


            //	FILE
            _newVolumeMenu = new ToolStripMenuItem("&New Volume");
            _openVolumeMenu = new ToolStripMenuItem("&Open Volume");
            _closeAllMenu = new ToolStripMenuItem("&Close All");
            _exitMenu = new ToolStripMenuItem("E&xit");
            _saveMenu = new ToolStripMenuItem("&Save");
            _saveAsMenu = new ToolStripMenuItem("S&ave As");
            _exportMenu = new ToolStripMenuItem("&Export");
            _recentVolumesMenu = new ToolStripMenuItem("Open &Recent");

            _newVolumeMenu.ShortcutKeys = Keys.Control | Keys.N;
            _openVolumeMenu.ShortcutKeys = Keys.Control | Keys.O;
            _saveMenu.ShortcutKeys = Keys.Control | Keys.S;

            _closeAllMenu.Enabled = false;
            _saveMenu.Enabled = false;
            _saveAsMenu.Enabled = false;
            _exportMenu.Enabled = false;
            _recentVolumesMenu.Enabled = false;

            _newVolumeMenu.Click += new EventHandler(mnuNewVolume_Click);
            _openVolumeMenu.Click += new EventHandler(OpenVolume);
            _exitMenu.Click += new EventHandler(mnuQuit_Click);
            _saveMenu.Click += new EventHandler(mnuSave_Click);
            _saveAsMenu.Click += new EventHandler(mnuSaveAs_Click);
            _closeAllMenu.Click += new EventHandler(mnuCloseAll_Click);

            _fileMenu.DropDownItems.AddRange(new ToolStripMenuItem[]{
		        _newVolumeMenu,
		        _openVolumeMenu,
		        _recentVolumesMenu,
		        _closeAllMenu,
		        _dividerMenu,
		        _saveMenu,
		        _saveAsMenu,
		        _exportMenu,
		        _dividerMenu,
		        _exitMenu
	        });


            //	EXPORT
            _exportToFlatTextMenu = new ToolStripMenuItem("&Text");
            _exportToXmlMenu = new ToolStripMenuItem("&Xml");

            _exportToFlatTextMenu.Click += new EventHandler(mnuExportToFlatText_Click);
            _exportToXmlMenu.Click += new EventHandler(mnuExportToXml_Click);

            _exportMenu.DropDownItems.AddRange(new ToolStripMenuItem[]{
			    _exportToFlatTextMenu,
			    _exportToXmlMenu
		    });

            //	VIEW
            _consoleMenu = new ToolStripMenuItem("&Console");
            _searchMenu = new ToolStripMenuItem("&Search");

            _consoleMenu.Click += new EventHandler(mnuConsole_Click);
            _searchMenu.Click += new EventHandler(mnuSearch_Click);

            _viewMenu.DropDownItems.AddRange(new ToolStripMenuItem[]{
				_consoleMenu,
				_searchMenu
			});

            //	HELP
            _aboutMenu = new ToolStripMenuItem("&About");
            _aboutMenu.Click += new EventHandler(mnuAbout_Click);
            _helpMenu.DropDownItems.AddRange(new ToolStripMenuItem[]{
			    _aboutMenu
			});

            this.MainMenuStrip = _topMenu;
            this.Controls.Add(_topMenu);

            this.ResumeLayout(false);
            this.PerformLayout();

		}
		
		#endregion
		

		#region FIELDS

		private readonly Container _components = null;
		

		// MENUS--------------------------------------
        private MenuStrip _topMenu;
        private ToolStripMenuItem _fileMenu;
        private ToolStripMenuItem _newVolumeMenu;
        private ToolStripMenuItem _openVolumeMenu;
        private ToolStripMenuItem _closeAllMenu;
        private ToolStripMenuItem _exitMenu;
        private ToolStripMenuItem _viewMenu;
        private ToolStripMenuItem _dividerMenu;
        private ToolStripMenuItem _consoleMenu;
        private ToolStripMenuItem _searchMenu;
        private ToolStripMenuItem _helpMenu;
        private ToolStripMenuItem _aboutMenu;
        private ToolStripMenuItem _saveMenu;
        private ToolStripMenuItem _saveAsMenu;
        private ToolStripMenuItem _exportMenu;
        private ToolStripMenuItem _exportToFlatTextMenu;
        private ToolStripMenuItem _exportToXmlMenu;
        private ToolStripMenuItem _recentVolumesMenu;
		//--------------------------------------------------------------------------

		
		/// <summary>
		/// 
		/// </summary>
		private static DockPanel _dockManager;
	
		/// <summary> 
		/// Holds the array of file names application is started with. other
		/// arguments are filtered from list in the Main() method 
		/// </summary>
		private static string[] _fileArgs;

		/// <summary> 
		/// String messenger object used to send the names of volume files to an 
		/// already-existing instance of this application. 
		/// </summary>
		private static 	StringMessenger _stringMessenger;
		
		/// <summary> 
		/// Static reference to the instantiated copy of this form - 
		/// static ref is needed by static methods in this class. 
		/// </summary>
		private static MainForm _this;

		/// <summary>
		/// Application-wide assembly accessor. Singleton. Exposed via 
		/// static property of this form
		/// </summary>
		static private AssemblyAccessor _assemblyAccessor;
		
		/// <summary> 
		/// Xml document containing listview configurations for several listviews
		/// in app. This doc is loaded up from an embedded assembly document.
		/// Singleton. Exposed via static property of this form
		/// </summary>
		static private XmlDocument m_dXmlListviewConfig = new XmlDocument();

		/// <summary>
		/// Set to true if application has been started in debug mode. This is not
		/// the same debug mode as visual studios. A release build can also be 
		/// started in debug mode, and would exhibit the samed debuggin behaviour.
		/// </summary>
		private static bool _debugMode;

		/// <summary>
		/// Used to store console messages which switching between threads.
		/// </summary>
		private static string _consoleMessage;

		/// <summary>
		/// Used to store exceptions for the console while switchign between
		/// threads
		/// </summary>
		private static Exception _e;
		
		/// <summary>
		/// Collection holding links to recently opened files. Singleton. Exposed
		/// via static property.
		/// </summary>
		private static FSLinksCollection _recentVolumes;
		
		/// <summary>
		/// Collection for holding links to recently searched folders
		/// </summary>
		private static FSLinksCollection _recentSearchFolders;

		/// <summary>
		/// Used to store the state of various objects. Singleton. Exposed via 
		/// static property of this form
		/// </summary>
		private static StateHolder _stateholder;

		/// <summary>
		/// The number of unsaved volumes open at any given time. Incremented 
		/// by 1 each time a new volume is created. When all unsaved volumes
		/// are either saved or destroyed, the number is set to 0 again.
		/// </summary>
		private static int _unsavedVolumesCount;

		#endregion
	

		#region PROPERTIES

		/// <summary>
		/// Gets or sets the number of unsaved volumes currently open. This number is reset to zero
		/// only when there are no more open unsaved volumes
		/// </summary>
		public static int UnsavedVolumesCount
		{
			get
			{
				return _unsavedVolumesCount;
			}
			set
			{
				_unsavedVolumesCount = value;
			}
		}


		/// <summary>
		/// Gets the instance of the applications's stateholder
		/// </summary>
		public static StateHolder StateBag
		{
			get
			{
				return _stateholder;
			}
		}


		/// <summary> 
		/// 
		/// </summary>
        public static DockPanel DockingManager
		{
			get
			{
				return _dockManager;
			}
		}

	
		/// <summary> 
		/// Exposes xml document with volume explorer list view config 
		/// </summary>
		public static XmlDocument XmlListViewConfig
		{
			get
			{
				return m_dXmlListviewConfig;
			}
		}
		

		/// <summary> 
		/// Gets  
		/// </summary>
		public static MainForm Instance
		{
			get
			{
				return _this;
			}
		}
		

		/// <summary> 
		/// Gets the application-wide assembly accessor
		/// </summary>
		public static AssemblyAccessor AssemblyAccessor
		{
			get
			{
				return _assemblyAccessor;
			}
		}


		/// <summary>
		/// Gets a collection of recently opened volumes
		/// </summary>
		public static FSLinksCollection RecentVolumes
		{
			get
			{
				return _recentVolumes;
			}
		}


		/// <summary>
		/// Gets a collection of recently searched folders. This only 
		/// applies to searches where the entire folder is specifed
		/// as a volume file holder
		/// </summary>
		public static FSLinksCollection RecentSearchFolders
		{
			get
			{
				return _recentSearchFolders;
			}
		}
		

		/// <summary>
		/// Gets the currently selected volumebrowser in the mdiparent.
		/// returns null if no volumebrowser is selected
		/// </summary>
		public static VolumeBrowser ActiveVolumeBrowser
		{
			get
			{
				if (_dockManager.ActiveDocument is VolumeBrowser)
					return (VolumeBrowser)_dockManager.ActiveDocument;

				return null;
			}
		}



		#endregion


		#region CONSTRUCTORS

		/// <summary>
		/// Constructor for MainForm. Note that very little application start
		/// logic is kept here. Most start logic is placed in the onload event
		/// handler for this form
		/// </summary>
		public MainForm(
			)
		{
			
			InitializeComponent();


			// ############################################################
			// sets up Weifen's docking manager. all dockable content in
			// application will be added to this manager. note that manager
			// fills the client area of the mainform
			// ------------------------------------------------------------
            _dockManager = new DockPanel();
			_dockManager.ActiveAutoHideContent = null;
			_dockManager.Dock = System.Windows.Forms.DockStyle.Fill;
			_dockManager.Location = new System.Drawing.Point(0, 28);
			_dockManager.Name = "dockManager";
			_dockManager.TabIndex = 1;

			this.Controls.Add(
				_dockManager);


			// ############################################################
			// sets properties
			// ------------------------------------------------------------
			this.Size = new Size(700,600);	// default size
			this.AllowDrop		= true;


			// ############################################################
			// sets events
			// ------------------------------------------------------------
			this.DragEnter	+= new DragEventHandler(this_DragEnter);
			this.DragDrop	+= new DragEventHandler(this_DragDrop);


		}


		#endregion


		#region DESTRUCTORS

		/// <summary> 
		/// Clean up any resources being used. 
		/// </summary>
		protected override void Dispose(
			bool disposing
			)
		{

			
			bool proceed	= true;
			IDirty[] dirty				= FormFinderLib.GetIDirty();


			// ############################################################
			// explicitly tries to close all forms which can be dirty. if
			// there are still forms open after attempting to shut them
			// all down, application shutdown aborts
			// ------------------------------------------------------------
			for (int i = 0 ; i < dirty.Length ; i ++)
				if (dirty[i].Dirty)
				{
					dirty[i].Dispose();

					if (dirty[i].Created)
					{
						proceed = false;
						break;
					}
				}



			// ############################################################
			// shut down app here
			// ------------------------------------------------------------
			if (proceed && disposing)
			{
				// saves this forms state
				_stateholder.Add("MainForm.Location", this.Location);
				_stateholder.Add("MainForm.Size", this.Size);
				_stateholder.Add("MainForm.WindowState", this.WindowState);
				
				// saves weifen lo's dock manager state
				string configFile = Application.StartupPath + "\\" + Constants.INTERNAL_DATA_FOLDER + "\\" + Constants.DOCK_MANAGER_PERSISTENCE_FILE;
				_dockManager.SaveAsXml(
					configFile);

				if (_components != null) 
					_components.Dispose();

				base.Dispose( disposing );
			}

		}


		#endregion


		#region MAIN


		/// <summary> Entry point for the application. Critical checks, ie,
		/// checks which will shutdown app if failed, are done here. </summary>
		[STAThread]
		static void Main(
			string[] args
			)
		{

			string missingFile		= String.Empty;

			try
			{
			

				// ##############################################################
				// Determines if app has been started in debug mode - use the 
				// switch " -debug " when starting browsemonkey to enable debug.
				// --------------------------------------------------------------
				_debugMode = false;
				for (int i = 0 ; i < args.Length ; i ++)
					if (args[i].ToLower().Trim() == "/debug" || args[i].ToLower().Trim() == "/d")
					{
						_debugMode = true;
						break;
					}
				

				// ##############################################################
				// remove all non-file name arguments from start args.
				// --------------------------------------------------------------
				ArrayList fileArgs = new ArrayList();
				foreach(string arg in args)
					if (!arg.StartsWith("/"))
						fileArgs.Add(arg);

				_fileArgs = (string[])fileArgs.ToArray(typeof(string));
				

				// ##############################################################
				// need to instantiate string messenger object - this will be 
				// used immediately if another instance of this application 
				// already exists - if that is the case, the volume file argument
				// used for this instance will be sent to the first instance, 
				// and this instance will terminate
				// 
				// also does startup checks # 1
				// Ensures that only one instance of this application is running 
				// on this pc
				// --------------------------------------------------------------
				_stringMessenger					= new StringMessenger();
				_stringMessenger.OnStringReceived	+= new EventHandler(OnMessageReceived);

				// gets a reference to the process of another instance
				// of this application
				Process otherProcess = ProcessLib.GetOtherRunningProcessOfCurrentApplication(
					Process.GetCurrentProcess());

				if (otherProcess != null)
				{
					// if objProcess is not null, it means there is
					// another instance of browsemonkey running. must
					// therefore not proceed with loading this instnace


					// sends each of the file names which this 
					// instance was supposed to open, to the
					// other instance, so that _that_instace
					// can handle the files instead. Note that switches start
					// with "/", so those are not sent
					for (int i = 0 ; i < args.Length ; i ++)
						if (!args[i].StartsWith("/")) 
							_stringMessenger.SendMessage(
								otherProcess,
								args[i]);	
					
					return;

				}
	
		

				
				// ##############################################################
				// sets up a "last chance" exception handler to trap an unhandled
				// exceptions in the application. This is ensure that the app
				// doesnt fail completely in the event of an unhanlded exception
				// --------------------------------------------------------------
				AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(MasterExceptionHandler);

				
				// ##############################################################
				// proceed with application running if startup checks passed
				// --------------------------------------------------------------
				// VERY NB : Application.Run MUSTMUSTMUST be the LAST thing done in this method. 
				// everything after application.run is unreachable
				Application.Run(new MainForm());

	
			}
			catch(Exception ex)
			{
				if (_debugMode)
					MessageBox.Show(
						"Fatal error - " + Application.ProductName + " is shutting down \r\n\r\n"+
						"Exception message : " + ex.Message + "\r\n" +
						"---------------------------------------------\r\n" + 
						"Stack trace : " + ex.StackTrace);
				else
					MessageBox.Show(
						"Fatal error - " + Application.ProductName + " is shutting down. For a detailed " +
						"error description run the application in debug mode.");
			}
		}


		#endregion


		#region METHODS
		

		/// <summary> 
		/// Need to override the base WndProc method to get access to 
		/// message stream - messages must be passed to 
		/// m_objStringMessenger to scan for incoming messages from 
		/// other instances of BrowseMonkey  
		/// </summary>
		/// <param name="message"></param>
		protected override void WndProc(
			ref Message message
			)
		{

			
			_stringMessenger.ProcessMessage(
				this,
				message);

			//be sure to pass along all messages to the base also
			base.WndProc(ref message);


		}

		
		/// <summary>
		/// Invokes "export to xml" on the currently selected volume browser. 
		/// </summary>
		private void ExportToXml(
			)
		{

			VolumeBrowser b = MainForm.ActiveVolumeBrowser;

			if (b == null)
				return;

			FormFactory.SpawnXmlDump(
				b.Volume.VolumeData,
				b.ForcedName);

		}

		
		/// <summary>
		/// Invokes "export to text" on the currently selected volume browser
		/// </summary>
		private void ExportToText(
			)
		{
			VolumeBrowser b = ActiveVolumeBrowser;

			if (b == null)
				return;

			FormFactory.ToggleFlatTextExport(
				b.Volume,
				b.ForcedName);		
		}


		/// <summary>
		/// Opens a file select dialog and attempts to open the files
		/// selected in the dialog
		/// </summary>
		public void OpenVolumeWithDialog(
			)
		{
			OpenFileDialog objDialog	= new OpenFileDialog();
			objDialog.Filter			= Constants.FILE_DIALOGUE_FILTER;
			objDialog.Multiselect		= true;
			objDialog.ShowDialog();


			if (objDialog.FileNames != null)
				OpenFileBoundVolumes(
					objDialog.FileNames);

			objDialog.Dispose();		
		}



		/// <summary>
		/// Saves the active document in "save as..." mode
		/// </summary>
		private void SaveActiveDocumentAs(
			)
		{
			if (MainForm.DockingManager.ActiveDocument is VolumeBrowser)
			{
				VolumeBrowser activeBrowser = (VolumeBrowser)MainForm.DockingManager.ActiveDocument;
				activeBrowser.SaveAs();	
			}
			else if (MainForm.DockingManager.ActiveDocument is ExportViewer)
			{
				ExportViewer textdump = (ExportViewer)MainForm.DockingManager.ActiveDocument;
				textdump.Save();
			}		
		}



		/// <summary>
		/// Saves the active document
		/// </summary>
		private void SaveActiveDocument(
			)
		{
			if (MainForm.DockingManager.ActiveDocument is VolumeBrowser)
			{
				VolumeBrowser activeBrowser = (VolumeBrowser)MainForm.DockingManager.ActiveDocument;
				activeBrowser.Save();			
			}
		}



		/// <summary>
		/// Closes all open documents
		/// </summary>
		private void CloseAllDocuments()
		{
			foreach(Form document in _dockManager.Documents)
				document.Dispose();
		}



		/// <summary>
		/// The top-level method for opening one or more file-bound volumes. All volume opening 
		/// should be routed through this method, as it performs standard checks, and enforces 
		/// version control on volumes too. Raw volume open requests can be sent here : ie, 
		/// calls directly from open dialogues, drag and drop, app startup args etc.
		/// </summary>
		/// <param name="volumes"></param>
		private static void OpenFileBoundVolumes(string[] inVolumes)
		{
			
			// ##################################################
			// convert array to arraylist
			// --------------------------------------------------
			ArrayList volumes = new ArrayList();
			foreach(string volume in inVolumes)
				volumes.Add(volume);


			// ##################################################
			// removes any invalid files (non exist or non
			// volumes). moves invalid volume versions to 
			// another arraylist 
			// --------------------------------------------------
			int count = volumes.Count;
			ArrayList invalidVolumes = new ArrayList();
			for (int i = 0 ; i < count ; i ++)
			{
				
				
				
				// removes any files that are locked
				try
				{
					FileStream s = new FileStream(
						volumes[count - i - 1].ToString(),
						FileMode.Open);

					s.Close();
				}
				catch
				{
					volumes.RemoveAt(count - i - 1);
					continue;
				}



				if(!File.Exists((string)volumes[count - i - 1]) ||
					!VolumeIdentificationLib.FileIsVolume((string)volumes[count - i - 1]))
					volumes.RemoveAt(count - i - 1);
				else 
				{

					string version = VolumeIdentificationLib.GetVolumeVersion((string)volumes[count - i - 1]);

					if (version == "1.01")
					{
						// moves volumes with invalid versions to "invalid" array
						invalidVolumes.Add(volumes[count - i - 1]);
						volumes.RemoveAt(count - i - 1);
					}
					else if (version == BrowseMonkeyData.Constants.CurrentVolumeVersion)
					{
						// do nothing
					}
					else
					{
						// volume is of a format not yet supported by this version of browsemonkey	
						MainForm.ConsoleAdd(
							volumes[count - i - 1] + " requires a newer version of BrowseMonkey.");
						volumes.RemoveAt(count - i - 1);
					}
				}
			}



			// ##################################################
			// tries to update older volume versions
			// --------------------------------------------------
			if (invalidVolumes.Count > 0)
			{
				VolumeConvertDialog dialog = new VolumeConvertDialog(
					(string[])invalidVolumes.ToArray(typeof(string)));
				FormLib.ScreenCenterForm(dialog);
				dialog.ShowDialog();
			
				// gets a list of all volumes which have been 
				// successfully converted and merges it back
				// into "volumes" array
				string[] updatedVolumes = dialog.ConvertedVolumes;
				foreach(string updateVolume in updatedVolumes)
					volumes.Add(updateVolume);
	
			}

			
			// ##################################################
			// open each volume - note that volumes is now a 
			// list of CONVERTED volumes
			// --------------------------------------------------
			foreach (string volume in volumes)
				FormFactory.SpawnVolumeBrowser(
					volume);


		}
	

		#endregion


		#region METHODS - CONSOLE


		/// <summary> 
		/// Passes an exception to the console
		/// </summary>
		/// <param name="e"></param>
		public static void ConsoleAdd(Exception e)
		{

			bool consoleAvailable	= false;
			_e						= e;	
			_consoleMessage			= ""; // must be done - using zero length as indicator that there is no ConsoleMessageType.
									

			WinFormActionDelegate dlgyConsoleUpdate = new WinFormActionDelegate(
				ConsoleAdd_ThreadSafe);

			// checks if console is available
			if (FormFactory.Console != null && FormFactory.Console.Created)
				consoleAvailable = true;

			if (consoleAvailable)
				FormFactory.Console.messageConsole.Invoke(
					dlgyConsoleUpdate);
			else
			{
				if (_debugMode)
					MessageBox.Show(". Emessage : " + _e.Message + ". STrace : " + _e.StackTrace);
				else
					MessageBox.Show("An unexpected error occurred");
			}	
		}



		/// <summary> 
		/// Passes a message to the console
		/// </summary>
		/// <param name="strMessage"></param>
		public static void ConsoleAdd(string strMessage)
		{

			bool consoleAvailable	= false;
			_e						= null;	
			_consoleMessage		= strMessage;

			WinFormActionDelegate dlgyConsoleUpdate = new WinFormActionDelegate(
				ConsoleAdd_ThreadSafe);
	
			// checks if console is available
			if (FormFactory.Console != null && FormFactory.Console.Created)
				consoleAvailable = true;

			if (consoleAvailable)
				FormFactory.Console.messageConsole.Invoke(
					dlgyConsoleUpdate);			
			else
				// console is not available - need to use messagebox instead
				MessageBox.Show(strMessage);

		}



		/// <summary> 
		/// Thread safe analogue to ConsoleAdd() - should be called only from that 
		/// method, and only via a delegate.
		/// </summary>
		/// <param name="args"></param>
		private static void ConsoleAdd_ThreadSafe()
		{

			string strDebugModeMessage	= "";

			if (_debugMode)
			{
				if (_consoleMessage.Length > 0)
					strDebugModeMessage += _consoleMessage;

				if (_e != null)
					strDebugModeMessage += ". Emessage : " + _e.Message + ". STrace : " + _e.StackTrace;

				FormFactory.Console.messageConsole.Add(
					strDebugModeMessage);
			}
			else
			{
				if (_consoleMessage.Length == 0)
					FormFactory.Console.messageConsole.Add(
						"An unexpected error occurred.");
				else
					FormFactory.Console.messageConsole.Add(
						_consoleMessage);
			}
			
		}
		

		
		#endregion


		#region EVENTS

		
		/// <summary> 
		/// Most of the default onload logic of the application is kept in this method 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load(object sender, EventArgs e)
		{
		
			try
			{

				System.Globalization.CultureInfo c = new System.Globalization.CultureInfo(
					Application.CurrentCulture.Name);

				// #####################################################
				// set form name
				// -----------------------------------------------------
				this.Text = "BrowseMonkey";
				if (_debugMode)
					this.Text += " (Debug mode)";



				// #####################################################
				// #2 - saves a reference to this form to a static 
				// member. This is done so the static methods of 
				// MainForm, which are used on application start up, can 
				// have a working reference to the instantiated copy of 
				// this form.
				// -----------------------------------------------------
				_this = this;



				// #####################################################
				// #3 - check for config file directory and if not exist, 
				// create. This is where all config files, Xml state and 
				// other application internal data files are stored. This 
				// folder has got nothing to do with volume file storage 
				// though
				// -----------------------------------------------------
				if (!Directory.Exists(Application.StartupPath + "\\" + Constants.INTERNAL_DATA_FOLDER))
					Directory.CreateDirectory(
						Application.StartupPath + "\\" + Constants.INTERNAL_DATA_FOLDER);




				// #####################################################
				// instantiate application-wide objects
				// -----------------------------------------------------
				// set up assembly accessor first, is several other objects require this one
				_assemblyAccessor = new AssemblyAccessor(
					Assembly.GetAssembly(typeof(MainForm)));
				
				_stateholder = new StateHolder(
					Application.StartupPath + "\\" + Constants.INTERNAL_DATA_FOLDER + "\\state.dat");

				_recentVolumes = new FSLinksCollection(
					Application.StartupPath + "\\" + Constants.INTERNAL_DATA_FOLDER + "\\recentFiles.dat",
					Constants.MAX_RECENT_FILES);
				_recentVolumes.ValidFilesOnly = false;

				_recentVolumes.ItemCountChanged += new EventHandler(RecentFilesCollection_Changed);

				_recentSearchFolders = new FSLinksCollection(
					Application.StartupPath + "\\" + Constants.INTERNAL_DATA_FOLDER + "\\recentSearchFolders.dat",
					Constants.MAX_RECENT_SEARCH_FOLDERS);
				_recentSearchFolders.ValidFilesOnly = false;

				m_dXmlListviewConfig = MainForm.AssemblyAccessor.GetXmlDocument(
					_assemblyAccessor.RootName + ".Resources.Xml.listviewConfigs.xml");



				// #####################################################
				// restores the state of the main form (ie, this)
				// -----------------------------------------------------
				if (_stateholder.Contains("MainForm.Location"))this.Location = (Point)_stateholder.Retrieve("MainForm.Location");
				if (_stateholder.Contains("MainForm.Size"))this.Size = (Size)_stateholder.Retrieve("MainForm.Size");
				if (_stateholder.Contains("MainForm.WindowState"))this.WindowState = (FormWindowState)_stateholder.Retrieve("MainForm.WindowState");



				// #####################################################
				// if the application is minimized when shut down, it 
				// will restart in minimized state, which is NOT desired. 
				// the problem is that the "normal" state will be set to 
				// minimized too, which prevents the user from setting 
				// the application to normal again ("normal" = minimized)
				// below is a "workaround" to this problem.
				// -----------------------------------------------------
				if (this.WindowState == System.Windows.Forms.FormWindowState.Minimized)
				{
					this.WindowState = System.Windows.Forms.FormWindowState.Normal;
					this.Size = new Size(400,300);
					this.DesktopLocation = new Point(50,50);
				}

				string configFile = Application.StartupPath + "\\" + Constants.INTERNAL_DATA_FOLDER + "\\" + Constants.DOCK_MANAGER_PERSISTENCE_FILE;

				// loads weifen luo's form state data
                /*
				if (File.Exists(configFile))
					_dockManager.LoadFromXml(
						configFile, 
						new DockContentHandler("",));
				*/

				if (FormFactory.Console == null)
					FormFactory.ToggleConsole();
				if (FormFactory.SearchArgs == null)
					FormFactory.ToggleSearch();

				// sets the icon of this application
				this.Icon = MainForm.AssemblyAccessor.GetIcon(
					Application.ProductName + ".Resources.Images.appIcon.ico");
					


				// #####################################################
				// throws up splash - when the splash is closed 
				// (disposed), this application will continue loading
				// -----------------------------------------------------
				FormFactory.ToggleSplash();	
				FormFactory.Splash.Disposed += new EventHandler(MainForm_Load_Continued);

			}
			catch(Exception ex)
			{
				MainForm.ConsoleAdd(ex);
			}
		}
		


		/// <summary> 
		/// Invoked after splash screen closes down -  continues application 
		/// loading, handling all stuff after splash screen 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainForm_Load_Continued(object sender, EventArgs e)
		{

			try
			{
                // ######################################################
				// Force the population of "recent files" menu items. do
				// this after creating menu, as menu reflects contents 
				// of this collection
				// ------------------------------------------------------
				RecentFilesCollection_Changed(null, null);

			

				// ######################################################
				// enable event handling for docking content only after 
				// menus are in place, as menu enabled properties will be 
				// be handled by these event handlers, which start firing 
				// the moment ANY forms are created.
				// ------------------------------------------------------
                /* todo : fix this
                _dockManager.ContentAdded += new DockContentHandler(DockManager_ContentCountChanged);
                _dockManager.ContentRemoved += new DockContentHandler(DockManager_ContentCountChanged);  
                 */ 
				_dockManager.ActiveDocumentChanged += DockManager_ActiveDocumentChanged;




				// ######################################################
				// when reach here, application has finished loading. if 
				// the application was opened to open a specified 
				// BrowseMonkey file, can now open that file
				// ------------------------------------------------------
				if (_fileArgs.Length > 0)
					OpenFileBoundVolumes(
						_fileArgs);


			}
			catch(Exception ex)
			{
				MainForm.ConsoleAdd(ex);
			}
		}




		/// <summary> 
		/// Invoked when items are dragged onto the client area of the main 
		/// application form (this form) 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void this_DragEnter(
			object sender, 
			DragEventArgs e
			)
		{
			try
			{
				// checks if the items being dropped are files (or folders) -
				// dragging behaviour is only allowed for files and folders
				if( e.Data.GetDataPresent(DataFormats.FileDrop, false))
					e.Effect = DragDropEffects.All;
			}
			catch(Exception ex)
			{
				MainForm.ConsoleAdd(ex);	
			}
		}
		


		/// <summary> 
		/// Invoked when dragged items are dropped on teh main application form (this form) 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void this_DragDrop(
			object sender, 
			DragEventArgs e
			)
		{
			
			try
			{

				// gets array of items dropped on this form
				string[] droppedItems = (string[])e.Data.GetData(DataFormats.FileDrop);
				
				// if a single folder is dropped, we send that folder to the volume
				// path selector to be turned into a volume. Else, assume the droppe
				// items are files, and try to treat them as volume files to be 
				// opened
				if (droppedItems.Length == 1 && Directory.Exists(droppedItems[0]))
					FormFactory.SpawnVolumePathSelector(droppedItems[0]);
				else
					OpenFileBoundVolumes(droppedItems);

			}
			catch(Exception ex)
			{
				MainForm.ConsoleAdd(ex);		
			}
		}



		/// <summary> 
		/// Invoked when another instance of this application sends a file name 
		/// message to this instance as a signal that that file must be 
		/// processed by this instance 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private static void OnMessageReceived(
			object sender,
			EventArgs e
			)
		{
			try
			{

				StringMessageEventArgs args = (StringMessageEventArgs)e;
				string file = args.Message;

				OpenFileBoundVolumes(
					new string[]{file});


				// ###########################################
				// receiving a window message can mess up the 
				// window of this app - it is necessary to 
				// manually redraw this app
				// -------------------------------------------
				_this.Show();

			}
			catch(Exception ex)
			{
				MainForm.ConsoleAdd(ex);		
			}
		}


		
		/// <summary> 
		/// Invoked by an unhandled exception - ensures
		/// that application doesnt fail in the event of something
		/// unplanned happening 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private static void MasterExceptionHandler(
			object sender,
			UnhandledExceptionEventArgs e
			)
		{
			
			if(e.ExceptionObject is ThreadAbortException)
			{
				// do nothing - thread abort exceptions must
				// always be supressed
			}
			else
				MainForm.ConsoleAdd(
					(Exception)e.ExceptionObject);

		}

		

		/// <summary>
		/// Invoked when the number of files in the files collection is changed
		/// </summary>
		public void RecentFilesCollection_Changed(
			object sender,
			EventArgs e			
			)
		{
			
			// remove existing links from menu first
            _recentVolumesMenu.DropDownItems.Clear();

			string[] files = _recentVolumes.Items;

			foreach (string file in files)
			{

                ToolStripMenuItem item = new ToolStripMenuItem(
					Path.GetFileName(file));
				
				item.Click += new EventHandler(RecentFileLink_Clicked);

                _recentVolumesMenu.DropDownItems.Add(
					item);
			}

			// if there are are no recent files, disable the "recent items"
			// parent menu item
			_recentVolumesMenu.Enabled = true;
            if (_recentVolumesMenu.DropDownItems.Count == 0)
				_recentVolumesMenu.Enabled = false;


		}



		/// <summary>
		/// Invoked when the content in dock manager is added or removed. This is used 
		/// in turn to set the properties of certain menu items which are dependant
		/// on active content in the dock manager
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void DockManager_ContentCountChanged(
			object sender,
			DockContentEventArgs e				
			)
		{
			
			// If there are any ISpawned forms open,
			// enable "close all"
			ISpawned[] forms = FormFinderLib.GetISpawned();
			
			_closeAllMenu.Enabled = false;
			if (forms.Length > 0)
				_closeAllMenu.Enabled = true;


		}


		
		/// <summary>
		/// Invoked when the different active content is selected. Responsible for setting
		/// the enabled/disabled status of various menu items 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void DockManager_ActiveDocumentChanged(
			object sender,
			EventArgs e
			)
		{

			// disable all volume-specific menu items
			_saveMenu.Enabled = false;
			_saveAsMenu.Enabled = false;
			_exportMenu.Enabled = false;


			// ##################################################################
			// if currently selected active content is a volumebrowser, re-enable 
			// volumebrowser stuff
			// ------------------------------------------------------------------
			if (_dockManager.ActiveDocument is VolumeBrowser)
			{

				_saveAsMenu.Enabled = true;
				_exportMenu.Enabled = true;

				// "save" should only ever be enable if the currently selected content
				// is a dirty volumbe browser
                VolumeBrowser b = _dockManager.ActiveDocument as VolumeBrowser;
				
				if (b.Dirty)
					_saveMenu.Enabled = true;

			}
			else if (_dockManager.ActiveDocument is ExportViewer)
				_saveAsMenu.Enabled = true;

		}

		

		/// <summary>
		/// Invoked whenever a volumebrowser changes its dirty status. This method 
		/// </summary>
		public void VolumeBrowserDirtyChanged(
			object sender,
			EventArgs e
			)
		{
			

			VolumeBrowser b = MainForm.ActiveVolumeBrowser;

			if (b == null)
				return;
	
			if (b.Dirty)
				_saveMenu.Enabled = true;
			else
				_saveMenu.Enabled = false;


		}



		/// <summary>
		/// Invoked when a volume is saved to file or closed. Responsible for setting 
		/// _unsavedVolumesCount back to 0 if there are no more open unbound volumes 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public static void VolumeSavedOrClosed(
			object sender,
			EventArgs e
			)
		{
			
			VolumeBrowser[] browsers = FormFinderLib.GetVolumeBrowsers();
			Volume volume = (Volume)sender;

			bool unsavedVolumesOpen = false;

			foreach(VolumeBrowser browser in browsers)
				if (!browser.Volume.BoundToFile && browser.Volume != volume)
				{
					unsavedVolumesOpen = true;
					break;
				}

			if (!unsavedVolumesOpen)
				_unsavedVolumesCount = 0;

		}



		#endregion


		#region EVENTS - Menu


		/// <summary>
		/// Invoked when one of the recent file links in the menu are clicked. The menu items
		/// dont contain the actual paths to the files - only the files names. The order of 
		/// the menu items matches the contents of the RecentFilesCollection however, so we
		/// know which path to extract from the collection by using the count of the menu
		/// item.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RecentFileLink_Clicked(
			object sender, 
			EventArgs e
			)
		{
            ToolStripItem clickedItem = sender as ToolStripItem;

            for (int i = 0; i < _recentVolumesMenu.DropDownItems.Count; i++)
            {
                if (clickedItem != _recentVolumesMenu.DropDownItems[i]) 
                    continue;

                string[] files = _recentVolumes.Items;

                // recent files collection can contain dead links
                // need to check for it here. this should not be
                // handled by the general volume opening logic,
                // because the error message given should be 
                // "recent files" specific
                if (!File.Exists(files[i]))
                {
                    MainForm.ConsoleAdd(Path.GetFileName(files[i]) + " no longer exists.");
                    return;
                }

                OpenFileBoundVolumes(
                    new[] { files[i] });

                break;
            }

		}
		

		/// <summary>
		/// Invoked when the "save" menu is clicked. Invokes the "save" process for the currently 
		/// selected volumebrowser's volume to be saved.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mnuSave_Click(object sender, EventArgs e)
		{
			SaveActiveDocument();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mnuSaveAs_Click(object sender, EventArgs e)
		{
			SaveActiveDocumentAs();
		}


		
		/// <summary> 
		/// Menu click : Toggles visbility of search form 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mnuSearch_Click(object sender, EventArgs e)
		{
			FormFactory.ToggleSearch();
		}
	

		/// <summary> 
		/// Menu click : Toggles visibility of console 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mnuConsole_Click(object sender, System.EventArgs e)
		{
			FormFactory.ToggleConsole();
		}
		

		/// <summary> 
		/// Menu click : Closes this application down 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mnuQuit_Click(object sender, System.EventArgs e)
		{
			this.Dispose();
		}
		


		/// <summary> 
		/// Menu click : Brings up "About" form 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mnuAbout_Click(object sender, System.EventArgs e)
		{
			FormFactory.ToggleAbout();
		}

        
		/// <summary> 
		/// Menu click :  Calls up Volume Creator form 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mnuNewVolume_Click(object sender, System.EventArgs e)
		{
			FormFactory.SpawnVolumePathSelector();
		}
		

		/// <summary> 
		/// Menu click : Invokes "open volume" dialog and process
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OpenVolume(object sender, System.EventArgs e)
		{
			OpenVolumeWithDialog();
		}
		
		

		/// <summary>
		/// Invoked when user click "Close all" menu item. Causes the closing of all 
		/// volumebrowsers
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mnuCloseAll_Click(object sender, System.EventArgs e)
		{
			CloseAllDocuments();
		}

        
		/// <summary>
		/// Invoked when the "export to flat text" menu item is clicked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mnuExportToFlatText_Click(object sender, System.EventArgs e)
		{
			ExportToText();
		}
        

		/// <summary>
		/// Invoked when the "export to Xml" menu item is clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mnuExportToXml_Click(object sender, System.EventArgs e)
		{
			ExportToXml();
		}

		#endregion
	}
}
