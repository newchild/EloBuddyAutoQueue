using PVPNetConnect;
using PVPNetConnect.RiotObjects.Platform.Matchmaking;
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

		public async void Pulse()
		{
			if(isConnected() && isLoggedIn())
			{
				if (_inQueue)
				{
					return;
				}

				_GameSearchNotification =  await _Connection.AttachToQueue(new MatchMakerParams()
				{
					QueueIds = new int[] { (int)_QueueType },
					BotDifficulty = "MEDIUM"
				});
				if(_GameSearchNotification.PlayerJoinFailures == null)
				{
					_curentStatus = Status.InQueue;
				}
            }
		}

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

		private void _Connection_OnMessageReceived(object sender, object message)
		{
			MessageBox.Show(message.GetType().ToString());
		}

		private void _connection_OnDisconnect(object sender, EventArgs e)
		{
			_Connected = false;
			_LoggedIn = false;
			_curentStatus = Status.Disconnected;
		}

		private void _connection_OnError(object sender, Error error)
		{
			throw new Exception(error.Message);
		}

		private void _connection_OnLoginQueueUpdate(object sender, int positionInLine)
		{
			_curentStatus = Status.InLoginQueue;
			_LoginQueueCount = positionInLine;
		}

		private void _connection_OnLogin(object sender, string username, string ipAddress)
		{
			_LoggedIn = true;
			_curentStatus = Status.LoggedIn;
		}

		private void _connection_OnConnect(object sender, EventArgs e)
		{
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
			_Connection.Connect(_Username, _Password, _Region, StaticData.GameVersion);
		}
	}
}
