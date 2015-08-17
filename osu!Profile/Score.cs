using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace osu_Profile
{
    public class Score
    {
        // <summary>
        // The beatmap id
        // </summary>
        [JsonProperty("beatmap_id", NullValueHandling = NullValueHandling.Ignore)]
        public int Beatmap_ID { get; set; }

        // <summary>
        // The score of the player
        // </summary>
        [JsonProperty("score", NullValueHandling = NullValueHandling.Ignore)]
        public int Points { get; set; }

        // <summary>
        // The username of the player
        // </summary>
        [JsonProperty("username", NullValueHandling = NullValueHandling.Ignore)]
        public string Username { get; set; }

        // <summary>
        // The max combo reached during the game
        // </summary>
        [JsonProperty("maxcombo", NullValueHandling = NullValueHandling.Ignore)]
        public int Max_Combo { get; set; }

        // <summary>
        // The number of 300
        // </summary>
        [JsonProperty("count300", NullValueHandling = NullValueHandling.Ignore)]
        public int Count_300 { get; set; }

        // <summary>
        // The number of 100
        // </summary>
        [JsonProperty("count100", NullValueHandling = NullValueHandling.Ignore)]
        public int Count_100 { get; set; }

        // <summary>
        // The number of 50
        // </summary>
        [JsonProperty("count50", NullValueHandling = NullValueHandling.Ignore)]
        public int Count_50 { get; set; }

        // <summary>
        // The number of misses
        // </summary>
        [JsonProperty("countmiss", NullValueHandling = NullValueHandling.Ignore)]
        public int Count_Miss { get; set; }

        // <summary>
        // The number of Katu
        // </summary>
        [JsonProperty("countkatu", NullValueHandling = NullValueHandling.Ignore)]
        public int Count_Katu { get; set; }

        // <summary>
        // The number of Geki
        // </summary>
        [JsonProperty("countgeki", NullValueHandling = NullValueHandling.Ignore)]
        public int Count_Geki { get; set; }

        // <summary>
        // If full combo
        // Values : 1 if perfect, 0 else
        // </summary>
        [JsonProperty("perfect", NullValueHandling = NullValueHandling.Ignore)]
        public int Perfect { get; set; }

        // <summary>
        // The mods used
        // </summary>
        [JsonProperty("enabled_mods", NullValueHandling = NullValueHandling.Ignore)]
        public int Enabled_Mods { get; set; }

        // <summary>
        // The user ID of the player
        // </summary>
        [JsonProperty("user_id", NullValueHandling = NullValueHandling.Ignore)]
        public int User_ID { get; set; }

        // <summary>
        // The date of the game
        // </summary>
        [JsonProperty("date", NullValueHandling = NullValueHandling.Ignore)]
        public string Date { get; set; }

        // <summary>
        // The grade got in the game
        // </summary>
        [JsonProperty("rank", NullValueHandling = NullValueHandling.Ignore)]
        public string Rank { get; set; }

        // <summary>
        // The PPs got by the player with this game
        // </summary>
        [JsonProperty("pp", NullValueHandling = NullValueHandling.Ignore)]
        public float PP { get; set; }

        // <summary>
        // The mods used
        // </summary>
        public string ModsString
        {
            get
            {
                int code = Enabled_Mods;
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
    }
}
