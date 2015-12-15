using System;
using LoLLauncher;
using LoLLauncher.RiotObjects.Platform.Catalog.Champion;
using LoLLauncher.RiotObjects.Platform.Clientfacade.Domain;
using LoLLauncher.RiotObjects.Platform.Matchmaking;
using LoLLauncher.RiotObjects.Platform.Statistics;

namespace EloBuddyAutoQueuer
{
	public class Account
	{
		private bool _Connected;
		private readonly LoLConnection _Connection;
		private Status _curentStatus;
		private SearchingForMatchNotification _GameSearchNotification;
		private bool _inQueue;
		private bool _LoggedIn;
		private LoginDataPacket _LoginPacket;
		private int _LoginQueueCount;
		private readonly string _Password;
		private QueueTypes _QueueType;
		private readonly Region _Region;
		private readonly string _Username;
		public bool inChampSelect;
		public bool QueuePop;
		public bool ready;

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
			//Naming of this one is wrong, it actually checks if it is the first tick of CHAMP_SELECT
			inChampSelect = true;
			QueuePop = false;
		}

		public ChampionDTO[] champions { get; private set; }

		public LoLConnection getConnectInfo()
		{
			return _Connection;
		}

		private void _Connection_OnMessageReceived(object sender, object message)
		{
			Events.Instance.InvokeOnReceiveMessage(this, new Events.ReceivedMessageArgs {Message = message});
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
			Events.Instance.InvokeOnReceiveMessage(this, new Events.ReceivedMessageArgs {Message = new EndOfGameStats()});
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