//////////////////////////////////////////////////////////////////////////////////////
// Author				: Shukri Adams												//
// Contact				: shukri.adams@gmail.com									//
//																					//
// vcFramework : A reuseable library of utility classes                             //
// Copyright (C)																	//
//////////////////////////////////////////////////////////////////////////////////////

using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using vcFramework.Delegates;

namespace vcFramework.Xml
{
	/// <summary>  Offers instantiated verion of XmlValidatorLib, allowing 
	/// progress tracking </summary>
	public class XmlValidator : IProgress
	{

		
		#region FIELDS
		
		/// <summary> Any errors produced during validation failure are appended to this </summary>
		private string m_strErrorMessage;

		/// <summary> Gets set to false if validation fails. Is reset to true each time validation
		/// starts </summary>
		private bool m_blnvalid;
		
		private string m_strXmlString;

		private string m_strXmlSchemaString;
		
		private long m_lngCurrentStep;

		private long m_lngSteps;

		public event EventHandler OnNext;
		
		public event EventHandler OnEnd;
		
		/// <summary>
		/// 
		/// </summary>
		private bool _running;

		#endregion


		#region PROPERTIES

		/// <summary> Gets error message for validation failure </summary>
		public string ErrorMessage	
		{
			get
			{
				return m_strErrorMessage;
			}
		}
		

		/// <summary> Returns current step </summary>
		public long CurrentStep		
		{
			get
			{
				return m_lngCurrentStep;
			}
		}
	

		/// <summary> Gets the number of steps to complete
		/// this object's operation </summary>
		public long Steps			
		{
			get
			{
				return m_lngSteps;
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

		
		#endregion
		

		#region CONSTRUCTORS


		/// <summary></summary>
		/// <param name="strXmlString"></param>
		/// <param name="strXmlSchemaString"></param>
		public XmlValidator(
			string strXmlString, 
			string strXmlSchemaString
			)
		{
			m_strXmlString = strXmlString;
			m_strXmlSchemaString = strXmlSchemaString;

			// calculates how many steps are needed to validate xmlstring
			XmlDocument dXmlCheck = new XmlDocument();

			try
			{
				dXmlCheck.LoadXml(m_strXmlString);
			
				XmlLib.NodesCount(
					dXmlCheck.DocumentElement,
					ref m_lngSteps);
			}
			catch
			{
				// do nothing - the Xml to check is most likely
				// corrupted, and will fail the test immediately.
				// We suppress this exception because the error
				// will be reported as a test result using the 
				// IsValid() method
			}

			// forcibly kills xml contents of xmldoc
			dXmlCheck.InnerXml = "<blank/>";

		}


		#endregion


		#region METHODS


		/// <summary> Validates an Xml string against an xsd string </summary>
		/// <param name="xmlString"></param>
		/// <param name="schemaString"></param>
		/// <returns></returns>
		public bool IsValid(
			)
		{

			try
			{
				_running = true;

				// must be reset to true each time this method is called
				m_blnvalid = true;

				XmlTextReader xmlTextReader = new XmlTextReader(
					new StringReader(m_strXmlString));

				XmlValidatingReader xmlValidatingReader = new XmlValidatingReader(
					xmlTextReader);
			
				// set to schema - only schema is ever used
				xmlValidatingReader.ValidationType = ValidationType.Schema;


				XmlSchema sXmlSchema = new XmlSchema();

				sXmlSchema = XmlSchema.Read(
					new StringReader(m_strXmlSchemaString),
					null);

				xmlValidatingReader.Schemas.Add(
					sXmlSchema);
			
				// sets the event handler which is invoked "OnError" in validation
				xmlValidatingReader.ValidationEventHandler += new ValidationEventHandler(ValidationEventHandler);
			
				// empty loop - necessary for reading through content
				try
				{
					while (xmlValidatingReader.Read())
					{
						m_lngCurrentStep ++;
						DelegateLib.InvokeSubscribers(OnNext,this);
					}
				}
				catch(XmlException)
				{
					// an error here means that the xml to valid 
					// could not be read properly, indicating it
					// is corrupt
					return false;
				}

				// fires ending event
				DelegateLib.InvokeSubscribers(OnEnd,this);

				if (m_blnvalid)
					return true;
				else
				{
					return false;
				}
			
			}
			finally
			{
				_running = false;
			}
		}



		#endregion


		#region EVENTS
		

		/// <summary> Catches errors  </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		private void ValidationEventHandler(
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
