using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Serilog.Core;

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

        public static async Task CompareVersions(string ModuleVersion, string githubFileUrl, ILogger Logger)
        {
            string githubVersion = await GetGithubVersion(githubFileUrl);

            if (githubVersion == null)
            {
                Logger.LogWarning("Unable to check for updates due to a problem fetching the GitHub version.");
                return; 
            }

            if (ModuleVersion == githubVersion)
            {
                Logger.LogInformation("=====================");
                Logger.LogInformation("Plugin is up to date.");
                Logger.LogInformation("=====================");
            }
            else
            {
                Logger.LogWarning("=================================================");
                Logger.LogWarning("Plugin is outdated! Please update it shortly.");
                Logger.LogWarning("https://github.com/asapverneri/CS2-ScoutzKnivez");
                Logger.LogWarning("=================================================");
            }
        }
    }
}