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
	/// Interaktionslogik für AddAccountWindow.xaml
	/// </summary>
	public partial class AddAccountWindow : Window
	{
		public AddAccountWindow()
		{
			InitializeComponent();
			Title = "Add Account";
		}

		

		private void button_Click(object sender, RoutedEventArgs e)
		{
			string username = textBox.Text;
			string password = textBox1.Password;
			string value = comboBox.SelectedItem.ToString().Replace("System.Windows.Controls.ComboBoxItem: ", "");
			Logging.Log(value);
            LoLLauncher.Region region = (LoLLauncher.Region)Enum.Parse(typeof(LoLLauncher.Region), value);
            var acc = new Account(username, password, region);
			acc.Login();
			Events.Instance.InvokeAddAcc();
			WindowHandler.Instance.CloseWindow(typeof(AddAccountWindow));
		}
	}
}
