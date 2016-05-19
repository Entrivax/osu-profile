using Newtonsoft.Json;

namespace osu_Profile.OsuAPIObjects
{
    public class Beatmap
    {
        /// <summary>
        /// The artist of the music
        /// </summary>
        [JsonProperty("artist")]
        public string Artist { get; set; }

        /// <summary>
        /// The title of the music
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// The beatmap id
        /// </summary>
        [JsonProperty("beatmap_id")]
        public int ID { get; set; }

        /// <summary>
        /// The beatmapset id
        /// </summary>
        [JsonProperty("beatmapset_id")]
        public int SetID { get; set; }

        /// <summary>
        /// The BPM of the music
        /// </summary>
        [JsonProperty("bpm")]
        public float BPM { get; set; }

        /// <summary>
        /// The creator of the beatmap
        /// </summary>
        [JsonProperty("creator")]
        public string Creator { get; set; }

        /// <summary>
        /// The number of stars of the beatmap
        /// </summary>
        [JsonProperty("difficultyrating")]
        public float Stars { get; set; }

        /// <summary>
        /// The circle size
        /// </summary>
        [JsonProperty("diff_size")]
        public float CircleSize { get; set; }

        /// <summary>
        /// The overall difficulty
        /// </summary>
        [JsonProperty("diff_overall")]
        public float OverallDifficulty { get; set; }

        /// <summary>
        /// The approach rate
        /// </summary>
        [JsonProperty("diff_approach")]
        public float ApproachRate { get; set; }

        /// <summary>
        /// The health drain
        /// </summary>
        [JsonProperty("diff_drain")]
        public float HealthDrain { get; set; }

        /// <summary>
        /// The difficulty name
        /// </summary>
        [JsonProperty("version")]
        public string Difficulty { get; set; }

        /// <summary>
        /// The mode of the beatmap (0=osu!, 1=Taiko, 2=CtB, 3=osu!mania)
        /// </summary>
        [JsonProperty("mode")]
        public int GameMode { get; set; }
    }
}
