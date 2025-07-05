# 🚀 Shellupdater 

![Windows](https://img.shields.io/badge/Windows-0078D6?style=for-the-badge&logo=windows&logoColor=white) 
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)

A Windows desktop application that manages application updates using Microsoft's Winget package manager.

![Application Screenshot](https://github.com/mabik11/Shellupdater/blob/master/Shellupdater/Sscreen/2025-07-05_02-07-44.png?raw=true)

## ✨ Features

- ✅ **Automatic Winget installation** if not detected
- ⚡ **One-click bulk updates** for all installed apps
- 📦 **Store-independent operation** - no Microsoft Store required
- 📊 **Real-time console output** with status messages
- 🛡️ **Automatic admin elevation** when needed

## 📥 Installation

### Pre-built Release
1. Download latest version from [Releases](https://github.com/mabik11/Shellupdater/releases)
2. Extract `WingetUpdater.zip`
3. Run `WingetUpdater.exe`

### Build from Source
```bash
git clone https://github.com/yourusername/winget-updater.git
cd winget-updater
msbuild WingetUpdater.sln /p:Configuration=Release
