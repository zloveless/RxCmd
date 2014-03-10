// -----------------------------------------------------------------------------
//  <copyright file="RxProtocolBase.cs" company="Zack Loveless">
//      Copyright (c) Zack Loveless.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

namespace RxCmd.Shared
{
	public abstract class RxProtocolBase : IRxProtocol
	{
		protected RxProtocolBase()
		{
			Remote.Instance.RxDataReceiveCallback += LogRead;
		}

		private void LogRead(string x)
		{
			if (x.Substring(0, 1) == "l")
			{
				if (RxLogRead != null)
				{
					RxLogRead(x.Substring(1));
				}
			}
		}

		#region Implementation of IRxProtocol

		public event RxLogReceiveCallback RxLogRead;

		public abstract void Authorize(string password);

		public abstract void Execute(string command);

		public abstract void Subscribe();

		public abstract void Unsubscribe();

		#endregion
	}
}
