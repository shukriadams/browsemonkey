//////////////////////////////////////////////////////////////////////////////////////
// Author				: Shukri Adams												//
// Contact				: shukri.adams@gmail.com									//
//																					//
// vcFramework : A reuseable library of utility classes                             //
// Copyright (C)																	//
//////////////////////////////////////////////////////////////////////////////////////

using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace vcFramework.Xml
{
	/// <summary> Static class for validating Xml against Xsd </summary>
	public class XmlValidatorLib 
	{

		#region FIELDS
		
		/// <summary> Any errors produced during validation failure are appended to this </summary>
		private static string m_strErrorMessage;

		/// <summary> Gets set to false if validation fails. Is reset to true each time validation
		/// starts </summary>
		private static bool m_blnvalid;
		
		#endregion


		#region PROPERTIES


		/// <summary> Gets error message for validation failure </summary>
		public static string ErrorMessage
		{
			get
			{
				return m_strErrorMessage;
			}
		}
		

		#endregion


		#region METHODS


		/// <summary> Validates an Xml string against an xsd string </summary>
		/// <param name="xmlString"></param>
		/// <param name="schemaString"></param>
		/// <returns></returns>
		public static bool XmlDocIsValid(
			string xmlString, 
			string schemaString
			)
		{
			// must be reset to true each time this method is called
			m_blnvalid = true;

			XmlTextReader xmlTextReader = new XmlTextReader(
				new StringReader(xmlString)
				);

			XmlValidatingReader xmlValidatingReader = new XmlValidatingReader(
				xmlTextReader
				);
			
			// set to schema - only schema is ever used
			xmlValidatingReader.ValidationType = ValidationType.Schema;


			XmlSchema sXmlSchema = new XmlSchema();

			sXmlSchema = XmlSchema.Read(
				new StringReader(schemaString),
				null
				);

			xmlValidatingReader.Schemas.Add(
				sXmlSchema
				);
			
			// sets the event handler which is invoked "OnError" in validation
			xmlValidatingReader.ValidationEventHandler += new ValidationEventHandler(ValidationEventHandler);
			
			// empty loop - necessary for reading through content
			while (xmlValidatingReader.Read())
			{
				
			}
			
			if (m_blnvalid)
				return true;
			else
			{
				return false;
			}

		}



		#endregion


		#region EVENTS
		

		/// <summary> Catches errors  </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		public static void ValidationEventHandler(
			object sender, 
			ValidationEventArgs args
			)
		{
			m_strErrorMessage += args.Message + "\r\n";
			m_blnvalid = false;
		}



		#endregion


	}
}
