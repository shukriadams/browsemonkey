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
using vcFramework.DataItems;

namespace vcFramework.Windows.Forms
{
	/// <summary> Extends ColumnHeader</summary>
	public class ColumnHeaderSP : System.Windows.Forms.ColumnHeader
	{
		
		#region MEMBERS

		/// <summary> </summary>
		private string m_strColumnInternalText;
		
		/// <summary> </summary>
		private vcFramework.Collections.SortingOrder m_SortingOrder = vcFramework.Collections.SortingOrder.Ascending;

		/// <summary> The content type available in this column</summary>
		private ContentTypes m_contentType;

		/// <summary> The width behaviour for this columns. </summary>
		private WidthBehaviours m_widthBehaviour;

		/// <summary> Width before collapse is the width the column has before it's width is set to zero by the collapse
		/// method. if a column is uncollapsed, its width is set back to this width. </summary>
		private int m_intWidthBeforeCollapse;

		/// <summary> List the types of contents available in a column </summary>
		public enum ContentTypes : int		
		{
			Image,
			Hidden,
			Default
		}
		
		
		/// <summary> Lists width adjustment behaviour available for columns</summary>
		public enum WidthBehaviours : int	
		{
			Autofit,
			LockedWidth,
			Default
		}

		
		/// <summary> Holds collection of hiddenColumns </summary>
		private StringItem[] m_arrHiddenColumns = null;
		
		private int m_intOriginalWidth;

		#endregion

		
		#region PROPERTIES
		

		public int OriginalWidth
		{
			get{return m_intOriginalWidth;}
		}


		public StringItem[] HiddenColumns
		{
			set{m_arrHiddenColumns = value;}
		}

		/// <summary> Gets or set internal label of this column - used as a data identification string, typically
		/// bound to a database column name </summary>
		public string InternalText											
		{
			set{m_strColumnInternalText = value;}
			get{return m_strColumnInternalText;}
		}

		
		/// <summary> Gets or sets the order (ascending or descending) this column will be sorted by</summary>
		public vcFramework.Collections.SortingOrder SortingOrder	
		{
			set{m_SortingOrder = value;}
			get{return m_SortingOrder;}
		}
		
		/// <summary> Gets content type for this column</summary>
		public ContentTypes ContentType
		{
			get{return m_contentType;}
		}

		/// <summary> Gets or sets widht behaviour for this column </summary>
		public WidthBehaviours WidthBehaviour
		{
			set{m_widthBehaviour = value;}
			get{return m_widthBehaviour;}
		}
		
		
		/// <summary> </summary>
		public int WidthBeforeCollapse
		{
			set{m_intWidthBeforeCollapse = value;}
			get{return m_intWidthBeforeCollapse;}
		}


		#endregion


		#region CONSTRUCTORS

		/// <summary> Default </summary>
		public ColumnHeaderSP(int Width)	
		{
			m_contentType = ContentTypes.Default;
			m_widthBehaviour = WidthBehaviours.Default;
			m_intOriginalWidth = Width;
			this.Width = m_intOriginalWidth;
		}
		
		/// <summary>  </summary>
		public ColumnHeaderSP(int Width, ContentTypes ContentType)	
		{
			m_contentType = ContentType;
			m_widthBehaviour = WidthBehaviours.Default;
			m_intOriginalWidth = Width;
			this.Width = m_intOriginalWidth;
		}		
		
		#endregion
		
	}
}
