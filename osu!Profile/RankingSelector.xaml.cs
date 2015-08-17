using MahApps.Metro.Controls;
using Osu_Profile;
using System.Windows;

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
            playcount.IsChecked = MainWindow.config.IniReadValue("User", "playcountbox", "true") == "true";
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

        private void playcount_Checked(object sender, RoutedEventArgs e)
        {
            MainWindow.config.IniWriteValue("User", "playcountbox", "true");
        }

        private void playcount_Unchecked(object sender, RoutedEventArgs e)
        {
            MainWindow.config.IniWriteValue("User", "playcountbox", "false");
        }

        private void topPP_Checked(object sender, RoutedEventArgs e)
        {
            MainWindow.config.IniWriteValue("User", "topPPbox", "true");
        }

        private void topPP_Unchecked(object sender, RoutedEventArgs e)
        {
            MainWindow.config.IniWriteValue("User", "topPPbox", "false");
        }
    }
}
