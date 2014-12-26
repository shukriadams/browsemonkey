using System;
using NUnit.Framework;
using FSXml;

namespace FSXmlUnitTest
{
	/// <summary>
	/// Summary description for FsXmlWriterTest.
	/// </summary>
	[TestFixture]
	public class FsXmlWriterTest
	{

		#region FIELDS

		/// <summary>
		/// Used by writer "stop" tests as a way to indicate that a writer has been actively stopped.
		/// </summary>
		private bool _writerStopped;


		/// <summary>
		/// Holds the value of the step which the writer was stopped on.
		/// </summary>
		private long _stopStep;

		#endregion


		#region METHODS

		/// <summary>
		/// Tests the writers fsxml creation functioanlity. There is no convenient automated way to 
		/// test if the fsxml produced accurately maps the file structure on the disk. But we can 
		/// at least verify that the writer doesnt through exceptions etc during data creation
		/// </summary>
		[Test]
		public void FSXmlCreate(
			)
		{

			FSXmlWriter writer = null;

			try
			{
				
				foreach(string path in Constants.DirectoriesToRead)
				{

					writer = new FSXmlWriter(path);

					writer.Start();
					
					// testing for null doesnt really say much - need to fidn
					// better test argument
					Assert.IsNotNull(writer.OutputXml);

				}

			}
			finally
			{
				if (writer != null)
					writer.Dispose();
			}

		}



		/// <summary>
		/// Tests stopping a writer while it is in the middle of processing.
		/// </summary>
		[Test]
		public void StopTest(
			)
		{

			FSXmlWriter writer = null;

			try
			{

				foreach(string path in Constants.DirectoriesToRead)
				{

					// reset test values
					_writerStopped = false;
					_stopStep = 0;

					writer = new FSXmlWriter(path);

					writer.OnNext += new EventHandler(FSXmlCreate_Next);
					writer.Start();

					// waits until writer is finished
					while(true)
					{
						// park thread a bit
						System.Threading.Thread.Sleep(100);

						if (!writer.Running)
							break;
					}

					// checks results :
					// if the writer was stopped explicitly by the FSXmlCreate_Next() eventhandler,
					// _writerStopped will be true
					Assert.IsTrue(_writerStopped);

				}

			}
			finally
			{
				writer.Dispose();
			}

		}
	


		/// <summary>
		/// Event handler for writer.OnNext event. This eventhandler is set from StopTest() method.
		/// This eventhandler will stop the fsxmlwriter halfway through its generation process.
		/// This is used to test that the stop function works properly
		/// 
		/// USED BY : StopTest()
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FSXmlCreate_Next(
			object sender,
			EventArgs e
			)
		{
			FSXmlWriter writer = (FSXmlWriter)sender;

			if (writer.CurrentStep >= (writer.Steps/2))
			{
				_writerStopped = true;
				_stopStep = writer.CurrentStep;
				writer.Stop();
			}

		}


		#endregion

	}
}
