using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace EloBuddyAutoQueuer
{
	/// <summary>
	/// Interaktionslogik für FirstRun.xaml
	/// </summary>
	public partial class FirstRun : Window
	{
		private bool finished = false;
		public FirstRun()
		{
			InitializeComponent();
			File.Create("settings.ini");
		}

		private void Finish_Click(object sender, RoutedEventArgs e)
		{
			Logging.Log(sender.ToString());
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.ShowDialog();
			string path = System.IO.Path.GetDirectoryName(ofd.FileName);
			Ini iniFile = new Ini("settings.ini");
			iniFile.WriteValue("EBLocation", path);
			iniFile.Save();
			StaticData.EBLocation = path;
			finished = true;
			
			WindowHandler.Instance.CloseWindow(typeof(FirstRun));
		}

		private void EBLoc_SelectionChanged(object sender, RoutedEventArgs e)
		{
			Logging.Log(sender.ToString());
		}
	}
}
