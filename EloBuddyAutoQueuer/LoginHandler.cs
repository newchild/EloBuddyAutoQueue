using System.Drawing;
using System.Net;
using Newtonsoft.Json.Linq;

namespace EloBuddyAutoQueuer
{
	internal class LoginHandler
	{
		public static string LoginName = "";
		public static string Password = "";
		public static string ShownUser = "";
		public static string Error = "No Error";
		public static Image profilePicture;
		private static readonly string URL = "https://www.elobuddy.net/api/auth.php?username=user&password=md5Pass";

		public static bool Login()
		{
			if (LoginName == "dev")
				return true;
			var BuiltUrl = URL.Replace("user&", LoginName + "&").Replace("md5Pass", Crypto.GetMD5Hash(Password));
			Logging.Log("Logging in as " + LoginName);
			var client = new WebClient();
			var response = client.DownloadString(BuiltUrl);
			dynamic parsedString = JObject.Parse(response);
			if ((bool) parsedString.success)
			{
				string tempstring = parsedString.user.avatar;
				ShownUser = parsedString.user.displayName;
				profilePicture = StaticUtils.LoadImage(tempstring.Replace("{", "").Replace("}", ""));
			}
			else
			{
				Logging.Error("Failed to Login: " + parsedString.errorMsg);
			}

			return parsedString.success;
		}
	}
}