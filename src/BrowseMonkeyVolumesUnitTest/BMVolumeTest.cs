using System;
using System.IO;
using System.Windows.Forms;
using FSXml;
using BrowseMonkeyData;
using NUnit.Framework;
using vcFramework.Arrays;

namespace BrowseMonkeyVolumesUnitTest
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	[TestFixture]
	public class BMVolumeTest
	{

		/// <summary>
		/// 
		/// </summary>
		string _readPath = "N:\\A";


		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void VolumePropertiesTest(
			)
		{
			FSXmlWriter writer = new FSXmlWriter(
				_readPath);

			writer.Start();

			Volume volume = new Volume(
				writer,
				Application.CurrentCulture);

			volume.Compressed = true;
			
			Console.WriteLine(volume.DateCreated.ToString());
			Console.WriteLine(volume.DateModified.ToString());
			Console.WriteLine(volume.Description);
			Console.WriteLine(volume.DirectoryCount.ToString());
			Console.WriteLine(volume.FileCount.ToString());
			Console.WriteLine(StringArrayLib.ConcatStringArrayContents(volume.FileTypes, "-"));
			Console.WriteLine(volume.Path);
			Console.WriteLine(volume.Version);
			Console.WriteLine(volume.VolumeContentsSize.ToString());
			Console.WriteLine(volume.VolumeData.OuterXml);

		}



		[Test]
		public void VolumeCreateTest(
			)
		{
			
			FSXmlWriter writer = new FSXmlWriter(
				_readPath);

			writer.Start();

			Volume volume = new Volume(
				writer,
				Application.CurrentCulture);

			volume.Compressed = true;

			volume.Save(
				"c:\\testvol.bmi");

			volume.Dispose();

		}



		[Test]
		public void VolumeCreateAndOpenTest(
			)
		{
			
			FSXmlWriter writer = new FSXmlWriter(_readPath);

			writer.Start();

			Volume volume = new Volume(
				writer,
				Application.CurrentCulture);

			volume.Compressed = true;

			volume.Save(
				"c:\\testvol.bmi");

			volume.Dispose();

			volume = new Volume(
				"c:\\testvol.bmi");
			
			Console.Write(
				volume.VolumeData.OuterXml);
		}



		[Test]
		public void VolumeCreateAndOpenAndResaveTest(
			)
		{
			
			FSXmlWriter writer = new FSXmlWriter(_readPath);

			writer.Start();

			Volume volume = new Volume(
				writer,
				Application.CurrentCulture);

			volume.Compressed = true;

			volume.Save(
				"c:\\testvol.bmi");

			volume.Dispose();

			volume = new Volume(
				"c:\\testvol.bmi");
			
			volume.Save();

			Console.Write(
				volume.VolumeData.OuterXml);
		}



		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void VolumeVersionTest(
			)
		{
			
			string volume = @"d:\test.bmi";
				
			string version = VolumeIdentificationLib.GetVolumeVersion(
				volume);

			Console.WriteLine(version);
		}



		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void VolumeConversionTest(
			)
		{
			
			string oldVolume = @"d:\oldvol.bmi";
			string newVolume = @"d:\newvol.bmi";
			
			if (File.Exists(newVolume))
				File.Delete(newVolume);

			VolumeConversionLib.Convert101_102(
				oldVolume,
				newVolume,
				Application.CurrentCulture);

			// test if new volume is valid
			Volume test = new Volume(newVolume);
		}


	}
}
