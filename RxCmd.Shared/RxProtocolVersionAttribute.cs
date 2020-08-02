// -----------------------------------------------------------------------------
//  <copyright file="RxProtocolVersionAttribute.cs" company="Zack Loveless">
//      Copyright (c) Zack Loveless.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

using System;
using System.ComponentModel.Composition;

namespace RxCmd.Shared
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class RxProtocolVersionAttribute : Attribute, IRxProtocolAttribute
    {
        public RxProtocolVersionAttribute(string version)
        {
            Version = version;
        }

        #region Implementation of IRxProtocolAttribute

        public string Version { get; }

        #endregion
    }
}