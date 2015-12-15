namespace EloBuddyAutoQueuer
{
	internal class Events
	{
		public delegate void AddAccount();

		public delegate void ReceiveMessage(Account sender, ReceivedMessageArgs args);

		private static Events _Instance;

		public static Events Instance
		{
			get
			{
				if (_Instance == null)
					_Instance = new Events();
				return _Instance;
			}
		}

		public event AddAccount OnAddAccount;

		public void InvokeAddAcc()
		{
			OnAddAccount();
		}

		public event ReceiveMessage onReceiveMessage;

		public void InvokeOnReceiveMessage(Account sender, ReceivedMessageArgs args)
		{
			onReceiveMessage(sender, args);
		}

		public class ReceivedMessageArgs
		{
			public object Message;
		}
	}
}