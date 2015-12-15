using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;

namespace EloBuddyAutoQueuer
{
	internal class VersionHandler
	{
		public static string GameVersion
		{
			get
			{
				var dragonJSON = "";
				using (var client = new WebClient())
				{
					dragonJSON = client.DownloadString("http://ddragon.leagueoflegends.com/realms/na.js");
				}
				dragonJSON = dragonJSON.Replace("Riot.DDragon.m=", "").Replace(";", "");
				var serializer = new JavaScriptSerializer();
				var deserializedJSON = serializer.Deserialize<Dictionary<string, object>>(dragonJSON);
				var Version = (string) deserializedJSON["v"];
				Logging.Log("Game version: " + Version);
				return Version;
			}
		}

		private static void CopyFile(string from, string to)
		{
			var byteArray = File.ReadAllBytes(from);
			File.WriteAllBytes(to, byteArray);
		}

		public static bool CopyEBFiles()
		{
			try
			{
				CopyFile(Path.Combine(StaticData.EBLocation, "System", "EloBuddy.Core.dll"), "EloBuddy.Core.dll");
				CopyFile(Path.Combine(StaticData.EBLocation, "System", "EloBuddy.dll"), "EloBuddy.dll");
				CopyFile(Path.Combine(StaticData.EBLocation, "System", "SharpDX.dll"), "SharpDX.dll");
				CopyFile(Path.Combine(StaticData.EBLocation, "System", "EloBuddy.SDK.dll"), "EloBuddy.SDK.dll");
				CopyFile(Path.Combine(StaticData.EBLocation, "System", "EloBuddy.Sandbox.dll"), "EloBuddy.Sandbox.dll");
				CopyFile(Path.Combine(StaticData.EBLocation, "System", "EloBuddy.Networking.dll"), "EloBuddy.Networking.dll");
				return true;
			}
			catch (Exception e)
			{
				Logging.Error(e.ToString());
				return false;
			}
		}

		/*
		class PatchInfo
		{
			class Files
			{

			}
		}

		public static downloadEBCore(string FileHash)
		{
			WebClient x = new WebClient();
            string response = "";
			response = x.DownloadString(StaticData.EBDependencies);
			JavaScriptSerializer serializer = new JavaScriptSerializer();
			Dictionary<string, Dictionary<string, object>> deserializedJSON = serializer.Deserialize<Dictionary<string, Dictionary<string, object>>>(response);
			
		}
		*/
	}
}