using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace osu_Profile.OsuAPIObjects
{
    public class Event
    {
        /// <summary>
        /// The beatmap id
        /// </summary>
        [JsonProperty("beatmap_id")]
        public int BeatmapID { get; set; }

        /// <summary>
        /// The score of the player
        /// </summary>
        [JsonProperty("score")]
        public long Score { get; set; }

        /// <summary>
        /// The mods used
        /// </summary>
        [JsonProperty("enabled_mods")]
        public int Mods { get; set; }

        /// <summary>
        /// The mods used
        /// </summary>
        public string ModsString {
            get{
                int code = Mods;
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
                    code -= 32; // Because it enables also SuddenDeath
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
                    code -= 64; // Because it also enables DoubleTime
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
        }

        /// <summary>
        /// The grade received on the map
        /// </summary>
        [JsonProperty("date")]
        public string Date { get; set; }

        /// <summary>
        /// The grade received on the map
        /// </summary>
        [JsonProperty("rank")]
        public string Grade { get; set; }

        /// <summary>
        /// PP gained on the map
        /// </summary>
        public float PP { get; set; }

        /// <summary>
        /// The beatmap linked to the event
        /// </summary>
        public Beatmap Beatmap { get; set; }

        public void Initialize(string apikey)
        {
            if (BeatmapID == 0)
                return;

            if (!Directory.Exists("Cache"))
                Directory.CreateDirectory("Cache");

            if (File.Exists("Cache\\" + BeatmapID))
            {
                string cache = File.ReadAllText("Cache\\" + BeatmapID);
                if (cache != null && cache != "")
                    Beatmap = JsonConvert.DeserializeObject<Beatmap>(cache);
            }
            else
            {

                bool downloaded = false;
                while (!downloaded)
                {
                    try
                    {
                        WebClient client = new WebClient();
                        string apiReturn = client.DownloadString("http://osu.ppy.sh/api/get_beatmaps?k=" + apikey + "&b=" + BeatmapID);
                        apiReturn = apiReturn.Substring(1, apiReturn.Length - 2);
                        Beatmap = JsonConvert.DeserializeObject<Beatmap>(apiReturn);
                        File.WriteAllText("Cache\\" + BeatmapID, apiReturn);
                        downloaded = true;
                    }
                    catch (Exception) { downloaded = false; }
                }
            }
        }
    }
}
