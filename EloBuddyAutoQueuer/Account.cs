using LoLLauncher;
using LoLLauncher.RiotObjects.Platform.Catalog.Champion;
using LoLLauncher.RiotObjects.Platform.Clientfacade.Domain;
using LoLLauncher.RiotObjects.Platform.Game;
using LoLLauncher.RiotObjects.Platform.Game.Message;
using LoLLauncher.RiotObjects.Platform.Matchmaking;
using LoLLauncher.RiotObjects.Platform.Statistics;
using LoLLauncher.RiotObjects;
using LoLLauncher.RiotObjects.Leagues.Pojo;
using LoLLauncher.RiotObjects.Platform.Game.Practice;
using LoLLauncher.RiotObjects.Platform.Harassment;
using LoLLauncher.RiotObjects.Platform.Leagues.Client.Dto;
using LoLLauncher.RiotObjects.Platform.Login;
using LoLLauncher.RiotObjects.Platform.Reroll.Pojo;
using LoLLauncher.RiotObjects.Platform.Statistics.Team;
using LoLLauncher.RiotObjects.Platform.Summoner;
using LoLLauncher.RiotObjects.Platform.Summoner.Boost;
using LoLLauncher.RiotObjects.Platform.Summoner.Masterybook;
using LoLLauncher.RiotObjects.Platform.Summoner.Runes;
using LoLLauncher.RiotObjects.Platform.Summoner.Spellbook;
using LoLLauncher.RiotObjects.Team;
using LoLLauncher.RiotObjects.Team.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;

namespace EloBuddyAutoQueuer
{
	public class Account
	{
		private string _Username;
		private LoginDataPacket _LoginPacket;
		private string _Password;
		private Region _Region;
		private LoLConnection _Connection;
		private int _LoginQueueCount;
		private bool _Connected;
		private bool _LoggedIn;
		private QueueTypes _QueueType;
		private bool _inQueue;
		private Status _curentStatus;
		private SearchingForMatchNotification _GameSearchNotification;
        public bool ready;

        public ChampionDTO[] champions { get; private set; }

        public Account(string Username, string Password, Region region, QueueTypes queue = QueueTypes.MEDIUM_BOT)
		{
			_curentStatus = Status.Disconnected;
			_inQueue = false;
			_QueueType = queue;
			_Connected = false;
			_Username = Username;
			_Password = Password;
			_Region = region;
			_Connection = new LoLConnection();
			_Connection.OnConnect += _connection_OnConnect;
			_Connection.OnLogin += _connection_OnLogin;
			_Connection.OnLoginQueueUpdate += _connection_OnLoginQueueUpdate;
			_Connection.OnError += _connection_OnError;
			_Connection.OnDisconnect += _connection_OnDisconnect;
			_Connection.OnMessageReceived += _Connection_OnMessageReceived;
		}

		private void _Connection_OnMessageReceived(object sender, object message)
		{
			Logging.Log("Message received: " + message.ToString());
		}

		private void _connection_OnDisconnect(object sender, EventArgs e)
		{
			Logging.Warning(_Username + " disconnected");
			_Connected = false;
			_LoggedIn = false;
			_curentStatus = Status.Disconnected;
		}

		private void _connection_OnError(object sender, Error error)
		{
			Logging.Error(error.Message);
		}

		private void _connection_OnLoginQueueUpdate(object sender, int positionInLine)
		{
			_curentStatus = Status.InLoginQueue;
			_LoginQueueCount = positionInLine;
		}

		private async void _connection_OnLogin(object sender, string username, string ipAddress)
		{
			Logging.Log(username + " logged in");
			_LoginPacket = await _Connection.GetLoginDataPacketForUser();
			var player = await _Connection.CreatePlayer();
			await _Connection.Subscribe("bc", _LoginPacket.AllSummonerData.Summoner.AcctId);
			await _Connection.Subscribe("cn", _LoginPacket.AllSummonerData.Summoner.AcctId);
			await _Connection.Subscribe("gn", _LoginPacket.AllSummonerData.Summoner.AcctId);
			Logging.Log("Subscribed to Notifications");
			_LoggedIn = true;
			_curentStatus = Status.LoggedIn;
            champions = await _Connection.GetAvailableChampions();
            Globals.accountList.Add(this);
            /*
            var queues = await _Connection.GetAvailableQueues();
            var botQueues = queues.Where(x => x.Type.Contains("BOT"));
            var botqueue = botQueues.ToArray()[2];
            var id = botqueue.Id;
            var parameters = new MatchMakerParams
            {
                QueueIds = new[]
                        {
            
                            Convert.ToInt32(id)
                        },
                BotDifficulty = "EASY"
            };
            
            var asdf = await _Connection.AttachToQueue(parameters);
            */
        }

		public int getLevel()
		{
			if (_LoginPacket == null)
				return -1;
			return Convert.ToInt32(_LoginPacket.AllSummonerData.SummonerLevel.Level);
		}

		public string getSummonerName()
		{
			if (_LoginPacket == null)
				return "Connecting...";
			return _LoginPacket.AllSummonerData.Summoner.Name;
		}

		private void _connection_OnConnect(object sender, EventArgs e)
		{
			Logging.Log(_Username + " connected");
			_Connected = true;
        }

		public bool isLoggedIn()
		{
			return _LoggedIn;
		}

		public bool isConnected()
		{
			return _Connected;
		}

		public int LogInQueuePos()
		{
			return _LoginQueueCount;
		}

		public void Login()
		{
			Logging.Log("Connecting...");
			_Connection.Connect(_Username, _Password, _Region, VersionHandler.GameVersion);
		}
	}
}
