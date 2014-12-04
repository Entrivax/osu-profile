using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;

namespace Osu_Profile
{
    class LoopUpdate
    {
        TextBox rankedbox;
        TextBox levelbox;
        TextBox totalbox;
        TextBox rankbox;
        TextBox ppbox;
        TextBox accuracybox;
        TextBox ppchangebox;
        TextBox totalscorechangebox;
        TextBox rankedscorechangebox;
        TextBox levelchangebox;
        TextBox rankchangebox;
        TextBox accuracychangebox;
        TextBox playedbox;
        int timer;

        public LoopUpdate(ref TextBox rankedbox, ref TextBox levelbox, ref TextBox totalbox, ref TextBox rankbox, ref TextBox ppbox, ref TextBox accuracybox, ref TextBox ppchangebox, ref TextBox totalscorechangebox, ref TextBox rankedscorechangebox, ref TextBox levelchangebox, ref TextBox rankchangebox, ref TextBox accuracychangebox, ref TextBox playedbox, ref int timer)
        {
            this.rankedbox = rankedbox;
            this.levelbox = levelbox;
            this.totalbox = totalbox;
            this.rankbox = rankbox;
            this.ppbox = ppbox;
            this.accuracybox = accuracybox;

            this.ppchangebox = ppchangebox;
            this.totalscorechangebox = totalscorechangebox;
            this.rankedscorechangebox = rankedscorechangebox;
            this.levelchangebox = levelchangebox;
            this.rankchangebox = rankchangebox;
            this.accuracychangebox = accuracychangebox;
            this.playedbox = playedbox;

            this.timer = timer;
        }

        public void setTimer(int time)
        {
            this.timer = time;
        }

        public void UpdateControls()
        {
            rankedbox.Text = MainWindow.osu_player.ranked_score.ToString("#,#", CultureInfo.InvariantCulture);
            levelbox.Text = Math.Round(MainWindow.osu_player.level, 4).ToString("#,#.####", CultureInfo.InvariantCulture);
            totalbox.Text = MainWindow.osu_player.total_score.ToString("#,#", CultureInfo.InvariantCulture);
            rankbox.Text = MainWindow.osu_player.pprank.ToString("#,#.###", CultureInfo.InvariantCulture);
            ppbox.Text = MainWindow.osu_player.pp.ToString("#,#.###", CultureInfo.InvariantCulture);
            accuracybox.Text = MainWindow.osu_player.accuracy.ToString("#,#.#####", CultureInfo.InvariantCulture);
            if (MainWindow.osu_player.ppchange > 0)
            {
                ppchangebox.Text = "+" + MainWindow.osu_player.ppchange.ToString("#,#0.##", CultureInfo.InvariantCulture);
                ppchangebox.Foreground = new SolidColorBrush(Colors.Green);
            }
            else if (MainWindow.osu_player.ppchange < 0)
            {
                ppchangebox.Text = MainWindow.osu_player.ppchange.ToString("#,#0.##", CultureInfo.InvariantCulture);
                ppchangebox.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                ppchangebox.Text = MainWindow.osu_player.ppchange.ToString("#,#.##", CultureInfo.InvariantCulture);
                ppchangebox.Foreground = new SolidColorBrush(Colors.Black);
            }

            if (MainWindow.osu_player.scorechange > 0)
            {
                totalscorechangebox.Text = "+" + MainWindow.osu_player.scorechange.ToString("#,#", CultureInfo.InvariantCulture);
                totalscorechangebox.Foreground = new SolidColorBrush(Colors.Green);
            }
            else if (MainWindow.osu_player.scorechange < 0)
            {
                totalscorechangebox.Text = MainWindow.osu_player.scorechange.ToString("#,#0", CultureInfo.InvariantCulture);
                totalscorechangebox.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                totalscorechangebox.Text = MainWindow.osu_player.scorechange.ToString("#,#", CultureInfo.InvariantCulture);
                totalscorechangebox.Foreground = new SolidColorBrush(Colors.Black);
            }

            if (MainWindow.osu_player.ranked_scorechange > 0)
            {
                rankedscorechangebox.Text = "+" + MainWindow.osu_player.ranked_scorechange.ToString("#,#", CultureInfo.InvariantCulture);
                rankedscorechangebox.Foreground = new SolidColorBrush(Colors.Green);
            }
            else if (MainWindow.osu_player.ranked_scorechange < 0)
            {
                rankedscorechangebox.Text = MainWindow.osu_player.ranked_scorechange.ToString("#,#", CultureInfo.InvariantCulture);
                rankedscorechangebox.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                rankedscorechangebox.Text = MainWindow.osu_player.ranked_scorechange.ToString("#,#", CultureInfo.InvariantCulture);
                rankedscorechangebox.Foreground = new SolidColorBrush(Colors.Black);
            }

            if (MainWindow.osu_player.levelchange > 0)
            {
                levelchangebox.Text = "+" + MainWindow.osu_player.levelchange.ToString("#,#0.####", CultureInfo.InvariantCulture);
                levelchangebox.Foreground = new SolidColorBrush(Colors.Green);
            }
            else if (MainWindow.osu_player.levelchange < 0)
            {
                levelchangebox.Text = MainWindow.osu_player.levelchange.ToString("#,#0.####", CultureInfo.InvariantCulture);
                levelchangebox.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                levelchangebox.Text = MainWindow.osu_player.levelchange.ToString("#,#.####", CultureInfo.InvariantCulture);
                levelchangebox.Foreground = new SolidColorBrush(Colors.Black);
            }

            if (MainWindow.osu_player.pprankchange > 0)
            {
                rankchangebox.Text = "+" + MainWindow.osu_player.pprankchange.ToString("#,#0.##", CultureInfo.InvariantCulture);
                rankchangebox.Foreground = new SolidColorBrush(Colors.Red);
            }
            else if (MainWindow.osu_player.pprankchange < 0)
            {
                rankchangebox.Text = MainWindow.osu_player.pprankchange.ToString("#,#0.##", CultureInfo.InvariantCulture);
                rankchangebox.Foreground = new SolidColorBrush(Colors.Green);
            }
            else
            {
                rankchangebox.Text = MainWindow.osu_player.pprankchange.ToString("#,#.##", CultureInfo.InvariantCulture);
                rankchangebox.Foreground = new SolidColorBrush(Colors.Black);
            }

            if (MainWindow.osu_player.accuracychange > 0)
            {
                accuracychangebox.Text = "+" + MainWindow.osu_player.accuracychange.ToString("#,#0.#####", CultureInfo.InvariantCulture);
                accuracychangebox.Foreground = new SolidColorBrush(Colors.Green);
            }
            else if (MainWindow.osu_player.accuracychange < 0)
            {
                accuracychangebox.Text = MainWindow.osu_player.accuracychange.ToString("#,#0.#####", CultureInfo.InvariantCulture);
                accuracychangebox.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                accuracychangebox.Text = MainWindow.osu_player.accuracychange.ToString("#,#.#####", CultureInfo.InvariantCulture);
                accuracychangebox.Foreground = new SolidColorBrush(Colors.Black);
            }



            for (int i = 0; i < MainWindow.files.Count; i++)
            {
                String output = MainWindow.contents[i];
                if (output != "")
                {
                    output = output.Replace("[/rs]", rankedbox.Text);
                    output = output.Replace("[/ts]", totalbox.Text);
                    output = output.Replace("[/l]", levelbox.Text);
                    output = output.Replace("[/r]", rankbox.Text);
                    output = output.Replace("[/pp]", ppbox.Text);
                    output = output.Replace("[/a]", accuracybox.Text);

                    if (rankedscorechangebox.Text != "0")
                        output = output.Replace("[/rsc]", rankedscorechangebox.Text);
                    else
                        output = output.Replace("[/rsc]", "");

                    if (totalscorechangebox.Text != "0")
                        output = output.Replace("[/tsc]", totalscorechangebox.Text);
                    else
                        output = output.Replace("[/tsc]", "");

                    if (levelchangebox.Text != "0")
                        output = output.Replace("[/lc]", levelchangebox.Text);
                    else
                        output = output.Replace("[/lc]", "");

                    if (rankchangebox.Text != "0")
                        output = output.Replace("[/rc]", rankchangebox.Text);
                    else
                        output = output.Replace("[/rc]", "");

                    if (ppchangebox.Text != "0")
                        output = output.Replace("[/ppc]", ppchangebox.Text);
                    else
                        output = output.Replace("[/ppc]", "");

                    if (accuracychangebox.Text != "0")
                        output = output.Replace("[/ac]", accuracychangebox.Text);
                    else
                        output = output.Replace("[/ac]", "");
                }
                try
                {
                    File.WriteAllText(MainWindow.files[i], output);
                }
                catch (Exception e) { }
            }
        }

        public void updatebeatmapscotrol()
        {
            playedbox.Text = MainWindow.osu_player.events;
        }
        
        public void loop()
        {
            while (Thread.CurrentThread.IsAlive)
            {
                TimeSpan interval = new TimeSpan(0, 0, timer);
                Thread.Sleep(interval);

                MainWindow.osu_player.update();

                Delegate myDelegate = (Action)UpdateControls;
                rankedbox.Dispatcher.Invoke(myDelegate);
            }
        }

        public void loop2()
        {
            while (Thread.CurrentThread.IsAlive)
            {
                TimeSpan interval = new TimeSpan(0, 0, timer);
                Thread.Sleep(interval);

                MainWindow.osu_player.updatebeatmaps();

                Delegate myDelegate = (Action)updatebeatmapscotrol;
                playedbox.Dispatcher.Invoke(myDelegate);
            }
        }
    }
}
