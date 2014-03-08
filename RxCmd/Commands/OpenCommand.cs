// -----------------------------------------------------------------------------
//  <copyright file="OpenCommand.cs" company="Zack Loveless">
//      Copyright (c) Zack Loveless.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

namespace RxCmd.Commands
{
	using System;
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

		public void Execute(params string[] args)
		{
			if (Remote.Instance.State == Remote.RxState.Open)
			{
				Console.WriteLine("Connection already open. Please close the current connection before continuing.");
				return;
			}

			// open <host> <port>
			if (args.Length < 2 && !Remote.Instance.IsInitialized)
			{
				Console.WriteLine("Usage: open <host> <port>");
				return;
			}

			if (!Remote.Instance.IsInitialized)
			{
				string host = args[0];
				int port;
				if (!Int32.TryParse(args[1], out port))
				{
					Console.WriteLine("Invalid port number: {0}", args[1]);
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

				Console.WriteLine();

				Remote.Instance.Password      = builder.ToString();
			}

			Remote.Instance.Connect();
		}

		#endregion
	}
}
