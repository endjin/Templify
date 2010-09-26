namespace Endjin.Templify.WizardFramework
{
    using System;
    using System.Windows.Forms;

	/// <summary>
	/// Base Form for use with forms that wish to implement the Wizard 
	/// Command
	/// </summary>
	public class BaseWizardForm : Form {
		
		#region Constructors

		/// <summary>
		/// Default constructor
		/// </summary>
		public BaseWizardForm() {
		}

		#endregion

		#region Private Variables

		//CommandIndex starts at 0
		private int _commandIndex = 0;
		private int _originCommandIndex = 0;

		#endregion

		#region Public Properties
		public int CommandIndex {
			get { return _commandIndex; }
			set { _commandIndex = value; }
		}

		public int OriginCommandIndex {
			get { return _originCommandIndex; }
			set { _originCommandIndex = value; }
		}
		#endregion

		#region Public Methods
		
		// Go back to the OriginCommandIndex - who sent the form to the particular index
		public void MoveToSender()
		{
			CommandIndex = OriginCommandIndex;
			this.Close();
			
		}

		// Move to a specific CommandIndex
		public void MoveTo(int index)
		{
			OriginCommandIndex = CommandIndex;	
			CommandIndex = index;
			this.Close();
		}
		// Move to the next CommandIndex in the list
		public void MoveNext() {
			CommandIndex++;
			this.Close();
		}
		
		// Move to the previous CommandIndex in the list
		public void MovePrevious() {
			CommandIndex--;
			this.Close();
		}

		// 
		public void MoveExit() {
			CommandIndex = -1;
		}

		#endregion

	}
}

