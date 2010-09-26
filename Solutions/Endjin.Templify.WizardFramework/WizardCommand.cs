namespace Endjin.Templify.WizardFramework
{
    using System;

	/// <summary>
	/// Abstract class that defines a Wziard Command object. This is used
	/// to implement the command pattern.
	/// </summary>
	public abstract class WizardCommand {

		/// <summary>
		/// Executes the wizard command object. The index of the current 
		/// command is passed so that the related state can be updated.
		/// </summary>
		/// <param name="cmdPointer">Index of the current command pointer</param>
		/// <returns>Index of the next command pointer</returns>
		public abstract int Execute(int cmdPointer, int lastCmdPointer);
		
		/// <summary>
		/// Initialises the command object
		/// </summary>
		public abstract void InitialiseCommand();

		/// <summary>
		/// Cleans up the command object
		/// </summary>
		public abstract void CleanupCommand();
	}


}

