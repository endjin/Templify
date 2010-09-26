namespace Endjin.Templify.WizardFramework
{
    using System;

	/// <summary>
	/// Base class that defines a WizardCommandList
	/// </summary>
	/// <history date="05-Aug-04" user="Matt Hall" action="Created"></history>
	public abstract class WizardCommandList {

		#region Private Variables
		
		/// <summary>
		/// Internal array list that stores the WizardCommand objects
		/// </summary>
		private System.Collections.ArrayList _cmdList = null;
		
		/// <summary>
		/// Integer that returns the pointer to the current WizardCommand
		/// </summary>
		private int _cmdPointer = 0;
		private int _lastCmdPointer = 0;

		#endregion

		#region Properties

		public int LastCmdPointer {
			get { return _lastCmdPointer; }
		}

		/// <summary>
		/// Integer that returns the pointer to the current WizardCommand
		/// </summary>
		public int CmdPointer {
			get { return _cmdPointer; }
			set { 
				_lastCmdPointer = _cmdPointer;
				_cmdPointer = value; 
			}
		}

		/// <summary>
		/// Integer that returns the number of WizardCommand objects in the
		/// list
		/// </summary>
		public int CmdLength {
			get { return _cmdList.Count; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Default constructor
		/// </summary>
		public WizardCommandList() {
			_cmdList = new System.Collections.ArrayList();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Adds the WizardCommand object to the internal array list
		/// </summary>
		/// <param name="cmd"></param>
		public void Add(WizardCommand cmd) {
			_cmdList.Add(cmd);
		}

		/// <summary>
		/// Determines whether this is the last command in the list
		/// </summary>
		/// <returns>Boolean of true if it is the last command in the list, otherwise false</returns>
		public bool IsLastCommand() {
			if (this.CmdPointer == this.CmdLength) {
				return true;
			} else {
				return false;
			}
		}

		#endregion

		#region Indexers

		/// <summary>
		/// Indexer that retrieves the WizardComand object at the specified
		/// index within the internal array
		/// </summary>
		public WizardCommand this[int index] {
			get { return (WizardCommand)_cmdList[index]; }
		}

		#endregion
	}

}
