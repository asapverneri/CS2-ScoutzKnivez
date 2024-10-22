## 🎮 CS2 ScoutzKnivez

This is a legendary gamemode made for CS2. It's in early state, but it should be pretty stable.
With this plugin, you only need maps where to play and it has English language ready to go.
This is intended to be a feature rich gamemode with many features already included in one plugin.
If you want to create translations or anything else, feel free to make a pull request!
I've made this for my future ScoutzKnivez server that I might create and I'll be working on this whenever I get some free time.
Tested on Windows for now, but should work with Linux aswell i guess.

![GitHub tag (with filter)](https://img.shields.io/github/v/tag/asapverneri/CS2-ScoutzKnivez?style=for-the-badge&label=Version)
![Last Commit](https://img.shields.io/github/last-commit/asapverneri/CS2-ScoutzKnivez?style=for-the-badge)

> [!CAUTION]  
> This is Beta version and there might be bugs.

---

## 📋 Features / Roadmap

- [x] Automatic weapon management
- [x] Wide configurations
- [x] Built-in FOV Changer
- [x] Built-in Hidelegs
- [x] VIP perks
- [x] Team win notifications
- [x] Multi language support
- [x] Lightweight
- [x] Built-in discord logs
- [x] Welcome message
- [x] Player requests via discord

- [ ] Settings menu for admins?
- [ ] More VIP perks?
- [ ] More discord logs
- [ ] DoubleJump?
- [ ] Bullet tracers?
- [ ] Some kind of basic point system?
- [ ] hitmarker?

---

## 📦 Installion

- Install latest [CounterStrike Sharp](https://github.com/roflmuffin/CounterStrikeSharp) & [Metamod:Source](https://www.sourcemm.net/downloads.php/?branch=master)
- Download the latest release from the releases tab and copy it into the addons folder.

**Example config:**
```json
{
  // GENERAL
  "PlayerWelcomeMessage": true,  // Enable/disable player welcome message.
  "WelcomeMessageTimer": 5,      // Duration (in seconds) before displaying the welcome message.
  "AutoBunnyHop": true,          // Enable/disable bunnyhopping.
  "Gravity": "200",              // Customize gravity setting.
  "ArmorValue": 100,             // Modify players armor. (0 = Disable)
  "TeamWinningNotify": true,     // Enable/disable Team winning notifications.
  "Fov": true,                   // Enable/disable FOV command.
  "FovCommand": "css_fov",       // Customize fov command.
  "JoinTeam": true,              // Enable/disable teamjoin commands.
  "JoinTCommand": "css_t",       // Customize terrorist command.
  "JoinCTCommand": "css_ct",     // Customize ct command.
  "JoinSpecCommand": "css_spec", // Customize spec command.
  "HideLegs": true,              // Enable/disable hidelegs command.
  "HideLegsCommand": "css_legs", // Customize hidelegs command.

  // VIP
  "VIPFeatures": true,           // Enable/disable VIP features.
  "VIPFlag": "@css/vip",         // Customize VIP flag.
  "VIPPerkMessage": true,        // Enable/disable VIP perk message.
  "VipDamageMultiplier": 2,      // Change damagemultiplier for VIP's. (0 = Disable)
  "VIPArmor": 120,               // Change armor for VIP's.
  "VIPHealtShot": true,          // Enable/disable healthshot for VIP's.

  // DISCORD
  "LogToDiscord": false,          // Enable/disable discord logging.
  "DiscordLogWebhook": "",        // Set discord webhook for logging.
  "LogTeamWinning": false,        // Enable/disable team winning logs.
  "LogFovUsage": false,           // Enable/disable fov usage logs.
  "LogRounds": false,             // Enable/disable round start/end logs.
  "RequestPlayers": false,        // Enable/disable requestplayer feature
  "RequestPlayersCommand": "css_request",      // Customize command.
  "DiscordWebhook": "",           // Set discord webhook for requestplayer.

  // DEBUGGING
  "DebugMode": false,
  "ConfigVersion": 1
}
```

---

## ⌨️ Commands
| Command         | Description                                                          | Permissions |
|-----------------|----------------------------------------------------------------------|-------------|
| !fov            | Enable/disable FOV command (Changeable)                              | -           |
| !t              | Join terrorist (Changeable)                                          | -           |
| !ct             | Join CT (Changeable)                                                 | -           |
| !spec           | Join spec (Changeable)                                               | -           |
| !request        | Request players to join the server via discord (Changeable)          | -           |
| !hidelegs       | Hide your lower body (Changeable)                                    | -           |

---

## 📫 Contact

<div align="center">
  <a href="https://discordapp.com/users/367644530121637888">
    <img src="https://img.shields.io/badge/Discord-7289DA?style=for-the-badge&logo=discord&logoColor=white" alt="Discord" />
  </a>
  <a href="https://steamcommunity.com/id/vvernerii/">
    <img src="https://img.shields.io/badge/Steam-000000?style=for-the-badge&logo=steam&logoColor=white" alt="Steam" />
  </a>
</div>

---
