using HtmlAgilityPack;
using MahApps.Metro;
using MahApps.Metro.Controls;
using Newtonsoft.Json;
using osu_Profile;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Osu_Profile
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        #region Variables
        public static IniFile config = new IniFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\config.ini");
        public Loop loopupdate = new Loop();

        Thread versioncheck;
        Thread loopthread;

        public static List<String> files = new List<String>(), contents = new List<String>();

        public static int mode = 0;
        public static int scoremode = 0;
        static String Username, APIKey;
        int rankingcomponents = 0;
        #endregion Variables

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            MWindow = this;
            settingsPanel.ConfigFile = config;

            loopthread = new Thread(new ThreadStart(loopupdate.loop));
            loopthread.IsBackground = true;


            versioncheck = new Thread(checkversion);
            versioncheck.IsBackground = true;
            versioncheck.Start();

            beatmapscheck.IsChecked = config.IniReadValue("User", "beatmaps", "false") == "true";
            playedbox.IsEnabled = config.IniReadValue("User", "beatmaps", "false") == "true";

            ThemeManager.AddAppTheme("FullLight", new Uri("pack://application:,,,/osu!Profile;component/Resources/MyStyle.xaml"));
        }
        #endregion

        #region Properties
        public String Ranked
        {
            get
            {
                return rankedbox.Text;
            }
            set
            {
                if (value == "0")
                    rankedbox.Text = "";
                else
                    rankedbox.Text = value;
            }
        }
        public String Level
        {
            get
            {
                return levelbox.Text;
            }
            set
            {
                if (value == "0")
                    levelbox.Text = "";
                else
                    levelbox.Text = value;
            }
        }
        public String Total
        {
            get
            {
                return totalbox.Text;
            }
            set
            {
                if (value == "0")
                    totalbox.Text = "";
                else
                    totalbox.Text = value;
            }
        }
        public String Rank
        {
            get
            {
                return rankbox.Text;
            }
            set
            {
                if (value == "0")
                    rankbox.Text = "";
                else
                    rankbox.Text = value;
            }
        }
        public String PP
        {
            get
            {
                return ppbox.Text;
            }
            set
            {
                if (value == "0")
                    ppbox.Text = "";
                else
                    ppbox.Text = value;
            }
        }
        public String Accuracy
        {
            get
            {
                return accuracybox.Text;
            }
            set
            {
                if (value == "0")
                    accuracybox.Text = "";
                else
                    accuracybox.Text = value;
            }
        }
        public String PlayCount
        {
            get
            {
                return playcountbox.Text;
            }
            set
            {
                if (value == "0")
                    playcountbox.Text = "";
                else
                    playcountbox.Text = value;
            }
        }
        public String TopPP
        {
            get
            {
                return topPPbox.Text;
            }
            set
            {
                if (value == "0")
                    topPPbox.Text = "";
                else
                    topPPbox.Text = value;
            }
        }

        public String RankedScoreChange
        {
            get
            {
                return rankedscorechangebox.Text;
            }
            set
            {
                if (value == "0")
                    rankedscorechangebox.Text = "";
                else
                    rankedscorechangebox.Text = value;
            }
        }
        public String LevelChange
        {
            get
            {
                return levelchangebox.Text;
            }
            set
            {
                if (value == "0")
                    levelchangebox.Text = "";
                else
                    levelchangebox.Text = value;
            }
        }
        public String TotalScoreChange
        {
            get
            {
                return totalscorechangebox.Text;
            }
            set
            {
                if (value == "0")
                    totalscorechangebox.Text = "";
                else
                    totalscorechangebox.Text = value;
            }
        }
        public String RankChange
        {
            get
            {
                return rankchangebox.Text;
            }
            set
            {
                if (value == "0")
                    rankchangebox.Text = "";
                else
                    rankchangebox.Text = value;
            }
        }
        public String PPChange
        {
            get
            {
                return ppchangebox.Text;
            }
            set
            {
                if (value == "0")
                    ppchangebox.Text = "";
                else
                    ppchangebox.Text = value;
            }
        }
        public String AccuracyChange
        {
            get
            {
                return accuracychangebox.Text;
            }
            set
            {
                if (value == "0")
                    accuracychangebox.Text = "";
                else
                    accuracychangebox.Text = value;
            }
        }
        public String PlayCountChange
        {
            get
            {
                return playcountchangebox.Text;
            }
            set
            {
                if (value == "0")
                    playcountchangebox.Text = "";
                else
                    playcountchangebox.Text = value;
            }
        }
        public String TopPPChange
        {
            get
            {
                return topPPchangebox.Text;
            }
            set
            {
                if (value == "0")
                    topPPchangebox.Text = "";
                else
                    topPPchangebox.Text = value;
            }
        }

        public TextBox RankedBox
        {
            get
            {
                return rankedbox;
            }
        }
        public TextBox LevelBox
        {
            get
            {
                return levelbox;
            }
        }
        public TextBox TotalBox
        {
            get
            {
                return totalbox;
            }
        }
        public TextBox RankBox
        {
            get
            {
                return rankbox;
            }
        }
        public TextBox PPBox
        {
            get
            {
                return ppbox;
            }
        }
        public TextBox AccuracyBox
        {
            get
            {
                return accuracybox;
            }
        }
        public TextBox PlayCountBox
        {
            get
            {
                return playcountbox;
            }
        }
        public TextBox TopPPBox
        {
            get
            {
                return topPPbox;
            }
        }

        public TextBox RankedScoreChangeBox
        {
            get
            {
                return rankedscorechangebox;
            }
        }
        public TextBox LevelChangeBox
        {
            get
            {
                return levelchangebox;
            }
        }
        public TextBox TotalScoreChangeBox
        {
            get
            {
                return totalscorechangebox;
            }
        }
        public TextBox RankChangeBox
        {
            get
            {
                return rankchangebox;
            }
        }
        public TextBox PPChangeBox
        {
            get
            {
                return ppchangebox;
            }
        }
        public TextBox AccuracyChangeBox
        {
            get
            {
                return accuracychangebox;
            }
        }
        public TextBox PlayCountChangeBox
        {
            get
            {
                return playcountchangebox;
            }
        }
        public TextBox TopPPChangeBox
        {
            get
            {
                return topPPchangebox;
            }
        }
        public TextBox PlayBox
        {
            get
            {
                return playedbox;
            }
        }

        public Player PlayerFirstState { get; set; }
        public Player PlayerPreviousState { get; set; }
        public Player PlayerActualState { get; set; }

        public static MainWindow MWindow { get; set; }
        #endregion

        #region Methods
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
                        ShowNewVersion();
                        return;
                    }
                    else if (int.Parse(remoteversionnumbers[0]) < int.Parse(actualversionnumbers[0]))
                    {
                        return;
                    }

                    if (int.Parse(remoteversionnumbers[1]) > int.Parse(actualversionnumbers[1]))
                    {
                        ShowNewVersion();
                        return;
                    }
                    else if (int.Parse(remoteversionnumbers[1]) < int.Parse(actualversionnumbers[1]))
                    {
                        return;
                    }

                    if (int.Parse(remoteversionnumbers[2]) > int.Parse(actualversionnumbers[2]))
                    {
                        ShowNewVersion();
                        return;
                    }
                }
                catch (Exception) { }
            }
        }

        private static void ShowNewVersion()
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

        public bool Start(string user, string apikey)
        {
            bool downloaded = false;
            short retry = 0;
            while (!downloaded && retry < 3)
            {
                try
                {
                    using (WebClient client = new WebClient())
                    {
                        string apiReturn = client.DownloadString("https://osu.ppy.sh/api/get_user?k=" + apikey + "&u=" + user + "&m=" + MainWindow.mode);
                        apiReturn = apiReturn.Substring(1, apiReturn.Length - 2);
                        PlayerActualState = JsonConvert.DeserializeObject<Player>(apiReturn);
                        PlayerActualState.TopRanks = JsonConvert.DeserializeObject<Score[]>(client.DownloadString("https://osu.ppy.sh/api/get_user_best?k=" + apikey + "&u=" + user + "&m=" + MainWindow.mode));
                        PlayerActualState.Mode = MainWindow.mode;
                        PlayerFirstState = PlayerPreviousState = PlayerActualState;
                        downloaded = true;
                        config.IniWriteValue("User", "APIkey", apikey);
                        config.IniWriteValue("User", "LastUsername", user);
                        Username = user;
                        APIKey = apikey;
                    }
                }
                catch (Exception e) { downloaded = false; retry++; Console.WriteLine(e.StackTrace); }
            }
            if (!downloaded)
                return false;
            if (PlayerActualState != null && PlayerActualState.ID != 0)
            {
                this.Title = "osu!Profile - " + PlayerActualState.Username;
                SetValue(rankedbox, PlayerActualState.RankedScore, "#,#");
                SetValue(levelbox, PlayerActualState.Level, "#,#.####");
                SetValue(totalbox, PlayerActualState.Score, "#,#");
                SetValue(rankbox, PlayerActualState.PPRank, "#,#");
                SetValue(ppbox, PlayerActualState.PP, "#,#.##");
                SetValue(accuracybox, PlayerActualState.Accuracy, "#,#.#####");
                SetValue(playcountbox, PlayerActualState.PlayCount, "#,#");
                if (PlayerActualState.TopRanks != null && PlayerActualState.TopRanks.Length > 0)
                    SetValue(topPPbox, PlayerActualState.TopRanks[0].PP, "#,#.#####");
                else
                    SetValue(topPPbox, 0, "");

                SetValue(levelchangebox, 0, "");
                SetValue(rankedscorechangebox, 0, "");
                SetValue(totalscorechangebox, 0, "");
                SetValue(rankchangebox, 0, "");
                SetValue(ppchangebox, 0, "");
                SetValue(accuracychangebox, 0, "");
                SetValue(playcountchangebox, 0, "");
                SetValue(topPPbox, 0, "");

                if (!loopthread.IsAlive)
                    loopthread.Start();

                new Thread(new ThreadStart((Action)(() =>
                {
                    HtmlWeb web = new HtmlWeb();
                    HtmlDocument doc = web.Load("http://osu.ppy.sh/u/" + PlayerActualState.ID);
                    var imgs = doc.DocumentNode.Descendants("img");
                    foreach (var img in imgs)
                    {
                        var alt = img.Attributes["alt"];
                        if (alt != null && alt.Value == "User avatar")
                        {
                            WebClient webClient = new WebClient();
                            byte[] data = webClient.DownloadData("http:" + img.Attributes["src"].Value);
                            MemoryStream stream = new MemoryStream(data);

                            BitmapImage image = new BitmapImage();
                            image.BeginInit();
                            image.StreamSource = stream;
                            image.CacheOption = BitmapCacheOption.OnLoad;
                            image.EndInit();
                            image.Freeze();

                            window.Dispatcher.Invoke((Action)(() => { ((MainWindow)window).avatar.Source = image; }));
                        }
                    }
                }))).Start();
                return true;
            }
            return false;
        }

        public static void SetValue(TextBox textbox, int obj, String format) {
            if (obj != 0)
            {
                textbox.Text = obj.ToString(format, CultureInfo.InvariantCulture);
            }
            else
            {
                textbox.Text = "";
            }
        }
        public static void SetValue(TextBox textbox, float obj, String format)
        {
            if (obj != 0)
            {
                textbox.Text = obj.ToString(format, CultureInfo.InvariantCulture);
            }
            else
            {
                textbox.Text = "";
            }
        }
        public static void SetValue(TextBox textbox, long obj, String format)
        {
            if (obj != 0)
            {
                textbox.Text = obj.ToString(format, CultureInfo.InvariantCulture);
            }
            else
            {
                textbox.Text = "";
            }
        }

        public void UpdateRankingDisplay()
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

            if (MainWindow.config.IniReadValue("User", "playcountbox", "true") == "true")
            {
                playcountLab.Visibility = Visibility.Visible;
                playcountbox.Visibility = Visibility.Visible;
                playcountchangebox.Visibility = Visibility.Visible;
                controls.Add(playcountLab);
                controls.Add(playcountbox);
                controls.Add(playcountchangebox);
            }
            else
            {
                playcountLab.Visibility = Visibility.Hidden;
                playcountbox.Visibility = Visibility.Hidden;
                playcountchangebox.Visibility = Visibility.Hidden;
            }

            if (MainWindow.config.IniReadValue("User", "topPPbox", "true") == "true")
            {
                topPPLab.Visibility = Visibility.Visible;
                topPPbox.Visibility = Visibility.Visible;
                topPPchangebox.Visibility = Visibility.Visible;
                controls.Add(topPPLab);
                controls.Add(topPPbox);
                controls.Add(topPPchangebox);
            }
            else
            {
                topPPLab.Visibility = Visibility.Hidden;
                topPPbox.Visibility = Visibility.Hidden;
                topPPchangebox.Visibility = Visibility.Hidden;
            }

            rankingcomponents = controls.Count / 3;
            int count = 0;
            foreach (Control control in controls)
            {
                if (count % 3 == 0)
                {
                    Canvas.SetTop(control, 31 * (count / 3) + 13);
                }
                else
                {
                    Canvas.SetTop(control, 31 * (count / 3) + 14);
                }
                count++;
            }

            TabControl_SelectionChanged(null, null);
        }
        public void UpdateRankingControls()
        {
            if (MainWindow.MWindow.PlayerActualState != null)
            {
                MainWindow.MWindow.RankedScoreChangeBox.Dispatcher.Invoke(new Action(() =>
                {
                    MainWindow.MWindow.Ranked = MainWindow.MWindow.PlayerActualState.RankedScore.ToString("#,#", CultureInfo.InvariantCulture);
                    MainWindow.MWindow.Level = MainWindow.MWindow.PlayerActualState.Level.ToString("#,#.####", CultureInfo.InvariantCulture);
                    MainWindow.MWindow.Total = MainWindow.MWindow.PlayerActualState.Score.ToString("#,#", CultureInfo.InvariantCulture);
                    MainWindow.MWindow.Rank = MainWindow.MWindow.PlayerActualState.PPRank.ToString("#,#", CultureInfo.InvariantCulture);
                    MainWindow.MWindow.PP = MainWindow.MWindow.PlayerActualState.PP.ToString("#,#.##", CultureInfo.InvariantCulture);
                    MainWindow.MWindow.Accuracy = MainWindow.MWindow.PlayerActualState.Accuracy.ToString("#,#.#####", CultureInfo.InvariantCulture);
                    MainWindow.MWindow.PlayCount = MainWindow.MWindow.PlayerActualState.PlayCount.ToString("#,#", CultureInfo.InvariantCulture);
                    if (MainWindow.MWindow.PlayerActualState.TopRanks != null && MainWindow.MWindow.PlayerActualState.TopRanks.Length > 0)
                        MainWindow.MWindow.TopPP = MainWindow.MWindow.PlayerActualState.TopRanks[0].PP.ToString("#,#.#####", CultureInfo.InvariantCulture);

                    int ppRankDif = 0, playCountDif = 0;
                    float levelDif = 0, ppDif = 0, accuracyDif = 0, topPPDif = 0;
                    long rankedScoreDif = 0, scoreDif = 0;
                    if (MainWindow.scoremode == 0) // Each game mode
                    {
                        rankedScoreDif = MainWindow.MWindow.PlayerActualState.RankedScore - MainWindow.MWindow.PlayerPreviousState.RankedScore;
                        levelDif = MainWindow.MWindow.PlayerActualState.Level - MainWindow.MWindow.PlayerPreviousState.Level;
                        scoreDif = MainWindow.MWindow.PlayerActualState.Score - MainWindow.MWindow.PlayerPreviousState.Score;
                        ppRankDif = MainWindow.MWindow.PlayerActualState.PPRank - MainWindow.MWindow.PlayerPreviousState.PPRank;
                        ppDif = MainWindow.MWindow.PlayerActualState.PP - MainWindow.MWindow.PlayerPreviousState.PP;
                        accuracyDif = MainWindow.MWindow.PlayerActualState.Accuracy - MainWindow.MWindow.PlayerPreviousState.Accuracy;
                        playCountDif = MainWindow.MWindow.PlayerActualState.PlayCount - MainWindow.MWindow.PlayerPreviousState.PlayCount;
                        if (MainWindow.MWindow.PlayerActualState.TopRanks != null && MainWindow.MWindow.PlayerActualState.TopRanks.Length > 0)
                            if (MainWindow.MWindow.PlayerPreviousState.TopRanks != null && MainWindow.MWindow.PlayerPreviousState.TopRanks.Length > 0)
                                topPPDif = MainWindow.MWindow.PlayerActualState.TopRanks[0].PP - MainWindow.MWindow.PlayerPreviousState.TopRanks[0].PP;
                            else
                                topPPDif = MainWindow.MWindow.PlayerActualState.TopRanks[0].PP;
                    }
                    else if (MainWindow.scoremode == 1) // This session mode
                    {
                        rankedScoreDif = MainWindow.MWindow.PlayerActualState.RankedScore - MainWindow.MWindow.PlayerFirstState.RankedScore;
                        levelDif = MainWindow.MWindow.PlayerActualState.Level - MainWindow.MWindow.PlayerFirstState.Level;
                        scoreDif = MainWindow.MWindow.PlayerActualState.Score - MainWindow.MWindow.PlayerFirstState.Score;
                        ppRankDif = MainWindow.MWindow.PlayerActualState.PPRank - MainWindow.MWindow.PlayerFirstState.PPRank;
                        ppDif = MainWindow.MWindow.PlayerActualState.PP - MainWindow.MWindow.PlayerFirstState.PP;
                        accuracyDif = MainWindow.MWindow.PlayerActualState.Accuracy - MainWindow.MWindow.PlayerFirstState.Accuracy;
                        playCountDif = MainWindow.MWindow.PlayerActualState.PlayCount - MainWindow.MWindow.PlayerFirstState.PlayCount;
                        if (MainWindow.MWindow.PlayerActualState.TopRanks != null && MainWindow.MWindow.PlayerActualState.TopRanks.Length > 0)
                            if (MainWindow.MWindow.PlayerFirstState.TopRanks != null && MainWindow.MWindow.PlayerFirstState.TopRanks.Length > 0)
                                topPPDif = MainWindow.MWindow.PlayerActualState.TopRanks[0].PP - MainWindow.MWindow.PlayerFirstState.TopRanks[0].PP;
                            else
                                topPPDif = MainWindow.MWindow.PlayerActualState.TopRanks[0].PP;
                    }
                    MainWindow.MWindow.RankedScoreChange = rankedScoreDif.ToString("#,#", CultureInfo.InvariantCulture);
                    MainWindow.MWindow.LevelChange = levelDif.ToString("#,#0.####", CultureInfo.InvariantCulture);
                    MainWindow.MWindow.TotalScoreChange = scoreDif.ToString("#,#", CultureInfo.InvariantCulture);
                    MainWindow.MWindow.RankChange = ppRankDif.ToString("#,#", CultureInfo.InvariantCulture);
                    MainWindow.MWindow.PPChange = ppDif.ToString("#,#0.##", CultureInfo.InvariantCulture);
                    MainWindow.MWindow.AccuracyChange = accuracyDif.ToString("#,#0.#####", CultureInfo.InvariantCulture);
                    MainWindow.MWindow.PlayCountChange = playCountDif.ToString("#,#", CultureInfo.InvariantCulture);
                    MainWindow.MWindow.TopPPChange = topPPDif.ToString("#,#0.#####", CultureInfo.InvariantCulture);


                    if (ppDif > 0)
                    {
                        MainWindow.MWindow.PPChange = "+" + MainWindow.MWindow.PPChange;
                        MainWindow.MWindow.PPChangeBox.Foreground = new SolidColorBrush(Colors.Green);
                    }
                    else
                    {
                        MainWindow.MWindow.PPChangeBox.Foreground = new SolidColorBrush(Colors.Red);
                    }

                    if (scoreDif > 0)
                    {
                        MainWindow.MWindow.TotalScoreChange = "+" + MainWindow.MWindow.TotalScoreChange;
                        MainWindow.MWindow.TotalScoreChangeBox.Foreground = new SolidColorBrush(Colors.Green);
                    }
                    else
                    {
                        MainWindow.MWindow.TotalScoreChangeBox.Foreground = new SolidColorBrush(Colors.Red);
                    }

                    if (rankedScoreDif > 0)
                    {
                        MainWindow.MWindow.RankedScoreChange = "+" + MainWindow.MWindow.RankedScoreChange;
                        MainWindow.MWindow.RankedScoreChangeBox.Foreground = new SolidColorBrush(Colors.Green);
                    }
                    else
                    {
                        MainWindow.MWindow.RankedScoreChangeBox.Foreground = new SolidColorBrush(Colors.Red);
                    }

                    if (levelDif > 0)
                    {
                        MainWindow.MWindow.LevelChange = "+" + MainWindow.MWindow.LevelChange;
                        MainWindow.MWindow.LevelChangeBox.Foreground = new SolidColorBrush(Colors.Green);
                    }
                    else
                    {
                        MainWindow.MWindow.LevelChangeBox.Foreground = new SolidColorBrush(Colors.Red);
                    }

                    if (ppRankDif > 0)
                    {
                        MainWindow.MWindow.RankChange = (-ppRankDif).ToString("#,#", CultureInfo.InvariantCulture);
                        MainWindow.MWindow.RankChangeBox.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    else if (ppRankDif != 0)
                    {
                        MainWindow.MWindow.RankChange = "+" + (-ppRankDif).ToString("#,#", CultureInfo.InvariantCulture);
                        MainWindow.MWindow.RankChangeBox.Foreground = new SolidColorBrush(Colors.Green);
                    }

                    if (accuracyDif > 0)
                    {
                        MainWindow.MWindow.AccuracyChange = "+" + MainWindow.MWindow.AccuracyChange;
                        MainWindow.MWindow.AccuracyChangeBox.Foreground = new SolidColorBrush(Colors.Green);
                    }
                    else
                    {
                        MainWindow.MWindow.AccuracyChangeBox.Foreground = new SolidColorBrush(Colors.Red);
                    }

                    if (playCountDif > 0)
                    {
                        MainWindow.MWindow.PlayCountChange = "+" + MainWindow.MWindow.PlayCountChange;
                        MainWindow.MWindow.PlayCountChangeBox.Foreground = new SolidColorBrush(Colors.Green);
                    }
                    else
                    {
                        MainWindow.MWindow.PlayCountChangeBox.Foreground = new SolidColorBrush(Colors.Red);
                    }

                    if (topPPDif > 0)
                    {
                        MainWindow.MWindow.TopPPChange = "+" + MainWindow.MWindow.TopPPChange;
                        MainWindow.MWindow.TopPPChangeBox.Foreground = new SolidColorBrush(Colors.Green);
                    }
                    else
                    {
                        MainWindow.MWindow.TopPPChangeBox.Foreground = new SolidColorBrush(Colors.Red);
                    }
                }));
            }
            else
            {
                MainWindow.MWindow.Ranked = "";
                MainWindow.MWindow.Level = "";
                MainWindow.MWindow.Total = "";
                MainWindow.MWindow.Rank = "";
                MainWindow.MWindow.PP = "";
                MainWindow.MWindow.Accuracy = "";
                MainWindow.MWindow.PlayCount = "";
                MainWindow.MWindow.TopPP = "";

                MainWindow.MWindow.RankedScoreChange = "";
                MainWindow.MWindow.LevelChange = "";
                MainWindow.MWindow.TotalScoreChange = "";
                MainWindow.MWindow.RankChange = "";
                MainWindow.MWindow.PPChange = "";
                MainWindow.MWindow.AccuracyChange = "";
                MainWindow.MWindow.PlayCountChange = "";
                MainWindow.MWindow.TopPPChange = "";
            }
        }

        public static void ChangeThemeColor(byte r, byte g, byte b)
        {
            ResourceDictionary dict = new ResourceDictionary();

            dict.Source = new Uri("pack://application:,,,/osu!Profile;component/Resources/Color.xaml");

            dict["HighlightColor"] = Color.FromArgb(0xFF, (byte)(r * 0.725f), (byte)(g * 0.725f), (byte)(b * 0.725f));
            dict["AccentColor"] = Color.FromArgb(0xCC, r, g, b);
            dict["AccentColor2"] = Color.FromArgb(0x99, r, g, b);
            dict["AccentColor3"] = Color.FromArgb(0x66, r, g, b);
            dict["AccentColor4"] = Color.FromArgb(0x33, r, g, b);

            ResourceDictionary dict2 = new ResourceDictionary();

            dict2.BeginInit();

            foreach (DictionaryEntry de in dict)
            {
                dict2.Add(de.Key, de.Value);
            }

            dict2.EndInit();

            Accent accent = ThemeManager.GetAccent(dict2);
            AppTheme theme = ThemeManager.GetAppTheme("FullLight");

            ThemeManager.ChangeAppStyle(Application.Current, accent, theme);
        }
        #endregion

        #region Handlers
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            int nfiles = 0;
            int.TryParse(config.IniReadValue("User", "files", "0"), out nfiles);

            this.Topmost = config.IniReadValue("User", "topmost", "false") == "true";

            for (int i = 0; i < nfiles; i++)
            {
                files.Add(config.IniReadValue("Files", "filename"+i, ""));
                contents.Add(config.IniReadValue("Files", "filecontent"+i, "").Replace("\\n", "\n"));
                settingsPanel.FileList.Add(config.IniReadValue("Files", "filename" + i, ""));
            }

            string startupShotcut = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "osu!profile.lnk");

            if (File.Exists(startupShotcut))
            {
                if (config.IniReadValue("User", "startWithWindows", "false") == "false")
                    File.Delete(startupShotcut);
            }
            else
            {
                if (config.IniReadValue("User", "startWithWindows", "false") == "true")
                {
                    osu_Profile.Shortcut.IShellLink link = (osu_Profile.Shortcut.IShellLink)new osu_Profile.Shortcut.ShellLink();

                    link.SetDescription("osu!profile");
                    link.SetPath(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

                    IPersistFile file = (IPersistFile)link;
                    file.Save(startupShotcut, false);
                }
            }

            UpdateRankingDisplay();
        }

        private void TabControl_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (tab.SelectedIndex == 0)
            {
                if (rankingcomponents == 0)
                    tab.Height = 200;
                else
                    tab.Height = 68 + rankingcomponents*31;
            }
            else if (tab.SelectedIndex == 1)
            {
                tab.Height = 238;
            }
            this.Height = tab.Height + 40;
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Options.IsOpen = true;
        }
        #endregion


        public class Loop
        {
            #region Variable
            private int timer = 5;
            #endregion

            #region Methods
            public void loop()
            {
                while (Thread.CurrentThread.IsAlive)
                {
                    UpdateRankingPanel();
                    ExportToFile();

                    if (MainWindow.config.IniReadValue("User", "beatmaps", "false") == "true")
                        UpdatePlayPanel();

                    TimeSpan interval = new TimeSpan(0, 0, timer);
                    Thread.Sleep(interval);
                }
            }

            public void setTimer(int time)
            {
                this.timer = time;
            }

            private void UpdateRankingPanel()
            {
                bool downloaded = false;
                while (!downloaded)
                {
                    try
                    {
                        WebClient client = new WebClient();
                        string apiReturn = client.DownloadString("https://osu.ppy.sh/api/get_user?k=" + APIKey + "&u=" + Username + "&m=" + MainWindow.mode);
                        apiReturn = apiReturn.Substring(1, apiReturn.Length - 2);
                        long score = MainWindow.MWindow.PlayerActualState.Score;
                        Player tempState = JsonConvert.DeserializeObject<Player>(apiReturn);

                        tempState.Mode = MainWindow.mode;
                        if (tempState.Mode != MainWindow.MWindow.PlayerFirstState.Mode || tempState.ID != MainWindow.MWindow.PlayerFirstState.ID)
                        {
                            MainWindow.MWindow.PlayerPreviousState = MainWindow.MWindow.PlayerFirstState = MainWindow.MWindow.PlayerActualState = tempState;
                        }

                        if (tempState.Score != score)
                        {
                            MainWindow.MWindow.PlayerPreviousState = MainWindow.MWindow.PlayerActualState;
                            MainWindow.MWindow.PlayerActualState = tempState;
                            //TODO : Add
                            if (MainWindow.MWindow.PlayerPreviousState.PP < MainWindow.MWindow.PlayerActualState.PP)
                            {
                                MainWindow.MWindow.PlayerActualState.TopRanks = JsonConvert.DeserializeObject<Score[]>(client.DownloadString("https://osu.ppy.sh/api/get_user_best?k=" + APIKey + "&u=" + Username + "&m=" + MainWindow.mode));
                            }

                            if (config.IniReadValue("User", "popupEachMap", "false") == "true" && MainWindow.MWindow.PlayerPreviousState.RankedScore != MainWindow.MWindow.PlayerActualState.RankedScore)
                            {
                                MainWindow.MWindow.RankedScoreChangeBox.Dispatcher.Invoke(new Action(() =>
                                {
                                    MainWindow.MWindow.Activate();
                                    MainWindow.MWindow.Focus();
                                }));
                            }
                            else if (config.IniReadValue("User", "popupPP", "false") == "true" && MainWindow.MWindow.PlayerPreviousState.PP < MainWindow.MWindow.PlayerActualState.PP)
                            {
                                MainWindow.MWindow.RankedScoreChangeBox.Dispatcher.Invoke(new Action(() =>
                                {
                                    MainWindow.MWindow.Activate();
                                    MainWindow.MWindow.Focus();
                                }));
                            }
                        }
                        downloaded = true;
                    }
                    catch (Exception) { downloaded = false; Thread.Sleep(new TimeSpan(0, 0, 1)); }
                }

               MainWindow.MWindow.UpdateRankingControls();
            }

            private void UpdatePlayPanel()
            {
                Event[] events = null;
                bool downloaded = false;
                while (!downloaded)
                {
                    try
                    {
                        WebClient client = new WebClient();
                        string apiReturn = client.DownloadString("https://osu.ppy.sh/api/get_user_recent?k=" + APIKey + "&u=" + Username + "&m=" + MainWindow.mode);
                        events = JsonConvert.DeserializeObject<Event[]>(apiReturn);
                        downloaded = true;
                    }
                    catch (Exception) { downloaded = false; Thread.Sleep(new TimeSpan(0, 0, 1)); }
                }
                                
                foreach(Event ev in events){
                    ev.Initialize(APIKey);
                }

                MainWindow.MWindow.PlayBox.Dispatcher.Invoke(new Action(() =>
                {
                    StringBuilder sb = new StringBuilder();

                    foreach (Event ev in events)
                    {
                        sb.AppendLine(ev.Beatmap.Artist + " - " + ev.Beatmap.Title + " [" + ev.Beatmap.Difficulty + "]");
                        sb.AppendLine("BPM : " + ev.Beatmap.BPM + " - Stars : " + ev.Beatmap.Stars);
                        sb.AppendLine("CS:" + ev.Beatmap.CircleSize + " AR: " + ev.Beatmap.ApproachRate + " OD:" + ev.Beatmap.OverallDifficulty + " HP:" + ev.Beatmap.HealthDrain);
                        sb.AppendLine("Score : " + ev.Score.ToString("#,#", CultureInfo.InvariantCulture) + " " + ev.ModsString);
                        sb.Append("Rank : " + ev.Grade);
                        if (MainWindow.MWindow.PlayerActualState.PP != MainWindow.MWindow.PlayerPreviousState.PP)
                        {
                            sb.AppendLine("PP : " + (MainWindow.MWindow.PlayerActualState.PP - MainWindow.MWindow.PlayerPreviousState.PP));
                        }
                        sb.AppendLine("\n");
                    }

                    MainWindow.MWindow.PlayBox.Text = sb.ToString();
                }));
            }

            private void ExportToFile()
            {
                for (int i = 0; i < MainWindow.files.Count; i++)
                {
                    String output = MainWindow.contents[i];
                    if (output != "")
                    {
                        MainWindow.MWindow.RankedScoreChangeBox.Dispatcher.Invoke(new Action(() =>
                        {
                            output = output.Replace("[/rs]", MainWindow.MWindow.Ranked);
                            output = output.Replace("[/ts]", MainWindow.MWindow.Total);
                            output = output.Replace("[/l]", MainWindow.MWindow.Level);
                            output = output.Replace("[/r]", MainWindow.MWindow.Rank);
                            output = output.Replace("[/pp]", MainWindow.MWindow.PP);
                            output = output.Replace("[/a]", MainWindow.MWindow.Accuracy);
                            output = output.Replace("[/pc]", MainWindow.MWindow.PlayCount);
                            output = output.Replace("[/toppp]", MainWindow.MWindow.TopPP);

                            output = output.Replace("[/rsc]", MainWindow.MWindow.RankedScoreChange);
                            output = output.Replace("[/tsc]", MainWindow.MWindow.TotalScoreChange);
                            output = output.Replace("[/lc]", MainWindow.MWindow.LevelChange);
                            output = output.Replace("[/rc]", MainWindow.MWindow.RankChange);
                            output = output.Replace("[/ppc]", MainWindow.MWindow.PPChange);
                            output = output.Replace("[/ac]", MainWindow.MWindow.AccuracyChange);
                            output = output.Replace("[/pcc]", MainWindow.MWindow.PlayCountChange);
                            output = output.Replace("[/topppc]", MainWindow.MWindow.TopPPChange);
                        }));
                    }
                    try
                    {
                        File.WriteAllText(MainWindow.files[i], output);
                    }
                    catch (Exception) { }
                }
            }
            #endregion
        }
    }
}
