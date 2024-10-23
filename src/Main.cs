using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Core.Translations;
using CounterStrikeSharp.API.Modules.Admin;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Config;
using CounterStrikeSharp.API.Modules.Cvars;
using CounterStrikeSharp.API.Modules.Entities;
using CounterStrikeSharp.API.Modules.Memory;
using CounterStrikeSharp.API.Modules.Memory.DynamicFunctions;
using CounterStrikeSharp.API.Modules.Utils;
using Microsoft.Extensions.Logging;
using scoutzknivez;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Linq;

namespace ScoutzKnivez;

public class ScoutzKnivez : BasePlugin, IPluginConfig<ScoutzKnivezConfig>
{
    public override string ModuleName => "ScoutzKnivez";
    public override string ModuleDescription => "https://github.com/asapverneri/CS2-ScoutzKnivez";
    public override string ModuleAuthor => "verneri";
    public override string ModuleVersion => "1.3";

    HashSet<ulong> Hiding = new HashSet<ulong>();
    public ScoutzKnivezConfig Config { get; set; } = new();

    public void OnConfigParsed(ScoutzKnivezConfig config)
	{
        Config = config;
    }

    public override void Load(bool hotReload)
    {

        RegisterEventHandler<EventGameStart>(OnGameStart);
        RegisterEventHandler<EventPlayerConnectFull>(OnPlayerConnectFull);
        RegisterEventHandler<EventRoundStart>(OnRoundStart);
        RegisterEventHandler<EventRoundEnd>(OnRoundEnd);
        RegisterEventHandler<EventPlayerSpawn>(OnPlayerSpawn);
        RegisterEventHandler<EventPlayerDeath>(OnPlayerDeath);

        AddCommand($"{Config.FovCommand}", "Set fov", FovCommand);
        AddCommand($"{Config.RequestPlayersCommand}", "request players", OnCommandRequest);
        AddCommand($"{Config.HideLegsCommand}", "Hide legs", HideLegsCommand);

        Console.WriteLine(" ");
        Console.WriteLine($"█▀ █▀▀ █▀█ █░█ ▀█▀ ▀█ █▄▀ █▄░█ █ █░█ █▀▀ ▀█");
        Console.WriteLine($"▄█ █▄▄ █▄█ █▄█ ░█░ █▄ █░█ █░▀█ █ ▀▄▀ ██▄ █▄");
        Console.WriteLine($"    LOADED SUCCEFFULLY! (VERSION v{ModuleVersion}) ");
        Console.WriteLine(" ");

        Task.Run(async () =>
        {
            bool isUpToDate = await Utils.CompareVersions(ModuleVersion, "https://raw.githubusercontent.com/asapverneri/CS2-ScoutzKnivez/main/pluginversion");

            if (isUpToDate)
            {
                Logger.LogInformation("=====================");
                Logger.LogInformation("Plugin is up to date.");
                Logger.LogInformation("=====================");
            }
            else
            {
                Logger.LogWarning("=================================================");
                Logger.LogWarning("Plugin is outdated! Please update it shortly.");
                Logger.LogWarning("GitHub Repository: github.com/asapverneri/CS2-ScoutzKnivez");
                Logger.LogWarning("=================================================");
            }
        });

        if (Config.DebugMode)
        {
            Logger.LogWarning($"===========================================");
            Logger.LogWarning($"       YOU ARE RUNNING ON DEBUGMODE!       ");
            Logger.LogWarning($"        DISABLE IT FROM THE CONFIG.        ");
            Logger.LogWarning($"===========================================");
        }

        if (Config.RequestPlayers && Config.DiscordWebhook == "")
        {
            Logger.LogInformation($"===========================================");
            Logger.LogInformation($"      YOU SHOULD SET WEBHOOK IN ORDER      ");
            Logger.LogInformation($"      TO GET DISCORD FEATURES TO WORK      ");
            Logger.LogInformation($"===========================================");
        }

    }

    public override void Unload(bool hotReload)
    {
        Console.WriteLine(" ");
        Console.WriteLine($"█▀ █▀▀ █▀█ █░█ ▀█▀ ▀█ █▄▀ █▄░█ █ █░█ █▀▀ ▀█");
        Console.WriteLine($"▄█ █▄▄ █▄█ █▄█ ░█░ █▄ █░█ █░▀█ █ ▀▄▀ ██▄ █▄");
        Console.WriteLine($"   UNLOADED SUCCEFFULLY! (VERSION v{ModuleVersion})");
        Console.WriteLine(" ");
    }

    private HookResult OnGameStart(EventGameStart @event, GameEventInfo info)
    {
        Utils.ExecuteCvars(Config, DebugMode);

        DebugMode($"EventGameStart");
        return HookResult.Continue;
    }

    private HookResult OnRoundStart(EventRoundStart @event, GameEventInfo info)
    {
        Utils.ExecuteCvars(Config, DebugMode);

        DebugMode($"EventRoundStart");
        return HookResult.Continue;
    }

    private HookResult OnRoundEnd(EventRoundEnd @event, GameEventInfo info)
    {
        var alivePlayers = Utilities.GetPlayers().Where(ply => ply.IsValid && ply.PawnIsAlive).ToList();

        if(Config.WinningNotify)
        {
            if (alivePlayers.Count == 1) {
                var lastPlayer = alivePlayers[0];
                Server.PrintToChatAll($"{Localizer["last.alive", lastPlayer.PlayerName]}");
            }
            else if (alivePlayers.Count > 1) {
                var ctPlayers = alivePlayers.Where(ply => ply.Team == CsTeam.CounterTerrorist).ToList();
                var tPlayers = alivePlayers.Where(ply => ply.Team == CsTeam.Terrorist).ToList();

                string winningTeam = ctPlayers.Count > tPlayers.Count ? $"{Localizer["CT"]}" : $"{Localizer["T"]}";

                Server.PrintToChatAll($"{Localizer["team.won", winningTeam]}");
            }
        }
        DebugMode($"EventRoundEnd");
        return HookResult.Continue;
    }

    private HookResult OnPlayerConnectFull(EventPlayerConnectFull @event, GameEventInfo info)
    {
        if (@event == null) return HookResult.Continue;
        var player = @event.Userid;
        if (player == null || !player.IsValid || player.IsBot) return HookResult.Continue;
        var Name = player.PlayerName;

        if (Config.PlayerWelcomeMessage)
        {
            AddTimer(Config.WelcomeMessageTimer, () =>
            {
                player.PrintToChat($"{Localizer["welcomemessage", Name]}");
                DebugMode($"EventPlayerConnectFull Welcome message");
            });
        }
        DebugMode($"EventPlayerConnectFull");
        return HookResult.Continue;
    }

    private HookResult OnPlayerSpawn(EventPlayerSpawn @event, GameEventInfo info)
    {
        if (@event == null) return HookResult.Continue;
        var player = @event.Userid;
        if (player == null || !player.IsValid) return HookResult.Continue;

        player.RemoveWeapons();
        AddTimer(0.10f, () =>
        {
            GiveGoods(player);
        });

        if (Hiding.Contains(player.SteamID))
            SpawnFixnextround(player);

        DebugMode($"EventPlayerSpawn");
        return HookResult.Continue;
    }

    private void GiveGoods(CCSPlayerController player)
    {
        if (player == null || !player.IsValid) return;

        player.RemoveWeapons();
        player.GiveNamedItem("weapon_ssg08");
        player.GiveNamedItem("weapon_knife");


        var playerPawn = player.PlayerPawn.Value;
        new CCSPlayer_ItemServices(playerPawn.ItemServices.Handle).HasHelmet = true;
        playerPawn.ArmorValue = Config.ArmorValue;

        if (AdminManager.PlayerHasPermissions(player, Config.VipFlag) && Config.VipFeatures)
        {
            new CCSPlayer_ItemServices(playerPawn.ItemServices.Handle).HasHelmet = true;
            playerPawn.ArmorValue = Config.VipArmor;
        }
        if (AdminManager.PlayerHasPermissions(player, Config.VipFlag) && Config.VipFeatures && Config.VipHealthShot)
        {
            player.GiveNamedItem("weapon_healthshot");
        }
        if (AdminManager.PlayerHasPermissions(player, Config.VipFlag) && Config.VipFeatures && Config.VipPerkMsg)
        {
            player.PrintToChat($"{Localizer["vip.perksgiven"]}");
        }
        DebugMode($"GiveGoods Fired");
    }
    private HookResult OnPlayerDeath(EventPlayerDeath @event, GameEventInfo info)
    {
        if (!Config.KillSound) 
            return HookResult.Continue;
        if (@event == null) 
            return HookResult.Continue;
        var victim = @event.Userid;
        var attacker = @event.Attacker;

        if (victim == null || !victim.IsValid) 
            return HookResult.Continue;
        if (attacker == null || !attacker.IsValid || attacker.IsBot) 
            return HookResult.Continue;
        if (AdminManager.PlayerHasPermissions(attacker, Config.KillSoundFlag))
            return HookResult.Continue;
        
        string killsound = Config.KillSoundPath;
        if (attacker != victim)
        {
            attacker.ExecuteClientCommand("play " + killsound);
            DebugMode($"Killsound played (OnPlayerDeath)");
        }
        return HookResult.Continue;
    }

    public void FovCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (player == null) return;
        if (!player.PlayerPawn.IsValid) return;
        if (!Config.Fov) {
            command.ReplyToCommand($"{Localizer["feature.disabled"]}");
            return;
        }
        if (AdminManager.PlayerHasPermissions(player, Config.FovFlag)){
            player.PrintToChat($"{Localizer["no.access"]}");
            return;
        }

        if (!Int32.TryParse(command.GetArg(1), out var desiredFov)) return;

        player.DesiredFOV = (uint)desiredFov;
        Utilities.SetStateChanged(player, "CBasePlayerController", "m_iDesiredFOV");
        command.ReplyToCommand($"{Localizer["fov.set", desiredFov]}");

    }

    public void HideLegsCommand(CCSPlayerController? player, CommandInfo command)
    {
        if (player == null || !player.IsValid || !player.PawnIsAlive || player.Team == CsTeam.Spectator || player.Team == CsTeam.None)
            return;

        if (!Config.HideLegs){
            command.ReplyToCommand($"{Localizer["feature.disabled"]}");
            return;
        }

        if (!Hiding.Contains(player.SteamID))
        {
            Hiding.Add(player.SteamID);
            player.PlayerPawn.Value.Render = Color.FromArgb(254, 255, 255, 255);
            Utilities.SetStateChanged(player.PlayerPawn.Value, "CBaseModelEntity", "m_clrRender");
            command.ReplyToCommand($"{Localizer["legs.hidden"]}");
        }
        else
        {
            Hiding.Remove(player.SteamID);
            player.PlayerPawn.Value.Render = Color.FromArgb(255, 255, 255, 255);
            Utilities.SetStateChanged(player.PlayerPawn.Value, "CBaseModelEntity", "m_clrRender");
            command.ReplyToCommand($"{Localizer["legs.shown"]}");
        }
    }

    public void OnCommandRequest(CCSPlayerController? player, CommandInfo command)
    {
        if (!player.IsValid) return;
        if (!Config.RequestPlayers) {
            command.ReplyToCommand($"{Localizer["feature.disabled"]}");
            return;
        }

        try
        {
            if (command.ArgCount < 1)
            {
                player.PrintToChat("Bro..");
                return;
            }
            string message = command.GetArg(1);
            if (message == null || message.Length < 1)
            {
                player.PrintToChat("Please type something");
                return;
            }

            _ = SendDiscordRequest(player.PlayerName, message);
            command.ReplyToCommand($"{Localizer["request.sent"]}");

        } catch (Exception ex)
        {
            DebugMode($"Cannot send discord request (OnCommandRequest) ({ex.Message})");
        }
        return;
    }


    private void SpawnFixnextround(CCSPlayerController? player)
    {
        if (player == null) return;
        AddTimer(0.66f, () =>
        {
            player.PlayerPawn.Value.Render = Color.FromArgb(254, 255, 255, 255);
            Utilities.SetStateChanged(player.PlayerPawn.Value, "CBaseModelEntity", "m_clrRender");
            DebugMode($"SpawnFixnextround (Hidelegs)");
        });
    }






    // Discord shit

    public async Task SendDiscordRequest(string PlayerName, string message)
    {
        using (var httpClient = new HttpClient())
        {
            var embed = new
            {
                title = $"{Localizer["discord.title"]}",
                description = $"{Localizer["discord.request", PlayerName, message]}",
                color = 65280,
                footer = new
                {
                    text = $"{Localizer["discord.footer", ModuleVersion]}"
                }
            };

            var payload = new
            {
                embeds = new[] { embed }
            };

            var jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(Config.DiscordWebhook, content);

            if (!response.IsSuccessStatusCode)
            {
                DebugMode($"Failed to send request message to Discord! code: {response.StatusCode}");
            }
        }
    }


    //DEBUGMODE
    private void DebugMode(string message)
    {
        if (Config.DebugMode)
        {
            Server.PrintToChatAll($"[DEBUG] {message}");
            Logger.LogInformation($"[DEBUG] {message}");
        }
    }
}