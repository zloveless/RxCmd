// -----------------------------------------------------------------------------
//  <copyright file="IRxProtocol.cs" company="Zack Loveless">
//      Copyright (c) Zack Loveless.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

namespace RxCmd.Shared
{
	public delegate void RxLogReceiveCallback(string logMessage);

	public interface IRxProtocol
	{
		event RxLogReceiveCallback RxLogRead;

		void Authorize(string password);

		void Execute(string command);

		void Subscribe();

		void Unsubscribe();
	}
}
