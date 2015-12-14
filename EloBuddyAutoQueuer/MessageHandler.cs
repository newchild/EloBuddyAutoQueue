using LoLLauncher;
using LoLLauncher.RiotObjects.Platform.Game;
using LoLLauncher.RiotObjects.Platform.Matchmaking;
using LoLLauncher.RiotObjects.Platform.Statistics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EloBuddyAutoQueuer
{
	class MessageHandler
	{
		private static List<int> possibleHeroes = new List<int>() { (int)Champ.Ashe, (int)Champ.Caitlyn, (int)Champ.Cassiopeia, (int)Champ.Ezreal};
		private static MessageHandler _Instance;
		public static void Setup()
		{
				if (_Instance == null)
					_Instance = new MessageHandler();
		}
		private MessageHandler()
		{
			Events.Instance.onReceiveMessage += Events_onReceiveMessage;
			Logging.Log("Set up event handler");
		}

		private async void Events_onReceiveMessage(Account sender, Events.ReceivedMessageArgs args)
		{
			Logging.Log(args.Message.ToString());
			if(args.Message is GameDTO)
			{
				var gameDTO = args.Message as GameDTO;
				Logging.Log(gameDTO.GameState);
				switch (gameDTO.GameState)
				{
					case "CHAMP_SELECT":
						if (sender.inChampSelect)
						{
							Random distributor = new Random();

							sender.inChampSelect = false;
							await sender.getConnectInfo().SetClientReceivedGameMessage(gameDTO.Id, "CHAMP_SELECT_CLIENT");
							await sender.getConnectInfo().SelectSpells((int)SummonerSpells.Heal, (int)SummonerSpells.Ghost);
							var heroes = await sender.getConnectInfo().GetAvailableChampions();
							foreach(var hero in heroes)
							{
								if(possibleHeroes.Contains(hero.ChampionId) && !hero.Banned)
								{
									await sender.getConnectInfo().SelectChampion(hero.ChampionId);
									Logging.Log("Selected " + ((Champ)hero.ChampionId).ToString());
									return;
								}
							}
							await sender.getConnectInfo().ChampionSelectCompleted();

                        }
						break;
					case "TERMINATED":
						sender.QueuePop = false;
						break;
					case "JOINING_CHAMP_SELECT":
						if(!sender.QueuePop && gameDTO.StatusOfParticipants.Contains(""))
						{
							sender.QueuePop = true;
							await sender.getConnectInfo().AcceptPoppedGame(true);
						}
						break;
					case "POST_CHAMP_SELECT":
						sender.inChampSelect = false;
						break;
				}
			}

			if(args.Message is EndOfGameStats)
			{
				var queues = await sender.getConnectInfo().GetAvailableQueues();
				var botQueues = queues.Where(x => x.Type.Contains("BOT"));
				var botqueue = botQueues.ToArray()[2];
				var id = botqueue.Id;
				var parameters = new MatchMakerParams
				{
					QueueIds = new[]
							{

							Convert.ToInt32(QueueTypes.MEDIUM_BOT)
						},
					BotDifficulty = "MEDIUM"
				};
				Logging.Log("Queueing up");
				await sender.getConnectInfo().AttachToQueue(parameters);
			}

			if(args.Message is PlayerCredentialsDto)
			{
				Logging.Log("Launching Game...");
				string str = Enumerable.Last<string>((IEnumerable<string>)Enumerable.OrderBy<string, DateTime>(Directory.EnumerateDirectories((StaticData.LoLLocation ?? "") + "\\RADS\\solutions\\lol_game_client_sln\\releases\\"), (Func<string, DateTime>)(f => new DirectoryInfo(f).CreationTime))) + "\\deploy\\";
				PlayerCredentialsDto credentials = args.Message as PlayerCredentialsDto;
				System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
				startInfo.CreateNoWindow = false;
				startInfo.WorkingDirectory = str;
				startInfo.FileName = "League of Legends.exe";
				startInfo.Arguments = "\"8394\" \"LoLLauncher.exe\" \"\" \"" + credentials.ServerIp + " " +
				credentials.ServerPort + " " + credentials.EncryptionKey + " " + credentials.SummonerId + "\"";
				Process.Start(startInfo);

			}
		}
	}
}
