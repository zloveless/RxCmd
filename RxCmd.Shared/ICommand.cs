// -----------------------------------------------------------------------------
//  <copyright file="ICommand.cs" company="Zack Loveless">
//      Copyright (c) Zack Loveless.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

namespace RxCmd.Shared
{
	using System.ComponentModel.Composition;

	[InheritedExport(typeof (ICommand))]
	public interface ICommand
	{
		string Name { get; }

		string[] Aliases { get; }

		string Description { get; }

		void Execute(params object[] args);
	}
}
