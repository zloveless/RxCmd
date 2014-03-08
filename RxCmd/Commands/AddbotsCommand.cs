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

		public void Execute(params object[] args)
		{
			if (Remote.Instance.State == Remote.RxState.Closed)
			{
				Program.Console.WriteLine("Remote client not connected.");
				return;
			}

			string[] argv = Array.ConvertAll(args, Convert.ToString);

			int botsToAdd;
			if (args.Length == 0 || !Int32.TryParse(argv[0], out botsToAdd))
			{
				Program.Console.WriteLine("Usage: addbots <number>");
				return;
			}

			Remote.Instance.ExecuteCommand("addbots {0}", botsToAdd);
		}

		#endregion
	}
}
