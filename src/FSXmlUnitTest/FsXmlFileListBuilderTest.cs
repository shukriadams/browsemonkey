using System;
using NUnit.Framework;
using FSXml;

namespace FSXmlUnitTest
{
	/// <summary>
	/// Summary description for FsXmlFileListBuilderTest.
	/// </summary>
	[TestFixture]
	public class FsXmlFileListBuilderTest
	{

		#region METHODS


		[Test]
		public void FSXmlFileList(
			)
		{
			
			FSXmlWriter writer = null;
			FSXmlFileListBuilder fileListBuilder = null;

			try
			{

				writer = new FSXmlWriter(
					Constants.DirectoriesToRead[0]);
			
				writer.Start();

				fileListBuilder = new FSXmlFileListBuilder(
					writer.OutputXml);
					
				fileListBuilder.Start();

				Console.Write(
					fileListBuilder.OutputXml.OuterXml);

			}
			finally
			{
				if (writer != null)
					writer.Dispose();
				if (fileListBuilder != null)
					fileListBuilder.Dispose();
			}
		}

		

		#endregion
	}
}
