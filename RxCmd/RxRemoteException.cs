// -----------------------------------------------------------------------------
//  <copyright file="RxRemoteException.cs" company="Zack Loveless">
//      Copyright (c) Zack Loveless.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

namespace RxCmd
{
	using System;

	public class RxRemoteException : Exception
	{
		public RxRemoteException(string message) : base(message)
		{
		}

		public RxRemoteException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
