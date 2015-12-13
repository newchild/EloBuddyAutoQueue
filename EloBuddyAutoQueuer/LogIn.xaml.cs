using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EloBuddyAutoQueuer
{
	/// <summary>
	/// Interaktionslogik für MainWindow.xaml
	/// </summary>
	public partial class Login : Window
	{
		public Login()
		{
			InitializeComponent();
			Title = "Log In";
			WindowHandler.Instance.setLogInWindow(this);
			WindowHandler.Instance.ShowWindow(typeof(Logger));
			if (!File.Exists("settings.ini")){
				WindowHandler.Instance.ShowWindow(typeof(FirstRun));
				//WindowHandler.Instance.CloseWindow(typeof(Login));
			}
			else
			{
				Ini iniFile = new Ini("settings.ini");
				StaticData.EBLocation = iniFile.GetValue("EBLocation");
			}
        }

		private void LogIn_Click(object sender, RoutedEventArgs e)
		{
			LoginHandler.LoginName = UserName.Text;
			LoginHandler.Password = Password.Password;
			if (LoginHandler.Login())
			{
				Logging.Log("Patching...");
				try
				{
					TempPatcher.Patch();
					Logging.Log("Patched successfully");
				}
				catch(Exception er)
				{
					Logging.Warning("Error occured while patching. This can lead to bugs ingame, but it can also just work fine");
					Logging.Error(er.ToString());
				}
				

				WindowHandler.Instance.ShowWindow(typeof(MainWindow));
				WindowHandler.Instance.CloseWindow(typeof(Login));
			}
		}
	}
}
