using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Admin;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Utils;
using Microsoft.Extensions.Logging;
using scoutzknivez;
using System.Drawing;

namespace ScoutzKnivez;

public class ScoutzKnivez : BasePlugin, IPluginConfig<ScoutzKnivezConfig>
{
    public override string ModuleName => "ScoutzKnivez";
    public override string ModuleDescription => "https://github.com/asapverneri/CS2-ScoutzKnivez";
    public override string ModuleAuthor => "verneri";
    public override string ModuleVersion => "1.4";

    HashSet<ulong> Hiding = new HashSet<ulong>();
    public ScoutzKnivezConfig Config { get; set; } = new();

    public void OnConfigParsed(ScoutzKnivezConfig config)
	{
        Config = config;
    }

    public override void Load(bool hotReload)
    {

        RegisterEventHandler<EventGameStart>(OnGameStart);
        RegisterEventHandler<EventRoundStart>(OnRoundStart);
        RegisterEventHandler<EventRoundEnd>(OnRoundEnd);
        RegisterEventHandler<EventPlayerSpawn>(OnPlayerSpawn);
        RegisterEventHandler<EventPlayerDeath>(OnPlayerDeath);

        AddCommand($"{Config.FovCommand}", "Set fov", FovCommand);
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
        if (AdminManager.PlayerHasPermissions(player, Config.VipFlag) && Config.VipFeatures && Config.VIPDeagle)
        {
            player.GiveNamedItem("weapon_deagle");
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
        if (!AdminManager.PlayerHasPermissions(attacker, Config.KillSoundFlag))
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
        if (!AdminManager.PlayerHasPermissions(player, Config.FovFlag)){
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