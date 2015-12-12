using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Script.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EloBuddyAutoQueuer
{
	class StaticData
	{
		public static string GameVersion
		{
			get
			{
				string dragonJSON = "";
				using (WebClient client = new WebClient())
				{
					dragonJSON = client.DownloadString("http://ddragon.leagueoflegends.com/realms/na.js");
				}
				dragonJSON = dragonJSON.Replace("Riot.DDragon.m=", "").Replace(";", "");
				JavaScriptSerializer serializer = new JavaScriptSerializer();
				Dictionary<string, object> deserializedJSON = serializer.Deserialize<Dictionary<string, object>>(dragonJSON);
				string Version = (string)deserializedJSON["v"];
				Logging.Log("Game version: " + Version);
				return Version;

			}
		}
	}
}
