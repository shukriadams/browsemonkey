//////////////////////////////////////////////////////////////////////////////////////
// Author				: Shukri Adams												//
// Contact				: shukri.adams@gmail.com									//
//																					//
// vcFramework : A reuseable library of utility classes                             //
// Copyright (C)																	//
//////////////////////////////////////////////////////////////////////////////////////

using System.Threading;
using System.Collections;

namespace vcFramework.Threads
{

	/// <summary> Stores and manages thread references </summary>
	public class ThreadCollection
	{

		#region MEMBERS
	
		/// <summary> All threads in object stored here </summary>
		private ArrayList m_arrLstThreads;
		
		#endregion
	

		#region PROPERTIES

		/// <summary> Gets the number of threads referenced in this object </summary>
		public int ThreadCount		
		{
			get{return m_arrLstThreads.Count;}
		}
		
		
		/// <summary> Gets the number of active threads references to this object </summary>
		public int ActiveThreadCount
		{
			get
			{
				int intActiveThreadCount = 0;
				Thread th = null;

				for (int i = 0 ; i < m_arrLstThreads.Count ; i ++)
				{
					th = (Thread)m_arrLstThreads[i];
					if (th.IsAlive)
						intActiveThreadCount ++;
				}

				return intActiveThreadCount;
			}
		}

		
		#endregion


		#region CONSTRUCTORS

		/// <summary> Default constructor </summary>
		public ThreadCollection(
			)
		{
			m_arrLstThreads = new ArrayList();
		}
		

		#endregion


		#region METHODS


		/// <summary> Adds a thread to this object </summary>
		/// <param name="th"></param>
		public void AddThread(
			Thread th
			)
		{
			m_arrLstThreads.Add(th);
		}



		/// <summary> Removes a specified thread object from this thread collection - the thread is NOT aborted</summary>
		/// <param name="th"></param>
		public void RemoveThread(
			Thread th
			)
		{
			m_arrLstThreads.Remove(th);
		}


		
		/// <summary> Aborts all threds referenced in objects - threads still remain referenced however </summary>
		public void AbortAllThreads(
			)
		{
			ThreadAbort(m_arrLstThreads, false);
		}
		

		
		/// <summary> Aborts and removes all threads referenced in object</summary>
		public void AbortAndRemoveAllThreads(
			)
		{
			ThreadAbort(m_arrLstThreads, true);
		}



		/// <summary> Aborts and removes a named thread </summary>
		/// <param name="strThreadName"></param>
		public void AbortAndRemoveThread(
			string strThreadName
			)
		{
			Thread th = null;

			for (int i = 0 ; i < m_arrLstThreads.Count ; i ++)	
			{
				th = (Thread)m_arrLstThreads[i];

				if (th.Name == strThreadName)
				{
					if (th.IsAlive)
					{
						//System.Windows.Forms.MessageBox.Show(strThreadName + " abortign");
						th.Abort();
						th.Join(100);		// this wait period is a total thumbsuck
					}

					break;
				}
			}
		}



		/// <summary> Aborts all threads references in an arraylist. Can delete thread references
		/// from arraylist if specified</summary>
		/// <param name="arrlstThreadCollection"></param>
		private void ThreadAbort(
			ArrayList arrlstThreadCollection, 
			bool blnDestroyReferences
			)
		{
			Thread th = null;

			
				if (blnDestroyReferences)
				{
					while(arrlstThreadCollection.Count > 0)
					{
						// always selects teh first thread in arraylst
						th = (Thread)arrlstThreadCollection[0];
						
						try
						{
							if (th.IsAlive)
							{
								th.Abort();
								th.Join(100);	// this wait period is a total thumbsuck
							}
						}
						catch(ThreadAbortException)
						{
							Thread.ResetAbort();
							//System.Windows.Forms.MessageBox.Show(e.Message);
						}

						arrlstThreadCollection.Remove(th);

					} // while
				}
				else
				{
					for (int i = 0 ; i < arrlstThreadCollection.Count ; i ++)
					{
						th = (Thread)arrlstThreadCollection[i];
					
						try
						{
							if (th.IsAlive)
							{	
								
								th.Abort();
								th.Join(100);	// this wait period is a total thumbsuck
							}
						}
						catch(ThreadAbortException)
						{
							Thread.ResetAbort();
							//System.Windows.Forms.MessageBox.Show(e.Message);
						}
				
					}// for
				} //if
			
		}



		#endregion


	}
}
