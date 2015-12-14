using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EloBuddyAutoQueuer
{
	class Events
	{
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
		
		public class ReceivedMessageArgs
		{
			public object Message;
		}

		public delegate void AddAccount();
		public event AddAccount OnAddAccount;
		public void InvokeAddAcc()
		{
			OnAddAccount();
		}

		public delegate void ReceiveMessage(Account sender, ReceivedMessageArgs args);
		public event ReceiveMessage onReceiveMessage;
		public void InvokeOnReceiveMessage(Account sender, ReceivedMessageArgs args)
		{
			onReceiveMessage(sender, args);
		}
	}
}
