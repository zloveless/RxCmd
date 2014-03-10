// -----------------------------------------------------------------------------
//  <copyright file="OpenCommand.cs" company="Zack Loveless">
//      Copyright (c) Zack Loveless.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

namespace RxCmd.Commands
{
	using System;
	using System.Text;
	using Shared;

	public class OpenCommand : ICommand
	{
		#region Implementation of ICommand

		public string Name
		{
			get { return "open"; }
		}

		public string[] Aliases
		{
			get { return new[] {"o"}; }
		}

		public string Description
		{
			get { return "Description of open!"; }
		}

		public void Execute(params object[] args)
		{
			if (Remote.Instance.State == Remote.RxState.Opened)
			{
				Program.Console.WriteLine("A connection is already open. Close the current connection before connecting somewhere else.");
				return;
			}

			// open <host> <port>
			// todo: alternative command: open host:port
			if (args.Length < 2)
			{
				Program.Console.WriteLine("Usage: open <host> <port>");
				return;
			}

			string[] argv = Array.ConvertAll(args, Convert.ToString);
			string host   = argv[0];

			int port;
			if (!Int32.TryParse(argv[1], out port))
			{
				Program.Console.WriteLine("Invalid port number: {0}", args[1]);
				return;
			}
			
			Console.Write("Enter password: ");

			StringBuilder builder = new StringBuilder();
			ConsoleKeyInfo ki;
			do
			{
				ki = Console.ReadKey(true);

				if (!char.IsControl(ki.KeyChar) && ki.Key != ConsoleKey.Backspace)
				{
					builder.Append(ki.KeyChar);
					Console.Write('*');
				}
			} while (ki.Key != ConsoleKey.Enter);

			Program.Console.WriteLine();

			Remote.Instance.Connect(host, port, builder.ToString());
		}

		#endregion
	}
}
