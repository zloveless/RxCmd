// -----------------------------------------------------------------------------
//  <copyright file="SayCommand.cs" company="Zack Loveless">
//      Copyright (c) Zack Loveless.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

namespace RxCmd.Commands
{
	using System;
	using System.Linq;

	public class SayCommand : ICommand
	{
		#region Implementation of ICommand

		public string Name
		{
			get { return "say"; }
		}

		public string[] Aliases
		{
			get { return new[] {"s", "msg"}; }
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

			if (args.Length == 0)
			{
				Program.Console.WriteLine("Usage: say <message>");
				return;
			}

			Remote.Instance.ExecuteCommand("say {0}", String.Join(" ", args));
		}

		#endregion
	}
}
