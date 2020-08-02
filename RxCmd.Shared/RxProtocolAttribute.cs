// -----------------------------------------------------------------------------
//  <copyright file="RxProtocolAttribute.cs" company="Zack Loveless">
//      Copyright (c) Zack Loveless.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

using System;
using System.ComponentModel.Composition;

namespace RxCmd.Shared
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RxProtocolAttribute : ExportAttribute
    {
        public RxProtocolAttribute() : base(typeof(IRxProtocol))
        {
        }
    }
}
