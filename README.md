![GitHub Downloads (specific asset, latest release)](https://img.shields.io/github/downloads/DogFoxX/potato-launcher/latest/potato-launcher.exe?style=flat-square&logo=github&label=Total%20Downloads&color=Lime)

# Potato Launcher
A small utility to keep Yuzu EA updated.
Since Yuzu doesn't have it's own built-in way of updating, this app will be a handy tool.

## Installing and Running
- Get `potato-launcher.exe` from the [Latest Release](https://github.com/DogFoxX/potato-launcher/releases/latest/download/potato-launcher.exe)
- If you have an install of Yuzu EA, place Potato Launcher at the root of Yuzu (where yuzu.exe is)
  - Alternatively, you may run the app from anywhere. A folder browser will open if yuzu.exe was not found and you may choose a folder for the app to unzip the downloaded release. This folder path will be saved and used everytime the app is run.

Yuzu will automatically run when the download and unzip processes have completed, or when an update is not available.

## What it does
- Potato Launcher fetches only the latest release from [PineappleEA/pineapple-src](https://github.com/pineappleEA/pineapple-src).
- Initially it will download the latest release automatically because there is no record of the current release already installed.
- After downloading, the release will be extracted to a pre-determined path.