// -----------------------------------------------------------------------------
//  <copyright file="RxVersionOne.cs" company="Zack Loveless">
//      Copyright (c) Zack Loveless.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

namespace RxCmd.Protocol
{
	using System;
	using Shared;

	[RxProtocol, RxProtocolVersion("v001")]
	public class RxVersionOne : RxProtocolBase
	{
		public RxVersionOne()
		{
			Remote.Instance.RxDataReceiveCallback += LogRead;
		}
		
		#region Overrides of RxProtocolBase

		public override event EventHandler<RxLogEventArgs> RxLog;
		public override event EventHandler<RxLogEventArgs> RxError;

		public override void Authorize(string password)
		{
			Remote.Instance.WriteLine("a{0}", password);
		}

		public override void Execute(string command)
		{
			Remote.Instance.WriteLine("c{0}", command);
		}

		public override void Subscribe()
		{
			Remote.Instance.WriteLine("s");
			Remote.Instance.State |= Remote.RxState.Subscribed;
		}

		public override void Unsubscribe()
		{
			Remote.Instance.State &= ~Remote.RxState.Subscribed;
			Remote.Instance.WriteLine("u");
		}

		#endregion

		private void LogRead(string x)
		{
			if (x.Substring(0, 1) == "l")
			{
				var handler = RxLog;
				if (handler != null)
				{
					handler(this, new RxLogEventArgs(x.Substring(1)));
				}
			}
			else if (x.Substring(0, 1) == "e")
			{
				var handler = RxError;
				if (handler != null)
				{
					handler(this, new RxLogEventArgs(x.Substring(1)));
				}
			}
		}
	}
}
