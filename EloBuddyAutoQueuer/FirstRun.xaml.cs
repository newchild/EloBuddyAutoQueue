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
		enum Progress
		{
			part1 = 0x0,
			part2 = 0x1,
			unfinished = 0x2
		}

		private Progress status;
		public FirstRun()
		{
			InitializeComponent();
			File.Create("settings.ini");
			status = Progress.unfinished;
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
			status |= Progress.part1;
			if((status & Progress.part2) == Progress.part2)
				WindowHandler.Instance.CloseWindow(typeof(FirstRun));
		}

		private void EBLoc_SelectionChanged(object sender, RoutedEventArgs e)
		{
			Logging.Log(sender.ToString());
		}

		private void Finish_Copy_Click(object sender, RoutedEventArgs e)
		{
			Logging.Log(sender.ToString());
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.ShowDialog();
			string path = System.IO.Path.GetDirectoryName(ofd.FileName);
			Ini iniFile = new Ini("settings.ini");
			iniFile.WriteValue("LoLLocation", path);
			iniFile.Save();
			StaticData.LoLLocation = path;
			status |= Progress.part1;
			if ((status & Progress.part1) == Progress.part1)
				WindowHandler.Instance.CloseWindow(typeof(FirstRun));
		}
	}
}
