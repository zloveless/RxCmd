// -----------------------------------------------------------------------------
//  <copyright file="ConsoleAdapter.cs" company="Zack Loveless">
//      Copyright (c) Zack Loveless.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

namespace RxCmd
{
	public delegate void ConsoleWriteDelegate(string arg0);

	public class ConsoleAdapter : IConsole
	{
		private readonly ConsoleWriteDelegate console;

		/// <summary>
		///     Initializes a new instance of the <see cref="T:System.Object" /> class.
		/// </summary>
		public ConsoleAdapter(ConsoleWriteDelegate console)
		{
			this.console = console;
		}

		#region Implementation of IConsole

		public void Write(string format, params object[] args)
		{
			if (Program.in_prompt/* || Program.in_command*/)
			{
				console("\n");
			}

			console(string.Format(format, args));
		}

		public void WriteLine()
		{
			console("\n");
		}

		public void WriteLine(string format, params object[] args)
		{
			if (Program.in_prompt/* || Program.in_command*/)
			{
				console("\n");
			}

			console(string.Format(format, args));
			console("\n");
		}

		#endregion
	}
}
