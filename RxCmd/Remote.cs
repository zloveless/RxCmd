// -----------------------------------------------------------------------------
//  <copyright file="Remote.cs" company="Zack Loveless">
//      Copyright (c) Zack Loveless.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

namespace RxCmd
{
	using System;
	using System.IO;
	using System.Net;
	using System.Net.Sockets;
	using System.Text;
	using System.Threading;
	using System.Threading.Tasks;

	public delegate void RxLogReceiveCallback(string logMessage);

	public class Remote
	{
		private static Remote instance;

		public static Remote Instance
		{
			get { return instance ?? (instance = new Remote()); }
		}

		private Remote()
		{
			State             = RxState.Closed;
			RxRemoteLogEvent += OnLogRead;
			source            = new CancellationTokenSource();
		}

		~Remote()
		{
			source.Cancel();
		}

		private TcpClient client;
		private Encoding encoding;
		private StreamReader reader;
		private Stream stream;
		private readonly CancellationTokenSource source;
		private StreamWriter writer;

		#region Events

		public event RxLogReceiveCallback RxRemoteLogEvent;

		#endregion
		
		#region Properties

		public bool IsInitialized
		{
			get
			{
				bool ret = true;

				if (string.IsNullOrEmpty(Host)) ret = false;
				else if (string.IsNullOrEmpty(Password)) ret = false;
				else if (Port <= 0) ret = false;

				return ret;
			}
		}

		/// <summary>
		/// Gets or sets a <see cref="T:System.String" /> value representing the host for the remote console.
		/// </summary>
		public string Host { get; set; }

		/// <summary>
		/// Sets a <see cref="T:System.String" /> value representing the password for the remote console.
		/// </summary>
		public string Password { private get; set; }

		/// <summary>
		/// Gets or sets a <see cref="T:System.Int32" /> value representing the port for the remote console.
		/// </summary>
		public int Port { get; set; }

		public bool RequestOutput { get; set; }

		public RxState State { get; private set; }

		public RxVersion Version { get; private set; }

		#endregion

		#region Methods

		private void Auth()
		{
			if (client == null)
			{
				if (!IsInitialized)
				{
					throw new RxRemoteException("Unable to continue. Remoting client has not been initialized");
				}

				Connect();
			}

			StringBuilder builder = new StringBuilder();
			if (Version == RxVersion.Version1)
			{
				builder.Append("a");
				builder.Append(Password);
				builder.AppendLine();
			}

			if (builder.Length > 0)
			{
				Write(builder.ToString());
			}
		}

		public void Connect()
		{
			if (State == RxState.Open) return;
			if (!IsInitialized)
			{
				throw new RxRemoteException("Unable to continue. Remoting client has not been initialized");
			}

			try
			{
				IPAddress address;
				if (!IPAddress.TryParse(Host, out address))
				{
					IPHostEntry entry = Dns.GetHostEntry(Host);
					if (entry.AddressList.Length > 0)
					{
						address = entry.AddressList[0];
					}
				}

				client        = new TcpClient();
				
				IPEndPoint ep = new IPEndPoint(address, Port);
				client.Connect(ep);
				
				stream = client.GetStream();

				byte[] buf  = new byte[1024];
				int count   = stream.Read(buf, 0, buf.Length);
				string data = Encoding.ASCII.GetString(buf, 0, count);

				if (data.Substring(0, 4) == "v001")
				{
					Version = RxVersion.Version1;
				}
				else if (data.Substring(0, 1) == "e")
				{
					throw new RxRemoteException(data.Substring(1));
				}
				else
				{
					throw new RxRemoteException("Remote server version unknown.",
						new NotSupportedException(
							"Server could be running a newer version of the protocol that this client doesn't support."));
				}

				encoding = new UTF8Encoding(false);
				reader   = new StreamReader(stream, encoding);
				writer   = new StreamWriter(stream, encoding);

				State = RxState.Open;

				Auth();

				reader.ReadLineAsync().ContinueWith(OnAsyncRead, source.Token);
			}
			catch (SocketException e)
			{
				State = RxState.Closed;

				throw new RxRemoteException("Unable to establish connection to remote server. See inner exception for details.", e);
			}
		}

		public void Close()
		{
			if (State != RxState.Open) return;
			if (client != null)
			{
				source.Cancel();
				reader.Dispose();
				writer.Dispose();

				client.Close();
				State = RxState.Closed;
			}
		}

		public void ExecuteCommand(string format, params object[] args)
		{
			if (State != RxState.Open) return;
			if (client == null)
			{
				if (!IsInitialized)
				{
					throw new RxRemoteException("Unable to continue. Remoting client has not been initialized");
				}

				Connect();
			}

			StringBuilder builder = new StringBuilder();
			if (Version == RxVersion.Version1)
			{
				builder.Append('c');
				builder.AppendFormat(format, args);
				builder.AppendLine();
			}

			if (builder.Length > 0)
			{
				Write(builder.ToString());
			}
		}

		private void OnAsyncRead(Task<String> t)
		{
			if (t.Exception == null && t.Result != null && !t.IsCanceled)
			{
				if (RxRemoteLogEvent != null)
				{
					RxRemoteLogEvent(t.Result);
				}

				reader.ReadLineAsync().ContinueWith(OnAsyncRead, source.Token);
			}
			else if (t.Exception != null)
			{
				throw new RxRemoteException("An error occurred while attempting to read from the remote client.", t.Exception);
			}
		}

		private void OnLogRead(string message)
		{
			if (RequestOutput)
			{
				Console.WriteLine(message);
			}
		}

		public void Subscribe()
		{
			if (Version == RxVersion.Version1)
			{
				Write("s\n");
			}
		}

		public void Unsubscribe()
		{
			if (Version == RxVersion.Version1)
			{
				Write("u\n");
			}
		}

		private void Write(string message)
		{
			if (writer != null)
			{
				writer.Write(message);
				writer.Flush();
			}
			else
			{
				byte[] buf = encoding.GetBytes(message);

				stream.Write(buf, 0, buf.Length);
			}
		}

		#endregion

		#region Nested type: RxVersion

		public enum RxVersion
		{
			Unknown = 0,
			Version1,
		}

		#endregion
		
		#region Nested type: RxState

		public enum RxState : byte
		{
			Closed = 0,
			Open   = 1,
		}

		#endregion
	}
}
