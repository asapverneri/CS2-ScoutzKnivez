using CounterStrikeSharp.API.Core;
using System.Text.Json.Serialization;

namespace ScoutzKnivez
{
    public class ScoutzKnivezConfig : BasePluginConfig
    {

        // GENERAL STUFF
        [JsonPropertyName("PlayerWelcomeMessage")]
        public bool PlayerWelcomeMessage { get; set; } = true;

        [JsonPropertyName("WelcomeMessageTimer")]
        public float WelcomeMessageTimer { get; set; } = 5.0f;

        [JsonPropertyName("AutoBunnyHop")]
        public bool Autobunnyhop { get; set; } = true;

        [JsonPropertyName("Gravity")]
        public string Gravity { get; set; } = "200";

        [JsonPropertyName("ArmorValue")]
        public int ArmorValue { get; set; } = 100;

        [JsonPropertyName("TeamWinningNotify")]
        public bool WinningNotify { get; set; } = true;

        [JsonPropertyName("Fov")]
        public bool Fov { get; set; } = true;

        [JsonPropertyName("FovCommand")]
        public string FovCommand { get; set; } = "css_fov";

        [JsonPropertyName("JoinTeam")]
        public bool JoinTeam { get; set; } = true;

        [JsonPropertyName("JoinTCommand")]
        public string JoinTCommand { get; set; } = "css_t";

        [JsonPropertyName("JoinCTCommand")]
        public string JoinCTCommand { get; set; } = "css_ct";

        [JsonPropertyName("JoinSpecCommand")]
        public string JoinSpecCommand { get; set; } = "css_spec";

        [JsonPropertyName("HideLegs")]
        public bool HideLegs { get; set; } = true;

        [JsonPropertyName("HideLegsCommand")]
        public string HideLegsCommand { get; set; } = "css_legs";

        // VIP STUFF
        [JsonPropertyName("VIPFeatures")]
        public bool VipFeatures { get; set; } = true;

        [JsonPropertyName("VIPFlag")]
        public string VipFlag { get; set; } = "@css/vip";

        [JsonPropertyName("VIPPerkMessage")]
        public bool VipPerkMsg { get; set; } = true;

        [JsonPropertyName("VipDamageMultiplier")]
        public float VipDamageMultiplier { get; set; } = 1.5f;

        [JsonPropertyName("VIPArmor")]
        public int VipArmor { get; set; } = 120;

        [JsonPropertyName("VIPHealtShot")]
        public bool VipHealtShot { get; set; } = true;

        // DISCORD STUFF
        [JsonPropertyName("LogToDiscord")]
        public bool LogToDiscord { get; set; } = false;

        [JsonPropertyName("DiscordLogWebhook")]
        public string DiscordLogWebhook { get; set; } = "";

        [JsonPropertyName("LogTeamWinning")]
        public bool LogTeamWinning { get; set; } = false;

        [JsonPropertyName("LogFovUsage")]
        public bool LogFovUsage { get; set; } = false;

        [JsonPropertyName("LogRounds")]
        public bool LogRounds { get; set; } = false;

        [JsonPropertyName("RequestPlayers")]
        public bool RequestPlayers { get; set; } = false;

        [JsonPropertyName("RequestPlayersCommand")]
        public string RequestPlayersCommand { get; set; } = "css_request";

        [JsonPropertyName("DiscordWebhook")]
        public string DiscordWebhook { get; set; } = "";

        // DEBUG
        [JsonPropertyName("DebugMode")]
        public bool DebugMode { get; set; } = false;

    }
}
