///////////////////////////////////////////////////////////////
// FSXml - A library for representing file system data as    //
// Xml.                                                      //
// Shukri Adams (shukri.adams@gmail.com)                     //
// https://github.com/shukriadams/browsemonkey               //
// MIT License (MIT) Copyright (c) 2014 Shukri Adams         //
///////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Xml;
using vcFramework;
using vcFramework.Delegates;
using vcFramework.IO;
using vcFramework.IO.Streams;
using vcFramework.Xml;

namespace FSXml
{
	/// <summary> 
	/// Takes any file system path and converts it and all its child items to FSXml. 
	/// </summary>
	public class FSXmlWriter : IProgress, IDisposable
	{

		#region FIELDS
		
		/// <summary> 
		/// Path which will be "captured" as xmlfilestructure data 
		/// </summary>
		private string _startDir;

		/// <summary> 
		/// Set to the number of folders that must be processed 
		/// under the given path. Mostly used for progress indication
		/// </summary>
		private int _folderCount;
		
		/// <summary> 
		/// event handler for progress of object as it processes xmlfilestructure data 
		/// </summary>
		public event System.EventHandler OnNext;

		/// <summary> 
		/// event handler for progress of object as it processes xmlfilestructure data 
		/// </summary>
		public event System.EventHandler OnEnd;
		
		/// <summary> The number of steps this process will require to finish</summary>
		private long _steps;

		/// <summary> 
		/// The step, otu of m_lngSteps, which this object is currently on 
		/// </summary>
		private long _currentStep;
		
		/// <summary> 
		/// FSXml is dumped into this Xmldocument, which can then be picked up once 
		/// this object is finished processing 
		/// </summary>
		private XmlTextWriter _output;

		/// <summary> 
		/// Set to true if we want to stop this process dead.  
		/// </summary>
		private bool _aborting;
		
		/// <summary> 
		/// Set to true if object is processing. Allows us to identify at
		/// any given stage what the running state of the object is 
		/// </summary>
		private bool _running;
		
		/// <summary> 
		/// 
		/// </summary>
		private MemoryStream _memStream;
		
		/// <summary> 
		/// 
		/// </summary>
		private Encoding _encoding;

		/// <summary> 
		/// 
		/// </summary>
		private XmlDocument _outputAsXml;
		
		/// <summary>
		/// Set to true if this object has processed it's directory data into Xml. Once
		/// done the object cannot process again
		/// </summary>
		private bool _done;

		/// <summary>
		/// Used to store names of files which generated errors upon reading
		/// </summary>
		private ArrayList _failedFiles;

		/// <summary>
		/// USed to store names of folders which generated errors upon reading
		/// </summary>
		private ArrayList _failedFolders;

		#endregion


		#region PROPERTIES
		

		/// <summary>
		/// Gets a list of all files which failed to be read
		/// </summary>
		public ReadError[] FailedFiles
		{
			get
			{
				return (ReadError[])_failedFiles.ToArray(typeof(ReadError));
			}
		}


		/// <summary>
		/// Gets a list of all folders which failed to be read
		/// </summary>
		public ReadError[] FailedFolders
		{
			get
			{
				return (ReadError[])_failedFolders.ToArray(typeof(ReadError));
			}
		}


		/// <summary>
		/// 
		/// </summary>
		public XmlDocument OutputXml
		{
			get
			{
				// creates internally cached object
				if (_outputAsXml == null)
				{
					_outputAsXml = new XmlDocument();

					_outputAsXml.LoadXml(
						StreamsLib.BinaryStreamToString(
						_memStream,
						64));	//arb block size
						
				}

				return _outputAsXml;

			}
		}

		
		/// <summary> 
		/// 
		/// </summary>
		public Stream Output
		{
			get
			{
				return _memStream;
			}
		}

		
		/// <summary> 
		/// 
		/// </summary>
		public Encoding Encoding
		{
			get
			{
				return _encoding;
			}
			set
			{
				_encoding = value;
			}
		}

		
		/// <summary> 
		/// 
		/// </summary>
		public long CurrentStep
		{
			get
			{
				return _currentStep;
			}
		}


		/// <summary> 
		/// Gets the number of files that will be processed to acquire the 
		/// xmldirectorystructure 
		/// </summary>
		public int FolderCount
		{
			get
			{
				return _folderCount;
			}
		}
		

		/// <summary> 
		/// 
		/// </summary>
		public long Steps
		{
			get
			{
				return _steps;
			}
		}

		
		/// <summary> 
		/// 
		/// </summary>
		public bool Running
		{
			get
			{
				return _running;
			}
		}

		
		/// <summary>
		/// Gets if this object has finished processing its progressible task
		/// </summary>
		public bool Done
		{
			get
			{
				return _done;
			}
		}


		#endregion


		#region CONSTRUCTORS

		/// <summary>
		/// 
		/// </summary>
		/// <param name="strPath"></param>
		public FSXmlWriter(
			string directoryPath
			)
		{

			_startDir = directoryPath;
			_aborting = false;
			_running = false;
			_done = false;
			
			// sets default encoding
			this.Encoding = Encoding.Default;

			// #########################################
			// checks if path to convert to FSXml exists
			// -----------------------------------------
			if (!Directory.Exists(directoryPath))
				throw new Exception(
					directoryPath + " is not a valid directory path");

			// counts how many folders will have to be processed
			_folderCount = 0;
			
			FileSystemLib.CountAllDirectoriesUnder(
				_startDir, 
				ref _folderCount);

			_steps = _folderCount;

		}

	
		#endregion


		#region METHODS


		/// <summary> 
		/// Starts the process of generating FSXml data
		/// </summary>
		/// <param name="strPath"></param>
		/// <returns></returns>
		public void Start(
			)
		{
			
			_running = true;

			_memStream = new MemoryStream();

			_output = new XmlTextWriter(
				_memStream,
				_encoding);

			_failedFolders = new ArrayList();
			_failedFiles = new ArrayList();

			GetFileListAtPoint(
				new DirectoryInfo(_startDir));

			_output.Flush();

			if (_aborting)
				this.Dispose();

			_running = false;
				
			DelegateLib.InvokeSubscribers(
				OnEnd,
				this);
			
			_done = true;

		}	
		

		
		/// <summary> 
		/// Aborts the process of generating FSXml data 
		/// </summary>
		public void Stop(
			)
		{
			_aborting = true;
		}



		/// <summary> 
		/// Method used in recursive loop to dump directory 
		/// structure into xml document 
		/// </summary>
		/// <param name="strPath"></param>
		/// <param name="nXmlNodeToAppendTo"></param>
		private  void GetFileListAtPoint(
			DirectoryInfo directory
			)
		{

			string creationTime		= "";
			string lastWriteTime	= "";
			FileInfo[] files		= null;
			DirectoryInfo[] subDirs	= null;
	

			// #####################################################
			// fires all subscribers to the OnStepNext eventhandler
			// -----------------------------------------------------
			DelegateLib.InvokeSubscribers(
				OnNext, 
				this);
			_currentStep ++;



			// ####################################################################
			// aborts if necessary
			// --------------------------------------------------------------------
			if (_aborting)
				return;


			// ####################################################################
			// write current directory
			// --------------------------------------------------------------------
			// reset values
			creationTime = DateTime.MinValue.ToString();
			lastWriteTime = DateTime.MinValue.ToString();

			try
			{
				creationTime = directory.CreationTime.ToString();
				lastWriteTime = directory.LastWriteTime.ToString();
			}
			catch(Exception ex)
			{
				_failedFolders.Add(
					new ReadError(ex, directory.FullName, false));
			}

			try
			{
				_output.WriteRaw(
					"<d>" + 
					"<n>" + XmlLib.FilterForReservedXmlCharacters(directory.Name) + "</n>" + 
					"<dc>" + creationTime + "</dc>" +
					"<dm>" + lastWriteTime + "</dm>" +
					"<o/>");
			}
			catch(Exception ex)
			{
				_failedFolders.Add(
					new ReadError(ex, directory.FullName, true));
			}
			


			// ####################################################################
			// write files
			// --------------------------------------------------------------------
			_output.WriteRaw("<fs>");
			
			try
			{
				files = new FileInfo[0];
				files = directory.GetFiles();
			}
			catch(Exception ex)
			{
				_failedFolders.Add(
					new ReadError(ex, directory.FullName, true));
			}

			foreach (FileInfo file in files)
			{
				// reset values
				creationTime = DateTime.MinValue.ToString();
				lastWriteTime = DateTime.MinValue.ToString();

				try
				{
					creationTime = file.CreationTime.ToString();
					lastWriteTime = file.LastWriteTime.ToString();
				}
				catch(Exception ex)
				{
					_failedFiles.Add(
						new ReadError(ex, file.FullName, false));
				}

				try
				{
					_output.WriteRaw( 
						"<f>" + 
						"<n>" + XmlLib.FilterForReservedXmlCharacters(file.Name) + "</n>" +												
						"<dc>" + creationTime + "</dc>" + 
						"<dm>" + lastWriteTime + "</dm>" +
						"<s>" + file.Length + "</s>" +
						"<o/>" +	
						"</f>");
				}
				catch(Exception ex)
				{
					_failedFiles.Add(
						new ReadError(ex, file.FullName, true));
				}
			}

			_output.WriteRaw("</fs>");



			
			// ####################################################################
			// calls this method again for all subdirectories in this directory. 
			// this is where recursion occurs
			// --------------------------------------------------------------------
			_output.WriteRaw("<ds>");

			try
			{
				subDirs = new DirectoryInfo[0];
				subDirs = directory.GetDirectories();
			}
			catch(Exception ex)
			{
				_failedFolders.Add(
					new ReadError(ex, directory.FullName, true));
			}

			foreach (DirectoryInfo subDir in subDirs)
				GetFileListAtPoint(subDir);

			_output.WriteRaw("</ds>");



			_output.WriteRaw("</d>");

		}


		
		/// <summary>
		/// 
		/// </summary>
		public void Dispose(
			)
		{

			// "flushes" xmldocument out by overwriting it's contents
			// with a short string. this appears to instantly release
			// memory held by very large xml document objects
			if (_output != null)
				_output.Close();

			if (_outputAsXml != null)
				_outputAsXml.InnerXml = "<empty/>";

		}



		#endregion


	}
}
