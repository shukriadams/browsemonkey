//////////////////////////////////////////////////////////////////////////////////////
// Author				: Shukri Adams												//
// Contact				: shukri.adams@gmail.com									//
//																					//
// vcFramework : A reuseable library of utility classes                             //
// Copyright (C)																	//
//////////////////////////////////////////////////////////////////////////////////////

using System;
using System.IO;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Xml;
using vcFramework.Parsers;

namespace vcFramework.Assemblies
{

    /// <summary> 
    /// Class for accessing items stored in the assembly 
    /// </summary>
    public class AssemblyAccessor
    {

        #region FIELDS

        Assembly _assembly;

        #endregion


        #region PROPERTIES

        /// <summary> </summary>
        public static string ExecutingAssemblyName
        {
            get
            {
                return ParserLib.ReturnUpto(
                    Assembly.GetExecutingAssembly().FullName,",");
            }
        }


        /// <summary> </summary>
        public static string CallingAssemblyName
        {
            get
            {
                return ParserLib.ReturnUpto(
                    Assembly.GetCallingAssembly().FullName,
                    ","
                    );
            }
        }


        /// <summary> </summary>
        public static string EntryAssemblyName
        {
            get
            {
                return ParserLib.ReturnUpto(
                    Assembly.GetEntryAssembly().FullName,
                    ","
                    );
            }
        }


        public string RootName
        {
            get
            {
                return AssemblyLib.GetAssemblyRootName(_assembly);
            }
        }


        #endregion


        #region CONSTRUCTORS

        /// <summary>  </summary>
        /// <param name="targetAssembly"></param>
        public AssemblyAccessor(Assembly targetAssembly)
        {
            _assembly = targetAssembly;
        }


        /// <summary>
        /// Constructor which finds uses the assembly which the given type is defined in. 
        /// </summary>
        /// <param name="type">The type, the assembly of which, this object will access</param>
        public AssemblyAccessor(Type type)
        {

            _assembly = Assembly.GetAssembly(type);
        }


        #endregion


        #region METHODS

        /// <summary> Returns a bitmap with the given 
        /// assembly name </summary>
        /// <param name="strBitmapAssemblyID"></param>
        /// <returns></returns>
        public Bitmap GetBitmap(string strAssemblyPath)
        {

            Stream imgStream = null;

            try
            {
                imgStream = _assembly.GetManifestResourceStream(strAssemblyPath);

                if (imgStream != null)
                    return Bitmap.FromStream(imgStream) as Bitmap;
                else
                    throw new Exception("Unable to find the resource for the path '" + strAssemblyPath + "'.");


            }
            finally
            {
                if (imgStream != null)
                {
                    imgStream.Flush();
                    imgStream.Close();
                }
            }
        }



        /// <summary> 
        /// Gets a resource in the given assembly as a stream
        /// </summary>
        /// <param name="strBitmapAssemblyID"></param>
        /// <returns></returns>
        public Stream GetStream(
            string strAssemblyPath
            )
        {

            Stream stream = null;

            stream = _assembly.GetManifestResourceStream(strAssemblyPath);

            if (stream != null)
                return stream;

            throw new Exception("Unable to find the resource for the path '" + strAssemblyPath + "'.");

        }



        /// <summary> Returns an icon with the given 
        /// assembly name </summary>
        /// <param name="strIconAssemblyID"></param>
        /// <returns></returns>
        public Icon GetIcon(
            string strAssemblyPath
            )
        {

            Stream imgStream = null;

            try
            {
                imgStream = _assembly.GetManifestResourceStream(strAssemblyPath);

                if (imgStream != null)
                    return new Icon(imgStream) as Icon;
                else
                    throw new Exception("Unable to find the resource for the path '" + strAssemblyPath + "'.");

            }
            finally
            {
                if (imgStream != null)
                {
                    imgStream.Flush();
                    imgStream.Close();
                }
            }
        }



        /// <summary> Returns an xmldocument with the 
        /// given assembly name </summary>
        /// <param name="strXmlDocumentAssemblyID"></param>
        /// <returns></returns>
        public XmlDocument GetXmlDocument(
            string strAssemblyPath
            )
        {
            XmlDocument dXmlReturnDoc = new XmlDocument();
            Stream textStream = null;

            try
            {
                textStream = _assembly.GetManifestResourceStream(strAssemblyPath);

                if (textStream != null)
                {
                    dXmlReturnDoc.Load(textStream);

                    return dXmlReturnDoc;
                }
                else
                    throw new Exception("Unable to find the resource for the path '" + strAssemblyPath + "'.");

            }
            finally
            {
                if (textStream != null)
                {
                    textStream.Flush();
                    textStream.Close();
                }
            }
        }



        /// <summary> 
        /// Returns a string with the convents  of the given path 
        /// </summary>
        /// <param name="assemblyPath"></param>
        /// <returns></returns>
        public string GetStringDocument(string assemblyPath)
        {

            Stream textStream = null;

            try
            {

                textStream = _assembly.GetManifestResourceStream(assemblyPath);

                if (textStream != null)
                {
                    StreamReader reader = new StreamReader(textStream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
                else
                    throw new Exception(string.Format("Unable to find the resource for the path '{0}'.", assemblyPath));

            }
            finally
            {
                if (textStream != null)
                {
                    textStream.Flush();
                    textStream.Close();
                }
            }
        }



        /// <summary> 
        /// Returns a string array with containing names of all items in the assembly  
        /// </summary>
        /// <returns></returns>
        public string[] GetAssemblyContentNames(
            )
        {
            return _assembly.GetManifestResourceNames();
        }

        #endregion
    }
}
