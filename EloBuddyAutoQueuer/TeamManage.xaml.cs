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
    /// Interaction logic for TeamManage.xaml
    /// </summary>
    public partial class TeamManage : Window
    {
        public TeamManage()
        {
            InitializeComponent();

            Title = "EloBuddy TeamQueuer";
            foreach (var item in Globals.accountList)
            {
                var listbox = new ComboBox();
                var champsNames = item.champions.Select(x => x.DisplayName);
                listbox.Items.Add(champsNames);
                listbox.SelectionChanged += Listbox_SelectionChanged;
                var obj = new ItemRow()
                {
                    Name = item.getSummonerName(),
                    Level = item.getLevel(),
                    ComboList = item.champions,
                    CheckBox = item.ready
                };
                dataGrid.Items.Add(obj);
            }
        }

        private void Listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }
    }

}
