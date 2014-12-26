///////////////////////////////////////////////////////////////
// BrowseMonkey - An offline filesystem browser              //
// Shukri Adams (shukri.adams@gmail.com)                     //
// https://github.com/shukriadams/browsemonkey               //
// MIT License (MIT) Copyright (c) 2014 Shukri Adams         //
///////////////////////////////////////////////////////////////

using System;
using System.Windows.Forms;
using System.Xml;
using vcFramework.Windows.Forms;
using BrowseMonkeyData;

namespace BrowseMonkey
{
	/// <summary> 
	/// Static library class for instantiating forms in the application
	/// </summary>
	public class FormFactory
	{

		#region FIELDS

		/// <summary>
		/// 
		/// </summary>
		private static VolumePathSelectDialog _volumeCreator;

		/// <summary>
		/// 
		/// </summary>
		private static AboutForm _about;
		
		/// <summary>
		/// 
		/// </summary>
		private static ApplicationConsole _applicationConsole;

		/// <summary>
		/// 
		/// </summary>
		private static SearchArguments _search;

		/// <summary>
		/// 
		/// </summary>
		private static Splash _splash;

		/// <summary>
		/// 
		/// </summary>
		private static TextExportDialog _textExporter;

		#endregion
		

		#region PROPERTIES
		

		/// <summary>
		/// Gets reference to application's console  
		/// </summary>
		static public ApplicationConsole Console
		{
			get
			{
				return _applicationConsole;	
			}
		}
		

		/// <summary> 
		/// Gets applications' search form 
		/// </summary>
		static public SearchArguments SearchArgs
		{
			get
			{
				return _search;
			}
		}


		/// <summary> 
		/// Gets application splash screen 
		/// </summary>
		static public Splash Splash
		{
			get
			{
				return _splash;
			}
		}
		

		#endregion


		#region METHODS

		/// <summary>
		/// 
		/// </summary>
		/// <param name="persistString"></param>
		/// <returns></returns>
		static public Form GetContentFromPersistString(
			string persistString
			)
		{

			if (persistString == typeof(SearchArguments).ToString())
				return _search;
			else if (persistString == typeof(ApplicationConsole).ToString())
				return _applicationConsole;

			return null;

		}



		/// <summary> 
		/// Spawns the Console form - note that this form is ALWAYS open, and cannot be 
		/// closed at runtime - only form invisibility changes 
		/// </summary>
		static public void ToggleConsole(
			)
		{

			if (_applicationConsole != null && _applicationConsole.Created)
			{
				_applicationConsole.Show();
				return;
			}

			_applicationConsole	= new ApplicationConsole();
			_applicationConsole.Text = "Console";
			_applicationConsole.messageConsole.ShowButtons = false;
			_applicationConsole.Show(
                // todo
				//MainForm.DockingManager,
				//DockState.DockBottom
                );

		}


	
		/// <summary> 
		/// Creates application search form - note that this form is ALWAYS 
		/// open, and cannot be closed at runtime - only form invisibility 
		/// changes
		/// </summary>
		static public void ToggleSearch(
			)
		{
			
			if (_search != null && _search.Created)
			{
				_search.Show();
				return;
			}


			// sets content of search form
			_search			= new SearchArguments();
			_search.Owner	= MainForm.Instance;
			_search.Show(
                // todo
				//MainForm.DockingManager,
				//DockState.DockRight
                );

		}


		
		/// <summary> 
		/// Spawns about form. 
		/// </summary>
		static public void ToggleAbout(
			)
		{

			// checks if a volumes explorer exists already
			if (_about != null && _about.Created)
			{
				_about.Show();
				return;
			}


			string about = MainForm.AssemblyAccessor.GetStringDocument(MainForm.AssemblyAccessor.RootName + ".Resources.About.txt");
			string credits = MainForm.AssemblyAccessor.GetStringDocument(MainForm.AssemblyAccessor.RootName + ".Resources.Credits.txt");
			string changeLog = MainForm.AssemblyAccessor.GetStringDocument(MainForm.AssemblyAccessor.RootName + ".Resources.ChangeLog.txt");
			
				// note that About form requires it's images to be passed in as constructor arguments
			_about = new AboutForm(
				MainForm.AssemblyAccessor.GetBitmap(Application.ProductName + ".Resources.Images.aboutTop.png"),
				about,
				credits,
				changeLog);

			FormLib.ScreenCenterForm(_about);
			_about.ShowDialog();

		}



		/// <summary> 
		/// Spawns splash screen. 
		/// </summary>
		static public void ToggleSplash(
			)
		{
		
			if (_splash != null && _splash.Created)
			{
				_splash.Show();
				return;
			}


			_splash = new Splash(
				MainForm.Instance,
				MainForm.AssemblyAccessor.GetBitmap(Application.ProductName + ".Resources.Images.splash.png"),
				2500,
				true);

			_splash.Show();

		}
		


		/// <summary>
		/// Launches the export to flat text dialogue
		/// </summary>
		static public void ToggleFlatTextExport(
			Volume exportVolume,
			string formName
			)
		{
			
			if (_textExporter != null && _textExporter.Created)
			{
				_textExporter.Show();
				return;
			}

			_textExporter = new TextExportDialog(exportVolume, formName);
			FormLib.ScreenCenterForm(_textExporter);
			_textExporter.ShowDialog();

			_textExporter.Dispose();

		}


		
		/// <summary> 
		/// 
		/// </summary>
		static public void SpawnVolumePathSelector(
			)
		{
			SpawnVolumePathSelector(string.Empty);
		}
		
		

		/// <summary>
		/// 
		/// </summary>
		/// <param name="path"></param>
		static public void SpawnVolumePathSelector(
			string path
			)
		{
			_volumeCreator = new VolumePathSelectDialog(path);
			_volumeCreator.Owner = MainForm.Instance;
			FormLib.ScreenCenterForm( _volumeCreator);
			_volumeCreator.ShowDialog();
		}

		
		
		/// <summary> 
		/// Spawns a search result form - all searches spawn ones of these
		/// </summary>
		/// <param name="m_arrStrSearchArgs"></param>
		/// <param name="m_arrStrSearchFiles"></param>
		/// <param name="m_blnReturnFolders"></param>
		/// <param name="m_blnReturnFiles"></param>
		/// <param name="m_blnIgnoreCase"></param>
		/// <param name="m_blnAllowPartialMatches"></param>
		static public void SpawnSearch(
			string[] arrStrSearchArgs,
			string[] arrStrSearchVolumesPathAndNames,
			bool blnAllowPartialMatches
			)
		{
			
			// performs search
			SearchDialog dialog = new SearchDialog(
				arrStrSearchArgs, 
				arrStrSearchVolumesPathAndNames, 
				blnAllowPartialMatches);
			FormLib.ScreenCenterForm(dialog);
			dialog.ShowDialog();


			if (!dialog.Cancelled)
			{

				// displays results
				SearchResults results = new SearchResults(
					dialog.ListViewRows,
					dialog.SearchFor,
					dialog.Duration);

				results.Show(MainForm.DockingManager);
			}

			// clean up
			dialog.Dispose();

		}
		
		

		/// <summary> 
		/// Spawns a form to to hold text dump 
		/// </summary>
		/// <param name="strDumptext"></param>
		/// <param name="strVolumeFile"></param>
		static public void SpawnTextDump(
			string header,
			string contents
			)
		{
			
			ExportViewer textDump = new ExportViewer(
				ExportTypes.Text);

			textDump.Contents	= contents;
			textDump.Owner		= MainForm.Instance;
			textDump.Text		= "Text Export : " + header;

			textDump.Show(
				MainForm.DockingManager);
		
		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="d"></param>
		static public void SpawnXmlDump(
			XmlDocument d,
			string volumepath
			)
		{

			ExportViewer textDump = new ExportViewer(
				ExportTypes.Xml);

			textDump.Contents	= d.OuterXml;
			textDump.Owner		= MainForm.Instance;
			textDump.Text		= "Xml Export : " + volumepath;
			textDump.Show(MainForm.DockingManager);
			textDump.IndentFormatXml();

		}


		
		/// <summary>
		/// Wraps process for exporting "files" listview contents. Process
		/// entails first producing an options dialog, exporting listview
		/// contents to text in that dialog, then sending the text to an
		/// export viewer form
		/// </summary>
		/// <param name="header"></param>
		/// <param name="listview"></param>
		static public void SpawnFileListExporter(
			string header,
			ListView listview
			)
		{

			FileListExportDialog dialog = null;

			try
			{

				dialog = new FileListExportDialog(listview);
				FormLib.ScreenCenterForm(dialog);
				dialog.Owner = MainForm.Instance;
				dialog.ShowDialog();

				if (dialog.Cancelled)
					return;

				ExportViewer textDump = new ExportViewer(
					ExportTypes.Text);

				textDump.Contents	= dialog.Output;
				textDump.Owner		= MainForm.Instance;
				textDump.Text		= "File List : " + header;
				textDump.Show(MainForm.DockingManager);

			}
			finally
			{
				dialog.Dispose();
			}
		}



		/// <summary>
		/// 
		/// </summary>
		static public void SpawnVolumeCreator(
			string readpath
			)
		{

			VolumeCreateDialog dialog = new VolumeCreateDialog(readpath);
			FormLib.ScreenCenterForm(dialog);
			dialog.Owner = MainForm.Instance;
			dialog.ShowDialog();

			if (dialog.Failed || dialog.Cancelled)
				return;

			SpawnVolumeBrowser(dialog.Volume);

		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="readpath"></param>
		static public void SpawnPathList(
			string readpath
			)
		{

			VolumeCreateDialog dialog = new VolumeCreateDialog(readpath);
			FormLib.ScreenCenterForm(dialog);
			dialog.Owner = MainForm.Instance;
			dialog.ShowDialog();

			ToggleFlatTextExport(dialog.Volume, readpath);

		}

		

		/// <summary> 
		/// Spawns a volume explorer for an existing in-memory volume. This is typically done
		/// when a new volume is created somewhere and the volume object must be "placed" in 
		/// a volume browser to make it saveable
		/// </summary>
		/// <param name="strVolumeFile"></param>
		static public void SpawnVolumeBrowser(
			Volume volume
			)
		{

			VolumeBrowser objFormBrowser = new VolumeBrowser(
				volume);

			objFormBrowser.Owner = MainForm.Instance;
			objFormBrowser.OnDirtyChanged += new EventHandler(MainForm.Instance.VolumeBrowserDirtyChanged);
			objFormBrowser.Show(
				MainForm.DockingManager);

		}



		/// <summary> 
		/// Spawns a volume explorer form 
		/// </summary>
		/// <param name="strVolumeFile"></param>
		static public void SpawnVolumeBrowser(
			string volume
			)
		{
			SpawnVolumeBrowser(
				volume,
				string.Empty);
		}



		/// <summary> 
		/// Spawns a volume explorer form 
		/// </summary>
		/// <param name="strVolumeFilePathAndName"></param>
		static public void SpawnVolumeBrowser(
			string volume, 
			string focusPath
			)
		{

			VolumeBrowser objFormBrowser = null;	
			VolumeBrowser[] volumeBrowsers = FormFinderLib.GetVolumeBrowsers(
				volume);

			if (volumeBrowsers.Length > 0)
			{	
				volumeBrowsers[0].Show();
				return;
			}
			
			objFormBrowser = new VolumeBrowser(
				volume, 
				focusPath);

			objFormBrowser.OnDirtyChanged += new EventHandler(MainForm.Instance.VolumeBrowserDirtyChanged);
			objFormBrowser.Owner = MainForm.Instance;
			objFormBrowser.Show(
				MainForm.DockingManager);

		}	
		


		#endregion


	}
}
