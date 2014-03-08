// -----------------------------------------------------------------------------
//  <copyright file="HelpCommand.cs" company="Zack Loveless">
//      Copyright (c) Zack Loveless.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

namespace RxCmd.Commands
{
	using System;

	public class HelpCommand : ICommand
	{
		#region Implementation of ICommand

		public string Name
		{
			get { return "help"; }
		}

		public string[] Aliases
		{
			get { return new[] {"?"}; }
		}

		public string Description
		{
			get { return ""; }
		}

		public void Execute(params object[] args)
		{
			// string[] argv = Array.ConvertAll(args, Convert.ToString);

			Program.Console.WriteLine("Help triggered.");
		}

		#endregion
	}
}
