using System;

namespace osu_Profile.IO
{
    public class OutputFile
    {
        private string name;
        private string content;
        private int time;
        private int timeleft;

        public String Name { get { return name; } set { name = value; } }
        public String Content { get { return content; } set { content = value; } }
        public int Time { get { return time; } set { time = value; } }
        public int TimeLeft { get { return timeleft; } set { timeleft = value; } }

        public OutputFile(string name, string content, int time)
        {
            this.name = name;
            this.content = content;
            Time = TimeLeft = time;
        }
    }
}
