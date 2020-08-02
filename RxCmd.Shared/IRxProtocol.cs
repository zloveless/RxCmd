// -----------------------------------------------------------------------------
//  <copyright file="IRxProtocol.cs" company="Zack Loveless">
//      Copyright (c) Zack Loveless.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

using System;

namespace RxCmd.Shared
{
    public interface IRxProtocol
    {
        event EventHandler<RxLogEventArgs> RxLog;

        event EventHandler<RxLogEventArgs> RxError;

        void Authorize(string password);

        void Execute(string command);

        void Subscribe();

        void Unsubscribe();
    }
}