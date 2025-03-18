using CounterStrikeSharp.API.Core;
using System.Text.Json.Serialization;

namespace ScoutzKnivez
{
    public class ScoutzKnivezConfig : BasePluginConfig
    {

        // GENERAL STUFF
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

        [JsonPropertyName("VIPArmor")]
        public bool VipArmor { get; set; } = true;

        [JsonPropertyName("VIPArmorValue")]
        public int VipArmorValue { get; set; } = 120;

        [JsonPropertyName("VIPHealthShot")]
        public bool VipHealthShot { get; set; } = true;

        [JsonPropertyName("VIPDeagle")]
        public bool VIPDeagle { get; set; } = true;


        // DEBUG
        [JsonPropertyName("DebugMode")]
        public bool DebugMode { get; set; } = false;

    }
}
