// -----------------------------------------------------------------------------
//  <copyright file="RxLogEventArgs.cs" company="Zack Loveless">
//      Copyright (c) Zack Loveless.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

using System;

namespace RxCmd.Shared
{
    public class RxLogEventArgs : EventArgs
    {
        public RxLogEventArgs(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}