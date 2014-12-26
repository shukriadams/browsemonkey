//////////////////////////////////////////////////////////////////////////////////////
// Author				: Shukri Adams												//
// Contact				: shukri.adams@gmail.com									//
//																					//
// vcFramework : A reuseable library of utility classes                             //
// Copyright (C)																	//
//////////////////////////////////////////////////////////////////////////////////////

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Web;
using vcFramework.IO.Streams;

namespace vcFramework.Serialization
{
	/// <summary> </summary>
	public class SerializeLib 
	{
		
		
		/// <summary> </summary>
		/// <param name="strSerContent"></param>
		/// <returns></returns>
		public static object DeserializeFromBinString(
			string strSerContent
			)
		{

			MemoryStream memStream = null;

			try
			{
				
				memStream = (MemoryStream)StreamsLib.StringToBinaryStream(strSerContent,16);
				IFormatter fmt = new BinaryFormatter();
				return fmt.Deserialize(memStream);

			}
			finally
			{
				memStream.Close();
			}
		}


		/// <summary> </summary>
		/// <param name="strSerContent"></param>
		/// <returns></returns>
		public static object DeserializeFromXmlString(
			string strSerContent
			)
		{

			MemoryStream memStream = null;

			try
			{
				
				memStream = (MemoryStream)StreamsLib.StringToBinaryStream(strSerContent,16);
				IFormatter fmt = new SoapFormatter();
				return fmt.Deserialize(memStream);

			}
			finally
			{
				memStream.Close();
			}
		}


		/// <summary> </summary>
		/// <param name="objPostBackCarrier"></param>
		/// <returns></returns>
		public static string SerializeToBinString(
			object serobj
			)
		{

			MemoryStream memStream = null;

			try
			{
				memStream = new MemoryStream();
				BinaryFormatter objFormatter = new BinaryFormatter();
				objFormatter.Serialize(memStream, serobj);
			
				return StreamsLib.BinaryStreamToString(
					memStream,
					16
					).ToString();

			}
			finally
			{
				memStream.Close();		
			}

		}



		/// <summary>
		/// 
		/// </summary>
		/// <param name="serobj"></param>
		/// <returns></returns>
		public static string SerializeToXmlString(
			object serobj
			)
		{

			MemoryStream memStream = null;

			try
			{
				memStream = new MemoryStream();
				SoapFormatter objFormatter = new SoapFormatter();
				objFormatter.Serialize(memStream, serobj);
			
				return StreamsLib.BinaryStreamToString(
					memStream,
					16
					).ToString();

			}
			finally
			{
				memStream.Close();		
			}

		}

		/// <summary> </summary>
		/// <param name="strSerContent"></param>
		/// <returns></returns>
		public static object DeserializeFromBinStringURLFormat(
			string strSerContent
			)
		{
			return DeserializeFromBinString(
				HttpUtility.UrlDecode(strSerContent)
				);
		}



		/// <summary> </summary>
		/// <param name="serobj"></param>
		/// <returns></returns>
		public static string SerializeToBinStringURLFormat(
			object serobj
			)
		{

			return HttpUtility.UrlEncode(SerializeToBinString(
				serobj
				));
		}


	}
}
