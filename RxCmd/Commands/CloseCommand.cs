// -----------------------------------------------------------------------------
//  <copyright file="CloseCommand.cs" company="Zack Loveless">
//      Copyright (c) Zack Loveless.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

namespace RxCmd.Commands
{
	using System;

	public class CloseCommand : ICommand
	{
		#region Implementation of ICommand

		public string Name
		{
			get { return "close"; }
		}

		public string[] Aliases
		{
			get { return new[] {"c"}; }
		}

		public string Description
		{
			get { throw new NotImplementedException(); }
		}

		public void Execute(params string[] args)
		{
			if (Remote.Instance.State != Remote.RxState.Open)
			{
				Console.WriteLine("Remote client already closed.");
				return;
			}

			Remote.Instance.Close();
		}

		#endregion
	}
}
