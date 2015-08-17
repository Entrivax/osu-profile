using Osu_Profile;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace osu_Profile
{
    /// <summary>
    /// Logique d'interaction pour Settings.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        #region Attributes
        protected IniFile config;
        protected int _numValue = 0;
        #endregion

        #region Constructor
        public Settings()
        {
            InitializeComponent();
            config = null;
        }
        #endregion

        #region Properties
        public IniFile ConfigFile
        {
            set
            {
                config = value;
            }
        }

        public string Username
        {
            set
            {
                userbox.Text = value;
            }
            get
            {
                return userbox.Text;
            }
        }

        public string APIKey
        {
            set
            {
                apibox.Password = value;
            }
            get
            {
                return apibox.Password;
            }
        }

        public string TimeToWait
        {
            set
            {
                if (!int.TryParse(value, out _numValue))
                {
                    _numValue = 5;
                }
                if (_numValue < 5)
                    _numValue = 5;
                txtNum.Text = _numValue.ToString();
                if (MainWindow.MWindow.loopupdate != null)
                    MainWindow.MWindow.loopupdate.setTimer(_numValue);
                config.IniWriteValue("User", "looptime", _numValue.ToString());
            }
            get
            {
                return txtNum.Text;
            }
        }

        public ItemCollection FileList
        {
            get
            {
                return filelist.Items;
            }
        }
        #endregion

        #region Handlers
        private void userbox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                e.Handled = true;
                Button_Click(sender, e);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.MWindow.Start(userbox.Text, apibox.Password))
                apibox.IsEnabled = false;
            else
                apibox.IsEnabled = true;
        }

        private void cmdUp_Click(object sender, RoutedEventArgs e)
        {
            TimeToWait = (_numValue + 1).ToString();
        }

        private void cmdDown_Click(object sender, RoutedEventArgs e)
        {
            TimeToWait = (_numValue - 1).ToString();
        }

        private void txtNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (MainWindow.MWindow != null)
                TimeToWait = txtNum.Text;
        }

        private void modelist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainWindow.mode != modelist.SelectedIndex)
            {
                MainWindow.mode = modelist.SelectedIndex;
                MainWindow.MWindow.Start(userbox.Text, apibox.Password);
                config.IniWriteValue("User", "mode", MainWindow.mode.ToString());
            }
        }

        private void modelist_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow.mode = int.Parse(config.IniReadValue("User", "mode", "0"));
            }
            catch (Exception) { }
            if (MainWindow.mode < 0) MainWindow.mode = 0; if (MainWindow.mode > 3) MainWindow.mode = 3;
            modelist.SelectedIndex = MainWindow.mode;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Username = config.IniReadValue("User", "LastUsername", "");
            APIKey = config.IniReadValue("User", "APIkey", "");
            TimeToWait = config.IniReadValue("User", "looptime", "5");
            alwaysontopcheck.IsChecked = config.IniReadValue("User", "topmost", "false") == "true";
            popupEachMap.IsChecked = config.IniReadValue("User", "popupEachMap", "false") == "true";
            popupPPUp.IsChecked = config.IniReadValue("User", "popupPP", "false") == "true";
            checkOnStart.IsChecked = config.IniReadValue("User", "checkOnStart", "false") == "true";
            startWithWindows.IsChecked = config.IniReadValue("User", "startWithWindows", "false") == "true";
            versiontext.Dispatcher.BeginInvoke(new Action(() =>
            {
                versiontext.Content = "Version " + Assembly.GetEntryAssembly().GetName().Version.ToString();
            }), DispatcherPriority.Background);

            if (config.IniReadValue("User", "checkOnStart", "false") == "true")
                if (MainWindow.MWindow.Start(userbox.Text, apibox.Password))
                    apibox.IsEnabled = false;
                else
                    apibox.IsEnabled = true;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            FilesWindow fwindow = new FilesWindow();
            fwindow.Owner = MainWindow.MWindow;
            fwindow.number = -1;
            fwindow.setlist(ref filelist);
            fwindow.Show();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            if (filelist.SelectedIndex >= 0)
            {
                FilesWindow fwindow = new FilesWindow();
                fwindow.Owner = MainWindow.MWindow;
                fwindow.number = filelist.SelectedIndex;
                fwindow.setlist(ref filelist);
                fwindow.file = MainWindow.files[filelist.SelectedIndex];
                fwindow.content = MainWindow.contents[filelist.SelectedIndex];
                fwindow.Show();
            }
        }

        private void remove_Click(object sender, RoutedEventArgs e)
        {
            if (filelist.SelectedIndex >= 0)
            {
                MainWindow.files.RemoveAt(filelist.SelectedIndex);
                MainWindow.contents.RemoveAt(filelist.SelectedIndex);
                config.IniWriteValue("User", "files", MainWindow.files.Count.ToString());

                filelist.Items.Clear();
                for (int i = 0; i < MainWindow.files.Count; i++)
                {
                    filelist.Items.Add(MainWindow.files[i]);
                }
            }
        }

        private void scoremodelist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainWindow.scoremode = scoremodelist.SelectedIndex;
            config.IniWriteValue("User", "scoremode", MainWindow.scoremode.ToString());
        }

        private void scoremodelist_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow.scoremode = int.Parse(config.IniReadValue("User", "scoremode", "0"));
            }
            catch (Exception) { }
            if (MainWindow.scoremode < 0) MainWindow.scoremode = 0; if (MainWindow.scoremode > 1) MainWindow.scoremode = 1;
            scoremodelist.SelectedIndex = MainWindow.scoremode;
        }

        private void rankingButton_Click(object sender, RoutedEventArgs e)
        {
            RankingSelector fwindow = new RankingSelector();
            fwindow.Owner = MainWindow.MWindow;
            fwindow.ShowDialog();
            MainWindow.MWindow.RankedScoreChangeBox.Dispatcher.Invoke(new Action(() =>
            {
                MainWindow.MWindow.UpdateRankingDisplay();
            }));
        }

        private void alwaysontopcheck_Unchecked(object sender, RoutedEventArgs e)
        {
            config.IniWriteValue("User", "topmost", "false");
            MainWindow.MWindow.Topmost = false;
        }

        private void alwaysontopcheck_Checked(object sender, RoutedEventArgs e)
        {
            config.IniWriteValue("User", "topmost", "true");
            MainWindow.MWindow.Topmost = true;
        }

        private void popupEachMap_Checked(object sender, RoutedEventArgs e)
        {
            config.IniWriteValue("User", "popupEachMap", "true");
        }

        private void popupEachMap_Unchecked(object sender, RoutedEventArgs e)
        {
            config.IniWriteValue("User", "popupEachMap", "false");
        }

        private void popupPP_Checked(object sender, RoutedEventArgs e)
        {
            config.IniWriteValue("User", "popupPP", "true");
        }

        private void popupPP_Unchecked(object sender, RoutedEventArgs e)
        {
            config.IniWriteValue("User", "popupPP", "false");
        }

        private void checkOnStart_Checked(object sender, RoutedEventArgs e)
        {
            config.IniWriteValue("User", "checkOnStart", "true");
        }

        private void checkOnStart_Unchecked(object sender, RoutedEventArgs e)
        {
            config.IniWriteValue("User", "checkOnStart", "false");
        }

        private void startWithWindows_Checked(object sender, RoutedEventArgs e)
        {
            config.IniWriteValue("User", "startWithWindows", "true");

            string startupShotcut = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "osu!profile.lnk");
            Console.WriteLine(startupShotcut);
            if (!File.Exists(startupShotcut))
            {
                osu_Profile.Shortcut.IShellLink link = (osu_Profile.Shortcut.IShellLink)new osu_Profile.Shortcut.ShellLink();

                link.SetDescription("osu!profile");
                link.SetPath(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

                IPersistFile file = (IPersistFile)link;
                file.Save(startupShotcut, false);
            }
        }

        private void startWithWindows_Unchecked(object sender, RoutedEventArgs e)
        {
            config.IniWriteValue("User", "startWithWindows", "false");

            string startupShotcut = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "osu!profile.lnk");
            if (File.Exists(startupShotcut))
            {
                File.Delete(startupShotcut);
            }
        }

        private void backgroundColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            Color SelectedColor = (Color)backgroundColor.SelectedColor;
            MainWindow.ChangeThemeColor(SelectedColor.R, SelectedColor.G, SelectedColor.B);
            config.IniWriteValue("User", "UI_red", SelectedColor.R.ToString());
            config.IniWriteValue("User", "UI_green", SelectedColor.G.ToString());
            config.IniWriteValue("User", "UI_blue", SelectedColor.B.ToString());
        }

        private void backgroundColor_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                byte r = byte.Parse(config.IniReadValue("User", "UI_red", "17"));
                byte g = byte.Parse(config.IniReadValue("User", "UI_green", "158"));
                byte b = byte.Parse(config.IniReadValue("User", "UI_blue", "218"));
                backgroundColor.SelectedColor = Color.FromRgb(r, g, b);
                MainWindow.ChangeThemeColor(r, g, b);
            }
            catch (Exception)
            {
                backgroundColor.SelectedColor = Color.FromRgb(0x11, 0x9E, 0xDA);
            }
        }
        #endregion

    }
}
