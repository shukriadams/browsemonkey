//////////////////////////////////////////////////////////////////////////////////////
// Author				: Shukri Adams												//
// Contact				: shukri.adams@gmail.com									//
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
using System.Windows.Forms;
using vcFramework.Delegates;

namespace vcFramework.Windows.Forms
{

	/// <summary>  
	/// Class for managing basic interaction between an application 
	/// and a  progress bar. Instead of manipulating a progress bar directly, 
	/// this class can be used to hide all calculations for progressing, and 
	/// exposes simplified methods for setting up a progress bar and stepping 
	/// through it. Note that this class can be used in two ways - it can be
	/// manipulated externally, in which case it must be instantiated with 
	/// the number of steps it will require, and all stepping events must be
	/// bound externally or ..... an IProgressable object can be passed in as
	/// a constructor argument, in which case all stepping and events are 
	/// handled by this class - this allows for very streamlined usage.
	/// 
	/// Note : the "accuracy" of this helper is at its worse when there are 199
	/// steps for the procedure being tracked. This is because of the way the 
	/// interval is calculated ... 
	///								interval = steps/progbar max 
	///	which would typically be 
	///								interval = 199/100
	///	here, interval would be 1, as the modulus of 199/100 is discarded. Thus,
	///	the progress bar behaves as if there are only 100 steps ... the additional
	///	99 steps dont have an effect. Obviously, if there are 299 steps, the 
	///	additional 99 there are also ignored, but the visual discrepancy decreases 
	///	every 100.
	/// </summary>
	public class ProgressBarHelper
	{

		#region MEMBERS
		
		/// <summary> 
		/// The progress bar this object is bound to
		/// </summary>
		private ProgressBar _progressBar;

		/// <summary> 
		/// The number of steps required to carry out full progress 
		/// </summary>
		private long _steps;

		/// <summary>
		/// The number of actual steps we ignore before moving the progress
		/// bar forward 1. This is done when there are more than 100 steps
		/// in total - the progress bar has a max of 100 steps. To handle
		/// over 100 actual, we need to group actual steps into intervals,
		/// and move the progress bar forward on intervals, not actual steps.
		/// </summary>
		private long _interval;

		/// <summary> 
		/// The curren
		/// </summary>
		private long _currentStepInInterval;

		/// <summary>
		/// Used to set the .Step property of the progress bar, if there
		/// are fewer than 100 steps in total. .Step should be set by a
		/// delegate to make it thread safe, so we do so by storing the
		/// value in a class member, and then invoke a method to fetch
		/// it again.
		/// </summary>
		private int _step;

		/// <summary> 
		/// Object, the progress of which will be tracked with progress bar 
		/// </summary>
		private	IProgress _progressable;
	
		#endregion


		#region CONSTRUCTORS

		/// <summary> 
		/// 
		/// </summary>
        /// <param name="progressBar"></param>
		/// <param name="intSteps"></param>
		public ProgressBarHelper(
			ProgressBar progressBar, 
			IProgress iProgressable
			)
		{
            _progressBar = progressBar;	        // sets member
			_progressBar.Maximum = 100;			// maxumum value of progress bar
			_progressBar.Value = 0;				// forces progress bar back to zero
			_currentStepInInterval = 0;			// must be set to 0, but this is probably unnecessary
            _progressable = iProgressable;	


			// Sets Steps
			// 1 is the default step value.
			// if intSteps >= 100 then step will always be 1
			_step = 1;
			_steps = _progressable.Steps;
			

			// set progress bar via delegate to make threadsafe
			if (_steps <= _progressBar.Maximum)
				// Note: safe to convert to in32 because it's less than m_objProgressBar.Maximumm which
				// is an int
				_step = _progressBar.Maximum / Convert.ToInt32(_steps);	
			else
				// if lngSteps is more than the Maximum, it could be MUCH 
				// more than .Maximum (max is int, and lngSteps is long).
				// This progressbarhelper will be called for EACH advance 
				// of lngStep, and we want to ignore most of those calls
				// - must only respond to every X call, to update progress
				// bar. This calculate that X (m_lngStepInterval)
				_interval = _steps / _progressBar.Maximum;
			
			

			// adds events
			_progressable.OnNext += this.PerformStep;
			_progressable.OnEnd += this.EndProcess;
			

			// to make thread safe, always invoke via delegate
			WinFormActionDelegate dlgt = SetStep_DelegateInvoked;

			_progressBar.Invoke(dlgt);


		}



		/// <summary> 
		/// 
		/// </summary>
        /// <param name="progressBar"></param>
        /// <param name="steps"></param>
		public ProgressBarHelper(
			ProgressBar progressBar, 
			long steps
			)
		{
            _progressBar = progressBar;	// sets member
			_progressBar.Maximum = 100;				// maxumum value of progress bar
			_progressBar.Value = 0;				// forces progress bar back to zero
			_currentStepInInterval = 0;				// must be set to 0, but this is probably unnecessary
			_progressable = null;
            _steps = steps;


			// Sets Steps
			// 1 is the default step value.
			// if intSteps = 100 or > 100 then step will always be 1
			_step = 1;
			

			// REDFLAG decision : m_lngSteps cannot be zero - it is used to divide
			// other numbers, and will therefore cause a dividebyzero error. I am
			// using a hack here and setting m_lngSteps  to 1 if it is zero. This
			// is a stopgap solution and needs to be looked at in greater detail
			if (_steps == 0)
				_steps = 1;
			

			// set progress bar via delegate to make
			// threadsafe
			if (_steps <= _progressBar.Maximum)
				// Note: safe to convert to in32 because it's less than m_objProgressBar.Maximumm which
				// is an int
				_step =  _progressBar.Maximum / Convert.ToInt32(_steps);	
			else
				// if lngSteps is more than the Maximum, it could be MUCH 
				// more than .Maximum (max is int, and lngSteps is long).
				// This progressbarhelper will be called for EACH advance 
				// of lngStep, and we want to ignore most of those calls
				// - must only respond to every X call, to update progress
				// bar. This calculate that X (m_lngStepInterval)
				_interval = _steps / _progressBar.Maximum;


			// to make thread safe, always invoke via delegate
			WinFormActionDelegate dlgt = SetStep_DelegateInvoked;
			_progressBar.Invoke(dlgt);
		}
		


		#endregion


		#region METHODS


		
		/// <summary> 
		/// Invoked by delagete for thread safety  (see constructor method)
		/// </summary>
		private void SetStep_DelegateInvoked(
			)
		{
			_progressBar.Step = _step;
		}
		

		
		/// <summary> 
		/// Event-friendly PerformStep() so PerformStep() can be invoked 
		/// directly by eventhanlder 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void PerformStep(
			object sender,
			EventArgs e
			)
		{
			PerformStep();
		}


		
		/// <summary> 
		/// Advances the progress bar 
		/// </summary>
		public void PerformStep(
			)
		{	

			// exit if progress bar is already at maximum
			if (_progressBar.Value == _progressBar.Maximum)
				return;

            if (_steps > _progressBar.Maximum)
            {
                if (_currentStepInInterval >= _interval)
                {

                    // invokes PerformStep() on progress bar via a delegate
                    // usin the PerformStep_DelegateInvoked() method
                    WinFormActionDelegate dlgt = PerformStep_DelegateInvoked;
                    _progressBar.Invoke(dlgt);

                    // resets
                    _currentStepInInterval = -1;
                }

                _currentStepInInterval++;
            }
            else
            {
                _progressBar.BeginInvoke((Action)(() => _progressBar.PerformStep()));
            }
			


		}

		

		/// <summary> 
		/// USed for thread-safe step advancing on  progress bar - see 
		/// PerformStep() method. This method can be called ONLY from 
		/// PerformStep()! 
		/// </summary>
		private void PerformStep_DelegateInvoked(
			)
		{
			_progressBar.PerformStep();
		}


		
		/// <summary> 
		/// Event-friednly EndProcess() implementation </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void EndProcess(
			object sender,
			EventArgs e			
			)
		{
			EndProcess();
		}



		/// <summary> 
		/// Thread-friendly end process - meant to be called when whatever 
		/// process is finished
		/// </summary>
		public void EndProcess(
			)
		{

			WinFormActionDelegate dlgt = EndProcess_DelegateInvoked;
			_progressBar.Invoke(dlgt);
		}



		/// <summary> 
		/// 
		/// </summary>
		private void EndProcess_DelegateInvoked(
			)
		{
			_progressBar.Value = _progressBar.Maximum;
		}


		#endregion

	}
}
