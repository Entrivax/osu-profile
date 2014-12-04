using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Diagnostics;
using System.Windows.Threading;
using System.Collections.Generic;
using osu_Profile;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Osu_Profile
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public static Osu_Player osu_player;
        public static IniFile config = new IniFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\config.ini");
        private LoopUpdate loopupdate;

        public static List<String> files = new List<String>(), contents = new List<String>();

        public static int mode = 0;
        public static int scoremode = 0;

        public MainWindow()
        {
            InitializeComponent();
            _numValue = 5;
            versiontext.Content = "Version " + Assembly.GetEntryAssembly().GetName().Version.ToString();
            loopupdate = new LoopUpdate(ref rankedbox, ref levelbox, ref totalbox, ref rankbox, ref ppbox, ref accuracybox, ref ppchangebox, ref totalscorechangebox, ref rankedscorechangebox, ref levelchangebox, ref rankchangebox, ref accuracychangebox, ref playedbox, ref _numValue);
            loopthread = new Thread(new ThreadStart(loopupdate.loop));
            loopthread.IsBackground = true;
            loopthread2 = new Thread(new ThreadStart(loopupdate.loop2));
            loopthread2.IsBackground = true;

            versioncheck = new Thread(checkversion);
            versioncheck.IsBackground = true;
            versioncheck.Start();

            beatmapscheck.IsChecked = config.IniReadValue("User", "beatmaps", "false") == "true";
            playedbox.IsEnabled = config.IniReadValue("User", "beatmaps", "false") == "true";
        }

        public static void checkversion()
        {

            using (WebClient client = new WebClient())
            {
                try
                {
                    String version = Regex.Split(client.DownloadString("http://entrivax.fr/osu!p/changelog.txt"), @"\r?\n|\r")[0].Trim().Substring(1);
                    String[] remoteversionnumbers = version.Split('.');
                    String[] actualversionnumbers = Assembly.GetEntryAssembly().GetName().Version.ToString().Split('.');
                    if (int.Parse(remoteversionnumbers[0]) > int.Parse(actualversionnumbers[0]))
                    {
                        if (Application.Current.Dispatcher.CheckAccess())
                        {
                            MessageBoxResult result = MessageBox.Show(Application.Current.MainWindow, "New version available, go download it?", "New version available", MessageBoxButton.YesNo);
                            if (result == MessageBoxResult.Yes)
                            {
                                Process.Start("http://entrivax.fr/osu!p");
                            }
                        }
                        else
                        {
                            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                            {
                                MessageBoxResult result = MessageBox.Show(Application.Current.MainWindow, "New version available, go download it?", "New version available", MessageBoxButton.YesNo);
                                if (result == MessageBoxResult.Yes)
                                {
                                    Process.Start("http://entrivax.fr/osu!p");
                                }
                            }));
                        }
                        return;
                    }
                    else if (int.Parse(remoteversionnumbers[0]) < int.Parse(actualversionnumbers[0]))
                    {
                        return;
                    }

                    if (int.Parse(remoteversionnumbers[1]) > int.Parse(actualversionnumbers[1]))
                    {
                        if (Application.Current.Dispatcher.CheckAccess())
                        {
                            MessageBoxResult result = MessageBox.Show(Application.Current.MainWindow, "New version available, go download it?", "New version available", MessageBoxButton.YesNo);
                            if (result == MessageBoxResult.Yes)
                            {
                                Process.Start("http://entrivax.fr/osu!p");
                            }
                        }
                        else
                        {
                            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                            {
                                MessageBoxResult result = MessageBox.Show(Application.Current.MainWindow, "New version available, go download it?", "New version available", MessageBoxButton.YesNo);
                                if (result == MessageBoxResult.Yes)
                                {
                                    Process.Start("http://entrivax.fr/osu!p");
                                }
                            }));
                        }
                        return;
                    }
                    else if (int.Parse(remoteversionnumbers[1]) < int.Parse(actualversionnumbers[1]))
                    {
                        return;
                    }

                    if (int.Parse(remoteversionnumbers[2]) > int.Parse(actualversionnumbers[2]))
                    {
                        if (Application.Current.Dispatcher.CheckAccess())
                        {
                            MessageBoxResult result = MessageBox.Show(Application.Current.MainWindow, "New version available, go download it?", "New version available", MessageBoxButton.YesNo);
                            if (result == MessageBoxResult.Yes)
                            {
                                Process.Start("http://entrivax.fr/osu!p");
                            }
                        }
                        else
                        {
                            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                            {
                                MessageBoxResult result = MessageBox.Show(Application.Current.MainWindow, "New version available, go download it?", "New version available", MessageBoxButton.YesNo);
                                if (result == MessageBoxResult.Yes)
                                {
                                    Process.Start("http://entrivax.fr/osu!p");
                                }
                            }));
                        }
                        return;
                    }
                }
                catch (Exception e) { }
            }
        }

        Thread versioncheck;

        Thread loopthread, loopthread2;


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            start(userbox.Text, apibox.Password);
        }

        public void start(string user, string apikey)
        {
            osu_player = new Osu_Player(this, user, apikey);
            if (osu_player.userid != 0) {
                rankedbox.Text = osu_player.ranked_score.ToString("#,#", CultureInfo.InvariantCulture);
                levelbox.Text = osu_player.level.ToString("#,#.####", CultureInfo.InvariantCulture);
                totalbox.Text = osu_player.total_score.ToString("#,#", CultureInfo.InvariantCulture);
                rankbox.Text = osu_player.pprank.ToString("#,#", CultureInfo.InvariantCulture);
                ppbox.Text = osu_player.pp.ToString("#,#.##", CultureInfo.InvariantCulture);
                accuracybox.Text = osu_player.accuracy.ToString("#,#.#####", CultureInfo.InvariantCulture);
                levelchangebox.Text = "0";
                rankedscorechangebox.Text = "0";
                totalscorechangebox.Text = "0";
                rankchangebox.Text = "0";
                ppchangebox.Text = "0";
                accuracychangebox.Text = "0";

                if (!loopthread.IsAlive)
                    loopthread.Start();

                if (!loopthread2.IsAlive)
                    loopthread2.Start();
            }
        }

        private void userbox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                e.Handled = true;
                Button_Click(sender, e);
            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            

            apibox.Password = config.IniReadValue("User", "APIkey", "");
            txtNum.Text = config.IniReadValue("User", "looptime", "5");
            if (!int.TryParse(txtNum.Text, out _numValue))
                txtNum.Text = _numValue.ToString();

            int nfiles = 0;
            int.TryParse(config.IniReadValue("User", "files", "0"), out nfiles);

            for (int i = 0; i < nfiles; i++)
            {
                files.Add(config.IniReadValue("Files", "filename"+i, ""));
                contents.Add(config.IniReadValue("Files", "filecontent"+i, "").Replace("\\n", "\n"));
                filelist.Items.Add(config.IniReadValue("Files", "filename" + i, ""));
            }

            UpdateRankingControls();
        }

        private void TabControl_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (tab.SelectedIndex == 0)
            {
                tab.Height = 52 + rankingcomponents*31;
            }
            else if (tab.SelectedIndex == 1)
            {
                tab.Height = 238;
            }
            else if (tab.SelectedIndex == 2)
            {
                tab.Height = 373;
            }
        }



        private int _numValue = 0;
        private void cmdUp_Click(object sender, RoutedEventArgs e)
        {
            _numValue++;
            txtNum.Text = _numValue.ToString();
            loopupdate.setTimer(_numValue);
            config.IniWriteValue("User", "looptime", _numValue.ToString());
        }

        private void cmdDown_Click(object sender, RoutedEventArgs e)
        {
            _numValue--;
            if (_numValue < 5)
                _numValue = 5;
            txtNum.Text = _numValue.ToString();
            loopupdate.setTimer(_numValue);
            config.IniWriteValue("User", "looptime", _numValue.ToString());
        }

        private void txtNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(txtNum.Text, out _numValue))
            {
                if (_numValue < 5)
                    _numValue = 5;
                txtNum.Text = _numValue.ToString();
                loopupdate.setTimer(_numValue);
                config.IniWriteValue("User", "looptime", _numValue.ToString());
            }
            else if(loopupdate != null)
            {
                if (_numValue < 5)
                    _numValue = 5;
                txtNum.Text = _numValue.ToString();
                loopupdate.setTimer(_numValue);
                config.IniWriteValue("User", "looptime", _numValue.ToString());
            }
        }

        private void modelist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mode = modelist.SelectedIndex;
            config.IniWriteValue("User", "mode", mode.ToString());
            if (osu_player != null)
            {
                start(osu_player.user, osu_player.apikey);
            }
        }

        private void modelist_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                mode = int.Parse(config.IniReadValue("User", "mode", "0"));
            }
            catch (Exception ex) { }
            if (mode < 0) mode = 0; if (mode > 3) mode = 3;
            modelist.SelectedIndex = mode;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            FilesWindow fwindow = new FilesWindow();
            fwindow.Owner = this;
            fwindow.number = -1;
            fwindow.setlist(ref filelist);
            fwindow.Show();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            if (filelist.SelectedIndex >= 0)
            {
                FilesWindow fwindow = new FilesWindow();
                fwindow.Owner = this;
                fwindow.number = filelist.SelectedIndex;
                fwindow.setlist(ref filelist);
                fwindow.file = files[filelist.SelectedIndex];
                fwindow.content = contents[filelist.SelectedIndex];
                fwindow.Show();
            }
        }

        private void remove_Click(object sender, RoutedEventArgs e)
        {
            if (filelist.SelectedIndex >= 0)
            {
                files.RemoveAt(filelist.SelectedIndex);
                contents.RemoveAt(filelist.SelectedIndex);
                config.IniWriteValue("User", "files", files.Count.ToString());

                filelist.Items.Clear();
                for (int i = 0; i < MainWindow.files.Count; i++)
                {
                    filelist.Items.Add(MainWindow.files[i]);
                }
            }
        }

        private void beatmapscheck_Checked(object sender, RoutedEventArgs e)
        {
            config.IniWriteValue("User", "beatmaps", "true");
            playedbox.IsEnabled = true;
        }

        private void beatmapscheck_Unchecked(object sender, RoutedEventArgs e)
        {
            config.IniWriteValue("User", "beatmaps", "false");
            playedbox.IsEnabled = false;
        }

        private void scoremodelist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            scoremode = scoremodelist.SelectedIndex;
            config.IniWriteValue("User", "scoremode", scoremode.ToString());
        }

        private void scoremodelist_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                scoremode = int.Parse(config.IniReadValue("User", "scoremode", "0"));
            }
            catch (Exception ex) { }
            if (scoremode < 0) scoremode = 0; if (scoremode > 1) scoremode = 1;
            scoremodelist.SelectedIndex = scoremode;
        }

        private void rankingButton_Click(object sender, RoutedEventArgs e)
        {
            RankingSelector fwindow = new RankingSelector();
            fwindow.Owner = this;
            fwindow.Show();
        }

        int rankingcomponents = 0;

        public void UpdateRankingControls()
        {
            List<Control> controls = new List<Control>();

            if (MainWindow.config.IniReadValue("User", "levelbox", "true") == "true")
            {
                levelLab.Visibility = Visibility.Visible;
                levelbox.Visibility = Visibility.Visible;
                levelchangebox.Visibility = Visibility.Visible;
                controls.Add(levelLab);
                controls.Add(levelbox);
                controls.Add(levelchangebox);
            }
            else
            {
                levelLab.Visibility = Visibility.Hidden;
                levelbox.Visibility = Visibility.Hidden;
                levelchangebox.Visibility = Visibility.Hidden;
            }

            if (MainWindow.config.IniReadValue("User", "rankscorebox", "true") == "true")
            {
                rscoreLab.Visibility = Visibility.Visible;
                rankedbox.Visibility = Visibility.Visible;
                rankedscorechangebox.Visibility = Visibility.Visible;
                controls.Add(rscoreLab);
                controls.Add(rankedbox);
                controls.Add(rankedscorechangebox);
            }
            else
            {
                rscoreLab.Visibility = Visibility.Hidden;
                rankedbox.Visibility = Visibility.Hidden;
                rankedscorechangebox.Visibility = Visibility.Hidden;
            }

            if (MainWindow.config.IniReadValue("User", "totalscorebox", "true") == "true")
            {
                tscoreLab.Visibility = Visibility.Visible;
                totalbox.Visibility = Visibility.Visible;
                totalscorechangebox.Visibility = Visibility.Visible;
                controls.Add(tscoreLab);
                controls.Add(totalbox);
                controls.Add(totalscorechangebox);
            }
            else
            {
                tscoreLab.Visibility = Visibility.Hidden;
                totalbox.Visibility = Visibility.Hidden;
                totalscorechangebox.Visibility = Visibility.Hidden;
            }

            if (MainWindow.config.IniReadValue("User", "rankbox", "true") == "true")
            {
                rankLab.Visibility = Visibility.Visible;
                rankbox.Visibility = Visibility.Visible;
                rankchangebox.Visibility = Visibility.Visible;
                controls.Add(rankLab);
                controls.Add(rankbox);
                controls.Add(rankchangebox);
            }
            else
            {
                rankLab.Visibility = Visibility.Hidden;
                rankbox.Visibility = Visibility.Hidden;
                rankchangebox.Visibility = Visibility.Hidden;
            }

            if (MainWindow.config.IniReadValue("User", "ppbox", "true") == "true")
            {
                ppLab.Visibility = Visibility.Visible;
                ppbox.Visibility = Visibility.Visible;
                ppchangebox.Visibility = Visibility.Visible;
                controls.Add(ppLab);
                controls.Add(ppbox);
                controls.Add(ppchangebox);
            }
            else
            {
                ppLab.Visibility = Visibility.Hidden;
                ppbox.Visibility = Visibility.Hidden;
                ppchangebox.Visibility = Visibility.Hidden;
            }

            if (MainWindow.config.IniReadValue("User", "accubox", "true") == "true")
            {
                accuLab.Visibility = Visibility.Visible;
                accuracybox.Visibility = Visibility.Visible;
                accuracychangebox.Visibility = Visibility.Visible;
                controls.Add(accuLab);
                controls.Add(accuracybox);
                controls.Add(accuracychangebox);
            }
            else
            {
                accuLab.Visibility = Visibility.Hidden;
                accuracybox.Visibility = Visibility.Hidden;
                accuracychangebox.Visibility = Visibility.Hidden;
            }

            rankingcomponents = controls.Count / 3;
            int count = 0;
            foreach (Control control in controls)
            {
                if (count % 3 == 0)
                {
                    Canvas.SetTop(control, 31 * (count / 3) - 1);
                }
                else {
                    Canvas.SetTop(control, 31 * (count / 3));
                }
                count++;
            }

            if (tab.SelectedIndex == 0)
            {
                tab.Height = 52 + rankingcomponents * 31;
            }
        }
    }
}
