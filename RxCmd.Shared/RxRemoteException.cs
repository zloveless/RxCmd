// -----------------------------------------------------------------------------
//  <copyright file="RxRemoteException.cs" company="Zack Loveless">
//      Copyright (c) Zack Loveless.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

using System;

namespace RxCmd.Shared
{
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