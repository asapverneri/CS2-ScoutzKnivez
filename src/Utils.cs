using CounterStrikeSharp.API;
using ScoutzKnivez;

namespace scoutzknivez
{
    internal class Utils
    {
        public static async Task<string> GetGithubVersion(string githubFileUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                return (await client.GetStringAsync(githubFileUrl)).Trim(); 
            }
        }

        public static async Task<bool> CompareVersions(string moduleVersion, string githubFileUrl)
        {
            string githubVersion = await GetGithubVersion(githubFileUrl);

            if (githubVersion == null)
            {
                Console.WriteLine("Unable to check for updates due to a problem fetching the GitHub version.");
                return false; 
            }

            return moduleVersion == githubVersion; 
        }

        public static void ExecuteCvars(ScoutzKnivezConfig config, Action<string> debugMode)
        {
            Server.ExecuteCommand($"sv_maxvelocity {config.MaxVelocity}");
            Server.ExecuteCommand($"sv_gravity {config.Gravity}");
            Server.ExecuteCommand("sv_staminamax 0");
            Server.ExecuteCommand("sv_staminajumpcost 0");
            Server.ExecuteCommand("sv_staminalandcost 0");
            Server.ExecuteCommand("sv_staminarecoveryrate 0");
            Server.ExecuteCommand($"sv_airaccelerate {config.AirAccelerate}");
            Server.ExecuteCommand("mp_buytime 0");
            Server.ExecuteCommand("mp_startmoney 0");
            Server.ExecuteCommand("mp_maxmoney 0");
            Server.ExecuteCommand("mp_playercashawards 0");

            if (config.Autobunnyhop)
            {
                Server.ExecuteCommand($"sv_autobunnyhopping 1");
                Server.ExecuteCommand($"sv_enablebunnyhopping 1");
            }
            else
            {
                Server.ExecuteCommand($"sv_autobunnyhopping 0");
                Server.ExecuteCommand($"sv_enablebunnyhopping 0");
            }

            debugMode("Executed Cvars");
        }
    }
}