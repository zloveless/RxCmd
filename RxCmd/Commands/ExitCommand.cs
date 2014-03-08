// -----------------------------------------------------------------------------
//  <copyright file="ExitCommand.cs" company="Zack Loveless">
//      Copyright (c) Zack Loveless.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

namespace RxCmd.Commands
{
	public class ExitCommand : ICommand
	{
		#region Implementation of ICommand

		public string Name
		{
			get { return "exit"; }
		}

		public string[] Aliases
		{
			get { return new[] {"quit"}; }
		}

		public string Description
		{
			get { return "Description of exit!"; }
		}

		public void Execute(params object[] args)
		{
			Program.exit = true;
		}

		#endregion
	}
}
