using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;

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
        public ListBox list;

        public void setlist(ref ListBox box)
        {
            list = box;
        }

        private void window1_Loaded(object sender, RoutedEventArgs e)
        {
            this.Owner.IsEnabled = false;
            filebox.Text = file;
            contentbox.Text = content;

            contentbox.ToolTip = "[/rs] for ranked score" + Environment.NewLine + "[/ts] for total score" + Environment.NewLine
                + "[/l] for level" + Environment.NewLine + "[/r] for performance rank" + Environment.NewLine
                + "[/pp] for PP" + Environment.NewLine + "[/a] for accuracy" + Environment.NewLine + Environment.NewLine
                + "[/rsc] for ranked score difference" + Environment.NewLine + "[/tsc] for total score difference" + Environment.NewLine
                + "[/lc] for level difference" + Environment.NewLine + "[/rc] for performance rank difference" + Environment.NewLine
                + "[/ppc] for PP difference" + Environment.NewLine + "[/ac] for accuracy difference";
        }

        private void valid_Click(object sender, RoutedEventArgs e)
        {
            if (number == -1)
            {
                if (MainWindow.files.Contains(filebox.Text.ToLower()))
                {
                    MessageBox.Show("File already exists!");
                    return;
                }
                MainWindow.files.Add(filebox.Text.ToLower());
                MainWindow.contents.Add(contentbox.Text);
                number = MainWindow.files.IndexOf(filebox.Text);
                MainWindow.config.IniWriteValue("Files", "filename" + number, filebox.Text.ToLower());
                MainWindow.config.IniWriteValue("Files", "filecontent" + number, contentbox.Text.Replace(Environment.NewLine, "\\n"));
                MainWindow.config.IniWriteValue("User", "files", MainWindow.files.Count.ToString());
            }
            else
            {
                if (MainWindow.files.Contains(filebox.Text.ToLower()) && file.ToLower() != filebox.Text.ToLower())
                {
                    MessageBox.Show("File already exists!");
                    return;
                }
                MainWindow.files[number] = filebox.Text.ToLower();
                MainWindow.contents[number] = contentbox.Text;
                MainWindow.config.IniWriteValue("Files", "filename" + number, filebox.Text.ToLower());
                MainWindow.config.IniWriteValue("Files", "filecontent" + number, contentbox.Text.Replace(Environment.NewLine, "\\n"));
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
                list.Items.Add(MainWindow.files[i]);
            }
        }
    }
}
