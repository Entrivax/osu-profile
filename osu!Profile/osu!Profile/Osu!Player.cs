using Procurios.Public;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace Osu_Profile
{
    public class Osu_Player
    {
        public int userid;
        public String user;
        public long ranked_score, oldranked_score, ranked_scorechange, originranked_score;
        public long total_score, oldscore, scorechange, origintotal_score;
        public int pprank, oldpprank, pprankchange, originpprank;
        public float level, oldlevel, levelchange, originlevel;
        public float pp, ppchange, oldpp, originpp;
        public float accuracy, oldaccuracy, accuracychange, originaccuracy;
        public String apikey;
        public Boolean connected = false;
        private Window window;

        public String events;

        public Osu_Player(Window window, String user, String apikey)
        {
            this.user = user;
            this.apikey = apikey;
            this.window = window;
            MainWindow.config.IniWriteValue("User", "APIkey", apikey);
            
            using (WebClient client = new WebClient())
            {
                try
                {
                    String htmlCode = client.DownloadString("https://osu.ppy.sh/api/get_user?k=" + apikey + "&u=" + user + "&type=string&m=" + MainWindow.mode);
                    ArrayList decode = (ArrayList)JSON.JsonDecode(htmlCode);
                    connected = true;
                    if (decode.Count > 0)
                    {
                        Hashtable list = (Hashtable)((ArrayList)JSON.JsonDecode(htmlCode))[0];
                        userid = int.Parse((String)list["user_id"]);
                        originranked_score = ranked_score = long.Parse((String)list["ranked_score"]);
                        origintotal_score = total_score = long.Parse((String)list["total_score"]);
                        originpprank = pprank = int.Parse((String)list["pp_rank"]);
                        originlevel = level = float.Parse(((String)list["level"]), CultureInfo.InvariantCulture);
                        originpp = pp = float.Parse(((String)list["pp_raw"]), CultureInfo.InvariantCulture);
                        originaccuracy = accuracy = float.Parse(((String)list["accuracy"]), CultureInfo.InvariantCulture);
                        oldpp = pp;
                        ppchange = 0;
                        oldscore = total_score;
                        oldranked_score = ranked_score;
                        oldpprank = pprank;
                        oldlevel = level;
                        oldaccuracy = accuracy;
                    }
                }
                catch (WebException e) { Delegate myDelegate = (Action)showCError; window.Dispatcher.Invoke(myDelegate); connected = false; }
                catch (NullReferenceException e) { Delegate myDelegate = (Action)showIKError; window.Dispatcher.Invoke(myDelegate); connected = false; }
                reconnected = false;
            }
        }

        public String parseBeatmap(Hashtable code)
        {
            String returnvalue = "";
            using (WebClient client = new WebClient())
            {
                try
                {
                    String htmlCode = client.DownloadString("https://osu.ppy.sh/api/get_beatmaps?k=" + apikey  + "&b=" + code["beatmap_id"]);
                    ArrayList decode = (ArrayList)JSON.JsonDecode(htmlCode);
                    if (decode.Count > 0)
                    {
                        Hashtable list = (Hashtable)((ArrayList)JSON.JsonDecode(htmlCode))[MainWindow.mode];
                        returnvalue += list["artist"] + " - " + list["title"] + " [" + list["version"] + "]\n";
                        returnvalue += "BPM : " + list["bpm"] + " - AR : " + list["diff_approach"] + " - Stars : " + list["difficultyrating"] + "\n";
                        returnvalue += "Score : " + String.Format(CultureInfo.InvariantCulture, "{0:#,0.########}", int.Parse((String)code["score"])) + " " + parseModes(int.Parse((String)code["enabled_mods"])) + "\n";
                        returnvalue += "Rank : " + code["rank"];
                    }
                }
                catch (WebException e) { if (!reconnected) { Delegate myDelegate = (Action)showCError; window.Dispatcher.Invoke(myDelegate); } reconnected = false; }
                catch (NullReferenceException e) { Delegate myDelegate = (Action)showIKError; window.Dispatcher.Invoke(myDelegate); reconnected = false; }
            }

            return returnvalue;
        }

        public String parseModes(int code)
        {
            String returnvalue = "";
            if (code - 4194304 >= 0)
            {
                code -= 4194304;
                returnvalue += "[LM] ";
            }
            if (code - 2097152 >= 0)
            {
                code -= 2097152;
                returnvalue += "[Rand] ";
            }
            if (code - 1048576 >= 0)
            {
                code -= 1048576;
                returnvalue += "[FadeIn] ";
            }
            if (code - 524288 >= 0)
            {
                code -= 524288;
                returnvalue += "[8K] ";
            }
            if (code - 262144 >= 0)
            {
                code -= 262144;
                returnvalue += "[7K] ";
            }
            if (code - 131072 >= 0)
            {
                code -= 131072;
                returnvalue += "[6K] ";
            }
            if (code - 65536 >= 0)
            {
                code -= 65536;
                returnvalue += "[5K] ";
            }
            if (code - 32768 >= 0)
            {
                code -= 32768;
                returnvalue += "[4K] ";
            }
            if (code - 16384 >= 0)
            {
                code -= 16384;
                returnvalue += "[PF] ";
            }
            if (code - 8192 >= 0)
            {
                code -= 8192;
                returnvalue += "[AP] ";
            }
            if (code - 4096 >= 0)
            {
                code -= 4096;
                returnvalue += "[SO] ";
            }
            if (code - 2048 >= 0)
            {
                code -= 2048;
                returnvalue += "[Auto] ";
            }
            if (code - 1024 >= 0)
            {
                code -= 1024;
                returnvalue += "[FL] ";
            }
            if (code - 512 >= 0)
            {
                code -= 512;
                returnvalue += "[NC] ";
            }
            if (code - 256 >= 0)
            {
                code -= 256;
                returnvalue += "[HT] ";
            }
            if (code - 128 >= 0)
            {
                code -= 128;
                returnvalue += "[Relax] ";
            }
            if (code - 64 >= 0)
            {
                code -= 64;
                returnvalue += "[DT] ";
            }
            if (code - 32 >= 0)
            {
                code -= 32;
                returnvalue += "[SD] ";
            }
            if (code - 16 >= 0)
            {
                code -= 16;
                returnvalue += "[HR] ";
            }
            if (code - 8 >= 0)
            {
                code -= 8;
                returnvalue += "[HD] ";
            }
            if (code - 4 >= 0)
            {
                code -= 4;
                returnvalue += "[NV] ";
            }
            if (code - 2 >= 0)
            {
                code -= 2;
                returnvalue += "[EZ] ";
            }
            if (code - 1 >= 0)
            {
                code -= 1;
                returnvalue += "[NF] ";
            }
            return returnvalue;
        }

        public Boolean reconnected = false;

        public void updatebeatmaps()
        {
            if (userid > 0 && connected && MainWindow.config.IniReadValue("User", "beatmaps", "false") == "true")
                using (WebClient client = new WebClient())
                {
                    try
                    {
                        String htmlCode = client.DownloadString("https://osu.ppy.sh/api/get_user_recent?k=" + apikey + "&u=" + user + "&type=string&m=" + MainWindow.mode);
                        ArrayList decode = (ArrayList)JSON.JsonDecode(htmlCode);
                        if (decode.Count > 0)
                        {
                            events = "";
                            for (int i = 0; i < 10; i++)
                            {
                                Hashtable list = (Hashtable)((ArrayList)JSON.JsonDecode(htmlCode))[i];
                                if (i != 9)
                                    events += parseBeatmap(list) + "\n\n";
                                else
                                    events += parseBeatmap(list);
                            }
                        }
                    }
                    catch (WebException e) { if (!reconnected) { Delegate myDelegate = (Action)showCError; window.Dispatcher.Invoke(myDelegate); } reconnected = false; }
                    catch (NullReferenceException e) { Delegate myDelegate = (Action)showIKError; window.Dispatcher.Invoke(myDelegate); reconnected = false; }
                }
        }
        public void update()
        {
            if(userid > 0 && connected)
            using (WebClient client = new WebClient())
            {
                try
                {
                    String htmlCode = client.DownloadString("https://osu.ppy.sh/api/get_user?k=" + apikey + "&u=" + userid + "&type=id&m=" + MainWindow.mode);
                    ArrayList decode = (ArrayList)JSON.JsonDecode(htmlCode);
                    reconnected = true;
                    if (decode.Count > 0)
                    {
                        Hashtable list = (Hashtable)((ArrayList)JSON.JsonDecode(htmlCode))[0];
                        userid = int.Parse((String)list["user_id"]);
                        ranked_score = long.Parse((String)list["ranked_score"]);
                        total_score = long.Parse((String)list["total_score"]);
                        pprank = int.Parse((String)list["pp_rank"]);
                        level = float.Parse(((String)list["level"]), CultureInfo.InvariantCulture);
                        pp = float.Parse(((String)list["pp_raw"]), CultureInfo.InvariantCulture);
                        accuracy = float.Parse(((String)list["accuracy"]), CultureInfo.InvariantCulture);
                        if (MainWindow.scoremode == 0)
                        {
                            if (total_score != oldscore)
                            {
                                scorechange = total_score - oldscore;
                                ranked_scorechange = ranked_score - oldranked_score;
                                pprankchange = pprank - oldpprank;

                                int temp = (int)(accuracy * 100000);
                                int temp2 = (int)(oldaccuracy * 100000);
                                int temp3 = temp - temp2;
                                accuracychange = temp3/100000f;

                                temp = (int)(level * 10000);
                                temp2 = (int)(oldlevel * 10000);
                                temp3 = temp - temp2;
                                levelchange = temp3 / 10000f;

                                temp = (int)(pp * 100);
                                temp2 = (int)(oldpp * 100);
                                temp3 = temp - temp2;
                                ppchange = temp3 / 100f;


                                oldpp = pp;
                                oldscore = total_score;
                                oldranked_score = ranked_score;
                                oldpprank = pprank;
                                oldlevel = level;
                                oldaccuracy = accuracy;
                            }
                        }
                        else if (MainWindow.scoremode == 1)
                        {
                            ppchange = pp - originpp;
                            scorechange = total_score - origintotal_score;
                            ranked_scorechange = ranked_score - originranked_score;
                            pprankchange = pprank - originpprank;
                            levelchange = level - originlevel;
                            accuracychange = accuracy - originaccuracy;

                            if (total_score != oldscore)
                            {
                                oldpp = pp;
                                oldscore = total_score;
                                oldranked_score = ranked_score;
                                oldpprank = pprank;
                                oldlevel = level;
                                oldaccuracy = accuracy;
                            }
                        }

                    }
                }
                catch (WebException e) { if (!reconnected) { Delegate myDelegate = (Action)showCError; window.Dispatcher.Invoke(myDelegate); } reconnected = false; }
                catch (NullReferenceException e) { Delegate myDelegate = (Action)showIKError; window.Dispatcher.Invoke(myDelegate); reconnected = false; }
            }
        }

        public void showCError()
        {
            MessageBox.Show(window, "Connection error!");
        }

        public void showIKError()
        {
            MessageBox.Show(window, "Invalid Key!");
        }
    }
}
