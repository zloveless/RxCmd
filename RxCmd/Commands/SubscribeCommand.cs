// -----------------------------------------------------------------------------
//  <copyright file="SubscribeCommand.cs" company="Zack Loveless">
//      Copyright (c) Zack Loveless.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

namespace RxCmd.Commands
{
	using System;
	using System.Linq;

	public class SubscribeCommand : ICommand
	{
		#region Implementation of ICommand

		public string Name
		{
			get { return "subscribe"; }
		}

		public string[] Aliases
		{
			get { return new[] { "s" }; }
		}

		public string Description
		{
			get { return "Subscribes from game events."; }
		}

		public void Execute(params string[] args)
		{
			if (Remote.Instance.State == Remote.RxState.Closed)
			{
				Console.WriteLine("Remote client not connected.");
				return;
			}

			Remote.Instance.Subscribe();
			Remote.Instance.RequestOutput = true;
		}

		#endregion
	}
}
