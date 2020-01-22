using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SettingsManager : ScriptableObject
{
    public Settings settings = new Settings();

    public SettingsManager()
    {
        LoadData();
    }

    public void SaveData()
    {
        if (!Directory.Exists("Configs"))
            Directory.CreateDirectory("Configs");

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Create("Configs/config.binary");

        formatter.Serialize(saveFile, settings);

        saveFile.Close();
    }

    public void LoadData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Open("Configs/config.binary", FileMode.Open);

        settings = (Settings)formatter.Deserialize(saveFile);

        saveFile.Close();
    }

    public void LoadDataFromFile(string path)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Open(path, FileMode.Open);

        settings = (Settings)formatter.Deserialize(saveFile);

        saveFile.Close();
    }

    public void ResetData()
    {
        ResetBalance();
        ResetScoring();
    }

    public void ResetBalance()
    {
        // Game Settings
        settings.swapSideOnEdgeEnable = true;

        // Player Settings
        settings.playerMoveSpeed = 2f;

        // Platforms Spawn Settings
        settings.platformSpawnTimer = 1.6f;
        settings.adaptiveDifficultySpawnLevel = true;
        settings.startPlatformSpawnTimer = 1.5f;
        settings.maximumPlatformSpawnTimer = 1.8f;
        settings.changeSteps = 15;
        settings.changeStepsAfterSpawn = 10;

        // Platforms Group Settings
        settings.groupPlatforms1Enable = true;
        settings.groupPlatforms2Enable = true;
        settings.groupPlatforms3Enable = true;

        settings.groupPlatforms1StandardPlatformEnable = true;
        settings.groupPlatforms1BreakablePlatformPlatformEnable = false;
        settings.groupPlatforms1SpeedPlatformRightPlatformEnable = false;
        settings.groupPlatforms1SpeedPlatformLeftPlatformEnable = false;
        settings.groupPlatforms1SpikePlatformEnable = false;
        settings.groupPlatforms1BouncePlatformEnable = false;

        settings.groupPlatforms2StandardPlatformEnable = false;
        settings.groupPlatforms2BreakablePlatformPlatformEnable = true;
        settings.groupPlatforms2SpeedPlatformRightPlatformEnable = true;
        settings.groupPlatforms2SpeedPlatformLeftPlatformEnable = true;
        settings.groupPlatforms2SpikePlatformEnable = true;
        settings.groupPlatforms2BouncePlatformEnable = false;

        settings.groupPlatforms3StandardPlatformEnable = false;
        settings.groupPlatforms3BreakablePlatformPlatformEnable = false;
        settings.groupPlatforms3SpeedPlatformRightPlatformEnable = false;
        settings.groupPlatforms3SpeedPlatformLeftPlatformEnable = false;
        settings.groupPlatforms3SpikePlatformEnable = false;
        settings.groupPlatforms3BouncePlatformEnable = true;

        settings.changeGroupPlatforms1After = 2;
        settings.changeGroupPlatforms2After = 1;
        settings.changeGroupPlatforms3After = 1;

        // All Platforms Speed Settings
        settings.allPlatformsSpeedEnable = true;
        settings.allPlatformsSpeed = 2f;
        settings.adaptiveDifficultyAllPlatformsSpeedLevel = true;
        settings.startAllPlatforsmSpeedLevel = 1.8f;
        settings.maximumAllPlatformsSpeedLevel = 4.0f;
        settings.changeStepsAllPlatformsSpeedLevel = 15;
        settings.changeStepsAllPlatformsSpeedLeveAfterSpawn = 10;

        // Platforms Settings
        settings.standardPlatformEnable = true;
        settings.standardPlatformSpeed = 2f;

        settings.breakablePlatformEnable = true;
        settings.breakablePlatformSpeed = 2f;

        settings.speedPlatformRightEnable = true;
        settings.speedPlatformRightSpeed = 2f;
        settings.speedPlatformRightPowerActionSpeed = 1f;

        settings.speedPlatformLeftEnable = true;
        settings.speedPlatformLeftSpeed = 2f;
        settings.speedPlatformLeftPowerActionSpeed = 1f;

        settings.spikePlatformEnable = true;
        settings.spikePlatformSpeed = 2f;
        
        settings.bouncePlatformEnable = true;
        settings.bouncePlatformSpeed = 2f;
        settings.bouncePlatformBounciness = 1.2f;
}

    public void ResetScoring()
    {
        settings.bouncePlatformBonusPoints = 100;
    }
}
