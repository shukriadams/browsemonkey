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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using vcFramework;
using vcFramework.IO;
using vcFramework.Windows.Forms;
using vcFramework.Assemblies;
using vcFramework.DataItems;
using vcFramework.Delegates;


namespace vcFramework.UserControls
{
	/// <summary> 
	/// Visual-studion style console
	/// </summary>
	public class MessageConsole :UserControl, IStateManageable
	{

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent(
			)
		{
			this.lvMessages = new vcFramework.Windows.Forms.ListViewSP();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnCopy = new System.Windows.Forms.Button();
			this.btnLog = new System.Windows.Forms.Button();
			this.btnClearConsole = new System.Windows.Forms.Button();
			this.cbLogging = new System.Windows.Forms.CheckBox();
			this.pnlFunctionButtonHolder = new System.Windows.Forms.Panel();
			this.pnlFunctionButtonHolder.SuspendLayout();
			this.SuspendLayout();
			// 
			// lvMessages
			// 
			this.lvMessages.AllowKeyboardDeleteKeyDeletion = false;
			this.lvMessages.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvMessages.FocusNewItemOnInsert = true;
			this.lvMessages.FullRowSelect = true;
			this.lvMessages.HideSelection = false;
			this.lvMessages.InsertPosition = vcFramework.Windows.Forms.ListViewSP.InsertPositions.Top;
			this.lvMessages.Location = new System.Drawing.Point(0, 0);
			this.lvMessages.LockWidthOnZeroWidthColumns = false;
			this.lvMessages.Name = "lvMessages";
			this.lvMessages.Size = new System.Drawing.Size(432, 368);
			this.lvMessages.TabIndex = 0;
			this.lvMessages.View = System.Windows.Forms.View.Details;
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSave.Location = new System.Drawing.Point(2, 2);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(20, 20);
			this.btnSave.TabIndex = 1;
			this.btnSave.Text = "s";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnCopy
			// 
			this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnCopy.Location = new System.Drawing.Point(32, 2);
			this.btnCopy.Name = "btnCopy";
			this.btnCopy.Size = new System.Drawing.Size(20, 20);
			this.btnCopy.TabIndex = 2;
			this.btnCopy.Text = "c";
			this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
			// 
			// btnLog
			// 
			this.btnLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnLog.Location = new System.Drawing.Point(64, 2);
			this.btnLog.Name = "btnLog";
			this.btnLog.Size = new System.Drawing.Size(20, 20);
			this.btnLog.TabIndex = 3;
			this.btnLog.Text = "l";
			this.btnLog.Click += new System.EventHandler(this.btnLog_Click);
			// 
			// btnClearConsole
			// 
			this.btnClearConsole.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClearConsole.Location = new System.Drawing.Point(408, 2);
			this.btnClearConsole.Name = "btnClearConsole";
			this.btnClearConsole.Size = new System.Drawing.Size(20, 20);
			this.btnClearConsole.TabIndex = 4;
			this.btnClearConsole.Text = "e";
			this.btnClearConsole.Click += new System.EventHandler(this.ClearConsole);
			// 
			// cbLogging
			// 
			this.cbLogging.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.cbLogging.Location = new System.Drawing.Point(96, 6);
			this.cbLogging.Name = "cbLogging";
			this.cbLogging.Size = new System.Drawing.Size(304, 16);
			this.cbLogging.TabIndex = 5;
			this.cbLogging.Text = "Not logging";
			this.cbLogging.CheckedChanged += new System.EventHandler(this.cbLogging_CheckedChanged);
			// 
			// pnlFunctionButtonHolder
			// 
			this.pnlFunctionButtonHolder.Controls.Add(this.btnCopy);
			this.pnlFunctionButtonHolder.Controls.Add(this.btnLog);
			this.pnlFunctionButtonHolder.Controls.Add(this.btnClearConsole);
			this.pnlFunctionButtonHolder.Controls.Add(this.cbLogging);
			this.pnlFunctionButtonHolder.Controls.Add(this.btnSave);
			this.pnlFunctionButtonHolder.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlFunctionButtonHolder.Location = new System.Drawing.Point(0, 344);
			this.pnlFunctionButtonHolder.Name = "pnlFunctionButtonHolder";
			this.pnlFunctionButtonHolder.Size = new System.Drawing.Size(432, 24);
			this.pnlFunctionButtonHolder.TabIndex = 6;
			// 
			// MessageConsole
			// 
			this.Controls.Add(this.pnlFunctionButtonHolder);
			this.Controls.Add(this.lvMessages);
			this.Name = "MessageConsole";
			this.Size = new System.Drawing.Size(432, 368);
			this.Load += new System.EventHandler(this.Console_Load);
			this.pnlFunctionButtonHolder.ResumeLayout(false);
			this.ResumeLayout(false);

		}


		#endregion

		
		#region FIELDS

		private System.ComponentModel.Container components = null;
		private vcFramework.Windows.Forms.ListViewSP lvMessages;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnCopy;
		private System.Windows.Forms.Button btnLog;
		private System.Windows.Forms.Button btnClearConsole;
		private System.Windows.Forms.CheckBox cbLogging;
		private System.Windows.Forms.Panel pnlFunctionButtonHolder;
		private System.Windows.Forms.ContextMenu mnuConsoleMenu;
		private System.Windows.Forms.MenuItem mnuClearConsole;
		private System.Windows.Forms.MenuItem mnuCopyConsole;

		/// <summary> 
		/// Holds icons for listview 
		/// </summary>
		private ImageList m_objImageList;
		
		/// <summary> 
		/// Path of file to log to 
		/// </summary>
		private string m_strLoggingFilePath;

		/// <summary> 
		/// Logger 
		/// </summary>
		private Logger m_objLogger;
		
		/// <summary> 
		/// contains console state in xml form 
		/// </summary>
		private XmlDocument m_dXmlConsoleState;

		/// <summary>
		/// </summary>
		public enum MessageTypes : int		
		{
			Information,
			Exception,
			Warning
		}
		

		/// <summary> </summary>
		public enum MessagePriorities : int	
		{
			Urgent,
			Important,
			Verbose
		}

		
		/// <summary> </summary>
		public bool m_blnShowConsoleFunctionButtons;
		
		#endregion
		

		#region PROPERTIES

		/// <summary> Gets or sets if buttons at bottom of 
		/// console will be displayed. These buttons are
		/// intended for advanced users and debugging,
		/// and by default are disabled.</summary>
		public bool ShowButtons
		{
			set
			{
				//m_blnShowConsoleFunctionButtons = value;
				
				//for (int i = 0 ; i < pnlFunctionButtonHolder.Controls.Count ; i ++)
				//	pnlFunctionButtonHolder.Controls[i].Visible = m_blnShowConsoleFunctionButtons;
				pnlFunctionButtonHolder.Visible = value;
			}
			get
			{
				//return m_blnShowConsoleFunctionButtons;
				return pnlFunctionButtonHolder.Visible;
			}
		}


		#endregion


		#region CONSTRUCTORS

		/// <summary> </summary>
		public MessageConsole(
			)
		{
			
			InitializeComponent();

			this.ShowButtons = false;

			// sets up empty Xml state document - actual
			// state data may be passed in sometime during
			// the lifetime of this instance of the console,
			// but if not, at least an empty state doc is 
			// needed for the console to function normally.
			m_dXmlConsoleState = new XmlDocument();
			m_dXmlConsoleState.InnerXml = 
				"<state>" +
				"<logging/>" +
				"<loggingPath/>" +
				"</state>";

			// set log path to blank. Log path can be set
			// in 1 of two ways - from state data passed in
			// as xml, or by a human setting the save path
			// from the log file path  dialogue on the console.
			m_strLoggingFilePath = "";

            
		}
	

		#endregion


		#region DESTRUCTORS

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion

		
		#region METHODS

		/// <summary> 
		/// Adds a message to console 
		/// </summary>
		/// <param name="strMessage"></param>
		public void Add(
			string strMessage
			)
		{
			StringItem[] arrNewItem = new StringItem[3];
            arrNewItem[0] = new StringItem();
            arrNewItem[1] = new StringItem();
            arrNewItem[2] = new StringItem();

			arrNewItem[0].Name = "icon";
			arrNewItem[0].Value = "";
			arrNewItem[1].Name = "time";
			arrNewItem[1].Value = System.DateTime.Now.ToShortTimeString();
			arrNewItem[2].Name = "message";
			arrNewItem[2].Value = strMessage;
			
			lvMessages.InsertRow(arrNewItem);
			lvMessages.SetRowIcon(0, 0);

			// use checkbox as indicator of if should log
			if (cbLogging.Checked)
				m_objLogger.WriteToLog(strMessage);

		}

        string _threadsafemessageholder;

        private void AddThreadSafeInternal()
        {
            Add(_threadsafemessageholder);
        }

        public void AddThreadSafe(
            string strMessage)
        {
            _threadsafemessageholder = strMessage;
            lvMessages.Invoke(new WinFormActionDelegate(AddThreadSafeInternal));
        }
		
		/// <summary> 
		/// Works as logging switch 
		/// </summary>
		private void SetLoggingIndicator(
			)
		{

			if (cbLogging.Checked)
			{
				// checks if logging is possible
				if (m_strLoggingFilePath.Length > 0)
				{
					try
					{
						// create a new instance of logger if one has not already been created
						if (m_objLogger == null)
						{
							m_objLogger = new Logger(
								m_strLoggingFilePath,
								10000000,				// approx 10 meg log file
								false,					// do not create path if path doesn't exist
								true					// resume logging on existing file if it exists
								);					
						}

						cbLogging.Text = "Logging to file";
					}
					catch
					{
						// if reach here, logging not possible
						cbLogging.Text = "Logging failed";
						cbLogging.Checked = false;
					}
				}
				else
				{
					// if reach here, no log file path has been specified
					cbLogging.Text = "No log path ...";
					cbLogging.Checked = false;
				}
				
			}
			else
			{
				if (m_objLogger != null)
				{
					cbLogging.Text = "Logging stopped";
					m_objLogger.Dispose();
					m_objLogger = null;
				}

			}
		}

		
		
		/// <summary> </summary>
		private void BuildMenu(
			)
		{
			mnuConsoleMenu = new ContextMenu();
			mnuClearConsole = new MenuItem();
			mnuCopyConsole = new MenuItem();

			mnuConsoleMenu.Popup += new EventHandler(ConsoleMenu_Popup);

			mnuCopyConsole.Text = "Copy console contents";
			mnuCopyConsole.Index = 0;
			mnuCopyConsole.Click += new EventHandler(this.CopyConsole);

			mnuClearConsole.Text = "Clear console";
			mnuClearConsole.Index = 1;
			mnuClearConsole.Click += new EventHandler(this.ClearConsole);

			
			mnuConsoleMenu.MenuItems.AddRange(
				new MenuItem[]{
								  mnuCopyConsole,
								  mnuClearConsole
							  });

			lvMessages.ContextMenu = mnuConsoleMenu;

		}


		/// <summary> 
		/// Gets state of console 
		/// in Xlm node. 
		/// </summary>
		/// <returns></returns>
		public XmlNode GetState(
			)
		{

			m_dXmlConsoleState.SelectSingleNode("//logging").InnerText = cbLogging.Checked.ToString();
				
			if (m_strLoggingFilePath != null)
				m_dXmlConsoleState.SelectSingleNode("//loggingPath").InnerText = m_strLoggingFilePath;
	
			return m_dXmlConsoleState.DocumentElement;		
		}



		/// <summary> 
		/// Sets state of console from
		/// Xml node. 
		/// </summary>
		/// <param name="nXmlState"></param>
		public void SetState(
			XmlNode nXmlState
			)
		{
			m_dXmlConsoleState.InnerXml = nXmlState.OuterXml;

			// always set path _before_ changing the checkbox checked status
			if (m_dXmlConsoleState.SelectSingleNode("//loggingPath").InnerText.Length > 0)
				m_strLoggingFilePath = m_dXmlConsoleState.SelectSingleNode("//loggingPath").InnerText;

			// must be done LAST - chanibg check state triggers event handler 
			// which in turn uses the path above to start logging
			if (m_dXmlConsoleState.SelectSingleNode("//logging").InnerText == "True"){cbLogging.Checked = true;}

		
		}



		#endregion


		#region EVENTS

		/// <summary> 
		/// Most start logic for this control
		/// goes here. 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Console_Load(
			object sender, 
			System.EventArgs e
			)
		{

			ToolTip objToolTip						= null;
			AssemblyAccessor objAssemblyAccessor	= null;

			 // sets up column structure
			XmlDocument dXmlColumnStructure = new XmlDocument();
			dXmlColumnStructure.InnerXml =	"<listview>" +
												"<column internalName='icon' width='20'/>" +
												"<column internalName='time' width='50'>Time</column>" +
												"<column internalName='message' width='100' widthBehaviour='Autofit'>Message</column>" +
											"</listview>";
			lvMessages.ColumnsSet(
				dXmlColumnStructure.DocumentElement.ChildNodes
				);


			// ########################################			
			// populates imagelist
			// ----------------------------------------
			objAssemblyAccessor = new AssemblyAccessor(
				Assembly.GetAssembly(typeof(MessageConsole)));

			m_objImageList = new ImageList();
			m_objImageList.Images.Add(objAssemblyAccessor.GetBitmap(objAssemblyAccessor.RootName + ".UserControls.info.png"));
			m_objImageList.Images.Add(objAssemblyAccessor.GetBitmap(objAssemblyAccessor.RootName + ".UserControls.close.png"));

			lvMessages.SmallImageList = m_objImageList;



			// ########################################			
			// sets listview properties
			// ----------------------------------------
			lvMessages.MultiSelect = false;
			lvMessages.FocusNewItemOnInsert = true;
			lvMessages.InsertPosition = ListViewSP.InsertPositions.Top;


			// ########################################			
			// set button images
			// ----------------------------------------
			btnSave.Image = new Bitmap(objAssemblyAccessor.GetBitmap(objAssemblyAccessor.RootName + ".UserControls.filesave.png"), 12,12);
			btnCopy.Image = new Bitmap(objAssemblyAccessor.GetBitmap(objAssemblyAccessor.RootName + ".UserControls.editcopy.png"), 12,12);
			btnLog.Image = new Bitmap(objAssemblyAccessor.GetBitmap(objAssemblyAccessor.RootName + ".UserControls.filesaveas.png"), 12,12);
			btnClearConsole.Image = new Bitmap(objAssemblyAccessor.GetBitmap(objAssemblyAccessor.RootName + ".UserControls.trash.png"), 12,12);

			// creates tooltips for buttons
			objToolTip = new ToolTip();
			objToolTip.SetToolTip(btnSave, "Save current console contents to text file");
			objToolTip = new ToolTip();
			objToolTip.SetToolTip(btnCopy, "Copy current console contents to memory clipboard");
			objToolTip = new ToolTip();
			objToolTip.SetToolTip(btnLog, "Select a log file to write console message to");
			objToolTip = new ToolTip();
			objToolTip.SetToolTip(btnClearConsole, "Clear current console contents");

			// 
			SetLoggingIndicator();

			// builds context menu
			BuildMenu();

		}
		

		
		/// <summary> Invoked when "save console
		/// contents to file" button is clicked
		///  </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSave_Click(
			object sender, 
			System.EventArgs e
			)
		{
			/*
			string strSavePath = "";
			string strListViewContents = "";

			strSavePath = FileSystemLib.GetFilePathFromSaveDialogue();
			strListViewContents = lvMessages.GetAllRowsAsText();
			
			if (Directory.Exists(FileSystemLib.GetFolderPath(strSavePath)))
				FileSystemLib.WriteToFile(strSavePath, strListViewContents, true);
			*/

		}



		/// <summary> Copies all console contents 
		/// to  memory clipboard </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCopy_Click(
			object sender, 
			System.EventArgs e
			)
		{
			string strListViewContents = "";
			strListViewContents = lvMessages.GetAllRowsAsText();

			Clipboard.SetDataObject(strListViewContents, true);
		}



		/// <summary> 
		/// sets log path after button press. note that log path can also be set 
		/// "behind the scenes" via console state resetting (done through a 
		/// property) 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnLog_Click(
			object sender, 
			System.EventArgs e
			)
		{
			/*
			string strLogPath = "";
			strLogPath = FileSystemLib.GetFilePathFromSaveDialogue();
			if (Directory.Exists(FileSystemLib.GetFolderPath(strLogPath)))
			{
				m_strLoggingFilePath = strLogPath;
			}
			*/
		}



		/// <summary> 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ConsoleMenu_Popup(
			object sender, 
			EventArgs e			
			)
		{
			mnuCopyConsole.Enabled = true;
			mnuClearConsole.Enabled = true;
			

			if (lvMessages.Items.Count == 0)
			{
				mnuCopyConsole.Enabled = false;
				mnuClearConsole.Enabled = false;
			}
		}



		/// <summary> 
		/// Clears console contents 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ClearConsole(
			object sender, 
			System.EventArgs e
			)
		{

			if (lvMessages.Items.Count > 0 && PromptLib.DialoguePrompt("", "Remove all console messages?"))
                lvMessages.Items.Clear();

		}
		


		/// <summary>
		/// Copies console contents to memory
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CopyConsole(
			object sender, 
			System.EventArgs e
			)
		{

			string strListViewContents = "";
			strListViewContents = lvMessages.GetAllRowsAsText();

			Clipboard.SetDataObject(
				strListViewContents, 
				true);
			
		}


	
		/// <summary> 
		/// Invoked by checkbox change - note, can be triggered by someone clicking the 
		/// checkbox, but also from "behind the scenes" by console state resetting (Done 
		/// via a property)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cbLogging_CheckedChanged(
			object sender, 
			System.EventArgs e
			)
		{
			SetLoggingIndicator();
		}
		


		#endregion


	}
}
