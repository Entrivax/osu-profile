using MahApps.Metro.Controls;
using Osu_Profile;
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

namespace osu_Profile
{
    /// <summary>
    /// Logique d'interaction pour RankingSelector.xaml
    /// </summary>
    public partial class RankingSelector : MetroWindow
    {
        public RankingSelector()
        {
            InitializeComponent();

            level.IsChecked = MainWindow.config.IniReadValue("User", "levelbox", "true") == "true";
            rankscore.IsChecked = MainWindow.config.IniReadValue("User", "rankscorebox", "true") == "true";
            totscore.IsChecked = MainWindow.config.IniReadValue("User", "totalscorebox", "true") == "true";
            rank.IsChecked = MainWindow.config.IniReadValue("User", "rankbox", "true") == "true";
            pp.IsChecked = MainWindow.config.IniReadValue("User", "ppbox", "true") == "true";
            accu.IsChecked = MainWindow.config.IniReadValue("User", "accubox", "true") == "true";
        }

        private void valid_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)this.Owner).UpdateRankingControls();
            this.Close();
        }

        private void level_Checked(object sender, RoutedEventArgs e)
        {
            MainWindow.config.IniWriteValue("User", "levelbox", "true");
        }

        private void rankscore_Checked(object sender, RoutedEventArgs e)
        {
            MainWindow.config.IniWriteValue("User", "rankscorebox", "true");
        }

        private void totscore_Checked(object sender, RoutedEventArgs e)
        {
            MainWindow.config.IniWriteValue("User", "totalscorebox", "true");
        }

        private void rank_Checked(object sender, RoutedEventArgs e)
        {
            MainWindow.config.IniWriteValue("User", "rankbox", "true");
        }

        private void pp_Checked(object sender, RoutedEventArgs e)
        {
            MainWindow.config.IniWriteValue("User", "ppbox", "true");
        }

        private void accu_Checked(object sender, RoutedEventArgs e)
        {
            MainWindow.config.IniWriteValue("User", "accubox", "true");
        }

        private void level_Unchecked(object sender, RoutedEventArgs e)
        {
            MainWindow.config.IniWriteValue("User", "levelbox", "false");
        }

        private void rankscore_Unchecked(object sender, RoutedEventArgs e)
        {
            MainWindow.config.IniWriteValue("User", "rankscorebox", "false");
        }

        private void totscore_Unchecked(object sender, RoutedEventArgs e)
        {
            MainWindow.config.IniWriteValue("User", "totalscorebox", "false");
        }

        private void rank_Unchecked(object sender, RoutedEventArgs e)
        {
            MainWindow.config.IniWriteValue("User", "rankbox", "false");
        }

        private void pp_Unchecked(object sender, RoutedEventArgs e)
        {
            MainWindow.config.IniWriteValue("User", "ppbox", "false");
        }

        private void accu_Unchecked(object sender, RoutedEventArgs e)
        {
            MainWindow.config.IniWriteValue("User", "accubox", "false");
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ((MainWindow)this.Owner).UpdateRankingControls();
        }
    }
}
