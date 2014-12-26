//////////////////////////////////////////////////////////////////////////////////////
// Author				: Shukri Adams												//
// Contact				: shukri.adams@gmail.com									//
//																					//
// vcFramework : A reuseable library of utility classes                             //
// Copyright (C)																	//
//////////////////////////////////////////////////////////////////////////////////////

using System.Xml;

namespace vcFramework.Xml
{
	/// <summary>
	/// Summary description for XmlSearchResult.
	/// </summary>
	public class XmlSearchResult
	{
		
		#region FIELDS

		/// <summary>
		/// 
		/// </summary>
		private XmlNode _node;

		/// <summary>
		/// 
		/// </summary>
		private double _score;

		#endregion
		
		
		#region PROPERTIES

		/// <summary>
		/// 
		/// </summary>
		public XmlNode Node
		{
			get
			{
				return _node;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		public double Score
		{
			get
			{
				return _score;
			}
		}


		#endregion


		#region CONSTRUCTORS

		/// <summary>
		/// 
		/// </summary>
		/// <param name="node"></param>
		/// <param name="score"></param>
		internal XmlSearchResult(
			XmlNode node,
			double score
			)
		{
			_node = node;
			_score = score;
		}

		
		#endregion

	}
}
