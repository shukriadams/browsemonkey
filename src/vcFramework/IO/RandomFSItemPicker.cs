//////////////////////////////////////////////////////////////////////////////////////
// Author				: Shukri Adams												//
// Contact				: shukri.adams@gmail.com									//
//																					//
// vcFramework : A reuseable library of utility classes                             //
// Copyright (C)																	//
//////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.IO;
using vcFramework.RandomItems;

namespace vcFramework.IO
{
	/// <summary>
	/// Used to get a list of randomly selected files or folders nested under a given location
	/// </summary>
	public class RandomFSItemPicker
	{


		#region FIELDS

		/// <summary>
		/// 
		/// </summary>
		private int _requiredItemCount;

		/// <summary>
		/// 
		/// </summary>
		private ArrayList _items;

		/// <summary>
		/// 
		/// </summary>
		private string _path;
		
		/// <summary>
		/// is set to true if getting files. false = getting folders
		/// </summary>
		private bool _getFiles;

		#endregion


		#region CONSTRUCTORS

		/// <summary>
		/// 
		/// </summary>
		/// <param name="path"></param>
		public RandomFSItemPicker(
			string path,
			bool getFiles
			)
		{
			
			_getFiles = getFiles;
			_path = path;
		

		}


		#endregion


		#region METHODS

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public string[] GetItems(
			int count
			)
		{
			_requiredItemCount = count;
			_items = new ArrayList();

			// ###################################
			// Ensures that there is at least one
			// file nested anywhere underneath
			// this folder
			// -----------------------------------
			bool failed = false;
			if (_getFiles)
			{
				FileSystemLib.CountAllFilesUnder(_path, ref count);

				if (count == 0)
					failed = true;
			}
			else
			{
				FileSystemLib.CountAllDirectoriesUnder(_path, ref count);

				if (count == 0)
					failed = true;
			}

			if (failed)
				return new string[0];




			while(_items.Count < _requiredItemCount)
				RandomFilePicker_internal(
					new DirectoryInfo(_path));

			return (string[])_items.ToArray(typeof(string));
	
		}



		/// <summary>
		/// 
		/// </summary>
		private void RandomFilePicker_internal(
			DirectoryInfo currentDir
			)
		{
			
			FileInfo[] fileList = currentDir.GetFiles();



			// gets files
			if (_getFiles)
				foreach(FileInfo file in fileList)
				{
					if (_items.Count >= _requiredItemCount)
						return;

					// roll a random number of length 1
					if (RandomLib.RandomNumber(1) == 0)
						_items.Add(file.FullName);
			
				}
			else
			{
				
				if (_items.Count >= _requiredItemCount)
					return;

				// roll a random number of length 1
				if (RandomLib.RandomNumber(1) == 0)
					_items.Add(currentDir.FullName);			
			}


			// handles child dirs 
			DirectoryInfo[] dirs = currentDir.GetDirectories();
			foreach(DirectoryInfo child in dirs)
				RandomFilePicker_internal(
					child);


		}


		#endregion

	}
}
