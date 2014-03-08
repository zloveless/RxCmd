// -----------------------------------------------------------------------------
//  <copyright file="IConsole.cs" company="Zack Loveless">
//      Copyright (c) Zack Loveless.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

namespace RxCmd
{
	public interface IConsole
	{
		void Write(string format, params object[] args);

		void WriteLine();

		void WriteLine(string format, params object[] args);
	}
}
