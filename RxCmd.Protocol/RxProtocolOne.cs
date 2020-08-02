﻿// -----------------------------------------------------------------------------
//  <copyright file="RxProtocolOne.cs" company="Zack Loveless">
//      Copyright (c) Zack Loveless.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

using System;
using RxCmd.Shared;

namespace RxCmd.Protocol
{
    [RxProtocol]
    [RxProtocolVersion("v001")]
    public class RxProtocolOne : RxProtocolBase
    {
        public RxProtocolOne()
        {
            Remote.Instance.RxDataReceiveCallback += LogRead;
        }

        private void LogRead(string x)
        {
            if (x.Substring(0, 1) == "l")
            {
                var handler = RxLog;
                if (handler != null) handler(this, new RxLogEventArgs(x.Substring(1)));
            }
            else if (x.Substring(0, 1) == "e")
            {
                var handler = RxError;
                if (handler != null) handler(this, new RxLogEventArgs(x.Substring(1)));
            }
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
    }
}