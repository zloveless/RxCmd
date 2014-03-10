// -----------------------------------------------------------------------------
//  <copyright file="Remote.cs" company="Zack Loveless">
//      Copyright (c) Zack Loveless.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

namespace RxCmd.Shared
{
	using System;
	using System.IO;
	using System.Net;
	using System.Net.Sockets;
	using System.Text;
	using System.Threading;
	using System.Threading.Tasks;

	public delegate void RxReceiveCallback(string message);

	public class Remote
	{
		#region Singleton

		private static Remote instance;

		/// <summary>
		/// Represents a singleton instance of the Remote class.
		/// </summary>
		public static Remote Instance
		{
			get { return instance ?? (instance = new Remote()); }
		}

		#endregion

		public Remote()
		{
			source = new CancellationTokenSource();

			source.Token.Register(() =>
			                      {
				                      client.Close();

									  // TODO: Unhack this and come up with a proper solution.
				                      instance = null;
			                      });

			RxDataReceiveCallback += OnDataRead;
		}

		~Remote()
		{
			client.Close();
			source.Dispose();

			source = null;
			Protocol = null;
		}

		#region Fields

		internal TcpClient client;
		private Encoding encoding;
		private StreamReader reader;
		private CancellationTokenSource source;
		private Stream stream;
		private EventWaitHandle wait;
		private StreamWriter writer;

		#endregion

		#region Events

		public event RxReceiveCallback RxDataReceiveCallback;
		
		#endregion

		#region Properties

		/// <summary>
		/// Gets an instance of the current protocol.
		/// </summary>
		public IRxProtocol Protocol { get; private set; }

		public RxState State { get; set; }

		#endregion
		
		#region Methods

		private void AuthCallback(string x)
		{
			if (x.Substring(0, 1) == "v")
			{
				string version = x.Substring(0, 4);

				// *prays that this versioning system doesn't go away*

				IRxProtocol p;
				if (!ProtocolManager.TryGetProtocol(version, out p))
				{
					throw new RxRemoteException(
						"Unknown version detected from server. This likely means the server is running a newer protocol version than this client supports.");
				}

				Protocol = p;

				State |= RxState.Opened;

				RxDataReceiveCallback -= AuthCallback;
				wait.Set();
			}
		}

		/// <summary>
		/// Connects to a Renegade-X server on the specified host and port using the specified password.
		/// </summary>
		/// <param name="host"></param>
		/// <param name="port"></param>
		/// <param name="password"></param>
		/// <exception cref="System.ArgumentException" />
		/// <exception cref="System.ArgumentNullException" />
		/// <exception cref="RxCmd.Shared.RxRemoteException" />
		public void Connect(string host, int port, string password)
		{
			if (host == null) throw new ArgumentNullException("host");
			if (password == null) throw new ArgumentNullException("password");
			if (port <= 0) throw new ArgumentException("port");

			try
			{
				IPAddress address;
				if (!IPAddress.TryParse(host, out address))
				{
					var entry = Dns.GetHostEntry(host);
					address   = entry.AddressList[0];
				}

				client = new TcpClient();
				client.Connect(new IPEndPoint(address, port));

				RxDataReceiveCallback += AuthCallback;

				InitializeStream();

				wait = new EventWaitHandle(false, EventResetMode.ManualReset);
				if (wait.WaitOne(TimeSpan.FromSeconds(5.0)))
				{
					Protocol.Authorize(password);
				}
			}
			catch (SocketException e)
			{
				throw new RxRemoteException("Unable to establish connection to remote server. See inner exception for details.", e);
			}
		}

		public void Close()
		{
			if (State == RxState.Closed || client == null) return;

			source.Cancel();
		}

		private void InitializeStream()
		{
			stream   = client.GetStream();
			encoding = new UTF8Encoding(false);

			reader = new StreamReader(stream, encoding);
			writer = new StreamWriter(stream, encoding);

			reader.ReadLineAsync().ContinueWith(OnAsyncRead, source.Token);
		}

		protected virtual void OnAsyncRead(Task<String> task)
		{
			if (task.Result != null && !task.IsFaulted && !task.IsCanceled)
			{
				if (RxDataReceiveCallback != null)
				{
					RxDataReceiveCallback(task.Result);
				}

				reader.ReadLineAsync().ContinueWith(OnAsyncRead, source.Token);
			}
			else if (task.Exception != null)
			{
				throw new RxRemoteException(
					"An error occurred reading from the remote client. See inner exception for more details.",
					task.Exception);
			}
			else if (task.IsCanceled)
			{
				State = RxState.Closed;
				client.Close();
			}
		}

		private void OnDataRead(string message)
		{
			if (message.Substring(0, 1) == "e")
			{
				throw new RxRemoteException(string.Format("A protocol error has occurred: {0}", message.Substring(1)));
			}
		}

		public void WriteLine(string format, params object[] args)
		{
			if (writer != null)
			{
				writer.WriteLine(format, args);
				writer.Flush();
			}
		}

		#endregion

		#region Nested type: RxState

		[Flags]
		public enum RxState : byte
		{
			Closed     = 0,
			Opened     = 1,
			Subscribed = 2,
		}

		#endregion
	}
}
