// -----------------------------------------------------------------------------
//  <copyright file="OpenCommand.cs" company="Zack Loveless">
//      Copyright (c) Zack Loveless.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

namespace RxCmd.Commands
{
	using System;
	using System.Linq;
	using System.Text;

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
			if (Remote.Instance.State == Remote.RxState.Open)
			{
				Program.Console.WriteLine("Connection already open. Please close the current connection before continuing.");
				return;
			}

			// open <host> <port>
			if (args.Length < 2 && !Remote.Instance.IsInitialized)
			{
				Program.Console.WriteLine("Usage: open <host> <port>");
				return;
			}

			string[] argv = Array.ConvertAll(args, Convert.ToString);

			if (!Remote.Instance.IsInitialized)
			{
				string host = argv[0];
				int port;
				if (!Int32.TryParse(argv[1], out port))
				{
					Program.Console.WriteLine("Invalid port number: {0}", args[1]);
					return;
				}

				Remote.Instance.Host = host;
				Remote.Instance.Port = port;

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

				Remote.Instance.Password      = builder.ToString();
			}

			Remote.Instance.Connect();
		}

		#endregion
	}
}
