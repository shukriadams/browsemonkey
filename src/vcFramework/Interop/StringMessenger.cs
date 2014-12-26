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
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using vcFramework.Delegates;

namespace vcFramework.Interop
{

	/// <summary> 
	/// Used for sending and receiving strings between Windows applicaton processes 
	/// </summary>
	public class StringMessenger
	{

		#region FIELDS

		/// <summary> Used to communicate between instances of
		/// BrowseMonkey </summary>
		/// <param name="hwnd"></param>
		/// <param name="Msg"></param>
		/// <param name="wParam"></param>
		/// <param name="lParam"></param>
		/// <returns></returns>
		[DllImport("user32.dll", CharSet=CharSet.Auto, SetLastError=true)]
		private static extern IntPtr SendMessage(IntPtr hwnd, uint Msg, IntPtr wParam, IntPtr lParam);

		/// <summary> Signifies start of transmission </summary>
		private const int M_INT_TRANSMISSION_START = 9999;
		
		/// <summary>  </summary>
		private const int M_INT_TRANSMISSION_END = 8888;
		
		/// <summary> The beginning sequence of digits for
		/// a char </summary>
		private const int M_INT_HOLDER = 111;

		public event EventHandler OnStringReceived;

		/// <summary> Arraylist holding received messages </summary>
		private ArrayList m_arrLstReceivedStrings;
		
		/// <summary> Used to build up the currently receiveing string/ </summary>
		private string m_strCurrentReceivingString;

		/// <summary> Set to true when begin to receive 
		/// a string, set to false when string finished
		/// receiving</summary>
		private bool m_blnReceivingString;

		#endregion


		#region PROPERTIES

		/// <summary> Gets if string have been received 
		/// and await for extraction </summary>
		public bool StringsPending
		{
			get
			{
				if (m_arrLstReceivedStrings != null && m_arrLstReceivedStrings.Count > 0)
					return true;
				return false;
			}
		}


		#endregion


		#region CONSTRUCTORS

		/// <summary>  </summary>
		public StringMessenger(
			)
		{
			m_arrLstReceivedStrings= new ArrayList();

			m_blnReceivingString = false;
		}


		#endregion


		#region METHODS

		/// <summary> Sends the given string message to
		/// the given process </summary>
		/// <param name="objTargetApplicationProcess"></param>
		/// <param name="strMessage"></param>
		public void SendMessage(
			Process objTargetApplicationProcess,
			string strMessage
			)
		{

			uint intCharCode = 0;
			string strFinalMessage = "";
			uint intMessage = 0;
			char chrCurrentItem = 'a';


			// sends start signal
			SendMessage(
				objTargetApplicationProcess.MainWindowHandle, 
				M_INT_TRANSMISSION_START, 
				IntPtr.Zero, 
				IntPtr.Zero);


		
			// send each item in string
			for (int i = 0 ; i < strMessage.Length ; i ++)
			{
				chrCurrentItem  = Convert.ToChar(strMessage.Substring(i,1));
				intCharCode		= Convert.ToUInt32(chrCurrentItem) ;
				
				strFinalMessage = M_INT_HOLDER.ToString() + intCharCode.ToString();
				intMessage		= Convert.ToUInt32(strFinalMessage);
				
				SendMessage(
					objTargetApplicationProcess.MainWindowHandle, 
					intMessage, 
					IntPtr.Zero, 
					IntPtr.Zero);			
			}




			// sends end signal
			SendMessage(
				objTargetApplicationProcess.MainWindowHandle, 
				M_INT_TRANSMISSION_END, 
				IntPtr.Zero, 
				IntPtr.Zero);
		


		}



		/// <summary> 
		/// All incoming windows messages must be
		/// passed through this method if message receiving
		/// is to be done 
		/// </summary>
		/// <param name="message"></param>
		public void ProcessMessage(
			object sender,
			Message message
			)
		{
			

			//filter the RF_TESTMESSAGE
			if (message.Msg == M_INT_TRANSMISSION_START)
			{
				m_blnReceivingString = true;

				// must explicitly blank the
				// string field incase it was
				// written to before
				m_strCurrentReceivingString = "";

			}
			else if (m_blnReceivingString && message.Msg.ToString().StartsWith( M_INT_HOLDER.ToString()))
			{
				
				string strReceivedChar = "";
				string strReceivedMessage = message.Msg.ToString();

				// gets digits after holder digits 
				strReceivedChar = strReceivedMessage.Substring(
					M_INT_HOLDER.ToString().Length,
					strReceivedMessage.Length - M_INT_HOLDER.ToString().Length);
				
				// converts digits to int and convert int to char and convert char to string :-)
				strReceivedChar = Convert.ToChar(Convert.ToInt32(strReceivedChar)).ToString();

				m_strCurrentReceivingString += strReceivedChar;

				
			}
			else if (message.Msg == M_INT_TRANSMISSION_END)
			{
				m_blnReceivingString = false;

				// move finished string to arraylist
				m_arrLstReceivedStrings.Add(
					m_strCurrentReceivingString);

				StringMessageEventArgs args = new StringMessageEventArgs(
					m_strCurrentReceivingString
					);

				// fire event
				DelegateLib.InvokeSubscribers(
					OnStringReceived,
					sender,
					args);


			}
		
		
		}



		#endregion

	}
}
