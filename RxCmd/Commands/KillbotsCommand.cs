// -----------------------------------------------------------------------------
//  <copyright file="KillbotsCommand.cs" company="Zack Loveless">
//      Copyright (c) Zack Loveless.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

namespace RxCmd.Commands
{
	using System;

	public class KillbotsCommand : ICommand
	{
		#region Implementation of ICommand

		public string Name
		{
			get { return "killbots"; }
		}

		public string[] Aliases
		{
			get { return new string[] {}; }
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

			Remote.Instance.ExecuteCommand("killbots");
		}

		#endregion
	}
}
