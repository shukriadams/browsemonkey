//////////////////////////////////////////////////////////////////////////////////////
// Author				: Shukri Adams												//
// Contact				: shukri.adams@gmail.com									//
//																					//
// vcFramework : A reuseable library of utility classes                             //
// Copyright (C)																	//
//////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.IO;
using System.Xml;
using vcFramework.Delegates;
using vcFramework.Xml;

namespace vcFramework.Collections
{

	/// <summary>
	/// Collection for managing links to files and folders
	/// </summary>
	public class FSLinksCollection
	{

		#region FIELDS

		/// <summary>
		/// 
		/// </summary>
		private XmlDocument _dXmlStorage;

		/// <summary>
		/// Path where Xml file is saved
		/// </summary>
		private string _storagePath;
		
		/// <summary>
		/// The maximum number of file links held in this collection. If the Xml storage file
		/// contains more than this number, the extra file links will be deleted when this
		/// collection is instantiated.
		/// </summary>
		private int _maxItems;

		/// <summary>
		/// If true, the Files property will return only links to files which exist (not broken
		/// files)
		/// </summary>
		private bool _validFilesOnly;
		
		/// <summary>
		/// Invoked when files are added or removed from collection
		/// </summary>
		public event EventHandler ItemCountChanged;

		#endregion


		#region PROPERTIES


		/// <summary>
		/// Gets a list of recently opened files, sorted by increasing date
		/// </summary>
		public string[] Items
		{
			get
			{

				ArrayList files = new ArrayList();
				string file = "";


				for (int i = 0 ; i < _dXmlStorage.DocumentElement.ChildNodes.Count ; i ++)
				{
					
					// "unfilters" Xml-compliant contents
					file = XmlLib.RestoreReservedCharacters(
						_dXmlStorage.DocumentElement.ChildNodes[i].InnerText);
					
					if (!_validFilesOnly || (File.Exists(file) || Directory.Exists(file)))
						files.Add(file);

				}


				return (string[])files.ToArray(typeof(string));

			}
		}

		
		/// <summary>
		/// Gets or set if only links to existing files will be returned. Otherwise, links
		/// can be to files which no longer exist. Default is true.
		/// </summary>
		public bool ValidFilesOnly
		{
			get
			{
				return _validFilesOnly;
			}
			set
			{
				_validFilesOnly = value;
			}
		}


		#endregion


		#region CONSTRUCTORS
        
		/// <summary>
		/// 
		/// </summary>
		public FSLinksCollection(
			string storagePath,
			int maxFiles
			)
		{
			
			_maxItems = maxFiles;
			_storagePath = storagePath;
			_dXmlStorage = new XmlDocument();

			if (maxFiles < 1)
				throw new Exception(
					"Max recent files count cannot be less than 1");

			this.ValidFilesOnly = true;

			// creates new xml file if none can be found
			if (!File.Exists(_storagePath))
				_dXmlStorage.InnerXml = "<files/>" ;
			else
				_dXmlStorage.Load(_storagePath);

			// if the xml doc contains more items than this object instance
			// is set to allow, this step removes those extra items
			while(_dXmlStorage.DocumentElement.ChildNodes.Count > _maxItems)
				_dXmlStorage.DocumentElement.RemoveChild(
					_dXmlStorage.DocumentElement.ChildNodes[_maxItems]);
			

		}


		#endregion


		#region METHODS

		/// <summary>
		/// Adds a new file to the file collection. If the file already exists in the collection, 
		/// it is merely moved to the latest file position
		/// </summary>
		/// <param name="file"></param>
		public void Add(
			string item
			)
		{

			// prevents adding zero-length item to collection
			if (item.Length == 0)
				return;


			item = XmlLib.FilterForReservedXmlCharacters(item);
		


			// check if file already exists. if so, remove it
			for (int i = 0 ; i < _dXmlStorage.DocumentElement.ChildNodes.Count ; i ++ )
				if (_dXmlStorage.DocumentElement.ChildNodes[i].InnerText == item)
				{
					_dXmlStorage.DocumentElement.RemoveChild(
						_dXmlStorage.DocumentElement.ChildNodes[i]);
					break;
				}

			
			// if there are too many file links in Xmldocument, removes unecessary ones
			while(_dXmlStorage.DocumentElement.ChildNodes.Count > _maxItems)
				_dXmlStorage.DocumentElement.RemoveChild(
					_dXmlStorage.DocumentElement.ChildNodes[_maxItems]);


			// inserts new file at top of list
			XmlDocumentFragment fXmlFile = _dXmlStorage.CreateDocumentFragment();
			fXmlFile.InnerXml= "<file>" + item + "</file>";
			
			if (_dXmlStorage.DocumentElement.ChildNodes.Count == 0)
				_dXmlStorage.DocumentElement.AppendChild(
					fXmlFile);
			else
				_dXmlStorage.DocumentElement.InsertBefore(
					fXmlFile,
					_dXmlStorage.DocumentElement.ChildNodes[0]);


			_dXmlStorage.Save(_storagePath);

			// fire event
			DelegateLib.InvokeSubscribers(
				ItemCountChanged,
				this);

		}



		/// <summary>
		/// 
		/// </summary>
		public void Clear(
			)
		{


			_dXmlStorage.DocumentElement.RemoveAll();

			_dXmlStorage.Save(_storagePath);

			// fire event
			DelegateLib.InvokeSubscribers(
				ItemCountChanged,
				this);

		}

		

		#endregion


	}
}
 