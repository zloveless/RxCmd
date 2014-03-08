// -----------------------------------------------------------------------------
//  <copyright file="ClearCommand.cs" company="Zack Loveless">
//      Copyright (c) Zack Loveless.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------
namespace RxCmd.Commands
{
	using System;

	public class ClearCommand : ICommand
	{
		#region Implementation of ICommand

		public string Name
		{
			get { return "clear"; }
		}

		public string[] Aliases
		{
			get { return new[] {"cls"}; }
		}

		public string Description
		{
			get { return "Clears the Console buffer."; }
		}

		public void Execute(params object[] args)
		{
			Console.Clear();
		}

		#endregion
	}
}