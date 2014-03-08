// -----------------------------------------------------------------------------
//  <copyright file="Program.cs" company="Zack Loveless">
//      Copyright (c) Zack Loveless.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

namespace RxCmd
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.Composition;
	using System.ComponentModel.Composition.Hosting;
	using System.Linq;
	using System.Reflection;

	public class Program
	{
		[ImportMany] private static IEnumerable<ICommand> commands;

		internal static bool exit = false;
		private static bool in_prompt;
		private static bool in_command;

		private static void Compose()
		{
			var asm       = Assembly.GetAssembly(typeof(Program));
			var catalog   = new AggregateCatalog(new AssemblyCatalog(asm));

			var container = new CompositionContainer(catalog);

			commands      = container.GetExportedValues<ICommand>();
		}

		public static void Main(string[] argv)
		{
			Console.Title = "RxCmd";

			AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
			Compose();

			do
			{
				PrintConsole();

				string line   = Console.ReadLine();
				if (line != null)
				{
					in_prompt = false;
					string command = line.Substring(0, line.IndexOf(' ') > 0 ? line.IndexOf(' ') : line.Length);

					if (String.IsNullOrEmpty(command)) continue;

					bool valid = false;
					foreach (ICommand c in commands)
					{
						if (c.Name.Equals(command, StringComparison.OrdinalIgnoreCase) ||
						    c.Aliases.Any(x => x.Equals(command, StringComparison.OrdinalIgnoreCase)))
						{
							string[] args = line.Split(' ').Skip(1).ToArray();

							in_command = true;
							c.Execute(args);

							valid      = true;
							in_command = false;
						}
					}

					if (!valid)
					{
						Console.WriteLine("The command \"{0}\" was not found. Please check spelling and try again.", command);
					}
				}
			} while (!exit);
		}

		private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs args)
		{
			Exception e = args.ExceptionObject as Exception;
			if (e != null)
			{
				if (in_prompt || in_command)
				{
					Console.Write('\n');
				}

				ConsoleColor c = Console.ForegroundColor;
				Console.ForegroundColor = ConsoleColor.Red;

				Console.WriteLine("Exception: {0}\n\nMessage: {1}", e.GetType().Name, e.Message);
				Console.ForegroundColor = c;
			}
		}
		
		public static void PrintConsole()
		{
			Console.Write(@"Rx:\> ");

			in_prompt = true;
		}
	}
}
