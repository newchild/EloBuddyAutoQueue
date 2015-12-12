using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EloBuddyAutoQueuer
{
	class WindowHandler
	{
		
		public static WindowHandler _Instance;
		public static WindowHandler Instance
		{
			get
			{
				if(_Instance == null)
				{
					_Instance = new WindowHandler();
				}
				return _Instance;
			}
		}
		private MainWindow _MainWindow;
		private Login _LoginWindow;
		private AddAccountWindow _AddAccountWindow;
		public WindowHandler()
		{
		}

		

		public void setLogInWindow(Login Window)
		{
			_LoginWindow = Window;
		}

		public void CloseWindow(Type WindowType)
		{
			
			if (WindowType == null)
				return;
			if(WindowType == typeof(MainWindow))
			{
				_MainWindow.Close();
				_MainWindow = null;
			}
			if (WindowType == typeof(Login))
			{
				_LoginWindow.Close();
				_LoginWindow = null;
			}
			if(WindowType == typeof(AddAccountWindow))
			{
				_AddAccountWindow.Close();
				_AddAccountWindow = null;
			}
		}

		public void ShowWindow(Type WindowType)
		{
			
			if (WindowType == null)
				return;
			if (WindowType == typeof(MainWindow))
			{
				_MainWindow = new MainWindow();
				_MainWindow.Show();
			}
			if (WindowType == typeof(Login))
			{
				_LoginWindow = new Login();
				_LoginWindow.Show();
			}
			if (WindowType == typeof(AddAccountWindow))
			{
				_AddAccountWindow = new AddAccountWindow();
				_AddAccountWindow.Show();
			}
		}
	}
}
