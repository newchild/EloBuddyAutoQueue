using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace EloBuddyAutoQueuer
{
	/// <summary>
	///     Interaktionslogik für FirstRun.xaml
	/// </summary>
	public partial class FirstRun : Window
	{
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
			var ofd = new OpenFileDialog();
			ofd.ShowDialog();
			var path = Path.GetDirectoryName(ofd.FileName);
			var iniFile = new Ini("settings.ini");
			iniFile.WriteValue("EBLocation", path);
			iniFile.Save();
			StaticData.EBLocation = path;
			status |= Progress.part1;
			if ((status & Progress.part2) == Progress.part2)
				WindowHandler.Instance.CloseWindow(typeof (FirstRun));
		}

		private void EBLoc_SelectionChanged(object sender, RoutedEventArgs e)
		{
			Logging.Log(sender.ToString());
		}

		private void Finish_Copy_Click(object sender, RoutedEventArgs e)
		{
			Logging.Log(sender.ToString());
			var ofd = new OpenFileDialog();
			ofd.ShowDialog();
			var path = Path.GetDirectoryName(ofd.FileName);
			var iniFile = new Ini("settings.ini");
			iniFile.WriteValue("LoLLocation", path);
			iniFile.Save();
			StaticData.LoLLocation = path;
			status |= Progress.part1;
			if ((status & Progress.part1) == Progress.part1)
				WindowHandler.Instance.CloseWindow(typeof (FirstRun));
		}

		private enum Progress
		{
			part1 = 0x0,
			part2 = 0x1,
			unfinished = 0x2
		}
	}
}