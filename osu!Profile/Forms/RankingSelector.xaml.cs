﻿using MahApps.Metro.Controls;
using System.Windows;

namespace osu_Profile.Forms
{
    /// <summary>
    /// Logique d'interaction pour RankingSelector.xaml
    /// </summary>
    public partial class RankingSelector : MetroWindow
    {
        #region Constructor
        public RankingSelector()
        {
            InitializeComponent();

            level.IsChecked = MainWindow.config.GetValue("User", "levelbox", "true") == "true";
            rankscore.IsChecked = MainWindow.config.GetValue("User", "rankscorebox", "true") == "true";
            totscore.IsChecked = MainWindow.config.GetValue("User", "totalscorebox", "true") == "true";
            rank.IsChecked = MainWindow.config.GetValue("User", "rankbox", "true") == "true";
            countryrank.IsChecked = MainWindow.config.GetValue("User", "countryrankbox", "true") == "true";
            pp.IsChecked = MainWindow.config.GetValue("User", "ppbox", "true") == "true";
            accu.IsChecked = MainWindow.config.GetValue("User", "accubox", "true") == "true";
            playcount.IsChecked = MainWindow.config.GetValue("User", "playcountbox", "true") == "true";
            topPP.IsChecked = MainWindow.config.GetValue("User", "topPPbox", "true") == "true";
            rankA.IsChecked = MainWindow.config.GetValue("User", "rankAbox", "true") == "true";
            rankS.IsChecked = MainWindow.config.GetValue("User", "rankSbox", "true") == "true";
            rankSH.IsChecked = MainWindow.config.GetValue("User", "rankSHbox", "true") == "true";
            rankSS.IsChecked = MainWindow.config.GetValue("User", "rankSSbox", "true") == "true";
            rankSSH.IsChecked = MainWindow.config.GetValue("User", "rankSSHbox", "true") == "true";
        }
        #endregion

        #region Handlers
        private void valid_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.config.SetValue("User", "levelbox", level.IsChecked ?? false ? "true" : "false");
            MainWindow.config.SetValue("User", "rankscorebox", rankscore.IsChecked ?? false ? "true" : "false");
            MainWindow.config.SetValue("User", "totalscorebox", totscore.IsChecked ?? false ? "true" : "false");
            MainWindow.config.SetValue("User", "rankbox", rank.IsChecked ?? false ? "true" : "false");
            MainWindow.config.SetValue("User", "countryrankbox", countryrank.IsChecked ?? false ? "true" : "false");
            MainWindow.config.SetValue("User", "ppbox", pp.IsChecked ?? false ? "true" : "false");
            MainWindow.config.SetValue("User", "accubox", accu.IsChecked ?? false ? "true" : "false");
            MainWindow.config.SetValue("User", "playcountbox", playcount.IsChecked ?? false ? "true" : "false");
            MainWindow.config.SetValue("User", "topPPbox", topPP.IsChecked ?? false ? "true" : "false");
            MainWindow.config.SetValue("User", "rankAbox", rankA.IsChecked ?? false ? "true" : "false");
            MainWindow.config.SetValue("User", "rankSbox", rankS.IsChecked ?? false ? "true" : "false");
            MainWindow.config.SetValue("User", "rankSHbox", rankSH.IsChecked ?? false ? "true" : "false");
            MainWindow.config.SetValue("User", "rankSSbox", rankSS.IsChecked ?? false ? "true" : "false");
            MainWindow.config.SetValue("User", "rankSSHbox", rankSSH.IsChecked ?? false ? "true" : "false");
            MainWindow.config.Export();
            ((MainWindow)this.Owner).UpdateRankingControls();
            this.Close();
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e) => ((MainWindow)this.Owner).UpdateRankingControls();
        #endregion
    }
}
