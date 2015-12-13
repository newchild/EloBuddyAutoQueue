using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EloBuddyAutoQueuer
{
	class Events
	{
		public delegate void AddAccount();
		public static event AddAccount OnAddAccount;
		public static void InvokeOnAddAccount()
		{
			OnAddAccount();
		}
	}
}
