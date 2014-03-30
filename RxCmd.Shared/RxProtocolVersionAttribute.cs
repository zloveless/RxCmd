// -----------------------------------------------------------------------------
//  <copyright file="RxProtocolVersionAttribute.cs" company="Zack Loveless">
//      Copyright (c) Zack Loveless.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

namespace RxCmd.Shared
{
	using System;
	using System.ComponentModel.Composition;

	[MetadataAttribute, AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class RxProtocolVersionAttribute : Attribute, IRxProtocolAttribute
	{
		public RxProtocolVersionAttribute(string version)
		{
			Version = version;
		}

		#region Implementation of IRxProtocolAttribute

		public string Version { get; private set; }

		#endregion
	}
}
