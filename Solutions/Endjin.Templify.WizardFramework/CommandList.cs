namespace Endjin.Templify.WizardFramework
{
    using System;
    
    /// <summary>
    /// Summary description for CommandList.
	/// </summary>
	public class CommandList : WizardCommandList
	{
		public CommandList()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public void ExecuteNext() 
		{

			// Get the WizardCommand object to execute
			WizardCommand formCmd = this[this.CmdPointer];
			
			// Execute the WizardCommand object and set the current command
			// pointer to that which is returned
			this.CmdPointer = formCmd.Execute(this.CmdPointer, this.LastCmdPointer);

			
		}
	}
}
