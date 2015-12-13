using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EloBuddyAutoQueuer
{
	class TempPatcher
	{
		public static void Patch()
		{
			VersionHandler.CopyEBFiles();
		}
	}
}
