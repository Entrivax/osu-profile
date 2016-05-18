using Newtonsoft.Json;

namespace osu_Profile.OsuAPIObjects
{
    public class Player
    {
        // <summary>
        // The user id of the player
        // </summary>
        [JsonProperty("user_id", NullValueHandling = NullValueHandling.Ignore)]
        public int ID { get; set; }

        // <summary>
        // The user name of the player
        // </summary>
        [JsonProperty("username")]
        public string Username { get; set; }

        // <summary>
        // Counts the best individual score on each ranked and approved beatmaps
        // </summary>
        [JsonProperty("ranked_score", NullValueHandling = NullValueHandling.Ignore)]
        public long RankedScore { get; set; }

        // <summary>
        // Counts every score on ranked and approved beatmaps
        // </summary>
        [JsonProperty("total_score", NullValueHandling = NullValueHandling.Ignore)]
        public long Score { get; set; }

        // <summary>
        // The rank in performance ranking of the player
        // </summary>
        [JsonProperty("pp_rank", NullValueHandling = NullValueHandling.Ignore)]
        public int PPRank { get; set; }

        // <summary>
        // The rank in performance ranking of the player in the country
        // </summary>
        [JsonProperty("pp_country_rank", NullValueHandling = NullValueHandling.Ignore)]
        public int PPCountryRank { get; set; }

        // <summary>
        // The level of the player
        // </summary>
        [JsonProperty("level", NullValueHandling = NullValueHandling.Ignore)]
        public float Level { get; set; }

        // <summary>
        // The number of PP of the player
        // </summary>
        [JsonProperty("pp_raw", NullValueHandling = NullValueHandling.Ignore)]
        public float PP { get; set; }

        // <summary>
        // The accuracy of the player
        // </summary>
        [JsonProperty("accuracy", NullValueHandling = NullValueHandling.Ignore)]
        public float Accuracy { get; set; }

        // <summary>
        // The number of maps played by the player
        // </summary>
        [JsonProperty("playcount", NullValueHandling = NullValueHandling.Ignore)]
        public int PlayCount { get; set; }

        // <summary>
        // User's top scores
        // <summary>
        public Score[] TopRanks { get; set;}

        // <summary>
        // Profile mode
        // </summary>
        public int Mode { get; set; }
    }
}
