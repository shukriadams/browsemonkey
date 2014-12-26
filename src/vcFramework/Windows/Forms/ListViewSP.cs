//////////////////////////////////////////////////////////////////////////////////////
// Author				: Shukri Adams												//
// Contact				: shukri.adams@gmail.com										//
// Compiler requirement : .Net 4.0													//
//																					//
// vcFramework : A reuseable library of utility classes                             //
// Copyright (C)																	//
//																					//
// This program is free software; you can redistribute it and/or modify it under	//
// the terms of the GNU General Public License as published by the Free Software	//
// Foundation; either version 2 of the License, or (at your option) any later		//
// version.																			//
//																					//
// This program is distributed in the hope that it will be useful, but WITHOUT ANY	//
// WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A	//
// PARTICULAR PURPOSE. See the GNU General Public License for more details.			//
//																					//
// You should have received a copy of the GNU General Public License along with		//
// this program; if not, write to the Free Software Foundation, Inc., 59 Temple		//
// Place, Suite 330, Boston, MA 02111-1307 USA										//
//////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using vcFramework;
using vcFramework.Collections;
using vcFramework.DataItems;
using vcFramework.Delegates;
using vcFramework.Threads;
using vcFramework.Xml;

namespace vcFramework.Windows.Forms
{
	public class ListViewSP : System.Windows.Forms.ListView, IStateManageable
	{

		#region MEMBERS
		
		/// <summary> Set to true if pressing the delete key 
		/// on this listview deletes currently selected items
		/// </summary>
		private bool m_blnAllowedKeyboardDeleteKeyDeleting;


		/// <summary> Set to true when columns are defined 
		/// </summary>
		private bool m_blnColumnsDefined;
	
	
		/// <summary> Set to true if this listview supports 
		/// hidden columns </summary>
		private bool m_blnSupportsHiddenColumns;

		
		/// <summary>  </summary>
		private bool m_blnContainsHiddenColumns;


		/// <summary> Set to true of this listview is using 
		/// an image list </summary>
		private bool m_blnUseImageList = false;								
		

		/// <summary> Set this to true to when listview is doing
		/// something during which content must not be changed
		/// from outside. This is mostly done when listview
		/// is busy with a background threaded insert of
		/// large amounts of data, and trying to intervene would
		/// cause thread locks or item collection errors. </summary>
		private bool m_blnBusyUpdating;


		/// <summary> Set to true if focus should 
		/// automatically be put on a newly-added listview 
		/// item <summary>
		private bool m_blnFocusNewItemOnInsert = true;

		
		/// <summary> Set to true if columns of width zero 
		/// should not be resizable</summary>
		private bool m_blnLockWidthOnZeroWidthColumns;


		/// <summary> Holds a count of how many visible
		/// colums there are in listview 
		/// (total columns - hidden columns = visibile columns 
		/// </summary>
		private int m_intVisibleColumnsCount;
		

		/// <summary> Holds the number of hidden columns.
		/// Using the m_arrStrHiddenColumnNames array
		/// is convenient only if there _are_ hidden
		/// columns. Otherwise there is too much null
		/// checking involved on m_arrStrHiddenColumnNames.
		///  </summary>
		private int m_intHiddenColumnCount;

		/// <summary> Array of bools matching column
		/// count - array is used as a map to indicate
		/// if a column of index matching the array 
		/// member's position is visible or not. For 
		/// example, if array member 0 is true, column
		/// 0 in listview is hidden. This is used as 
		/// a rapid way of determining, on row insert,
		/// if a given row item should be assigned to a
		/// visible or hidden column.</summary>
		private bool[] m_blnArrColumnIsHidden;


		/// <summary> Contains names of hidden columnsin 
		/// this listview in the order which they occur
		/// in </summary>
		private string[] m_arrStrHiddenColumnNames = null;


		/// <summary> Contains names of all columns, 
		/// hidden and visibile, in the order which 
		/// they occur in </summary>
		private string[] m_arrStrColumnNames = null;

		
		/// <summary> This int array is the size of _all_
		/// the columns in listview, ie, hidden + visible.
		/// It is used to rapidly map out the visibile column
		/// index for inserted data. Inserted data can be
		/// both visible and hidden - match the inserted items
		/// row position to the position in this array - the
		/// number there will indicate which visible column the
		/// data item must be in. A value of -1 is used to 
		/// indicate hidden columns  </summary>
		private int[] m_intArrVisibleColumnMapping;


		/// <summary> Xml doc that acts as holder for all 
		/// xml data returned by this listview object</summary>
		private XmlDocument m_dXmlCachedData = new XmlDocument();
		

		/// <summary> When inserting a large amount of rows into
		/// listview at once as a background threaded process,
		/// the xml is temporariyl stored in this field. </summary>
		private XmlNode m_nXmlLargeDataSetHolder;

		
		/// <summary> Used to hold the xmlnodelist used 
		/// to set the listview structure </summary>
		private XmlDocument m_dXmlCachedColumnStructure;


		/// <summary> Insert positions for new items for 
		/// this listview object </summary>
		private InsertPositions m_listViewInsertPosition;


		/// <summary> Sorting object used to sort columns 
		/// in this listview object </summary>
		private ColumnStringSorter m_objColumnSorter = new ColumnStringSorter();	
		

		/// <summary> Image list object used to hold icons 
		/// for this listview  </summary>
		private ImageList m_illvMain;


		/// <summary> Holds threads for processes such as as
		/// background Xml inserter  </summary>
		private ThreadCollection m_objThreadCollection;


		/// <summary> Thread object used to run background row 
		/// insert process (for inserting large numbers of rows
		/// in one step)</summary>
		private Thread m_thInsertRowProcess;


		/// <summary> Insert positions available for this 
		/// listivew object </summary>
		public enum InsertPositions : int 
		{
			Top,
			Bottom
		}
			

		
		/// <summary> Invoked whenever a  row is added or 
		/// removed from listview </summary>
		public event EventHandler RowCountChanged;
		

		
			
		#endregion
		

		#region PROPERTIES
		
		/// <summary> Gets if listview is busy with an 
		/// internal process updating its contents - 
		/// when in this state, doing ANYTHING from 
		/// outside the listview object could cause
		/// serious errors </summary>
		public bool BusyUpdating
		{
			get
			{
				return m_blnBusyUpdating;
			}
		}


		
		/// <summary> Gets or set the new item 
		/// insert position for this ListView 
		/// </summary>
		public InsertPositions InsertPosition		
		{
			set
			{
				m_listViewInsertPosition = value;
			}
			get
			{
				return m_listViewInsertPosition;
			}
		}

		

		/// <summary> Gets or sets if new item 
		/// should be focused upon insertion 
		/// </summary>
		public bool FocusNewItemOnInsert			
		{
			set
			{
				m_blnFocusNewItemOnInsert = value;
			}
			get
			{
				return m_blnFocusNewItemOnInsert;
			}
		}
		

		
		/// <summary> Gets or sets if deleting 
		/// directly from keyboard delete key is 
		/// allowed on this listview </summary>
		public bool AllowKeyboardDeleteKeyDeletion	
		{
			get
			{
				return m_blnAllowedKeyboardDeleteKeyDeleting;
			}
			set
			{
				m_blnAllowedKeyboardDeleteKeyDeleting = value;
			}
		}


		
		/// <summary> Gets or sets if zero width 
		/// columns should not be resizable 
		/// </summary>
		public bool LockWidthOnZeroWidthColumns		
		{
			set
			{
				m_blnLockWidthOnZeroWidthColumns = value;
			}
			get
			{
				return m_blnLockWidthOnZeroWidthColumns;
			}
		}
		

		#endregion
		

		#region CONSTRUCTORS

		/// <summary> Default constructor </summary>
		public ListViewSP(
			)
		{

			// eventhandling
			this.ColumnClick					+= new System.Windows.Forms.ColumnClickEventHandler(This_ColumnClick);
			this.KeyDown						+= new KeyEventHandler(This_KeyDown);
			this.Resize							+= new EventHandler(This_Resize);

			this.View							= View.Details;
			this.ListViewItemSorter				= m_objColumnSorter;

			// properties of base class which are set to force 
			// a particular "style" on this listview /
			this.FullRowSelect					= true;
			this.HideSelection					= false;
			this.AllowKeyboardDeleteKeyDeletion = false;
			m_blnSupportsHiddenColumns			= true;
			m_blnContainsHiddenColumns			= false;

			m_objThreadCollection = new ThreadCollection();

            // prevents flickering when updating
            this.DoubleBuffered = true;
			

		}
		

		#endregion
	

		#region DESTRUCTORS


		/// <summary> </summary>
		/// <param name="disposing"></param>
		protected override void Dispose(
			bool disposing
			)
		{
			// cannot close form while it is busy updating
			// content - doing so will cause horrible
			// cataclysms!
			//if (this.BusyUpdating)
			//	return;

			if (disposing)
				m_objThreadCollection.AbortAndRemoveAllThreads();

			base.Dispose(
				disposing
				);
		}


		#endregion

		
		#region METHODS
		

		
		/// <summary>Populates the list view from an Xml source 
		/// file, which MUST be of a certain format</summary>
		public void ColumnsSet(
			XmlNodeList lXmlColumnInfo
			)
		{

			ColumnHeaderSP objColumn			= null;
			bool blnColumIsHidden				= false;
			string[] arrStrTempHiddenColums		= null;
			int intColumnWidth					= 0;
			int intVisibileColumnMapperCount	= 0;

			// save nodelist info
			m_dXmlCachedColumnStructure				= new XmlDocument();
			m_dXmlCachedColumnStructure.InnerXml	= "<columnStructure/>";
			XmlDocumentFragment fXmlColumns			= m_dXmlCachedColumnStructure.CreateDocumentFragment();
			fXmlColumns.InnerXml					= lXmlColumnInfo[0].ParentNode.InnerXml;
				
			m_dXmlCachedColumnStructure.DocumentElement.AppendChild(fXmlColumns);

			// sets teh default hidden columns count -
			// actual value set at end of method.
			m_intHiddenColumnCount = 0;

			// sets default number of visible columns - invisible
			// columns will be factored in later in method
			m_intVisibleColumnsCount = lXmlColumnInfo.Count;


			// creates array of bools used to map out column visibility.
			m_blnArrColumnIsHidden = new bool[lXmlColumnInfo.Count];

				
			// creates array of strings to hold _all_ listview
			// column names in the order they occur in
			m_arrStrColumnNames = new string[lXmlColumnInfo.Count];


			// creates array for visible column mapping - note that
			// default value is set to -1 for all members. -1 
			// indicates a hidden column
			m_intArrVisibleColumnMapping = new int[lXmlColumnInfo.Count];
			for (int i = 0 ; i < m_intArrVisibleColumnMapping.Length ; i ++)
				m_intArrVisibleColumnMapping[i] = -1;


			//GENERATES ALL COLUMNS FROM XML FILE'S "HEADER" SECTION
			for ( int i = 0; i < lXmlColumnInfo.Count ; i ++)
			{

				// saves name to all-columns string array
				m_arrStrColumnNames[i] = lXmlColumnInfo[i].Attributes["internalName"].Value;


				// determines if column is hidden
				blnColumIsHidden			= false;
				m_blnArrColumnIsHidden[i]	= false;

				if (m_blnSupportsHiddenColumns && lXmlColumnInfo[i].Attributes["contentType"] != null)
				{
					if (lXmlColumnInfo[i].Attributes["contentType"].Value == ColumnHeaderSP.ContentTypes.Hidden.ToString())
					{	
						blnColumIsHidden = true;
						m_blnArrColumnIsHidden[i] = true;
					}
				}

				// gets column width from xml- needed as constructor argument
				intColumnWidth = 0;
				if(lXmlColumnInfo[i].Attributes["width"] != null)
					intColumnWidth = Int32.Parse(lXmlColumnInfo[i].Attributes["width"].Value); 


				//CREATES NEW COLUMN OBJECT - determines if there is to be a constructor argument or not
				if (lXmlColumnInfo[i].Attributes["contentType"] != null)
					if (lXmlColumnInfo[i].Attributes["contentType"].Value == ColumnHeaderSP.ContentTypes.Hidden.ToString())
					{
						// do nothing here ! hidden column has already been detected above
					}
					else
						objColumn = new ColumnHeaderSP(
							intColumnWidth, 
							(ColumnHeaderSP.ContentTypes)Enum.Parse(typeof(ColumnHeaderSP.ContentTypes), lXmlColumnInfo[i].Attributes["contentType"].Value));
				else
					objColumn = new ColumnHeaderSP(intColumnWidth);

					
				// sets properties of new column object, or if it is to be a hidden colum, sets up hidden column
				if (!blnColumIsHidden)
				{
					// sets value for current columns' position in visible column
					// mapping array
					m_intArrVisibleColumnMapping[i] = intVisibileColumnMapperCount;
					intVisibileColumnMapperCount ++;

					//SET PROPERTIES OF COLUMN OBJECT FROM XML FILE
					objColumn.Text			= lXmlColumnInfo[i].InnerText;
					objColumn.InternalText	= lXmlColumnInfo[i].Attributes["internalName"].Value;

					// gets width behaviour
					if (lXmlColumnInfo[i].Attributes["widthBehaviour"] != null)
						objColumn.WidthBehaviour = (ColumnHeaderSP.WidthBehaviours)Enum.Parse(typeof(ColumnHeaderSP.WidthBehaviours), lXmlColumnInfo[i].Attributes["widthBehaviour"].Value);

					// sets column alignment
					if (lXmlColumnInfo[i].Attributes["hAlign"] != null)
						objColumn.TextAlign = (HorizontalAlignment)Enum.Parse(typeof(HorizontalAlignment), lXmlColumnInfo[i].Attributes["hAlign"].Value);

					//ADDS COLUMN OBJECT TO LIST VIEW
					this.Columns.AddRange(new System.Windows.Forms.ColumnHeader[]{objColumn});

				}
				else
				{
					// hidden column made here
					if (m_arrStrHiddenColumnNames == null)
					{
						m_arrStrHiddenColumnNames = new string[1];
						m_arrStrHiddenColumnNames[0] = lXmlColumnInfo[i].Attributes["internalName"].Value;
					}
					else
					{
						// creates a new temp array, one larger than member array
						arrStrTempHiddenColums = new string[m_arrStrHiddenColumnNames.Length + 1];
							
						// transfers all member array contents to temp array
						for (int j = 0 ; j < m_arrStrHiddenColumnNames.Length ; j ++)
							arrStrTempHiddenColums[j] = m_arrStrHiddenColumnNames[j];
							
						// adds hidden colum to temp array
						arrStrTempHiddenColums[arrStrTempHiddenColums.Length - 1] = lXmlColumnInfo[i].Attributes["internalName"].Value;

						// overwrites contents of member array with contents of temp array
						m_arrStrHiddenColumnNames = arrStrTempHiddenColums;
					}
						
					// member will be used to indicate that this listview now contains
					// hidden columns - important because just because a listview SUPPORTS hidden columns,
					// doesnt mean that it actually has any. using m_blnContainsHiddenColumns makes it easier
					// to prevent accessing null arrays of hidden column names
					m_blnContainsHiddenColumns = true;
				}

				//DESTROYS TEMP
				objColumn = null;
			}
				
			//ASSIGNS IMAGE LIST, IF ONE HAS BEEN PASSED IN
			//NOTE - A COMPLETE IMAGE LIST MUST BE PASSED IN IN ORDER FOR THIS TO WORK
			if (m_illvMain != null)
			{
				this.SmallImageList = m_illvMain;
				m_blnUseImageList = true;

			}

			// factors hidden columns into visivle column count
			if (m_arrStrHiddenColumnNames != null)
			{	
				m_intHiddenColumnCount	= m_arrStrHiddenColumnNames.Length;
				m_intVisibleColumnsCount -= m_arrStrHiddenColumnNames.Length;
			}

			//SETS MEMBER TO TRUE
			m_blnColumnsDefined = true;

			// autofits columns to fit listview
			AutoFitColumns();
				
		}
		


		/// <summary> Returns XmlNodeList used to set the column 
		/// structure for this nodelist  </summary>
		/// <returns></returns>
		public XmlNodeList GetColumnStructure(
			)
		{
			if (m_dXmlCachedColumnStructure != null) 
				return m_dXmlCachedColumnStructure.DocumentElement.ChildNodes;
			return null;
		}
		


		/// <summary>Adds a line of data to the list box. Note
		/// that inserted items must be in the same order as
		/// their matching columns, or an exception with be 
		/// thrown.</summary>
		public void InsertRow(
			XmlNodeList lXmlLineData
			)
		{

			bool blnResetMultiSelect			= false;
			int intCurrentArrayIndex			= 0;
			int intHiddenColProcessCount		= 0;
			string [] strItems					= null;
			StringItem[] arrHiddenColumns		= null;
			ListViewItemSP objItem				= null;


				
				
			// ####################################################
			// CAN ONLY ADD A NEW LINE IF THE LIST VIEW HAS ALREADY 
			// BEEN POPULATED AND THE XML DOCUMENT USED TO DO THIS 
			// IS CACHED IN A CLASS MEMBER
			// ----------------------------------------------------
			if (!m_blnColumnsDefined)
				throw new Exception(
					"List view structure not yet defined."
					);


			// ####################################################
			// items placed in in visible columns will be inserted
			// via the string array strArrItems. If there are 
			// hidden columns, the size of this "visible" string
			// array will be reduced by the number of hidden
			// columns
			// ----------------------------------------------------
			strItems = new string[m_intVisibleColumnsCount];



			// ####################################################
			// makes an array of StringItems to hold hidden items
			// for this row
			// ----------------------------------------------------
			arrHiddenColumns = new StringItem[m_intHiddenColumnCount];



			// adds incoming xml data to "new row" string array
			for(int j = 0; j < lXmlLineData.Count ; j ++)
			{

				// ####################################################
				// Note that the count and naming sequence of inserted
				// items must match the column layout of the listview.
				// ----------------------------------------------------
				if (lXmlLineData[j].Name != m_arrStrColumnNames[j])
					throw new Exception(
						"Order of inserted items do not match the column order of listview."
						);

						

				// ####################################################
				// handles if it is hidden
				// ----------------------------------------------------
				if (m_blnArrColumnIsHidden[j])
				{
					// makes a new struct of data and adds to arraylist
					StringItem strtHiddenItem	= new StringItem();
					strtHiddenItem.Name			= lXmlLineData[j].Name;
					strtHiddenItem.Value		= XmlLib.RestoreReservedCharacters(lXmlLineData[j].InnerText);
							
					arrHiddenColumns[intHiddenColProcessCount] = strtHiddenItem;
					intHiddenColProcessCount ++;
				}
				else
				{
					// ####################################################
					// if item does not belong in a hidden column, must 
					// assign it to a visible column
					// ----------------------------------------------------
					strItems[intCurrentArrayIndex] = XmlLib.RestoreReservedCharacters(lXmlLineData[j].InnerText);
					intCurrentArrayIndex ++;
				} 


			} //for


			// creates a new listview item. Visible 
			// items are added here (string array)
			objItem = new ListViewItemSP(
				strItems
				);


			// hidden items are added to new listview item here
			if (m_blnContainsHiddenColumns)
				objItem.HiddenColumCollection = arrHiddenColumns;


			// fires eventhandler
			DelegateLib.InvokeSubscribers(
				RowCountChanged, 
				this);


			//ADDS NEW ITEM
			if (m_listViewInsertPosition == InsertPositions.Bottom)
			{

				// adds item				
				this.Items.Insert(
					this.Items.Count, 
					objItem
					);
						

				// focuses item
				if (m_blnFocusNewItemOnInsert)
				{

					// a focus can only be given, never taken away, 
					// therefore, if setting focus to new item,
					// each added item will gain focus. to ensure 
					// that only the latest added item is focused,
					// need to temporarily set multiselect to false 
					// when assigning focus. multiselect is restored
					// to its first value immediately afterwards
					if (this.MultiSelect)
					{
						this.MultiSelect = false;
						blnResetMultiSelect = true;
					}


					// focus assigned here
					//this.Items[this.Items.Count - 1].Selected = true;
					this.EnsureVisible(
						objItem.Index
						);


					// reset multifocus
					if (blnResetMultiSelect)
						this.MultiSelect = true;

				}//if 
			}
			else if (m_listViewInsertPosition == InsertPositions.Top)
			{

				// adds item
				this.Items.Insert(
					0, 
					objItem
					);
						
				// focuses item
				if (m_blnFocusNewItemOnInsert)
				{
					// set comments above
					if (this.MultiSelect)
					{
						this.MultiSelect = false;
						blnResetMultiSelect = true;
					}
							
					// focus assigned here
					this.EnsureVisible(
						objItem.Index
						);

					// reset multifocus
					if (blnResetMultiSelect)
						this.MultiSelect = true;
				}					
			} // if
						

		}



		/// <summary> Used for inserting very large amounts of data at once.
		/// is "hidden" behind two other methods </summary>
		private void InsertRows(
			)
		{



			bool blnResetMultiSelect		= false;
			int intCurrentArrayIndex		= 0;
			int intHiddenColProcessCount	= 0;
			string [] strItems				= null;
			StringItem[] arrHiddenColumns	= null;
			ListViewItemSP[] allItems		= new ListViewItemSP[m_nXmlLargeDataSetHolder.ChildNodes.Count];
			XmlNodeList lXmlLineData;
				
				
			// ####################################################
			// CAN ONLY ADD A NEW LINE IF THE LIST VIEW HAS ALREADY 
			// BEEN POPULATED AND THE XML DOCUMENT USED TO DO THIS 
			// IS CACHED IN A CLASS MEMBER
			// ----------------------------------------------------
			if (!m_blnColumnsDefined)
				throw new Exception(
					"List view structure not yet defined."
					);


			// ####################################################
			// items placed in in visible columns will be inserted
			// via the string array strArrItems. If there are 
			// hidden columns, the size of this "visible" string
			// array will be reduced by the number of hidden
			// columns
			// ----------------------------------------------------
			strItems = new string[m_intVisibleColumnsCount];



			// ####################################################
			// makes an array of StringItems to hold hidden items
			// for this row
			// ----------------------------------------------------
			arrHiddenColumns = new StringItem[m_intHiddenColumnCount];
			
			for (int i = 0 ; i < m_nXmlLargeDataSetHolder.ChildNodes.Count ; i ++)
			{

				lXmlLineData = m_nXmlLargeDataSetHolder.ChildNodes[i].ChildNodes;

				intCurrentArrayIndex = 0;
				intHiddenColProcessCount = 0;

				// adds incoming xml data to "new row" string array
				for(int j = 0; j < lXmlLineData.Count ; j ++)
				{

					// ####################################################
					// Note that the count and naming sequence of inserted
					// items must match the column layout of the listview.
					// ----------------------------------------------------
					if (lXmlLineData[j].Name != m_arrStrColumnNames[j])
						throw new Exception(
							"Order of inserted items do not match the column order of listview");

						

					// ####################################################
					// handles if it is hidden
					// ----------------------------------------------------
					if (m_blnArrColumnIsHidden[j])
					{
						// makes a new struct of data and adds to arraylist
						StringItem strtHiddenItem	= new StringItem();
						strtHiddenItem.Name			= lXmlLineData[j].Name;
						strtHiddenItem.Value		= XmlLib.RestoreReservedCharacters(lXmlLineData[j].InnerText);
						
						arrHiddenColumns[intHiddenColProcessCount] = strtHiddenItem;
						intHiddenColProcessCount ++;
					}
					else
					{
						// ####################################################
						// if item does not belong in a hidden column, must 
						// assign it to a visible column
						// ----------------------------------------------------
						strItems[intCurrentArrayIndex] = XmlLib.RestoreReservedCharacters(lXmlLineData[j].InnerText);
						intCurrentArrayIndex ++;
					} 


				} //for
/*
				// creates a new listview item. Visible 
				// items are added here (string array)
				ListViewItemSP row = new ListViewItemSP(
					strItems);

				// hidden items are added to new listview item here
				if (m_blnContainsHiddenColumns)
					row.HiddenColumCollection = arrHiddenColumns;
*/
				allItems[i] = new ListViewItemSP(
					strItems);

				if (m_blnContainsHiddenColumns)
					allItems[i].HiddenColumCollection = arrHiddenColumns;
			


				
			//	this.Items.Insert(
			//		0,
			//		row);	

			}


			this.Items.AddRange(
				allItems);	

			// fires eventhandler
			DelegateLib.InvokeSubscribers(
				RowCountChanged, 
				this);




		}


		/// <summary>Adds a line of data to the list box. Note
		/// that inserted items must be in the same order as
		/// their matching columns, or an exception with be 
		/// thrown.</summary>
		public void InsertRow(
			StringItem[] arrDataItems
			)
		{

			bool blnResetMultiSelect			= false;
			int intHiddenColProcessCount		= 0;
			int intCurrentArrayIndex			= 0;
			string [] strItems					= null;
			StringItem[] arrHiddenColumns		= null;
			ListViewItemSP objItem				= null;

				
			// CAN ONLY ADD A NEW LINE IF THE LIST VIEW HAS ALREADY BEEN POPULATED 
			// AND THE XML DOCUMENT USED TO DO THIS IS CACHED IN A CLASS MEMBER
			if (!m_blnColumnsDefined)
			{
				Exception e = new Exception("List view structure not yet defined.");
				throw e;
			}
				

			// ####################################################
			// items placed in in visible columns will be inserted
			// via the string array strArrItems. If there are 
			// hidden columns, the size of this "visible" string
			// array will be reduced by the number of hidden
			// columns
			// ----------------------------------------------------
			strItems = new string[m_intVisibleColumnsCount];



			// ####################################################
			// makes an array of StringItems to hold hidden items
			// for this row
			// ----------------------------------------------------
			arrHiddenColumns = new StringItem[m_intHiddenColumnCount];



			// adds incoming xml data to "new row" string array
			for(int j = 0; j < arrDataItems.Length ; j ++)
			{

				// ####################################################
				// Note that the count and naming sequence of inserted
				// items must match that of the listview.
				// ----------------------------------------------------
				if (arrDataItems[j].Name != m_arrStrColumnNames[j])
				{
					Exception e = new Exception("Order of inserted items do not match the column order of listview.");
					throw e;
				}
						



				// ####################################################
				// handles if it is hidden
				// ----------------------------------------------------
				if (m_blnArrColumnIsHidden[j])
				{
					// makes a new struct of data and adds to arraylist
					StringItem strtHiddenItem	= new StringItem();
					strtHiddenItem.Name			= arrDataItems[j].Name;
					strtHiddenItem.Value		= XmlLib.RestoreReservedCharacters(arrDataItems[j].Value);

					arrHiddenColumns[intHiddenColProcessCount] = strtHiddenItem;
					intHiddenColProcessCount ++;
				}
				else
				{
					strItems[intCurrentArrayIndex] = arrDataItems[m_intArrVisibleColumnMapping[j]].Value;
					intCurrentArrayIndex ++;
				}

			} //for


			//ADDS STRING ARRAY TO LISTBOX
			objItem = new ListViewItemSP(
				strItems
				);


			// adds hidden items to new listViewItem
			if (m_blnContainsHiddenColumns)
				objItem.HiddenColumCollection = arrHiddenColumns;


			// invokes eventhandler
			DelegateLib.InvokeSubscribers(
				RowCountChanged, 
				this);


			//ADDS NEW ITEM
			if (m_listViewInsertPosition == InsertPositions.Bottom)
			{

				// adds item				
				this.Items.Insert(
					this.Items.Count, 
					objItem
					);

				// focuses item
				if (m_blnFocusNewItemOnInsert)
				{
		
					// a focus can only be given, never taken away, therefore, if setting focus to new item,
					// each added item will gain focus. to ensure that only the latest added item is focused,
					// need to temporarily set multiselect to false when assigning focus. multiselect is restored
					// to its first value immediately afterwards
					if (this.MultiSelect)
					{
						this.MultiSelect = false;
						blnResetMultiSelect = true;
					}

					// focus assigned here
					//this.Items[this.Items.Count - 1].Selected = true;
					this.EnsureVisible(
						objItem.Index
						);

					// reset multifocus
					if (blnResetMultiSelect)
						this.MultiSelect = true;

				}
			}
			else if (m_listViewInsertPosition == InsertPositions.Top)
			{
				// adds items
				this.Items.Insert(
					0, 
					objItem
					);
						

				// focuses item
				if (m_blnFocusNewItemOnInsert)
				{
				
					// set comments above
					if (this.MultiSelect)
					{
						this.MultiSelect = false;
						blnResetMultiSelect = true;
					}
							
					// focus assigned here
					this.EnsureVisible(
						objItem.Index
						);

					// reset multifocus
					if (blnResetMultiSelect)
						this.MultiSelect = true;
				} //if					
			} // if
					

		
		}



		/// <summary> Inserts a large number of records into listview
		/// using a background thread process. Inserting will 
		/// fail if this object is busy with a previous job </summary>
		/// <param name="nXmlAllData"></param>
		public void InsertRows(
			XmlNode nXmlAllData
			)
		{

	
				if (!m_blnBusyUpdating)
				{
					m_blnBusyUpdating					= true;

					// stores xlm to member
					m_nXmlLargeDataSetHolder			= nXmlAllData;

					
					m_thInsertRowProcess				= new Thread(new ThreadStart(InsertRows_AsBackgroundThread));
					m_thInsertRowProcess.Name			= "BackgroundXmlInsertProcess";	
					m_thInsertRowProcess.IsBackground	= true;
					m_thInsertRowProcess.Priority		= System.Threading.ThreadPriority.Normal;
			
					m_objThreadCollection.AddThread(m_thInsertRowProcess);
			
					// NOTE : due to problems with stability, the threaded 
					// process for inserting data is being temporarily
					// disabled here
					//		m_thInsertRowProcess.Start();
					InsertRows_AsBackgroundThread();

				}


		}
		

		
		/// <summary> Back-end method for InsertRows(). Is meant
		/// to be called as a background thread, and will insert 
		/// all data in m_nXmlLargeDataSetHolder member into
		/// listview.  </summary>
		private void InsertRows_AsBackgroundThread(
			)
		{

			bool blnOriginFocusNewItemOnInsert = this.FocusNewItemOnInsert;
			this.FocusNewItemOnInsert = false;

			// you will probably notice that we dont user BeginUpdate() and
			// EndUpdate() here ... that's because doing so can lock the 
			// entire app UI when populating the listview ...
			

			this.InsertRows();


			//for (int i = 0 ; i < m_nXmlLargeDataSetHolder.ChildNodes.Count ; i ++)
			//	this.InsertRow(
			//		m_nXmlLargeDataSetHolder.ChildNodes[i].ChildNodes
			//		);

			this.FocusNewItemOnInsert = blnOriginFocusNewItemOnInsert;

			m_blnBusyUpdating = false;

		}


		
		/// <summary> Gets a cell value for a given row at a 
		/// given column name</summary>
		/// <param name="intRowNumber"></param>
		/// <param name="strColumnName"></param>
		/// <returns></returns>
		public string GetCellValue(
			int intRowNumber, 
			string strColumnName
			)
		{
			
			int intColNumber = -1;
			
			ColumnHeaderSP objTempColHeader = null;
			ListViewItemSP objRow = null;

			try
			{
				if (this.Items.Count > intRowNumber)
				{
					
					//DETERMINES THE COLUMN NUMBER OF strColumnName
					for (int i = 0 ; i < this.Items[0].SubItems.Count ; i ++)
					{
						objTempColHeader = (ColumnHeaderSP)this.Columns[i];

						if (objTempColHeader.InternalText == strColumnName)
						{
							intColNumber = i;
							break;
						}
					
					}
					
					// tries to find value in hidden columns if column has not yet been found in visible columns, and this
					// listview supports hidden columns
					if (intColNumber == -1 && m_blnContainsHiddenColumns)
					{

						objRow = (ListViewItemSP)this.Items[intRowNumber];

						// returns value and exits method
						for (int i = 0 ; i < objRow.HiddenColumCollection.Length ; i ++)
							if (objRow.HiddenColumCollection[i].Name == strColumnName)
								return objRow.HiddenColumCollection[i].Value;

					}
					else
					{
						// "normal" operation
						if (intColNumber != -1 && this.Items[intRowNumber].SubItems[intColNumber] != null)
							return (this.Items[intRowNumber].SubItems[intColNumber].Text);
					}
					
				} //if
			}
			finally
			{
				objTempColHeader = null;
			}
			return null;// if no match, returns null
		}

		
		
		/// <summary>
		/// Exposes XmlNodeList of data from row of view list that is currently selected
		/// </summary>
		public XmlNodeList GetSelectedRows(
			)
		{
			//CREATES AN XML FRAGMENT TO HOLD TEMPORARY SELECTED DATA
			XmlDocumentFragment fXmlSelectedData = m_dXmlCachedData.CreateDocumentFragment();
			//RESETS THE SELECTED DATA XML DOCUMENT TO EMPTY
			m_dXmlCachedData.InnerXml = "<SelectedData/>";
			string strTempLineXML = "";
			ColumnHeaderSP objTempColumnHeader = null;
			ListViewItemSP objRow = null;

		
			try
			{
				

				//FOR EACH SELECTED ROW, CREATES XML AND APPENDS TO m_dXmlSelectedData 
				for (int i = 0 ; i < this.SelectedItems.Count ; i ++)
				{
					strTempLineXML = "";

					// adds visible columns data to xml
					for (int j = 0 ; j < this.SelectedItems[i].SubItems.Count ; j ++)
					{
						objTempColumnHeader = (ColumnHeaderSP)this.Columns[j];
						strTempLineXML += "<" + objTempColumnHeader.InternalText + ">" + XmlLib.FilterForReservedXmlCharacters(this.SelectedItems[i].SubItems[j].Text) + "</" + objTempColumnHeader.InternalText + ">";
					}
							
					// adds hidden column data to xml
					if (m_blnContainsHiddenColumns)
					{
						objRow = (ListViewItemSP)this.SelectedItems[i];
								
						for (int k = 0 ; k < objRow.HiddenColumCollection.Length ; k ++)
							strTempLineXML += "<" + objRow.HiddenColumCollection[k].Name + ">" + XmlLib.FilterForReservedXmlCharacters(objRow.HiddenColumCollection[k].Value) + "</" + objRow.HiddenColumCollection[k].Name + ">";
								
					}

					strTempLineXML				= "<selectedLine>" + strTempLineXML + "</selectedLine>";
					fXmlSelectedData.InnerXml	= strTempLineXML;
					m_dXmlCachedData.DocumentElement.AppendChild(fXmlSelectedData);

				}




			}
			finally
			{
				fXmlSelectedData = null;
				objTempColumnHeader = null;
			}
			
			return m_dXmlCachedData.DocumentElement.ChildNodes;
		}
		
	
	
		/// <summary> Exposes XmlNodeList of data from a 
		/// specified row number</summary>
		/// <returns></returns>
		public XmlNodeList GetRow(
			int intRowNumber
			)
		{

			string strTempLineXML					= "";
			ColumnHeaderSP objTempColumnHeader		= null;
			ListViewItemSP objRow					= null;
			XmlDocumentFragment fXmlSelectedData	= m_dXmlCachedData.CreateDocumentFragment();	//CREATES AN XML FRAGMENT TO HOLD TEMPORARY SELECTED DATA
			m_dXmlCachedData.InnerXml				= "<SelectedData/>";	//RESETS THE SELECTED DATA XML DOCUMENT TO EMPTY

			try
			{
				
				if (this.Items.Count > intRowNumber && this.Items.Count > 0)
				{
					strTempLineXML = "";
					
					// gets visible items
					for (int j = 0 ; j < this.Items[intRowNumber].SubItems.Count ; j ++)
					{
						objTempColumnHeader = (ColumnHeaderSP)this.Columns[j];
						strTempLineXML += "<" + objTempColumnHeader.InternalText + ">" + XmlLib.FilterForReservedXmlCharacters(this.Items[this.Items[intRowNumber].Index].SubItems[j].Text) + "</" + objTempColumnHeader.InternalText + ">";
					}

					// gets hidden columns
					if (m_blnContainsHiddenColumns)
					{
						objRow = (ListViewItemSP)this.Items[intRowNumber];
								
						for (int k = 0 ; k < objRow.HiddenColumCollection.Length ; k ++)
						{
							strTempLineXML += "<" + objRow.HiddenColumCollection[k].Name + ">" + XmlLib.FilterForReservedXmlCharacters(objRow.HiddenColumCollection[k].Value) + "</" + objRow.HiddenColumCollection[k].Name + ">";
						}
								
					}

					strTempLineXML = "<selectedLine>" + strTempLineXML + "</selectedLine>";
					fXmlSelectedData.InnerXml = strTempLineXML;
					m_dXmlCachedData.DocumentElement.AppendChild(fXmlSelectedData);
				}
			}
			finally
			{
				fXmlSelectedData = null;
				objTempColumnHeader = null;
			}
			return m_dXmlCachedData.DocumentElement.ChildNodes;
		}

		
		
		/// <summary> Exposes XmlNodeList of all data from 
		/// list view  </summary>
		/// <returns></returns>
		public XmlNodeList GetAllRows(
			)
		{
			//CREATES AN XML FRAGMENT TO HOLD TEMPORARY SELECTED DATA
			XmlDocumentFragment fXmlData = m_dXmlCachedData.CreateDocumentFragment();
			//RESETS THE SELECTED DATA XML DOCUMENT TO EMPTY
			m_dXmlCachedData.InnerXml = "<Page/>";
			string strTempLineXML = "";
			ColumnHeaderSP objTempColumnHeader = null;
			ListViewItemSP objRow = null;

			try
			{
				//FOR EACH SELECTED ROW, CREATES XML AND APPENDS TO m_dXmlSelectedData 
				for (int i = 0 ; i < this.Items.Count ; i ++)
				{
					strTempLineXML = "";
					
					// gets visible items
					for (int j = 0 ; j < this.Items[i].SubItems.Count ; j ++)
					{
						objTempColumnHeader = (ColumnHeaderSP)this.Columns[j];
						strTempLineXML += "<" + objTempColumnHeader.InternalText + ">" + XmlLib.FilterForReservedXmlCharacters(this.Items[this.Items[i].Index].SubItems[j].Text) + "</" + objTempColumnHeader.InternalText + ">";
					}

					// gets hidden columns
					if (m_blnContainsHiddenColumns)
					{
						objRow = (ListViewItemSP)this.Items[i];
								
						for (int k = 0 ; k < objRow.HiddenColumCollection.Length ; k ++)
							strTempLineXML += "<" + objRow.HiddenColumCollection[k].Name + ">" + XmlLib.FilterForReservedXmlCharacters(objRow.HiddenColumCollection[k].Value) + "</" + objRow.HiddenColumCollection[k].Name + ">";
								
					}

					strTempLineXML = "<Line>" + strTempLineXML + "</Line>";
					fXmlData.InnerXml = strTempLineXML;
					m_dXmlCachedData.DocumentElement.AppendChild(fXmlData);
				}
			}
			finally
			{
				fXmlData = null;
				objTempColumnHeader = null;
			}
			return m_dXmlCachedData.DocumentElement.ChildNodes;		
		}

		

		/// <summary> Returns all data in list view as 
		/// tab-deliniated, line-broken ASCII text </summary>
		/// <returns></returns>
		public string GetAllRowsAsText(
			)
		{
			bool [] arrBlnDummy = null;
			int intColumnCount = 0;

			// makes bool array matching count of visible + invisible columns
			intColumnCount = this.Columns.Count;
			if (m_arrStrHiddenColumnNames !=null )
				intColumnCount += m_arrStrHiddenColumnNames.Length;

			arrBlnDummy = new bool[intColumnCount];
				
			// makes a dummy array of bools with all set to true - ensures that data for all columns will be returned
			for (int i = 0 ; i < intColumnCount ; i ++)
			{
				arrBlnDummy[i] = true;
			}

			return InternalAllRowsAsText("\t", "\r\n", arrBlnDummy);



			return "";
		}
		


		/// <summary> Returns all data in list view as ASCII 
		/// text, with user-defined line breaks and 
		/// deliniators </summary>
		/// <param name="strDelimiter"></param>
		/// <returns></returns>
		public string AllRowsAsText(
			string strDelimiter, 
			string strLineBreak
			)
		{
			bool [] arrBlnDummy = null;
			int intColumnCount = 0;

				// makes bool array matching count of visible + invisible columns
				intColumnCount = this.Columns.Count;
				if (m_arrStrHiddenColumnNames !=null )
					intColumnCount += m_arrStrHiddenColumnNames.Length;

				arrBlnDummy = new bool[intColumnCount];


				// makes a dummy array of bools with all set to true - ensures that data for all columns will be returned
				for (int i = 0 ; i < intColumnCount ; i ++)
				{
					arrBlnDummy[i] = true;
				}
				
				return InternalAllRowsAsText(strDelimiter, strLineBreak, arrBlnDummy);
		}
		


		/// <summary> Internal method : converts all data in 
		/// listview into ascii text using the supplied
		/// delimiter. </summary>
		/// <param name="strDelimiter"></param>
		/// <returns></returns>
		private string InternalAllRowsAsText(
			string strDelimiter, 
			string strLineBreak, 
			bool [] intColumnNrs
			)
		{
			StringBuilder strOutput = new StringBuilder();
			bool blnLineHasText = false;
			ListViewItemSP objRow = null;

			//FOR EACH SELECTED ROW, CREATES XML AND APPENDS TO m_dXmlSelectedData 
			for (int i = 0 ; i < this.Items.Count ; i ++)
			{	
				blnLineHasText = false;
					
				// handles visible columns
				for (int j = 0 ; j < this.Items[i].SubItems.Count ; j ++)
				{
						
					if (intColumnNrs[j] == true)
					{
						strOutput.Append (this.Items[this.Items[i].Index].SubItems[j].Text + strDelimiter);
						blnLineHasText = true;
					}

				}

				// handles hidden columns
				if (m_blnContainsHiddenColumns)
				{
					objRow = (ListViewItemSP)this.Items[i];

					for (int j = 0 ; j < objRow.HiddenColumCollection.Length ; j ++)
					{
						
						strOutput.Append (objRow.HiddenColumCollection[j].Value + strDelimiter) ;
						blnLineHasText = true;
					}
				}

				// adds linebreak, only if text has been added for this line
				if (blnLineHasText)
					strOutput.Append(strLineBreak);
			}

			return strOutput.ToString();

		}		
		


		/// <summary>HIDDEN - ERROR - hidden columns render 
		/// this method unuseable - phase it out. Sets a 
		/// specific cell value in the list view</summary>
		private void SetCellValue(
			string strValue, 
			int intRowNumber,
			int intColNumber
			)
		{

			if (m_blnColumnsDefined)
			{
				if (this.Items.Count > intRowNumber)
				{
					//SETS VALUE IN LISTBOX
					if (this.Items[intRowNumber].SubItems[intColNumber] != null)
						this.Items[intRowNumber].SubItems[intColNumber].Text = strValue;
				
				}
				
			}

		}



		/// <summary>Sets a specific cell value in a list 
		/// view, taking column name as input instead of 
		/// column number</summary>
		public void SetCellValue(
			string strValue, 
			int intRowNumber, 
			string strColumnName
			)
		{
			
			int intColNumber = -1 ;
			ColumnHeaderSP objTempColHeader = null;
			ListViewItemSP objRow = null;

			try
			{
				if (m_blnColumnsDefined)
				{
					if (this.Items.Count > intRowNumber)
					{
						// tries to set value in hidden columns
						if (m_blnContainsHiddenColumns)
						{
							objRow = (ListViewItemSP)this.Items[intRowNumber];
							for (int i = 0 ; i < objRow.HiddenColumCollection.Length ; i ++)
							{
								if (objRow.HiddenColumCollection[i].Name == strColumnName)
								{
									objRow.HiddenColumCollection[i].Value = strValue;
									// exits !
									return;
								}
							}

						}
						
							
						// tries to set value in visible column
						for (int i = 0 ; i < this.Items[0].SubItems.Count ; i ++)
						{
							objTempColHeader = (ColumnHeaderSP)this.Columns[i];

							if (objTempColHeader.InternalText == strColumnName)
							{
								intColNumber = i;
								break;
							}
						
						}

						if (intColNumber != -1 && this.Items[intRowNumber].SubItems[intColNumber] != null)
						{
							this.Items[intRowNumber].SubItems[intColNumber].Text = strValue;
						}
					
					
					}
				
				}
			}
			finally
			{
				objTempColHeader = null;
			}
		}


		
		/// <summary>Removes a specific line of data from 
		/// listbox </summary>
		public void RemoveRow(
			int intLineNumber
			)
		{

			if (this.Items.Count > intLineNumber)
			{
				//REMOVES LINE FROM LISTBOX
				this.Items.Remove(this.Items[intLineNumber]);

				// invokes eventhandler
				DelegateLib.InvokeSubscribers(RowCountChanged, this);

			}

	
		}



		/// <summary>Removes selected lines of data, as 
		/// well as internally cached data</summary>
		public void RemoveSelectedRows(
			)
		{

			ArrayList arrSelectedNumbers	= new ArrayList();
			
			int i = 0;
			int intSelectedRowCount = 0;
			
			try
			{
				if (this.SelectedItems.Count > 0)
				{
					//ASSIGNS SELECTED ROW COUNT TO VARIABLE
					intSelectedRowCount = this.SelectedItems.Count;
					
					//FOR EACH SELECTED ROW, GETS XML DATA FROM CACHED DATA AND COPIES TO SELECTED-DATA XML DOCUMENT
					for (i = 0 ; i < intSelectedRowCount ; i ++)
					{
						arrSelectedNumbers.Add(
							this.SelectedItems[i].Index
							);
					}

					//REMOVES SELECTED ROWS FROM LIST VIEW
					for (i = 0 ; i < intSelectedRowCount ; i ++)
					{
						this.Items.RemoveAt((int)arrSelectedNumbers[intSelectedRowCount - 1 - i]);

						// invokes eventhandler
						DelegateLib.InvokeSubscribers(
							RowCountChanged, 
							this);

					}

				}
			}
			finally
			{
				arrSelectedNumbers = null;
			}
		}



		/// <summary>Sets image index, allowing icon to 
		/// be set on a row</summary>
		public void SetRowIcon(
			int intRowNumber, 
			int intImageIndex
			)
		{

			if (this.Items.Count > intRowNumber)
				if (intImageIndex < this.SmallImageList.Images.Count)
					this.Items[intRowNumber].ImageIndex = intImageIndex;

		}
		


		/// <summary> Sets the row icon for all selected 
		/// rows </summary>
		/// <param name="intImageIndex"></param>
		private void SetSelectedRowsIcon(
			int intImageIndex
			)
		{

			if (this.SelectedItems.Count > 0)
				for (int i = 0 ; i < this.SelectedItems.Count ; i ++)
					SetRowIcon(this.SelectedItems[i].Index, intImageIndex);

		}
		

	
		/// <summary></summary>
		/// <param name="evenColor"></param>
		/// <param name="oddColor"></param>
		public void SetRowColors(
			Color evenColor,
			Color oddColor
			)
		{

			for (int i = 0 ; i < this.Items.Count ; i ++ )
			{
				if (i % 2 == 0)
					this.Items[i].BackColor = evenColor;
				else
					this.Items[i].BackColor = oddColor;
			}

		}



		/// <summary>Sets the font color for a given 
		/// row.</summary>
		public void SetRowFontColor(
			int intRowNumber, 
			Color color
			)
		{
			if (intRowNumber < this.Items.Count)
			{
				this.Items[intRowNumber].ForeColor = color;
			}
		}



		/// <summary> Sorts the listview object to 
		/// the column name specified </summary>
		/// <param name="strColumnName"></param>
		public void SortByColumn(
			string strColumnName
			)
		{
			// ** THIS METHOD DOESNT WORK YET !!!
			ColumnHeaderSP objColumnCheck = null;

			// finds the column object
			for (int i = 0 ; i < this.Columns.Count ; i ++)
			{
				objColumnCheck = (ColumnHeaderSP)this.Columns[i];
				if (objColumnCheck.Text == strColumnName || objColumnCheck.InternalText == strColumnName)
				{
					//m_objColumnSorter.currentColumn  = objColumnCheck;
					//objTempColHeader = objColumnCheck ;//(clsColumnHeaderSP)this.Columns[e.Column];
					m_objColumnSorter.sortOrder = objColumnCheck.SortingOrder;
				
					//sets default sort order
					objColumnCheck.SortingOrder = vcFramework.Collections.SortingOrder.Descending;
					
					//if (objTempColHeader.SortingOrder = vcFramework.Collections.clsSorting.SortingOrder.Ascending)
					this.Sort();
					
					break;
				}
			}

				

		}


		
		/// <summary> Collapses a column of a given name 
		/// - ie, sets its width to zero, but in a state 
		/// where the width can be reset to the 
		/// precollapse state again </summary>
		/// <param name="strColumnName"></param>
		public void CollapseColumn(
			string strColumnName
			)
		{
			ColumnHeaderSP objTempColumn = null;

			for (int i = 0 ; i < this.Columns.Count ; i ++)
			{
				objTempColumn = (ColumnHeaderSP)this.Columns[i];
				if (objTempColumn.InternalText == strColumnName)
				{
					objTempColumn.WidthBeforeCollapse = objTempColumn.Width;
					objTempColumn.Width = 0;
					AutoFitColumns();

					break;
				}
			}
		}
		


		/// <summary> Expands a column of a given name. 
		/// Only affects columns that have been previously 
		/// collapsed, and which currently have a weight 
		/// of zero. </summary>
		/// <param name="strColumnName"></param>
		public void UncollapseColumn(
			string strColumnName
			)
		{
			ColumnHeaderSP objTempColumn = null;

			for (int i = 0 ; i < this.Columns.Count ; i ++)
			{
				objTempColumn = (ColumnHeaderSP)this.Columns[i];
				
				if (objTempColumn.InternalText == strColumnName && objTempColumn.Width == 0 && objTempColumn.WidthBeforeCollapse != 0)
				{
					objTempColumn.Width = objTempColumn.WidthBeforeCollapse;
					objTempColumn.WidthBeforeCollapse = 0;
					AutoFitColumns();

					break;
				}
			}		
		}
		
		
		
		public void ToggleColumnCollapseState(
			string strColumnName
			)
		{
			ColumnHeaderSP objTempColumn = null;

			for (int i = 0 ; i < this.Columns.Count ; i ++)
			{
				objTempColumn = (ColumnHeaderSP)this.Columns[i];
				
				if (objTempColumn.InternalText == strColumnName)
				{
					// determines what columns' current state is, and what reaction to take to it
					if (objTempColumn.Width == 0 && objTempColumn.WidthBeforeCollapse != 0)
					{
						// column is collapse and can be expanded
						UncollapseColumn(strColumnName);
						break;
					}
					else if (objTempColumn.Width != 0 && objTempColumn.WidthBeforeCollapse == 0)
					{
						CollapseColumn(strColumnName);
						break;
					}

					break;
				}
			}			
		}
		
		

		/// <summary> Controls autmatatic column resize 
		/// behaviour </summary>
		public void AutoFitColumns(
			)
		{
			ColumnHeaderSP objTempColHeader = null;
			int intWidthOffset = 0;
			int intAutofitColumnCount = 0;

			// calculates how much width change IN TOTAL will be necesary
			// also, calculates how many autofit colums are in listvew
			for (int i = 0 ; i < this.Columns.Count ; i ++)
			{
				// autofitcolum count stuff
				objTempColHeader = (ColumnHeaderSP)this.Columns[i];
				// NOTE : zero width columns are ignored, even their widthbehaviour IS set to autofit - this is to prevent
				// "collapsed" columns that are ALSO autfitting to autofit when in collapsed state
				if (objTempColHeader.WidthBehaviour == ColumnHeaderSP.WidthBehaviours.Autofit && objTempColHeader.Width != 0)
					intAutofitColumnCount++;
	
				// offset stuff
				intWidthOffset += this.Columns[i].Width;
			}

			// offset stuff
			intWidthOffset = this.Width - intWidthOffset;
			
			// need to factor the vertical scrollbar's presence as well - if it is drawn
			// its width interferes with autofitting, because it takes up part of the client
			// area of the listview - ideally, need to find out if it is visible or not, and factor
			// it into calculations. for now, assume it is always present
			intWidthOffset -= 21;	// the number substracted here is a thumbsuck

			// adjusts all columns that are set to autofit
			for (int i = 0 ; i < this.Columns.Count ; i ++)
			{
				objTempColHeader = (ColumnHeaderSP)this.Columns[i];
				// NOTE - collapsed columns are NOT resized, even if they are set to autofit
				if (objTempColHeader.WidthBehaviour == ColumnHeaderSP.WidthBehaviours.Autofit && objTempColHeader.Width != 0)
					this.Columns[i].Width += intWidthOffset/intAutofitColumnCount;
			}	
		}


		
		/// <summary> Counts how many times a given column 
		/// contains a given value</summary>
		/// <param name="strColumnName"></param>
		/// <param name="strValue"></param>
		/// <returns></returns>
		public int ColumnValueCount(
			string strColumnName, 
			string strValue
			)
		{
			int intCount = 0;
			ListViewItemSP objRow = null;
			bool blnColumnIsHidden = false;

			// returns 0 if there is no data in listview
			if (this.Items.Count == 0)
				return 0;
			
			// determines if column is hidden or not
			objRow = (ListViewItemSP)this.Items[0];

			for (int i = 0 ; i < objRow.HiddenColumCollection.Length ; i++)
			{
				if (objRow.HiddenColumCollection[i].Name == strColumnName)
				{	
					blnColumnIsHidden = true;
					break;
				}
			}


			for (int i = 0 ; i < this.Items.Count ; i ++)
			{
				if (blnColumnIsHidden)
				{
					
					// finds value in hidden columns
					objRow = (ListViewItemSP)this.Items[i];
					for (int j = 0 ; j < objRow.HiddenColumCollection.Length ; j++)
					{
						if (objRow.HiddenColumCollection[j].Name == strColumnName && objRow.HiddenColumCollection[j].Value == strValue)
						{	
							intCount++;
							break;
						}
					}
				
				}
				else
				{
					// finds value in visible column
					if (GetCellValue(i, strColumnName) == strValue)
					{
						intCount++;
					}
				}
			}

			return intCount;
		}



		/// <summary> Gets an xmlnode representing the 
		/// state of this object </summary>
		/// <returns></returns>
		public XmlNode GetState(
			)
		{
			XmlDocument dXmlState = new XmlDocument();
			XmlDocumentFragment fXmlColumnState = null;
			dXmlState.InnerXml = "<state Name='" + this.Name + "'/>";
			ColumnHeaderSP objColumn = null;
			string strColumnState = 
				"<Column InternalText=''>" +
				"<Width/>" +
				"<WidthBeforeCollapse/>" +
				"</Column>";
	
			
			for (int i = 0 ; i < this.Columns.Count ; i ++)
			{
				objColumn = (ColumnHeaderSP)this.Columns[i];

				fXmlColumnState = dXmlState.CreateDocumentFragment();
				fXmlColumnState.InnerXml = strColumnState;
				
				// assigns values from column properties to xml
				fXmlColumnState.SelectSingleNode("//Column").Attributes["InternalText"].Value = objColumn.InternalText;
				fXmlColumnState.SelectSingleNode("//Width").InnerText = objColumn.Width.ToString();
				fXmlColumnState.SelectSingleNode("//WidthBeforeCollapse").InnerText = objColumn.WidthBeforeCollapse.ToString();

				dXmlState.DocumentElement.AppendChild(fXmlColumnState);
			}

			return dXmlState.DocumentElement;
		}



		/// <summary> Sets the state of this object 
		/// using an xml node</summary>
		public void SetState(
			XmlNode nXmlState
			)
		{
			ColumnHeaderSP objColumn = null;

			for (int i = 0 ; i < nXmlState.ChildNodes.Count ; i ++)
			{
				for (int j = 0 ; j < this.Columns.Count ; j ++)
				{
					objColumn = (ColumnHeaderSP)this.Columns[j];

					if (objColumn.InternalText == nXmlState.ChildNodes[i].Attributes["InternalText"].Value)
					{
						// assigns values from xmldata to column properties
						objColumn.Width = Convert.ToInt32(nXmlState.ChildNodes[i].SelectSingleNode(".//Width").InnerText);
						objColumn.WidthBeforeCollapse = Convert.ToInt32(nXmlState.ChildNodes[i].SelectSingleNode(".//WidthBeforeCollapse").InnerText);

						break;

					} //if
				} //for
			} //for
		}
				

		
		#endregion


		#region EVENTS

		/// <summary> Invoked by column header click - invokes 
		/// sorting on columns, amongst other things </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void This_ColumnClick(
			object sender, 
			ColumnClickEventArgs e
			)
		{
			ColumnHeaderSP objTempColHeader			= null;
			

			m_objColumnSorter.currentColumn		= e.Column;
			objTempColHeader					= (ColumnHeaderSP)this.Columns[e.Column];
			m_objColumnSorter.sortOrder			= objTempColHeader.SortingOrder;
				
			//REVERSES THE SORT ORDER ON THE COLUMN THAT WAS JUST CLICKED
			if (objTempColHeader.SortingOrder == SortingOrder.Ascending)
				objTempColHeader.SortingOrder = SortingOrder.Descending;
			else if (objTempColHeader.SortingOrder == SortingOrder.Descending)
				objTempColHeader.SortingOrder = SortingOrder.Ascending;
				
			this.Sort();		


		}
		

		
		/// <summary>  </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void This_KeyDown(
			object sender, 
			KeyEventArgs e
			)
		{
		
			if (e.KeyCode == Keys.Delete && this.AllowKeyboardDeleteKeyDeletion)
				this.RemoveSelectedRows();

		}



		/// <summary> </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void This_Resize(
			object sender, 
			EventArgs e
			)
		{
			AutoFitColumns();
		}



		#endregion

	}
	
}
