// -----------------------------------------------------------------------------
//  <copyright file="ProtocolManager.cs" company="Zack Loveless">
//      Copyright (c) Zack Loveless.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------
namespace RxCmd.Shared
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.Composition;
	using System.ComponentModel.Composition.Hosting;
	using System.Linq;

	public static class ProtocolManager
	{
		[ImportMany] private static IEnumerable<Lazy<IRxProtocol, IRxProtocolAttribute>> protocols;

		public static void Load()
		{
			var container = new CompositionContainer(new DirectoryCatalog("."));
			protocols     = container.GetExports<IRxProtocol, IRxProtocolAttribute>();
		}

		public static IRxProtocol GetProtocol(string version)
		{
			if (protocols.Any(x => x.Metadata.Version == version))
			{
				var p = protocols.SingleOrDefault(x => x.Metadata.Version == version);
				if (p != null)
				{
					return p.Value;
				}
			}

			return null;
		}

		public static bool TryGetProtocol(string version, out IRxProtocol protocol)
		{
			if (protocols.Any(x => x.Metadata.Version == version))
			{
				var p = protocols.SingleOrDefault(x => x.Metadata.Version == version);
				if (p != null)
				{
					protocol = p.Value;
					return true;
				}
			}

			protocol = null;
			return false;
		}
	}
}
