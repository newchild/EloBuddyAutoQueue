using System;
using System.Collections.Generic;
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
			
        }

		private void LogIn_Click(object sender, RoutedEventArgs e)
		{
			LoginHandler.LoginName = UserName.Text;
			LoginHandler.PasswordHash = Password.Text;
			if (LoginHandler.Login())
			{
				WindowHandler.Instance.setLogInWindow(this);
				WindowHandler.Instance.ShowWindow(typeof(MainWindow));
				WindowHandler.Instance.CloseWindow(typeof(Login));
				
			}
		}
	}
}
