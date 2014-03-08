// -----------------------------------------------------------------------------
//  <copyright file="AddbotsCommand.cs" company="Zack Loveless">
//      Copyright (c) Zack Loveless.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

namespace RxCmd.Commands
{
	using System;

	public class AddbotsCommand : ICommand
	{
		#region Implementation of ICommand

		public string Name
		{
			get { return "addbots"; }
		}

		public string[] Aliases
		{
			get { return new[] {"bots"}; }
		}

		public string Description
		{
			get { return ""; }
		}

		public void Execute(params string[] args)
		{
			if (Remote.Instance.State == Remote.RxState.Closed)
			{
				Console.WriteLine("Remote client not connected.");
				return;
			}

			int botsToAdd;
			if (args.Length == 0 || !Int32.TryParse(args[0], out botsToAdd))
			{
				Console.WriteLine("Usage: addbots <number>");
				return;
			}

			Remote.Instance.ExecuteCommand("addbots {0}", botsToAdd);
		}

		#endregion
	}
}
