using MahApps.Metro.Controls;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Osu_Profile
{
    /// <summary>
    /// Logique d'interaction pour FilesWindow.xaml
    /// </summary>
    public partial class FilesWindow : MetroWindow
    {
        public FilesWindow()
        {
            InitializeComponent();
        }

        public String file = "";
        public String content = "";
        public int number = -1;
        public int time = 0;
        public ListBox list;

        public string TimeToWait
        {
            set
            {
                if (!int.TryParse(value, out time))
                {
                    time = 0;
                }
                if (time < 0)
                    time = 0;
                txtNum.Text = time.ToString();
                if (MainWindow.MWindow.loopupdate != null)
                    MainWindow.MWindow.loopupdate.setTimer(time);
            }
            get
            {
                return txtNum.Text;
            }
        }

        public void setlist(ref ListBox box)
        {
            list = box;
        }

        private void window1_Loaded(object sender, RoutedEventArgs e)
        {
            this.Owner.IsEnabled = false;
            filebox.Text = file;
            contentbox.Text = content;
            TimeToWait = time.ToString();

            contentbox.ToolTip = "[/rs] for ranked score" + Environment.NewLine + "[/ts] for total score" + Environment.NewLine
                + "[/l] for level" + Environment.NewLine + "[/r] for performance rank" + Environment.NewLine
                + "[/cr] for country rank" + Environment.NewLine
                + "[/pp] for PP" + Environment.NewLine + "[/a] for accuracy" + Environment.NewLine + "[/pc] for play count" + Environment.NewLine
                + "[/toppp] for the top PP"
                + Environment.NewLine + Environment.NewLine
                + "[/rsc] for ranked score difference" + Environment.NewLine + "[/tsc] for total score difference" + Environment.NewLine
                + "[/lc] for level difference" + Environment.NewLine + "[/rc] for performance rank difference" + Environment.NewLine
                + "[/crc] for country rank difference" + Environment.NewLine
                + "[/ppc] for PP difference" + Environment.NewLine + "[/ac] for accuracy difference" + Environment.NewLine
                + "[/pcc] for play count difference" + Environment.NewLine + "[/topppc] for the top PP difference"
                + Environment.NewLine + Environment.NewLine

                + "[/lpbArtist] for the last played beatmap's artist" + Environment.NewLine
                + "[/lpbTitle] for the last played beatmap's title" + Environment.NewLine
                + "[/lpbBPM] for the last played beatmap's BPM" + Environment.NewLine
                + "[/lpbCreator] for the last played beatmap's creator" + Environment.NewLine
                + "[/lpbDifficulty] for the last played beatmap's difficulty name" + Environment.NewLine
                + "[/lpbID] for the last played beatmap's ID" + Environment.NewLine
                + "[/lpbSetID] for the last played beatmap's set ID" + Environment.NewLine

                + "[/lpbAR] for the last played beatmap's approach rate" + Environment.NewLine
                + "[/lpbCS] for the last played beatmap's circle size rate" + Environment.NewLine
                + "[/lpbHP] for the last played beatmap's health drain rate" + Environment.NewLine
                + "[/lpbOD] for the last played beatmap's overrall difficulty rate" + Environment.NewLine
                + "[/lpbStars] for the last played beatmap's stars number" + Environment.NewLine

                + "[/lpbGrade] for the last played beatmap's grade" + Environment.NewLine
                + "[/lpbMods] for the last played beatmap's mods enabled" + Environment.NewLine
                + "[/lpbScore] for the last played beatmap's mods score";

            txtNum.ToolTip = "The time in seconds to show something on the output after a change. (0 = unlimited)";
        }

        private void cmdUp_Click(object sender, RoutedEventArgs e)
        {
            TimeToWait = (time + 1).ToString();
        }

        private void cmdDown_Click(object sender, RoutedEventArgs e)
        {
            TimeToWait = (time - 1).ToString();
        }

        private void txtNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (MainWindow.MWindow != null)
                TimeToWait = txtNum.Text;
        }



        private void valid_Click(object sender, RoutedEventArgs e)
        {
            if (number == -1)
            {
                if (MainWindow.ContainsFilename(filebox.Text))
                {
                    MessageBox.Show("File already exists!");
                    return;
                }
                OutputFile outputFile = new OutputFile(filebox.Text, contentbox.Text, time);
                MainWindow.files.Add(outputFile);
                number = MainWindow.IndexOfFilename(filebox.Text);
                MainWindow.config.IniWriteValue("Files", "filename" + number, filebox.Text.ToLower());
                MainWindow.config.IniWriteValue("Files", "filecontent" + number, contentbox.Text.Replace(Environment.NewLine, "\\n"));
                MainWindow.config.IniWriteValue("Files", "filetime" + number, time.ToString());
                MainWindow.config.IniWriteValue("User", "files", MainWindow.files.Count.ToString());
            }
            else
            {
                if (MainWindow.ContainsFilename(filebox.Text) && file.ToLower() != filebox.Text.ToLower())
                {
                    MessageBox.Show("File already exists!");
                    return;
                }
                MainWindow.files[number].Name = filebox.Text.ToLower();
                MainWindow.files[number].Content = contentbox.Text;
                MainWindow.files[number].Time = time;
                MainWindow.config.IniWriteValue("Files", "filename" + number, filebox.Text.ToLower());
                MainWindow.config.IniWriteValue("Files", "filecontent" + number, contentbox.Text.Replace(Environment.NewLine, "\\n"));
                MainWindow.config.IniWriteValue("Files", "filetime" + number, time.ToString());
                MainWindow.config.IniWriteValue("User", "files", MainWindow.files.Count.ToString());
            }
            this.Close();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void window1_Closed(object sender, EventArgs e)
        {
            this.Owner.IsEnabled = true;
            list.Items.Clear();
            for (int i = 0; i < MainWindow.files.Count; i++)
            {
                list.Items.Add(MainWindow.files[i].Name);
            }
        }
    }
}
