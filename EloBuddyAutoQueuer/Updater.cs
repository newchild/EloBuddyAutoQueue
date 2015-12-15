using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EloBuddyAutoQueuer
{
	class Updater
	{
		public static bool isUpdated()
		{
			WebClient x = new WebClient();
			var version = x.DownloadString(@"https://raw.githubusercontent.com/newchild/EloBuddyAutoQueue/master/version.txt");
			if (version == StaticData.LocalVersion)
				return true;
			return false;
		}

		public static void Update()
		{
			WebClient x = new WebClient();
			var file = x.DownloadData()
		}
	}
}
