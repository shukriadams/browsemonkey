///////////////////////////////////////////////////////////////
// BrowseMonkeyData - A class library for the data file      // 
// format of BrowseMonkey.                                   //
// Shukri Adams (shukri.adams@gmail.com)                     //
// https://github.com/shukriadams/browsemonkey               //
// MIT License (MIT) Copyright (c) 2014 Shukri Adams         //
///////////////////////////////////////////////////////////////

using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using vcFramework.Assemblies;
using vcFramework.Delegates;
using vcFramework.IO.Streams;
using vcFramework.Xml;
using FSXml;

namespace BrowseMonkeyData
{

	/// <summary>
	/// Contains fsxml data and accessory information in a form which can be conveniently 
	/// "packaged", saved to binary file format, etc. Is the "data type" for BrowseMonkey,
	/// in that all volumes created in browsemonkey are actually types of this class.
	/// </summary>
	public class Volume : IDisposable
	{	

		
		#region FIELDS

		/// <summary> 
		/// Holds the file path and name for volume file 
		/// </summary>
		private string _path;

		/// <summary> 
		/// Holds summary data 
		/// </summary>
		private XmlDocument _summary;
		
		/// <summary> 
		/// block size for bytes read from raw volume data. This seems
		/// to be pretty arbitrary. 
		/// </summary>
		private const int BLOCKSIZE = 128;
		
		/// <summary> 
		/// This is position in the Volume file where the divider flag
		/// is located. The position is calculated by the base Volume class (ie,
		/// this class), and is made availale to derived classes via a protected
		/// property. The position is required for correct placement of volume Data.
		/// </summary>
		private long _seperatorPosition;
		
		/// <summary>
		/// Invoked when the Save() method is called
		/// </summary>
		public event EventHandler OnSave;

		/// <summary>
		/// Invoked when the volume is disposed
		/// </summary>
		public event EventHandler OnDispose;
		
		/// <summary>
		/// Xml document object holding the main FSXml data for this volume object
		/// </summary>
		private XmlDocument _volumeData;
		
		/// <summary>
		/// File stream used to read volume file
		/// </summary>
		//private Stream _volumeReader;
		
		/// <summary>
		/// 
		/// </summary>
		private FSXmlWriter _fsXmlWriter;

		/// <summary>
		/// string array holding file types for all files archived in this volume. The file type
		/// is the file extension, minus the "." delimiter. This list is not type sensitive - all
		/// entensions are lower case. If a file type "ExE" and "eXe" occur, they both appear as a 
		/// single "exe".
		/// </summary>
		private string[] _fileTypes;
		
		/// <summary>
		/// Is true when the volume object is saved to a file, or when it is instantiated using a file
		/// path.
		/// </summary>
		private bool _boundToFile;
		
		/// <summary>
		/// 
		/// </summary>
		private CultureInfo _culture;

		#endregion

		
		#region PROPERTIES

		/// <summary>
		/// Gets the cultureinfo under which this volume was created
		/// </summary>
		public CultureInfo CultureInfo
		{
			get
			{
				if (_culture == null)
					_culture = new CultureInfo(
						_summary.SelectSingleNode("//i/cu").InnerText);	

				return _culture;
			}
		}


		/// <summary>
		/// Gets if the volume object has been saved to file, or has been instantiated in association
		/// with an existing file.
		/// </summary>
		public bool BoundToFile
		{
			get
			{
				return _boundToFile;
			}
		}


		/// <summary> 
		/// Gets or sets if volume is compressed. If changing compression
		/// state, changes will only be reflected when the volume file is saved.
		/// </summary>
		public bool Compressed
		{
			set
			{

				_summary.SelectSingleNode("//i/c").InnerText = value.ToString();
			}
			get
			{
				return Convert.ToBoolean(_summary.SelectSingleNode("//i/c").InnerText);
			}

		}
		
	
		/// <summary> 
		/// Gets the volume file path. 
		/// </summary>
		public string Path
		{
			get
			{
				return _path;
			}
		}

		
		/// <summary> 
		/// Gets or sets volume description text 
		/// </summary>
		public string Description
		{
			set
			{
				_summary.SelectSingleNode("//i/de").InnerText = value;
			}
			get
			{
				return _summary.SelectSingleNode("//i/de").InnerText;
			}
		}

		
		/// <summary> 
		/// Gets the index count of directories in this volume 
		/// </summary>
		public long DirectoryCount
		{
			get
			{
				return Convert.ToInt64(_summary.SelectSingleNode("//i/d").InnerText);
			}
		}
		

		/// <summary> 
		/// Gets index count of files in this volume 
		/// </summary>
		public long FileCount
		{
			get
			{
				return Convert.ToInt64(_summary.SelectSingleNode("//i/f").InnerText);
			}
		}
		

		/// <summary> 
		/// Gets volume creation datetime
		/// </summary>
		public DateTime DateCreated
		{
			get
			{
				return DateTime.Parse(
					_summary.SelectSingleNode("//i/dc").InnerText,
					this.CultureInfo);
			}
		}
		
		
		/// <summary> 
		/// Gets volume modification datetime 
		/// </summary>
		public DateTime DateModified
		{
			get
			{
				return Convert.ToDateTime(
					_summary.SelectSingleNode("//i/dm").InnerText,
					this.CultureInfo);
			}
		}
		

		/// <summary> 
		/// Gets version of this file 
		/// </summary>
		public string Version
		{
			get
			{
				return _summary.SelectSingleNode("//i/v").InnerText;
			}
		}
		
		
		/// <summary> 
		/// Gets volume contents size 
		/// </summary>
		public long VolumeContentsSize
		{
			get
			{
				return Convert.ToInt64(_summary.SelectSingleNode("//i/cs").InnerText);
			}
		}
		
		
		/// <summary> 
		/// Gets or sets xlm data document 
		/// </summary>
		public XmlDocument VolumeData
		{
			get
			{
				
				// volume data is not loaded immediately when a volume object
				// is instantiated. data is initialized in xmlformat only 
				// when it is first neededs
				if (_volumeData == null)
				{

					/*
					progress for volume data loader
					steps:
					1) load data into memory
					2) uncompress data of compressed
					3) load Xml data
					4) validate Xml using xsd
					*/

					Stream s							= null;	// used to hold the Volume file's contents in memory
					MemoryStream memS					= null;	// used to hold uncompressed Data if Volume file is compressed
					XmlDocument dXmlDataSchema			= null;	// used to validate xml structure of Data section of volume
					bool valid							= false;// is set to true if volume data is valid
					StreamSplitter splitter				= null;
					StreamUncompressor uncompressor		= null;
					AssemblyAccessor assemblyAccessor	= null;
					bool cachedCompressionState			= false;
					FileStream volumeReader				= null;
					try
					{
						
						// ##############################################################
						// opens file stream if necessary
						// --------------------------------------------------------------
						if (this.BoundToFile)
							volumeReader = new FileStream(
								_path,
								FileMode.Open);
						else if (_fsXmlWriter == null)
							throw new Exception(
								"No source for volume data is available.");
						


						// ##############################################################
						// read data section of volume into its own stream
						// --------------------------------------------------------------
						// reads out the Data section of Volume file using the seperator flag
						// position calculated by the base class
						if (_fsXmlWriter == null)
						{
							splitter = new StreamSplitter(
								volumeReader,
								_seperatorPosition + Constants.DIVIDER_FLAG.Length,	// start position
								volumeReader.Length,								// end position
								BLOCKSIZE);

							s = splitter.Split();
						}
						else
						{
							s = _fsXmlWriter.Output;
							cachedCompressionState = this.Compressed;
							this.Compressed = false;	// need to force uncompressed state - data from fsxmlwriter is never compressed
							
						}

						_volumeData = new XmlDocument();


						// ##############################################################
						// uncompresses data if necessary
						// --------------------------------------------------------------
						if (this.Compressed)
						{

							// data is compressed - gets uncompressed memorystream of
							// compressed data
							uncompressor = new StreamUncompressor(
								s, 
								BLOCKSIZE);

							memS = (MemoryStream)uncompressor.GetUnzippedStream();

							try
							{
								// cannot load xml contents from stream using Load() method - if the stream contains 
								// non-english chars (like �) the load process fails. Must instead load data from a 
								// string using the LoadXml() method
								string test = StreamsLib.BinaryStreamToString(memS, 120).ToString();
								_volumeData.LoadXml(
									StreamsLib.BinaryStreamToString(memS, BLOCKSIZE).ToString());

							}
							catch(Exception ex)
							{
								// XML is corrupt and cannot be used
								// to create a viable xmldocument object
								throw new VolumeException(
									VolumeExceptionTypes.BadlyFormedXml,
									ex,
									this.Path + " contains invalid data and could not be opened.");

							}
					
							// clean up compressed-specific stuff
							if (memS != null)
								memS.Close();

						}
						

						// ##############################################################
						// handles uncompressed volumes
						// --------------------------------------------------------------
						if (!Compressed)
						{
							// because data is not compressed, can load
							// xlmdoc direct from memory stream
					
							try
							{

								// cannot load xml contents from stream using Load() method - if the stream contains 
								// non-english chars (like �) the load process fails. Must instead load data from a 
								// string using the LoadXml() method
								string test = StreamsLib.BinaryStreamToString(s, BLOCKSIZE).ToString();

								_volumeData.LoadXml(
									StreamsLib.BinaryStreamToString(s, BLOCKSIZE).ToString());

							}
							catch(Exception ex)
							{
								// THROW EXCEPTION - XML is corrupt and cannot be used
								// to create a viable xmldocument object
								throw new VolumeException(
									VolumeExceptionTypes.BadlyFormedXml,
									ex,
									this.Path + " contains invalid data and could not be opened.");
							}

						}

						// reset compresison state
						if (_fsXmlWriter != null)
							this.Compressed = cachedCompressionState;


						// ##############################################################
						// does XSD validation
						// --------------------------------------------------------------
						// loads schema xml up from embedded xsd file
						dXmlDataSchema = new XmlDocument();
						assemblyAccessor = new AssemblyAccessor(
							System.Reflection.Assembly.GetAssembly(typeof(Volume)));
						dXmlDataSchema = assemblyAccessor.GetXmlDocument(
							"BrowseMonkeyData.volumeDataFormat.xsd"	);

						// loads xsd validator object using xsd xml and data xml
						XmlValidator objXmlValidator = new XmlValidator(
							_volumeData.OuterXml,
							dXmlDataSchema.OuterXml);

						valid = objXmlValidator.IsValid();

						if (!valid)
							// xml failed schema test - data is corrupt
							throw new VolumeException(
								VolumeExceptionTypes.XmlSchemaError,
								this.Path + " Data failed schema test during opening.");

					}
					finally
					{
			
						if (s != null)
							s.Close();
						if (volumeReader != null)
							volumeReader.Close();

					}					
				}

				return _volumeData;

			}
		}

		
		/// <summary>
		/// Gets a list of file extensions for all file types listed
		/// in this volume
		/// </summary>
		public string[] FileTypes
		{
			get
			{
				
				if (_fileTypes == null)
				{
					_fileTypes = new string[_summary.SelectSingleNode("//i/ft").ChildNodes.Count];

					for (int i = 0 ; i < _summary.SelectSingleNode("//i/ft").ChildNodes.Count ; i ++)
						_fileTypes[i] = _summary.SelectSingleNode("//i/ft").ChildNodes[i].InnerText;

				}

				return _fileTypes;
			}
		}


		#endregion


		#region CONSTRUCTORS
		
		/// <summary>
		/// Constructor for creating volume for an existing volume file.
		/// </summary>
		/// <param name="strFilePathAndName"></param>
		public Volume(
			string path
			)
		{

			XmlDocument schema					= null;		// used to validate xml structure of index
			bool valid							= false;	// is set to true if index xml passes schema test
			byte[] flag							= null;		// used to hold the divider flag
			Stream s							= null;		// volume data is a mix of ASCII Xml and compresed binary data, and can therefore not
			StreamSplitter splitter				= null;		
			AssemblyAccessor assemblyAccessor	= null;
			FileStream volumeReader			= null;
			_path = path;
			_boundToFile = true;

			try
			{


				// #######################################################
				// opens the volume up a as a filestream
				// -------------------------------------------------------
				// open volume file as stream, and find position of dividerflag. this flag 
				// seperates the index and data of volume. once its position is known, the 
				// index and data can be read into byte arrays
				try
				{

					// tries to open file - if something goes wrong here, the file
					// is not available or worse. 
					volumeReader = new FileStream(
						this.Path,
						FileMode.Open);

				}
				catch(Exception ex)
				{
					// throw a "volume cannot be opened" 
					// exception
					throw new VolumeException(
						VolumeExceptionTypes.FileCannotBeOpened,
						ex,
						this.Path + " could not be opened.");
				}




				// #######################################################
				// determines position of divider flag
				// -------------------------------------------------------
				// gets byte array of divider flag. divider is a string
				// which must be converted to a byte array
				flag = StreamsLib.StringToByteArray(
					Constants.DIVIDER_FLAG);
			

				// gets position of divider flag in volume file
				_seperatorPosition = StreamsLib.ByteArrayPositionInStream(
					volumeReader,
					flag);




				// #######################################################
				// loads index part of volume file				
				// -------------------------------------------------------
				// streams out index section of volume file into 
				// a memory stream.
				splitter = new StreamSplitter(
					volumeReader,
					0,							// starts at beginning of volume file
					_seperatorPosition,		// reads up to seperator position
					BLOCKSIZE);

				s = splitter.Split();
			
				// creates Xmldocument and popultes from memory stream
				_summary = new XmlDocument();

				try
				{
					// cannot load xml contents from stream using Load() method - if the stream contains 
					// non-english chars (like �) the load process fails. Must instead load data from a 
					// string using the LoadXml() method
					_summary.LoadXml(
						StreamsLib.BinaryStreamToString(s, BLOCKSIZE).ToString());
				}
				catch(Exception ex)
				{
					// Xml is corrupt or invalid
					throw new VolumeException(
						VolumeExceptionTypes.BadlyFormedXml,
						ex,
						this.Path + " Index is not valid Xml and could not be opened.");

				}

				// checks xml validity - first load up validation schema
				assemblyAccessor = new AssemblyAccessor(
					System.Reflection.Assembly.GetAssembly(typeof(Volume)));
				
				schema = assemblyAccessor.GetXmlDocument(
					"BrowseMonkeyData.volumeIndexFormat.xsd");

				// schema validation done here
				valid = XmlValidatorLib.XmlDocIsValid(
					_summary.OuterXml,
					schema.OuterXml);

				// xml failed schema test - data is corrupt
				if (!valid)
					throw new VolumeException(
						VolumeExceptionTypes.XmlSchemaError,
						this.Path + " Index failed schema test during opening.");
			
			}
			finally
			{
				// clean up

				if (volumeReader != null)
					volumeReader.Close();

				if (s != null)
					s.Close();
		
			}

		}



		/// <summary> 
		/// Constructor for creating a volume from a fsxmlwriter. This is typically used
		/// when creating a volume "from scratch" . Requires an FSXmlWriter which in turn
		/// has been used to create Xml for the file structure data to be stored in this
		/// volume
		/// </summary>
		/// <param name="writer"></param>
		public Volume(
			FSXmlWriter writer,
			CultureInfo culture
			)
		{

			long directoryCount		= 0;
			long fileCount			= 0;
			long volContentsSize	= 0;

			_culture = culture;
			_boundToFile = false;
			_path = String.Empty;
			_fsXmlWriter = writer;


			// ############################################################
			// ensures that fsxml writer has processed its data
			// ------------------------------------------------------------
			if (!_fsXmlWriter.Done)
				_fsXmlWriter.Start();


			
			// ############################################################
			// Creates summary data section
			// ------------------------------------------------------------
			_summary = new XmlDocument();
			
			StringBuilder x = new StringBuilder();
			
			// Index raw structure is defined here
			x.Append ("<i>");
			x.Append ("<!-- " + Constants.UniversalVolumeIdenfifier  + " -->");						// used to identify that the bin file is actually a volume
			x.Append ("<!-- description --><de/>");													// leave blank		
			x.Append ("<!-- date this volume created --><dc>" + DateTime.Now.ToString() + "</dc>");	// default is now
			x.Append ("<!-- date this volume modifed --><dm>" + DateTime.Now.ToString() + "</dm>");	// default is now
			x.Append ("<!-- version --><v>" + Constants.CurrentVolumeVersion + "</v>");														// leave blank
			x.Append ("<!-- type --><t>" + "</t>");				// default Type is "unknown". MUST have a value at all times. directly exposed in property for this object
			x.Append ("<!-- compressed (True or False) --><c>True</c>");							// default is compression set to true. this node must always have value, as it directly feeds this Volume objects "Compressed" property
			x.Append ("<!-- directorycount --><d>0</d>");											// default is 0
			x.Append ("<!-- filecount --><f>0</f>");												// default is 0
			x.Append ("<!-- volume contents size --><cs>0</cs>");									// default is 0
			x.Append ("<!-- file types --><ft/>");
			x.Append ("<!-- culture --><cu>" + culture.Name + "</cu>");
			x.Append ("</i>");

			_summary.InnerXml = x.ToString();

			
			// note that the counting calls below call this.VolumeData. Accessing this method
			// forces the xml in the FSXmlReader to be read into this object's own XML member,
			// which is needed for saving that Xml data down to file. 

			// conuts nr of directories in volume
			XmlLib.NamedNodeCount(
				this.VolumeData,
				"d",
				ref directoryCount);
					
			// gets number of files listed
			XmlLib.NamedNodeCount(
				this.VolumeData,
				"f",
				ref fileCount);

			// gets the size of files represented in volume
			XmlLib.NamedNodeValueTotal(
				this.VolumeData,
				"s",
				false,
				ref volContentsSize);

			_summary.SelectSingleNode("//i/d").InnerText = directoryCount.ToString();
			_summary.SelectSingleNode("//i/f").InnerText = fileCount.ToString();
			_summary.SelectSingleNode("//i/cs").InnerText = volContentsSize.ToString();


			// generates a list of all filetypes listed in this volume
			string[] fileTypes = FSXmlLib.GetFileExtensions(
				this.VolumeData);
			
			for (int i = 0 ; i < fileTypes.Length ; i ++)
			{
				XmlDocumentFragment fileType = _summary.CreateDocumentFragment();
				fileType.InnerXml = "<t>" + fileTypes[i] + "</t>";
				_summary.SelectSingleNode(".//ft").AppendChild(
					fileType);
			}



			// ############################################################
			// close off writer ... we dont need it anymore. by reading the
			// xml inthe writer via the volumedata property of this object
			// we forced teh xmldata in the writer to be loaded into this
			// object's own Xmldoc object
			// ------------------------------------------------------------
			writer.Dispose();

		}



		/// <summary>
		/// Constructor used for creating a new volume from an existing one. This is used
		/// for volume copying. The volume created is externally identical to a volume 
		/// created from an FSXML writer.
		/// Note that this constructor is private - it can only be used by member methods, 
		/// in this case the Copy() method
		/// </summary>
		private Volume(
			Volume volume
			)
		{

			_boundToFile = false;
			_path = String.Empty;

			// ############################################################
			// Creates summary data section
			// ------------------------------------------------------------
			StringBuilder x = new StringBuilder();
			
			// gets xml-formated list of filetypes
			StringBuilder fileTypes = new StringBuilder();
			for (int i = 0 ; i < volume.FileTypes.Length ; i ++ )
				fileTypes.Append("<t>" + volume.FileTypes[i] + "</t>");
			
			// builds index
			x.Append ("<i>");
			x.Append ("<!-- " + Constants.UniversalVolumeIdenfifier  + " -->");					// used to identify that the bin file is actually a volume
			x.Append ("<!-- description (alias) --><de>" + volume.Description + "</de>");											// leave blank		
			x.Append ("<!-- date this volume created --><dc>" + volume.DateCreated + "</dc>");	// default is now
			x.Append ("<!-- date this volume modifed --><dm>" + volume.DateModified + "</dm>");	// default is now
			x.Append ("<!-- version --><v>" + volume.Version + "</v>");														// leave blank
			x.Append ("<!-- type --><t>" + "</t>");				
			x.Append ("<!-- compressed (True or False) --><c>False</c>");						// must always be false when copying xml from one volume to another. compression is enabled after copying
			x.Append ("<!-- directorycount --><d>" + volume.DirectoryCount + "</d>");											// default is 0
			x.Append ("<!-- filecount --><f>" + volume.FileCount + "</f>");												// default is 0
			x.Append ("<!-- volume contents size --><cs>" + volume.VolumeContentsSize + "</cs>");									// default is 0
			x.Append ("<!-- file types --><ft>" + fileTypes.ToString() + "</ft>");
			x.Append ("<!-- culture --><cu>" + volume.CultureInfo.Name + "</cu>");
			x.Append ("</i>");
			
			_summary = new XmlDocument();
			_summary.InnerXml = x.ToString();


			// builds data section
			_volumeData = new XmlDocument();
			_volumeData.InnerXml = volume.VolumeData.OuterXml;
			
			// need to explicitly re-enable compression if necessary
			this.Compressed = volume.Compressed;

		}



		#endregion
		

		#region METHODS
		

		/// <summary>
		/// Copies the contents of one volume instance into another
		/// </summary>
		/// <param name="volume"></param>
		/// <returns></returns>
		public static Volume Copy(
			Volume source
			)
		{
			return new Volume(source);
		}



		/// <summary> 
		/// This method must be called to clean up resources - this object consumes 
		/// a lot of memory 
		/// </summary>
		public virtual void Dispose(
			)
		{

			// fires ondispose event
			DelegateLib.InvokeSubscribers(
				OnDispose,
				this);
			
			// destroy xml objects
			if (_summary != null)
				_summary.InnerXml = "<n/>";

			if (_volumeData != null)
				_volumeData.InnerXml = "<n/>";

			if (_fsXmlWriter != null)
				_fsXmlWriter.Dispose();
	

		}


		
		/// <summary> 
		/// Saves the data in this object down to file. Note that any existing file 
		/// data is is completely destroyed and written anew. 
		/// </summary>
		public virtual void Save(
			)
		{	


			MemoryStream mStream	= null;	// used when converting ASCII Xml to binary data so can write down to volume file
			Stream uStream			= null;	// holds data if compression necessary
			FileStream volumeWriter = null;

			try
			{

				
				// ####################################################
				// ----------------------------------------------------
				if (_path == String.Empty)
					throw new Exception(
						@"Cannot save this volume because its file path has not been set. 
						Please use a Save() overload which allows file path to be set.");



				// ####################################################
				// opens file stream to volume file, overwriting and 
				// thus destroying the existing file
				// ----------------------------------------------------
				try
				{
					volumeWriter = new FileStream(
						this.Path, 
						FileMode.Create);
				}
				catch(Exception ex)
				{
					//volume cannot be saved
					throw new VolumeException(
						VolumeExceptionTypes.SaveError,
						ex,
						this.Path + " produced a file creation error during saving : saved failed.");
				}




				// ####################################################
				// writes index xml to file
				// ----------------------------------------------------
				// dumps contents of index xml doc into binary mem stream
				mStream = (MemoryStream)StreamsLib.StringToBinaryStream(
					_summary.OuterXml,
					BLOCKSIZE);
				
			
				// writes that binary mem stream to file stream via "merging"
				StreamsLib.MergeStream(
					volumeWriter,
					mStream,
					0,
					BLOCKSIZE);
			
				// clean up - even though the object variable is being reused, 
				// must still clean up the instance
				mStream.Close();



				// ####################################################
				// writes divider flag
				// ---------------------------------------------------
				// writes dividing flag to file
				
				// converts divider flag to binary (memory) stream
				mStream = (MemoryStream)StreamsLib.StringToBinaryStream(
					Constants.DIVIDER_FLAG,
					BLOCKSIZE);

				// writes that binary mem stream to file stream via "merging"
				StreamsLib.MergeStream(
					volumeWriter,
					mStream,		
					volumeWriter.Length,	// writes at end of filewriter stream
					BLOCKSIZE);
				
			

				// ####################################################
				// writes data section
				// ---------------------------------------------------
				// sends data xml document contents to memory stream
				mStream = (MemoryStream)StreamsLib.StringToBinaryStream(
					_volumeData.OuterXml,
					BLOCKSIZE);

				if (this.Compressed)
				{

					// compresses Xml data down to compressed memory stream
					uStream = (Stream)SharpZipWrapperLib.ZipStream(
						mStream,			// stream to be compressed
						5,					// compression level (hardcoded)
						BLOCKSIZE);			// buffer size - kinda arbitrary

					// write memstream down to volume file 
					StreamsLib.MergeStream(
						volumeWriter,			// stream to merge to
						uStream,			// stream to merge
						volumeWriter.Length,		// position in merge-to stream where data will be written - in this case, the END of the file
						BLOCKSIZE);			// buffer size - kinda arbitrary

				}
				else
					// no need to compress - write ASCII xml straight down to volume file
					StreamsLib.MergeStream(
						volumeWriter,			// stream to merge to
						mStream,			// stream to merge
						volumeWriter.Length,		// position in merge-to stream where data will be written - in this case, the END of the file
						BLOCKSIZE);			// buffer size - kinda arbitrary


				// finally, fire the "onsaved" event
				DelegateLib.InvokeSubscribers(
					OnSave,
					this);

			}
			finally
			{
				// clean up 
				if (mStream != null)
					mStream.Close();
				if (uStream != null)
					uStream.Close();
				if (volumeWriter != null)
					volumeWriter.Close();
			}
		}
		
		

		/// <summary>
		/// 
		/// </summary>
		/// <param name="?"></param>
		public virtual void Save(
			string path
			)
		{
			_path = path;

			this.Save();

			_boundToFile = true;
			
		}


		#endregion

	}
}
