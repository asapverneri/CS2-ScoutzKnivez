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

        [JsonPropertyName("MaxVelocity")]
        public string MaxVelocity { get; set; } = "10000";

        [JsonPropertyName("AirAccelerate")]
        public string AirAccelerate { get; set; } = "12";

        [JsonPropertyName("ArmorValue")]
        public int ArmorValue { get; set; } = 100;

        [JsonPropertyName("TeamWinningNotify")]
        public bool WinningNotify { get; set; } = true;        

        [JsonPropertyName("KillSound")]
        public bool KillSound { get; set; } = true;

        [JsonPropertyName("KillSoundFlag")]
        public string KillSoundFlag { get; set; } = "";

        [JsonPropertyName("KillSoundPath")]
        public string KillSoundPath { get; set; } = "sounds/training/timer_bell";

        [JsonPropertyName("Fov")]
        public bool Fov { get; set; } = true;

        [JsonPropertyName("FovFlag")]
        public string FovFlag { get; set; } = "";

        [JsonPropertyName("FovCommand")]
        public string FovCommand { get; set; } = "css_fov";

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

        [JsonPropertyName("VIPHealthShot")]
        public bool VipHealthShot { get; set; } = true;

        // DISCORD STUFF
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
