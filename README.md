<div align="center">
  <img src="https://i.ibb.co/VpkWD2C3/SCOUTZ-N-KNIVEZ-2.png" width="500"/>
  <h3>🎮 CS2 ScoutzKnivez</h3>
  <p>This is a legendary gamemode made for CS2. It's in early state, but it should be pretty stable.
  <br>With this plugin, you only need maps where to play and it has English language ready to go.
  <br>
  <br>If you want to create translations or anything else, feel free to make a pull request!
  <br>I've made this for my future ScoutzKnivez server that I might create and I'll be working on this whenever I get some free time.
  <br>Tested on Windows for now, but should work with Linux aswell i guess.</p>
</div>
<div align="center">
  <img src="https://img.shields.io/github/v/tag/asapverneri/CS2-ScoutzKnivez?style=for-the-badge&label=Version" alt="GitHub tag (with filter)" />
  <img src="https://img.shields.io/github/last-commit/asapverneri/CS2-ScoutzKnivez?style=for-the-badge" alt="Last Commit" />
  <blockquote>
    <strong>⚠️ <span style="color:red;">CAUTION</span></strong>  
    <br><span style="color:red;">This is a Beta version, and there might be bugs.</span>
  </blockquote>
</div>

---

## 📋 Features / Roadmap

<p>✅ Automatic weapon management</p>
<p>✅ Wide configurations</p>
<p>✅ Built-in FOV Changer</p>
<p>✅ Built-in Hidelegs</p>
<p>✅ Built-in killsound</p>
<p>✅ VIP perks</p>
<p>✅ Team win notifications</p>
<p>✅ Multi language support</p>
<p>✅ Version checker</p>
<br>
<p>⬜ Settings menu for admins?</p>
<p>⬜ More VIP perks?</p>

---

## 📦 Installion

- Install latest [CounterStrike Sharp](https://github.com/roflmuffin/CounterStrikeSharp) & [Metamod:Source](https://www.sourcemm.net/downloads.php/?branch=master)
- Download the latest release from the releases tab and copy it into the csgo folder.
You might find maps for this gamemode [Here](https://steamcommunity.com/workshop/browse/?appid=730&searchtext=scout&childpublishedfileid=0&browsesort=textsearch&section=)

**Example config:**
```json
{
  // GENERAL
  "AutoBunnyHop": true,          // Enable/disable bunnyhopping.
  "Gravity": "200",              // Customize gravity value.
  "MaxVelocity": "10000",        // Customize maxvelocity value.
  "AirAccelerate": "12",         // Customize airaccelerate value.
  "ArmorValue": 100,             // Modify players armor. (0 = Disable)
  "TeamWinningNotify": true,     // Enable/disable Team winning notifications.
  "KillSound": true,             // Enable/disable killsounds.
  "KillSoundFlag": "",           // Set permission for killsounds.
  "KillSoundPath": "sounds/training/timer_bell",       // Customize sound path.
  "Fov": true,                   // Enable/disable FOV command.
  "FovFlag": "",                 // Set permission for command use.
  "FovCommand": "css_fov",       // Customize fov command.
  "HideLegs": true,              // Enable/disable hidelegs command.
  "HideLegsCommand": "css_legs", // Customize hidelegs command.

  // VIP
  "VIPFeatures": true,           // Enable/disable VIP features.
  "VIPFlag": "@css/vip",         // Customize VIP flag.
  "VIPPerkMessage": true,        // Enable/disable VIP perk message.
  "VIPArmor": true,              // Enable/disable VIP armor.
  "VIPArmorValue": 120,          // Change armor for VIP's.
  "VIPHealthShot": true,         // Enable/disable healthshot for VIP's.
  "VIPDeagle": true,             // Enable/disable Deagle on spawn for VIP's.

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
| !legs           | Hide your lower body (Changeable)                                    | -           |


## 💡 Useful Addons/Plugins

- [cs2-DoubleJump](https://github.com/fidarit/cs2-DoubleJump): CS2 implementation of double jump.
- [CS2-ConnectMSG](https://github.com/asapverneri/CS2-ConnectMSG): A plugin to announce player connections and disconnections.

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
