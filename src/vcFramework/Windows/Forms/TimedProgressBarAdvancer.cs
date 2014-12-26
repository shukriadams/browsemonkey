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
using System.Timers;
using System.Windows.Forms;
using vcFramework.Windows.Forms;

namespace vcFramework.Windows.Forms
{
	/// <summary> 
	///  Implements timed advancing of a progress bar.
	/// </summary>
	public class TimedProgressBarAdvancer
	{
		
		#region FIELDS
		
		/// <summary>
		/// 
		/// </summary>
		private System.Timers.Timer _timer;
		
		/// <summary>
		/// 
		/// </summary>
		private ProgressBar _progressBar;

		#endregion

	
		#region CONSTRUCTORS

		/// <summary>
		/// 
		/// </summary>
		/// <param name="duration">Time progress bar will take from zero to final position (in MILLISECONDS)</param>
		/// <param name="progressBar"></param>
		public TimedProgressBarAdvancer(
			int duration,
			ProgressBar progressBar
			)
		{

			_progressBar = progressBar;

			_progressBar.Maximum = duration;
			_progressBar.Step = 1;
			_progressBar.Value = 0;

		}

		
		#endregion


		#region METHODS
		
		/// <summary>
		/// 
		/// </summary>
		public void Start(
			)
		{
			_timer = new System.Timers.Timer(1000);
			_timer.Elapsed += new ElapsedEventHandler(Click_Elapsed);
			_timer.Start();
		}
		

		/// <summary>
		/// 
		/// </summary>
		public void Stop(
			)
		{
			if (_timer != null && _timer.Enabled)
			{
				_timer.Stop();
				_timer.Dispose();
				_timer	= null;
			}
		}
		

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Click_Elapsed(
			object sender,
			ElapsedEventArgs e
			)
		{
			_progressBar.PerformStep();
		}


		#endregion

	}
}
