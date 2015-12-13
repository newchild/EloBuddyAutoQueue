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
			image.Source = StaticUtils.GetImageStream(LoginHandler.profilePicture);
			label.Content = LoginHandler.ShownUser;
			Title = "EloBuddy AutoQueuer";
		}

		private void newAccountButton_Click(object sender, RoutedEventArgs e)
		{
			WindowHandler._Instance.ShowWindow(typeof(AddAccountWindow));
		}
	}
}
