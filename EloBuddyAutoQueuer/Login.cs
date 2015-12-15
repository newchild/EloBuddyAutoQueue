using System;
using System.IO;
using System.Windows;

namespace EloBuddyAutoQueuer
{
	/// <summary>
	///     Interaktionslogik für MainWindow.xaml
	/// </summary>
	public partial class Login : Window
	{
		public Login()
		{
			InitializeComponent();
			Title = "Log In";
			WindowHandler.Instance.setLogInWindow(this);
			WindowHandler.Instance.ShowWindow(typeof (Logger));
			MessageHandler.Setup();
			if (!File.Exists("settings.ini"))
			{
				WindowHandler.Instance.ShowWindow(typeof (FirstRun));
				//WindowHandler.Instance.CloseWindow(typeof(Login));
			}
			else
			{
				var iniFile = new Ini("settings.ini");
				StaticData.EBLocation = iniFile.GetValue("EBLocation");
				StaticData.LoLLocation = iniFile.GetValue("LoLLocation");
			}
		}

		private void LogIn_Click(object sender, RoutedEventArgs e)
		{
			LoginHandler.LoginName = UserName.Text;
			LoginHandler.Password = Password.Password;
			if (!LoginHandler.Login()) return;
			Logging.Log("Patching...");
			try
			{
				TempPatcher.Patch();
				Logging.Log("Patched successfully");
			}
			catch (Exception er)
			{
				Logging.Warning("Error occured while patching. This can lead to bugs ingame, but it can also just work fine");
				Logging.Error(er.ToString());
			}


			WindowHandler.Instance.ShowWindow(typeof (MainWindow));
			WindowHandler.Instance.CloseWindow(typeof (Login));
		}
	}
}