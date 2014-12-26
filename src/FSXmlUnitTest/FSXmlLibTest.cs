using System.Collections;
using System.IO;
using System.Xml;
using NUnit.Framework;
using FSXml;
using vcFramework.IO;

namespace FSXmlUnitTest
{
	/// <summary> </summary>
	[TestFixture]
	public class FSXmlLibTest
	{

		#region METHODS


		/// <summary>
		/// Tests the method which retrieves file extensions from fsxml
		/// </summary>
		[Test]
		public void GetFileExtensionsTest(
			)
		{

			FSXmlWriter writer = null;

			foreach(string path in Constants.DirectoriesToRead)
			{

				writer = new FSXmlWriter(path);

				writer.Start();
					
				string[] extensions = FSXmlLib.GetFileExtensions(writer.OutputXml);
				
				// need a better way to evaluate outcome!
				Assert.IsTrue(extensions.Length > 0);
	
			}

		}



		/// <summary>
		/// Tests the retrieval of an Xml node from a fsXml structure using the file system
		/// pathname of the item represented by that node
		/// </summary>
		[Test]
		public void GetNodeTest(
			)
		{
			FSXmlWriter writer = null;

			foreach(string path in Constants.DirectoriesToRead)
			{

				// #######################################################
				// generate fsxml
				// -------------------------------------------------------
				writer = new FSXmlWriter(path);
				writer.Start();

			
				// #######################################################
				// generates a list of random files found nested under the
				// main path
				// -------------------------------------------------------
				RandomFSItemPicker picker = new RandomFSItemPicker(path,true);

				string[] items = picker.GetItems(10);

				ArrayList nodes = new ArrayList();

				foreach(string file in items)
				{

					FileInfo fileInfo = new FileInfo(file);

					// remaps path
					string shortedFileName = fileInfo.FullName;
					shortedFileName = shortedFileName.Replace(
						path,
						Path.GetFileName(path));


					XmlNode node = FSXml.FSXmlLib.GetNode(
						shortedFileName, 
						writer.OutputXml.DocumentElement,
						NodeTypes.File);

					if (node != null)
						nodes.Add(node);

				}

				Assert.AreEqual(items.Length, nodes.Count);



				// #######################################################
				// generates a list of random folders found nested under 
				// the main path
				// -------------------------------------------------------
				picker = new RandomFSItemPicker(path, false);

				items = picker.GetItems(10);
				nodes = new ArrayList();

				foreach(string folder in items)
				{
					DirectoryInfo dirInfo = new DirectoryInfo(folder);

					// remaps path
					string shortedFolderName = dirInfo.FullName;
					shortedFolderName = shortedFolderName.Replace(
						path,
						Path.GetDirectoryName(path));

					XmlNode node = FSXml.FSXmlLib.GetNode(
						shortedFolderName, 
						writer.OutputXml.DocumentElement,
						NodeTypes.Folder);

					if (node != null)
						nodes.Add(node);

				}

				Assert.AreEqual(items.Length, nodes.Count);

			}

		}




		[Test]
		public void FSXmlTextDumpTest(
			)
		{
			/*

			FSXmlWriter writer = null;
			FSXmlTextExporter textDump = null;

			try
			{

				writer = new FSXmlWriter(
					"C:\\R"
					);

				writer.Start();
			
				textDump = new FSXmlTextExporter(
					writer.OutputXml
					);

				textDump.Start();

				Console.Write(
					textDump.OutputString
					);
			

			}
			finally
			{
				if (textDump != null)
					textDump.Dispose();

				if (writer != null)
					writer.Dispose();
			}

			*/
		}


		#endregion

	}
}
