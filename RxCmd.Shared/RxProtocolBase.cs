// -----------------------------------------------------------------------------
//  <copyright file="RxProtocolBase.cs" company="Zack Loveless">
//      Copyright (c) Zack Loveless.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

namespace RxCmd.Shared
{
	using System;

	public abstract class RxProtocolBase : IRxProtocol
	{
		#region Implementation of IRxProtocol

		public abstract event EventHandler<RxLogEventArgs> RxLog;
		public abstract event EventHandler<RxLogEventArgs> RxError;

		public abstract void Authorize(string password);

		public abstract void Execute(string command);

		public abstract void Subscribe();

		public abstract void Unsubscribe();

		#endregion
	}
}
