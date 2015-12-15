using System;
using System.Windows;
using LoLLauncher;

namespace EloBuddyAutoQueuer
{
	/// <summary>
	///     Interaktionslogik für AddAccountWindow.xaml
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
			var username = textBox.Text;
			var password = textBox1.Password;
			var value = comboBox.SelectedItem.ToString().Replace("System.Windows.Controls.ComboBoxItem: ", "");
			Logging.Log(value);
			var region = (Region) Enum.Parse(typeof (Region), value);
			var acc = new Account(username, password, region);
			acc.Login();
			Events.Instance.InvokeAddAcc();
			WindowHandler.Instance.CloseWindow(typeof (AddAccountWindow));
		}
	}
}