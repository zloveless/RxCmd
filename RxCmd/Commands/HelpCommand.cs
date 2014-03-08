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
			get { throw new NotImplementedException(); }
		}

		public void Execute(params string[] args)
		{
			Console.WriteLine("Help triggered.");
		}

		#endregion
	}
}
