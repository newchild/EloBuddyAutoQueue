using PVPNetConnect;
using PVPNetConnect.RiotObjects.Platform.Catalog.Champion;
using PVPNetConnect.RiotObjects.Platform.Clientfacade.Domain;
using PVPNetConnect.RiotObjects.Platform.Game;
using PVPNetConnect.RiotObjects.Platform.Game.Message;
using PVPNetConnect.RiotObjects.Platform.Matchmaking;
using PVPNetConnect.RiotObjects.Platform.Statistics;
using PVPNetConnect.RiotObjects;
using PVPNetConnect.RiotObjects.Leagues.Pojo;
using PVPNetConnect.RiotObjects.Platform.Game.Practice;
using PVPNetConnect.RiotObjects.Platform.Harassment;
using PVPNetConnect.RiotObjects.Platform.Leagues.Client.Dto;
using PVPNetConnect.RiotObjects.Platform.Login;
using PVPNetConnect.RiotObjects.Platform.Reroll.Pojo;
using PVPNetConnect.RiotObjects.Platform.Statistics.Team;
using PVPNetConnect.RiotObjects.Platform.Summoner;
using PVPNetConnect.RiotObjects.Platform.Summoner.Boost;
using PVPNetConnect.RiotObjects.Platform.Summoner.Masterybook;
using PVPNetConnect.RiotObjects.Platform.Summoner.Runes;
using PVPNetConnect.RiotObjects.Platform.Summoner.Spellbook;
using PVPNetConnect.RiotObjects.Team;
using PVPNetConnect.RiotObjects.Team.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EloBuddyAutoQueuer
{
	class Account
	{
		private string _Username;
		private LoginDataPacket _LoginPacket;
		private string _Password;
		private Region _Region;
		private PVPNetConnection _Connection;
		private int _LoginQueueCount;
		private bool _Connected;
		private bool _LoggedIn;
		private QueueType _QueueType;
		private bool _inQueue;
		private Status _curentStatus;
		private SearchingForMatchNotification _GameSearchNotification;

		
				
	

		public Account(string Username, string Password, Region region, QueueType queue = QueueType.Bot)
		{
			_curentStatus = Status.Disconnected;
			_inQueue = false;
			_QueueType = queue;
			_Connected = false;
			_Username = Username;
			_Password = Password;
			_Region = region;
			_Connection = new PVPNetConnection();
			_Connection.OnConnect += _connection_OnConnect;
			_Connection.OnLogin += _connection_OnLogin;
			_Connection.OnLoginQueueUpdate += _connection_OnLoginQueueUpdate;
			_Connection.OnError += _connection_OnError;
			_Connection.OnDisconnect += _connection_OnDisconnect;
			_Connection.OnMessageReceived += _Connection_OnMessageReceived;
		}

		private async void _Connection_OnMessageReceived(object sender, object message)
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

		public async void Login()
		{
			Logging.Log("Connecting...");
			_Connection.Connect(_Username, _Password, _Region, StaticData.GameVersion);
			
			
		}
	}
}
