//////////////////////////////////////////////////////////////////////////////////////
// Author				: Shukri Adams												//
// Contact				: shukri.adams@gmail.com									//
//																					//
// vcFramework : A reuseable library of utility classes                             //
// Copyright (C)																	//
//////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Xml;
using vcFramework.Parsers;
using vcFramework.Delegates;


namespace vcFramework.Xml
{
	/// <summary> 
	/// Simple search engine for Xml data. Searchs for occurences of strings in 
	/// node.text, and returns an array of nodes containing all nodes where a 
	/// hit occurred
	/// </summary>
	public class XmlSearch : IProgress
	{
		
		#region MEMBERS
		

		/// <summary> 
		/// string array containing stirngs to search for 
		/// </summary>
		private string[] _searchArgs;

		/// <summary> 
		/// String array of xml node names in which searching can be 
		/// done - if this array is used, any xml node not named in 
		/// it cannot be searched in 
		/// </summary>
		private string[] _searchNodeNames;

		/// <summary> 
		/// Array list that holds search results - this will not be 
		/// returned by the object though - the final results are 
		/// transferred to an XmlNode array 
		/// </summary>
		private ArrayList _results;
		
		/// <summary> 
		/// Set to true if should ignore case in searches 
		/// </summary>
		private bool _ignoreCase = false;
		
		/// <summary> 
		/// Set to true if allow partial word matches 
		/// </summary>
		private bool _partialMatch = false;
		
		/// <summary> 
		/// Xmlnode array to search in 
		/// </summary>
		private XmlNode[] _searchNodes;
		
		/// <summary> Holds how many steps will be needed 
		/// to perform search ( calculated by counting nodes 
		/// for scanning) </summary>
		private long _steps;
		
		/// <summary> Holds the step (out of a total 
		/// of m_lngSteps) that search is currently 
		/// on</summary>
		private long _currentStep;
		
		/// <summary>
		/// Invoked when the next stsp in search process occurs
		/// </summary>
		public event EventHandler OnNext;

		/// <summary>
		/// Invoked when the search ends
		/// </summary>
		public event EventHandler OnEnd;
		
		/// <summary>
		/// 
		/// </summary>
		private bool _running;

		#endregion
		
	
		#region PROPERTIES

		/// <summary> 
		/// Gets if cases will be ignored for searches.
		/// </summary>
		public bool IgnoreCaseInSearch		
		{
			get
			{
				return _ignoreCase;
			}
		}


		/// <summary> 
		/// Gets if partial word matches will be allowed 
		/// </summary>
		public bool AllowPartialWordMatches	
		{
			get
			{
				return _partialMatch;
			}
		}
		

		/// <summary> 
		/// Gets how many steps will be needed to perform search 
		/// </summary>
		public long Steps					
		{
			get
			{
				return _steps;
			}
		}

		
		/// <summary> 
		/// Gets which step search is on
		/// </summary>
		public long CurrentStep
		{
			get
			{
				return _currentStep;
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


		#region CONSRUCTORS


		/// <summary> 
		/// Creates a search Searches an xml node and all its children nodes 
		/// for a set of string values 
		/// </summary>
		/// <param name="nXmlSearch"></param>
		/// <param name="arrStrSearchArguments"></param>
		public XmlSearch(
			XmlNode[] arrNXmlSearch, 
			string[] arrStrSearchArguments, 
			bool blnAllowPartialWordMatches, 
			bool blnIgnoreCaseInSearches
			)
		{
			_searchNodes = arrNXmlSearch;
			_searchArgs = arrStrSearchArguments;
			_searchNodeNames = null;
			_partialMatch = blnAllowPartialWordMatches;
			_ignoreCase = blnIgnoreCaseInSearches;

			// counts how many nodes will have to be processed
			CountSteps();

			// for all to lower if ignoring case
			if (_ignoreCase)
				for (int i = 0 ; i < arrStrSearchArguments.Length ; i ++)
					arrStrSearchArguments[i] = arrStrSearchArguments[i].ToLower();
			
		}



		/// <summary> 
		/// Searches an xml node and all its children nodes for a set of 
		/// string values 
		/// </summary>
		/// <param name="nXmlSearch"></param>
		/// <param name="arrStrSearchArguments"></param>
		/// <param name="arrStrAllowedSearchNodes"></param>
		public XmlSearch(
			XmlNode[] arrNXmlSearch, 
			string[] arrStrSearchArguments, 
			bool blnAllowPartialWordMatches, 
			bool blnIgnoreCaseInSearches, 
			string[] arrStrAllowedSearchNodes
			)
		{
			_searchNodes = arrNXmlSearch;
			_searchArgs = arrStrSearchArguments;
			_searchNodeNames = arrStrAllowedSearchNodes;
			_partialMatch = blnAllowPartialWordMatches;
			_ignoreCase = blnIgnoreCaseInSearches;

			// counts how many nodes will have to be processed
			CountSteps();

			// for all to lower if ignoring case
			if (_ignoreCase)
				for (int i = 0 ; i < arrStrSearchArguments.Length ; i ++)
					arrStrSearchArguments[i] = arrStrSearchArguments[i].ToLower();

		}



		#endregion
		

		#region METHODS

		
		/// <summary> 
		/// Determines how many steps the search will take. This is needed to implement the 
		/// "progressible" behaviour of this object.
		/// </summary>
		private void CountSteps(
			)
		{
			// counts how many nodes will have to be processed
			long stepsTemp = 0;

			for (int i = 0 ; i < _searchNodes.Length ; i ++)
			{
				stepsTemp = 0;

				XmlLib.NodesCount(
					_searchNodes[i],
					ref stepsTemp);
				
				_steps += stepsTemp;
			}		
		}



		/// <summary> 
		/// Entry point for search 
		/// </summary>
		/// <param name="nXmlSearch"></param>
		/// <param name="arrStrSearchArguments"></param>
		/// <param name="arrStrAllowedSearchNodes"></param>
		/// <returns></returns>
		public XmlSearchResult[] Search(
			)
		{

			try
			{
				_running = true;

				_results = new ArrayList();

				// starts search - the called method will recurse to handle all nodes in array
				for (int i = 0 ; i < _searchNodes.Length ; i ++)
					SearchInXmlNode(
						_searchNodes[i]);

				// invokes "finish" event
				DelegateLib.InvokeSubscribers(
					OnEnd, 
					this);

				return (XmlSearchResult[])_results.ToArray(typeof(XmlSearchResult));
			}
			finally
			{
				_running = false;
			}
		}
		
		
		
		/// <summary> 
		/// Recursive loop that does string search on an xml node 
		/// </summary>
		/// <param name="nXmlSearch"></param>
		private void SearchInXmlNode(
			XmlNode currentNode
			)
		{
		
			string strSearchFor				= "";
			string strSearchIn				= "";
			bool blnPartialMatchCheckPassed = false;
			bool brnProceed					= true;
			bool blnThisCheck				= false;
			int intFindPosition				= 0;
			int score						= 0;
			

			// #########################################################
			// steps ahead one
			// ---------------------------------------------------------
			DelegateLib.InvokeSubscribers(
				OnNext, 
				this);

			_currentStep ++;


			// #########################################################
			// do not search current node if it is the document element.
			// this is because we text search in node.value, which is
			// always needs a parent. doc element cannot have a parent
			// ---------------------------------------------------------
			if (currentNode.GetType() == typeof(XmlDocument))
				brnProceed = false;


			// #########################################################
			// check if current node's name is in seach node name list.
			// if node name list is null or empty, all nodes are 
			// searched
			// ---------------------------------------------------------
			if (brnProceed && (_searchNodeNames != null || _searchNodeNames.Length > 0))
			{
				// checks to see if current xmlNode's name is in the array of 
				// names to be searched in,
				for (int j = 0 ; j < _searchNodeNames.Length ; j ++)
					if (_searchNodeNames[j] == currentNode.ParentNode.Name)
					{
						blnThisCheck = true;
						break;
					}

				// if fail this test, this xmlnode will not be searched in
				if (!blnThisCheck)
					brnProceed = false;
			}


		
			
			// #########################################################
			// do not search current node if it has no value
			// ---------------------------------------------------------
			if (brnProceed && currentNode.Value == null)
				brnProceed = false;



			// #########################################################
			// ---------------------------------------------------------
			if (brnProceed)
			{

				
				// if case not important, force to lower
				strSearchIn = currentNode.Value;

				if (_ignoreCase)
					strSearchIn = strSearchIn.ToLower();


				for (int i = 0 ; i < _searchArgs.Length ; i ++)
				{

					// isolates searchFor and searchIn strings - this is necessary because more
					// advanced searches require that these strings be manipulated
					strSearchFor = _searchArgs[i];

						
					// actual match done here
					if (strSearchIn.IndexOf(strSearchFor) != -1) // <- actual match done here!
					{
						if (_partialMatch)
						{
							score ++;
						}
						else 
						{

							// if full word matches are needed, end up here
							// need to determine if the match made is for a full word - the chars immediately before
							// and after the matched searchIn string must be non-alphanumeric or whitespace
							blnPartialMatchCheckPassed = true;
								

							// #################################################
							// handles start of word 
							// if there is text before match + // if char is alphanumeric
							// -------------------------------------------------
							intFindPosition = strSearchIn.IndexOf(strSearchFor);
							if (intFindPosition != 0 && (StringTypeTestLib.IsAlphanumeric(strSearchIn.Substring(intFindPosition - 1, 1))))
								blnPartialMatchCheckPassed = false;
								


							// #################################################
							// handles end of word
							// if char is alphanumeric
							// -------------------------------------------------
							intFindPosition = strSearchIn.IndexOf(strSearchFor) + strSearchFor.Length;
							if (intFindPosition < strSearchIn.Length - 1 &&(StringTypeTestLib.IsAlphanumeric(strSearchIn.Substring(intFindPosition, 1))))
								blnPartialMatchCheckPassed = false;

								

							if (blnPartialMatchCheckPassed)
								score ++;

						}// if
					}// if
				}// for

				if(score > 0)
				{
					double finalScore = (Convert.ToDouble(score)/_searchArgs.Length)*100;
					XmlSearchResult result = new XmlSearchResult(
						currentNode.ParentNode, 
						finalScore);
					
					_results.Add(result);

				}

			} // if


			// recursion happens here 
			for (int i = 0 ; i < currentNode.ChildNodes.Count ; i ++)
				SearchInXmlNode(currentNode.ChildNodes[i]);


		}

		

		#endregion

	} 
}
