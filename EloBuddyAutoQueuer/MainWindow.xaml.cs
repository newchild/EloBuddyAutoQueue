using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace EloBuddyAutoQueuer
{
	/// <summary>
	/// Interaktionslogik für MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		
        public MainWindow()
		{
			InitializeComponent();
			//image.Source = StaticUtils.GetImageStream(LoginHandler.profilePicture);
			Title = "EloBuddy AutoQueuer";
			Events.OnAddAccount += Events_OnAddAccount;
		}

		private void Events_OnAddAccount()
		{
			List<AccountInformation> accinf = new List<AccountInformation>();
			foreach (var user in Globals.accountList)
			{
				accinf.Add(new AccountInformation(user));
			}
			Dispatcher.Invoke((Action)delegate ()
			{
				dataGrid.ItemsSource = accinf;
			});
		}

		
		class AccountInformation
		{
			string summonerName;
			string Level;
			public AccountInformation(Account acc)
			{
				summonerName = acc.getSummonerName();
				Level = acc.getLevel().ToString();
			}
		}

		private void newAccountButton_Click(object sender, RoutedEventArgs e)
		{
			WindowHandler._Instance.ShowWindow(typeof(AddAccountWindow));
		}

        private void team_Click(object sender, RoutedEventArgs e)
        {
            var teamWindow = new TeamManage();
            teamWindow.Show();
        }
    }
}
