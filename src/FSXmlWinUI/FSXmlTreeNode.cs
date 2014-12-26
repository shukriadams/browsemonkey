///////////////////////////////////////////////////////////////
// FSXmlWinUI - A library of Windows controls specifically   //  
// made for working with FSXml data.                         // 
// Shukri Adams (shukri.adams@gmail.com)                     //
// https://github.com/shukriadams/browsemonkey               //
// MIT License (MIT) Copyright (c) 2014 Shukri Adams         //
///////////////////////////////////////////////////////////////

using System.Windows.Forms;

namespace FSXmlWinUI
{
	/// <summary> 
	/// </summary>
	public class FSXmlTreeNode : TreeNode
	{

		#region FIELDS

		/// <summary> 
		/// The nodeType of this object 
		/// </summary>
		private FSXmlTreeNodeTypes _nodeType;
		
		#endregion


		#region PROPERTIES

		/// <summary> 
		/// Gets or sets the NodeType of this node object 
		/// </summary>
		public FSXmlTreeNodeTypes NodeType
		{
			set
			{
				_nodeType = value;
			}
			get
			{
				return _nodeType;
			}
		}

		#endregion


		#region CONSTRUCTORS

		/// <summary>
		/// 
		/// </summary>
		/// <param name="strNodeName"></param>
		public FSXmlTreeNode(
			string nodeName
			)
		{
			base.Text = nodeName;
		}

		
		#endregion

	}
}
