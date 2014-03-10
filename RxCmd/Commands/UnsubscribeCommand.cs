// -----------------------------------------------------------------------------
//  <copyright file="UnsubscribeCommand.cs" company="Zack Loveless">
//      Copyright (c) Zack Loveless.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

namespace RxCmd.Commands
{
	using System;
	using Shared;

	public class UnsubscribeCommand : ICommand
	{
		#region Implementation of ICommand

		public string Name
		{
			get { return "unsubscribe"; }
		}

		public string[] Aliases
		{
			get { return new[] {"u"}; }
		}

		public string Description
		{
			get { return "Unsubscribes from game events."; }
		}

		public void Execute(params object[] args)
		{
			if (Remote.Instance.State == Remote.RxState.Closed)
			{
				Console.WriteLine("Remote client not connected.");
				return;
			}

			Remote.Instance.Protocol.Unsubscribe();
			
		}

		#endregion
	}
}
