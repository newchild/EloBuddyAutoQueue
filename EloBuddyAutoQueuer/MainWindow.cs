using System.Collections.Generic;
using System.Windows;

namespace EloBuddyAutoQueuer
{
	/// <summary>
	///     Interaktionslogik für MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			image.Source = StaticUtils.GetImageStream(LoginHandler.profilePicture);
			label.Content = "Welcome back, " + LoginHandler.ShownUser;
			Title = "EloBuddy AutoQueuer";
			Events.Instance.OnAddAccount += Events_OnAddAccount;
		}

		private void Events_OnAddAccount()
		{
			var accinf = new List<AccountInformation>();
			foreach (var user in Globals.accountList)
			{
				accinf.Add(new AccountInformation(user));
			}
			//Dispatcher.Invoke(delegate { dataGrid.ItemsSource = accinf; });
		}

		private void newAccountButton_Click(object sender, RoutedEventArgs e)
		{
			WindowHandler._Instance.ShowWindow(typeof (AddAccountWindow));
		}

		private void team_Click(object sender, RoutedEventArgs e)
		{
			var teamWindow = new TeamManage();
			teamWindow.Show();
		}


		private class AccountInformation
		{
			private string Level;
			private string summonerName;

			public AccountInformation(Account acc)
			{
				summonerName = acc.getSummonerName();
				Level = acc.getLevel().ToString();
			}
		}
	}
}