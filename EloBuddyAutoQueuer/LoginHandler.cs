using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Windows;
using System.Drawing;

namespace EloBuddyAutoQueuer
{
	class LoginHandler
	{
		public static string LoginName = "";
		public static string Password = "";
		public static string ShownUser = "";
		public static string Error = "No Error";
		public static Image profilePicture;
		private static string URL = "https://www.elobuddy.net/api/auth.php?username=user&password=md5Pass";
        public static bool Login()
		{
			string BuiltUrl = URL.Replace("user&", LoginName + "&").Replace("md5Pass", Crypto.GetMD5Hash(Password));
			
			WebClient client = new WebClient();
			string response = client.DownloadString(BuiltUrl);
			dynamic parsedString = JObject.Parse(response);
			if ((bool)parsedString.success)
			{
				string tempstring = parsedString.user.avatar;
				ShownUser = parsedString.user.displayName;
				profilePicture = StaticUtils.LoadImage(tempstring.Replace("{", "").Replace("}", ""));
			}
			else
			{
				Error = parsedString.errorMsg;
			}
			
			return parsedString.success;
		}
	}
}
