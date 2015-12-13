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
		private Logger _LoggerWindow;
		private Login _LoginWindow;
		private AddAccountWindow _AddAccountWindow;
		private FirstRun _FirstRunWindow;
		

		

		public void setLogInWindow(Login Window)
		{
			_LoginWindow = Window;
			
		}

		

		public void CloseWindow(Type WindowType)
		{
			if (_LoggerWindow != null)
			{
				Logging.Log("Closing Window of Type " + WindowType.ToString());
			}
			if (WindowType == null)
				return;
			if(WindowType == typeof(MainWindow))
			{
				_MainWindow.Close();
				_MainWindow = null;
			}

			if (WindowType == typeof(FirstRun))
			{
				_FirstRunWindow.Close();
				_FirstRunWindow = null;
			}

			if (WindowType == typeof(Logger))
			{
				_LoggerWindow.Close();
				_LoggerWindow = null;
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

		public bool isAnyWindowOpen()
		{
			if (_LoginWindow == null && _MainWindow == null && _AddAccountWindow == null)
				return false;
			return true;
		}

		public void ShowWindow(Type WindowType)
		{
			if(_LoggerWindow != null)
			{
				Logging.Log("Opening Window of Type " + WindowType.ToString());
			}
			if (WindowType == null)
				return;
			if (WindowType == typeof(MainWindow))
			{
				_MainWindow = new MainWindow();
				_MainWindow.Show();
			}
			if (WindowType == typeof(FirstRun))
			{
				_FirstRunWindow = new FirstRun();
				_FirstRunWindow.ShowDialog();
			}
			if (WindowType == typeof(Login))
			{
				_LoginWindow = new Login();
				_LoginWindow.Show();
			}
			if (WindowType == typeof(AddAccountWindow))
			{
				_AddAccountWindow = new AddAccountWindow();
				_AddAccountWindow.ShowDialog();
			}
			if(WindowType == typeof(Logger))
			{
				_LoggerWindow = new Logger();
				_LoggerWindow.Show();
			}
		}

		public Logger getLoggerInstance()
		{
			return _LoggerWindow;
		}
	}
}
