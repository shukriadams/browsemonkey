///////////////////////////////////////////////////////////////
// BrowseMonkey - An offline filesystem browser              //
// Shukri Adams (shukri.adams@gmail.com)                     //
// https://github.com/shukriadams/browsemonkey               //
// MIT License (MIT) Copyright (c) 2014 Shukri Adams         //
///////////////////////////////////////////////////////////////

using System.Collections;
using System.Windows.Forms;
using vcFramework.Interfaces;

namespace BrowseMonkey
{
	/// <summary> 
	/// Static library class for tracking/finding forms in the application 
	/// </summary>
	public class FormFinderLib
	{
		

		/// <summary> Gets arraylist holding all 
		/// VolumbeBrowser instances in application 
		/// </summary>
		static public VolumeBrowser[] GetVolumeBrowsers(
			)
		{

			ArrayList lVolumeBrowsers = new ArrayList();

			for (int i = 0 ; i < MainForm.DockingManager.Contents.Count ; i ++)
				if (MainForm.DockingManager.Contents[i] is VolumeBrowser)
					lVolumeBrowsers.Add(
						MainForm.DockingManager.Contents[i]);

			return (VolumeBrowser[])lVolumeBrowsers.ToArray(typeof(VolumeBrowser));

		}


		
		/// <summary> Gets arraylist holding all 
		/// VolumbeBrowser instances in application 
		/// </summary>
		static public VolumeBrowser[] GetVolumeBrowsers(
			string volumePath
			)
		{
			ArrayList lVolumeBrowsers		= new ArrayList();
			VolumeBrowser volumbBrowser		= null;

			for (int i = 0 ; i < MainForm.DockingManager.Contents.Count ; i ++)
			{
				if (MainForm.DockingManager.Contents[i] is VolumeBrowser)
				{}
				else
					continue;

				volumbBrowser = (VolumeBrowser)MainForm.DockingManager.Contents[i];
			
				if (volumbBrowser.VolumePath == volumePath)
					lVolumeBrowsers.Add(
						MainForm.DockingManager.Contents[i]);
			}

			return (VolumeBrowser[])lVolumeBrowsers.ToArray(typeof(VolumeBrowser));

		}



		/// <summary> Gets arraylist holding all 
		/// VolumbeBrowser instances in application 
		/// </summary>
		static public IDirty[] GetIDirty(
			)
		{

			ArrayList lUserInput = new ArrayList();

			for (int i = 0 ; i < MainForm.DockingManager.Contents.Count ; i ++)
				if (MainForm.DockingManager.Contents[i] is IDirty)
					lUserInput.Add(
						MainForm.DockingManager.Contents[i]);

			return (IDirty[])lUserInput.ToArray(typeof(IDirty));

		}


		
		/// <summary>
		/// Returns an array of all open ISpawned forms in application
		/// </summary>
		/// <returns></returns>
		static public ISpawned[] GetISpawned(
			)
		{
			
			ArrayList ISpawnedForms = new ArrayList();

            foreach (Form content in MainForm.DockingManager.Contents)
				if (content is ISpawned)
					ISpawnedForms.Add(
						content);

			return (ISpawned[])ISpawnedForms.ToArray(typeof(ISpawned));

		}





	}
}
