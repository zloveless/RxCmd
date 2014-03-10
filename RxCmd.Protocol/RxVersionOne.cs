// -----------------------------------------------------------------------------
//  <copyright file="RxVersionOne.cs" company="Zack Loveless">
//      Copyright (c) Zack Loveless.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

namespace RxCmd.Protocol
{
	using Shared;

	[RxProtocol, RxProtocolVersion("v001")]
	public class RxVersionOne : RxProtocolBase
	{
		#region Overrides of RxProtocolBase

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
	}
}
