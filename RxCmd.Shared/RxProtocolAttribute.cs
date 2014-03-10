// -----------------------------------------------------------------------------
//  <copyright file="RxProtocolAttribute.cs" company="Zack Loveless">
//      Copyright (c) Zack Loveless.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

namespace RxCmd.Shared
{
	using System;
	using System.ComponentModel.Composition;

	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class RxProtocolAttribute : ExportAttribute
	{
		public RxProtocolAttribute() : base(typeof (IRxProtocol))
		{
		}
	}
}
