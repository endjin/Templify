namespace Endjin.Templify.WizardFramework
{
    using System;
	
	/// <summary>
	/// Abstract class that defines a wizard command for Windows Forms. Implements
	/// the abstract wizard command class.
	/// </summary>
	public abstract class FormWizardCommand : WizardCommand {
		
		#region Private Variables

		/// <summary>
		/// The Windows Form used for this command
		/// </summary>
		private BaseWizardForm _commandForm = null;

		#endregion

		#region Properties

		/// <summary>
		/// he Windows Form used for this command
		/// </summary>
		public BaseWizardForm CommandForm {
			get { return _commandForm; }
			set { _commandForm = value; }
		}

		#endregion

		#region Methods

		/// <summary>
		/// Executes the WizardCommand object
		/// </summary>
		/// <returns>TODO</returns>
		public override int Execute(int cmdPointer, int lastCmdPointer) {
			
			// Setup the template - this is implemented within the sub class
			InitialiseCommand();

			// Show the command form
			this.CommandForm.CommandIndex = cmdPointer;
			this.CommandForm.OriginCommandIndex = lastCmdPointer;
            //this.CommandForm.Visible = true;
            var res = this.CommandForm.ShowDialog();
			

			// Cleanup the template data - this is implemented within the
			// sub class
			CleanupCommand();

			return this.CommandForm.CommandIndex;

		}

		#endregion

	}
}
