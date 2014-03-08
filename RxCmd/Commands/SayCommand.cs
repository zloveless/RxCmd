// -----------------------------------------------------------------------------
//  <copyright file="SayCommand.cs" company="Zack Loveless">
//      Copyright (c) Zack Loveless.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

namespace RxCmd.Commands
{
	using System;

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

		public void Execute(params string[] args)
		{
			if (Remote.Instance.State == Remote.RxState.Closed)
			{
				Console.WriteLine("Remote client not connected.");
				return;
			}

			if (args.Length == 0)
			{
				Console.WriteLine("Usage: say <message>");
				return;
			}

			Remote.Instance.ExecuteCommand("say {0}", args);
		}

		#endregion
	}
}
