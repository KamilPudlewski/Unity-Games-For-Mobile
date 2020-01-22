using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BalansMenu
{
    private SettingsManager settingsManager;

    private static Texture2D playerCubeTexture = null;
    private static Texture2D standardPlatformTexture = null;
    private static Texture2D breakablePlatformTexture = null;
    private static Texture2D speedPlatformRightTexture = null;
    private static Texture2D speedPlatformLeftTexture = null;
    private static Texture2D spikePlatformTexture = null;
    private static Texture2D bouncePlatformTexture = null;

    public BalansMenu(SettingsManager settingsManager)
    {
        this.settingsManager = settingsManager;

        playerCubeTexture = (Texture2D)Resources.Load("Player Cube", typeof(Texture2D));
        standardPlatformTexture = (Texture2D)Resources.Load("Platform/Standard Platform", typeof(Texture2D));
        breakablePlatformTexture = (Texture2D)Resources.Load("Platform/Breakable Platform", typeof(Texture2D));
        speedPlatformRightTexture = (Texture2D)Resources.Load("Platform/Speed Platform Right", typeof(Texture2D));
        speedPlatformLeftTexture = (Texture2D)Resources.Load("Platform/Speed Platform Left", typeof(Texture2D));
        spikePlatformTexture = (Texture2D)Resources.Load("Platform/Spike Platform", typeof(Texture2D));
        bouncePlatformTexture = (Texture2D)Resources.Load("Platform/Bounce Platform", typeof(Texture2D));
    }

    public void View()
    {
        GUILayout.Label("Game Settings", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        settingsManager.settings.swapSideOnEdgeEnable = EditorGUILayout.Toggle(new GUIContent("Swap Side On Edge", "Enable Swaping Side On Edges"), settingsManager.settings.swapSideOnEdgeEnable);
        EditorGUILayout.EndHorizontal();
        GUILayout.Label("");

        GUILayout.Label("Player Cube Settings", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label(playerCubeTexture, GUILayout.Width(50), GUILayout.Height(50));
        settingsManager.settings.playerMoveSpeed = EditorGUILayout.Slider(new GUIContent("Player Cube Speed", "Player Cube Right And Left Speed Moves"), settingsManager.settings.playerMoveSpeed, 0.1f, 20f);
        EditorGUILayout.EndHorizontal();
        GUILayout.Label("");

        GUILayout.Label("Platform Settings", EditorStyles.boldLabel);
        settingsManager.settings.allPlatformsSpeedEnable = EditorGUILayout.Toggle(new GUIContent("Enable One Speed For All Platforms", "Set Speed For All Platforms"), settingsManager.settings.allPlatformsSpeedEnable);
        AllPlatformsSpeedChange();
        GUILayout.Label("Standard Platform", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label(standardPlatformTexture, GUILayout.Width(100), GUILayout.Height(30));
        settingsManager.settings.standardPlatformEnable = EditorGUILayout.Toggle(new GUIContent("Enable Standart Platform", "Enable Spawn Standart Platform"), settingsManager.settings.standardPlatformEnable);
        StandardPlatformChange();
        EditorGUILayout.EndHorizontal();

        GUILayout.Label("Breakable Platform", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label(breakablePlatformTexture, GUILayout.Width(100), GUILayout.Height(30));
        settingsManager.settings.breakablePlatformEnable = EditorGUILayout.Toggle(new GUIContent("Enable Breakable Platform", "Enable Spawn Breakable Platform"), settingsManager.settings.breakablePlatformEnable);
        BreakablePlatformChange();
        EditorGUILayout.EndHorizontal();

        GUILayout.Label("Speed Platform", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label(speedPlatformRightTexture, GUILayout.Width(100), GUILayout.Height(30));
        settingsManager.settings.speedPlatformRightEnable = EditorGUILayout.Toggle(new GUIContent("Enable Speed Platform Right", "Enable Spawn Speed Platform Right"), settingsManager.settings.speedPlatformRightEnable);
        SpeedPlatformRightChange();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label(speedPlatformLeftTexture, GUILayout.Width(100), GUILayout.Height(30));
        settingsManager.settings.speedPlatformLeftEnable = EditorGUILayout.Toggle(new GUIContent("Enable Speed Platform Left", "Enable Spawn Speed Platform Left"), settingsManager.settings.speedPlatformLeftEnable);
        SpeedPlatformLeftChange();
        EditorGUILayout.EndHorizontal();

        GUILayout.Label("Spike Platform", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label(spikePlatformTexture, GUILayout.Width(100), GUILayout.Height(30));
        settingsManager.settings.spikePlatformEnable = EditorGUILayout.Toggle(new GUIContent("Enable Spike Platform", "Enable Spawn Spike Platform"), settingsManager.settings.spikePlatformEnable);
        SpikePlatformChange();
        EditorGUILayout.EndHorizontal();

        GUILayout.Label("Bounce Platform", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label(bouncePlatformTexture, GUILayout.Width(100), GUILayout.Height(30));
        settingsManager.settings.bouncePlatformEnable = EditorGUILayout.Toggle(new GUIContent("Enable Bounce Platform", "Enable Spawn Bounce Platform"), settingsManager.settings.bouncePlatformEnable);
        BouncePlatformChange();
        EditorGUILayout.EndHorizontal();

        GUILayout.Label("Platform Spawn Settings", EditorStyles.boldLabel);
        settingsManager.settings.adaptiveDifficultySpawnLevel = EditorGUILayout.Toggle(new GUIContent("Adaptive Spawn Difficulty", "Enable Changing Spawn Platform Time"), settingsManager.settings.adaptiveDifficultySpawnLevel);
        AdaptiveDifficultyLevelChange();
        GUILayout.Label("Spawn Rules", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        SpawnRuleChange();
        EditorGUILayout.EndHorizontal();
        GUILayout.Label("");

        GUILayout.Label("Platform Group Change Settings", EditorStyles.boldLabel);
        if (settingsManager.settings.groupPlatforms1Enable)
            settingsManager.settings.changeGroupPlatforms1After = EditorGUILayout.IntSlider(new GUIContent("Change Group 1 After", "Change Group Platforms 1 After {X} Platforms"), settingsManager.settings.changeGroupPlatforms1After, 1, 10);
        if (settingsManager.settings.groupPlatforms2Enable)
            settingsManager.settings.changeGroupPlatforms2After = EditorGUILayout.IntSlider(new GUIContent("Change Group 2 After", "Change Group Platforms 2 After {X} Platforms"), settingsManager.settings.changeGroupPlatforms2After, 1, 10);
        if (settingsManager.settings.groupPlatforms3Enable)
            settingsManager.settings.changeGroupPlatforms3After = EditorGUILayout.IntSlider(new GUIContent("Change Group 3 After", "Change Group Platforms 3 After {X} Platforms"), settingsManager.settings.changeGroupPlatforms3After, 1, 10);
    }

    private void AllPlatformsSpeedChange()
    {
        if (settingsManager.settings.allPlatformsSpeedEnable)
        {
            settingsManager.settings.adaptiveDifficultyAllPlatformsSpeedLevel = EditorGUILayout.Toggle(new GUIContent("Adaptive Platforms Speed Level", "Adaptive Difficulty All Platforms Speed Level"), settingsManager.settings.adaptiveDifficultyAllPlatformsSpeedLevel);
            
            if (settingsManager.settings.adaptiveDifficultyAllPlatformsSpeedLevel)
            {
                settingsManager.settings.startAllPlatforsmSpeedLevel = EditorGUILayout.Slider(new GUIContent("Starting Platforms Speed", "Starting All Platform Speed Level"), settingsManager.settings.startAllPlatforsmSpeedLevel, 0.1f, 5f);
                settingsManager.settings.maximumAllPlatformsSpeedLevel = EditorGUILayout.Slider(new GUIContent("Maximum Platforms Speed", "Maximum All Platform Speed After Enable Hardest Difficulty"), settingsManager.settings.maximumAllPlatformsSpeedLevel, 0.1f, 10f);
                settingsManager.settings.changeStepsAllPlatformsSpeedLevel = EditorGUILayout.IntSlider(new GUIContent("Change Speed Difficulty Steps", "Numbers Of Speed Difficulty Change Steps"), settingsManager.settings.changeStepsAllPlatformsSpeedLevel, 1, 30);
                settingsManager.settings.changeStepsAllPlatformsSpeedLeveAfterSpawn = EditorGUILayout.IntSlider(new GUIContent("Change Speed Difficulty After Spawn", "Change Speed Difficulty Step After Spawn {X} Platforms"), settingsManager.settings.changeStepsAllPlatformsSpeedLeveAfterSpawn, 1, 30);
            }
            else
            {
                settingsManager.settings.allPlatformsSpeed = EditorGUILayout.Slider(new GUIContent("All Platforms Speed", "All Platforms Velocity"), settingsManager.settings.allPlatformsSpeed, 0.1f, 5f);
            }
        }
    }

    private void SpawnRuleChange()
    {
        // Platforms Group 1
        settingsManager.settings.groupPlatforms1Enable = EditorGUILayout.BeginToggleGroup(new GUIContent("Platform Group 1", "Enable Platform Group 1"), settingsManager.settings.groupPlatforms1Enable);
        if (settingsManager.settings.standardPlatformEnable)
        {
            settingsManager.settings.groupPlatforms1StandardPlatformEnable = EditorGUILayout.Toggle(new GUIContent("Standard Platform", "Enable Standard Platform In Group 1"), settingsManager.settings.groupPlatforms1StandardPlatformEnable);
            GUILayout.Label(standardPlatformTexture, GUILayout.Width(100), GUILayout.Height(30));
        }
        if (settingsManager.settings.breakablePlatformEnable)
        {
            settingsManager.settings.groupPlatforms1BreakablePlatformPlatformEnable = EditorGUILayout.Toggle(new GUIContent("Breakable Platform", "Enable Breakable Platform In Group 1"), settingsManager.settings.groupPlatforms1BreakablePlatformPlatformEnable);
            GUILayout.Label(breakablePlatformTexture, GUILayout.Width(100), GUILayout.Height(30));
        }
        if (settingsManager.settings.speedPlatformRightEnable)
        {
            settingsManager.settings.groupPlatforms1SpeedPlatformRightPlatformEnable = EditorGUILayout.Toggle(new GUIContent("Speed Platform Right", "Enable Speed Platform Right In Group 1"), settingsManager.settings.groupPlatforms1SpeedPlatformRightPlatformEnable);
            GUILayout.Label(speedPlatformRightTexture, GUILayout.Width(100), GUILayout.Height(30));
        }
        if (settingsManager.settings.speedPlatformLeftEnable)
        {
            settingsManager.settings.groupPlatforms1SpeedPlatformLeftPlatformEnable = EditorGUILayout.Toggle(new GUIContent("Speed Platform Left", "Enable Speed Platform Left In Group 1"), settingsManager.settings.groupPlatforms1SpeedPlatformLeftPlatformEnable);
            GUILayout.Label(speedPlatformLeftTexture, GUILayout.Width(100), GUILayout.Height(30));
        }
        if (settingsManager.settings.spikePlatformEnable)
        {
            settingsManager.settings.groupPlatforms1SpikePlatformEnable = EditorGUILayout.Toggle(new GUIContent("Spike Platform", "Enable Spike Platform In Group 1"), settingsManager.settings.groupPlatforms1SpikePlatformEnable);
            GUILayout.Label(spikePlatformTexture, GUILayout.Width(100), GUILayout.Height(30));
        }
        if (settingsManager.settings.bouncePlatformEnable)
        {
            settingsManager.settings.groupPlatforms1BouncePlatformEnable = EditorGUILayout.Toggle(new GUIContent("Bounce Platform", "Enable Bounce Platform In Group 1"), settingsManager.settings.groupPlatforms1BouncePlatformEnable);
            GUILayout.Label(bouncePlatformTexture, GUILayout.Width(100), GUILayout.Height(30));
        }
        EditorGUILayout.EndToggleGroup();

        // Platforms Group 2
        if (settingsManager.settings.groupPlatforms1Enable)
        {
            settingsManager.settings.groupPlatforms2Enable = EditorGUILayout.BeginToggleGroup(new GUIContent("Platform Group 2", "Enable Platform Group 2"), settingsManager.settings.groupPlatforms2Enable);
        }
        else
        {
            settingsManager.settings.groupPlatforms2Enable = false;
            EditorGUILayout.BeginToggleGroup(new GUIContent("Platform Group 2", ""), settingsManager.settings.groupPlatforms2Enable);
        } 
        if (settingsManager.settings.standardPlatformEnable)
        {
            settingsManager.settings.groupPlatforms2StandardPlatformEnable = EditorGUILayout.Toggle(new GUIContent("Standard Platform", "Enable Standard Platform In Group 2"), settingsManager.settings.groupPlatforms2StandardPlatformEnable);
            GUILayout.Label(standardPlatformTexture, GUILayout.Width(100), GUILayout.Height(30));
        }
        if (settingsManager.settings.breakablePlatformEnable)
        {
            settingsManager.settings.groupPlatforms2BreakablePlatformPlatformEnable = EditorGUILayout.Toggle(new GUIContent("Breakable Platform", "Enable Breakable Platform In Group 2"), settingsManager.settings.groupPlatforms2BreakablePlatformPlatformEnable);
            GUILayout.Label(breakablePlatformTexture, GUILayout.Width(100), GUILayout.Height(30));
        }
        if (settingsManager.settings.speedPlatformRightEnable)
        {
            settingsManager.settings.groupPlatforms2SpeedPlatformRightPlatformEnable = EditorGUILayout.Toggle(new GUIContent("Speed Platform Right", "Enable Speed Platform Right In Group 2"), settingsManager.settings.groupPlatforms2SpeedPlatformRightPlatformEnable);
            GUILayout.Label(speedPlatformRightTexture, GUILayout.Width(100), GUILayout.Height(30));
        }
        if (settingsManager.settings.speedPlatformLeftEnable)
        {
            settingsManager.settings.groupPlatforms2SpeedPlatformLeftPlatformEnable = EditorGUILayout.Toggle(new GUIContent("Speed Platform Left", "Enable Speed Platform Left In Group 2"), settingsManager.settings.groupPlatforms2SpeedPlatformLeftPlatformEnable);
            GUILayout.Label(speedPlatformLeftTexture, GUILayout.Width(100), GUILayout.Height(30));
        }
        if (settingsManager.settings.spikePlatformEnable)
        {
            settingsManager.settings.groupPlatforms2SpikePlatformEnable = EditorGUILayout.Toggle(new GUIContent("Spike Platform", "Enable Spike Platform In Group 2"), settingsManager.settings.groupPlatforms2SpikePlatformEnable);
            GUILayout.Label(spikePlatformTexture, GUILayout.Width(100), GUILayout.Height(30));
        }
        if (settingsManager.settings.bouncePlatformEnable)
        {
            settingsManager.settings.groupPlatforms2BouncePlatformEnable = EditorGUILayout.Toggle(new GUIContent("Bounce Platform", "Enable Bounce Platform In Group 2"), settingsManager.settings.groupPlatforms2BouncePlatformEnable);
            GUILayout.Label(bouncePlatformTexture, GUILayout.Width(100), GUILayout.Height(30));
        }
        EditorGUILayout.EndToggleGroup();

        // Platforms Group 3
        if (settingsManager.settings.groupPlatforms2Enable)
        {
            settingsManager.settings.groupPlatforms3Enable = EditorGUILayout.BeginToggleGroup(new GUIContent("Platform Group 3", "Enable Platform Group 3"), settingsManager.settings.groupPlatforms3Enable);
        }
        else
        {
            settingsManager.settings.groupPlatforms3Enable = false;
            EditorGUILayout.BeginToggleGroup(new GUIContent("Platform Group 3", ""), settingsManager.settings.groupPlatforms3Enable);
        }
        if (settingsManager.settings.standardPlatformEnable)
        {
            settingsManager.settings.groupPlatforms3StandardPlatformEnable = EditorGUILayout.Toggle(new GUIContent("Standard Platform", "Enable Standard Platform In Group 3"), settingsManager.settings.groupPlatforms3StandardPlatformEnable);
            GUILayout.Label(standardPlatformTexture, GUILayout.Width(100), GUILayout.Height(30));
        }
        if (settingsManager.settings.breakablePlatformEnable)
        {
            settingsManager.settings.groupPlatforms3BreakablePlatformPlatformEnable = EditorGUILayout.Toggle(new GUIContent("Breakable Platform", "Enable Breakable Platform In Group 3"), settingsManager.settings.groupPlatforms3BreakablePlatformPlatformEnable);
            GUILayout.Label(breakablePlatformTexture, GUILayout.Width(100), GUILayout.Height(30));
        }
        if (settingsManager.settings.speedPlatformRightEnable)
        {
            settingsManager.settings.groupPlatforms3SpeedPlatformRightPlatformEnable = EditorGUILayout.Toggle(new GUIContent("Speed Platform Right", "Enable Speed Platform Right In Group 3"), settingsManager.settings.groupPlatforms3SpeedPlatformRightPlatformEnable);
            GUILayout.Label(speedPlatformRightTexture, GUILayout.Width(100), GUILayout.Height(30));
        }
        if (settingsManager.settings.speedPlatformLeftEnable)
        {
            settingsManager.settings.groupPlatforms3SpeedPlatformLeftPlatformEnable = EditorGUILayout.Toggle(new GUIContent("Speed Platform Left", "Enable Speed Platform Left In Group 3"), settingsManager.settings.groupPlatforms3SpeedPlatformLeftPlatformEnable);
            GUILayout.Label(speedPlatformLeftTexture, GUILayout.Width(100), GUILayout.Height(30));
        }
        if (settingsManager.settings.spikePlatformEnable)
        {
            settingsManager.settings.groupPlatforms3SpikePlatformEnable = EditorGUILayout.Toggle(new GUIContent("Spike Platform", "Enable Spike Platform In Group 3"), settingsManager.settings.groupPlatforms3SpikePlatformEnable);
            GUILayout.Label(spikePlatformTexture, GUILayout.Width(100), GUILayout.Height(30));
        }
        if (settingsManager.settings.bouncePlatformEnable)
        {
            settingsManager.settings.groupPlatforms3BouncePlatformEnable = EditorGUILayout.Toggle(new GUIContent("Bounce Platform", "Enable Bounce Platform In Group 3"), settingsManager.settings.groupPlatforms3BouncePlatformEnable);
            GUILayout.Label(bouncePlatformTexture, GUILayout.Width(100), GUILayout.Height(30));
        }
        EditorGUILayout.EndToggleGroup();
    }

    private void StandardPlatformChange()
    {
        if (settingsManager.settings.standardPlatformEnable)
        {
            if (!settingsManager.settings.allPlatformsSpeedEnable)
                settingsManager.settings.standardPlatformSpeed = EditorGUILayout.Slider(new GUIContent("Standard Platform Speed", "Standard Platform Velocity"), settingsManager.settings.standardPlatformSpeed, 0.1f, 5f);
        }
    }

    private void BreakablePlatformChange()
    {
        if (settingsManager.settings.breakablePlatformEnable)
        {
            if (!settingsManager.settings.allPlatformsSpeedEnable)
                settingsManager.settings.breakablePlatformSpeed = EditorGUILayout.Slider(new GUIContent("Breakable Platform Speed", "Breakable Platform Velocity"), settingsManager.settings.breakablePlatformSpeed, 0.1f, 5f);
        }
    }

    private void SpeedPlatformRightChange()
    {
        if (settingsManager.settings.speedPlatformRightEnable)
        {
            if (!settingsManager.settings.allPlatformsSpeedEnable)
                settingsManager.settings.speedPlatformRightSpeed = EditorGUILayout.Slider(new GUIContent("Speed Platform Speed Right", "Speed Platform Right Velocity"), settingsManager.settings.speedPlatformRightSpeed, 0.1f, 5f);
            
            settingsManager.settings.speedPlatformRightPowerActionSpeed = EditorGUILayout.Slider(new GUIContent("Platform Right Power Action", "Speed Platform Right Move Player Power Action"), settingsManager.settings.speedPlatformRightPowerActionSpeed, 0.5f, 5f);
        }
    }
    private void SpeedPlatformLeftChange()
    {
        if (settingsManager.settings.speedPlatformLeftEnable)
        {
            if (!settingsManager.settings.allPlatformsSpeedEnable)
                settingsManager.settings.speedPlatformLeftSpeed = EditorGUILayout.Slider(new GUIContent("Speed Platform Speed Left", "Speed Platform Left Velocity"), settingsManager.settings.speedPlatformLeftSpeed, 0.1f, 5f);

            settingsManager.settings.speedPlatformLeftPowerActionSpeed = EditorGUILayout.Slider(new GUIContent("Platform Left Power Action", "Speed Platform Left Move Player Power Action"), settingsManager.settings.speedPlatformLeftPowerActionSpeed, 0.5f, 5f);
        }
    }

    private void SpikePlatformChange()
    {
        if (settingsManager.settings.spikePlatformEnable)
        {
            if (!settingsManager.settings.allPlatformsSpeedEnable)
                settingsManager.settings.spikePlatformSpeed = EditorGUILayout.Slider(new GUIContent("Spike Platform Speed", "Spike Platform Velocity"), settingsManager.settings.spikePlatformSpeed, 0.1f, 5f);
        }
    }

    private void BouncePlatformChange()
    {
        if (settingsManager.settings.bouncePlatformEnable)
        {
            if (!settingsManager.settings.allPlatformsSpeedEnable)
                settingsManager.settings.bouncePlatformSpeed = EditorGUILayout.Slider(new GUIContent("Bounce Platform Speed", "Bounce Platform Velocity"), settingsManager.settings.bouncePlatformSpeed, 0.1f, 5f);
            settingsManager.settings.bouncePlatformBounciness = EditorGUILayout.Slider(new GUIContent("Bounce Platform Bounciness", "Bounce Platform Bounciness Power"), settingsManager.settings.bouncePlatformBounciness, 0.1f, 2f);
        }
    }

    private void AdaptiveDifficultyLevelChange()
    {
        if (settingsManager.settings.adaptiveDifficultySpawnLevel)
        {
            settingsManager.settings.startPlatformSpawnTimer = EditorGUILayout.Slider(new GUIContent("Starting Time To Spawn", "Starting Platform Spawn Time"), settingsManager.settings.startPlatformSpawnTimer, 0.1f, 2.5f);
            settingsManager.settings.maximumPlatformSpawnTimer = EditorGUILayout.Slider(new GUIContent("Maximum Time To Spawn", "Maximum Enemy Spawn Time After Enable Hardest Difficulty"), settingsManager.settings.maximumPlatformSpawnTimer, 0.1f, 2.5f);
            settingsManager.settings.changeSteps = EditorGUILayout.IntSlider(new GUIContent("Change Difficulty Steps", "Numbers Of Difficulty Change Steps"), settingsManager.settings.changeSteps, 1, 30);
            settingsManager.settings.changeStepsAfterSpawn = EditorGUILayout.IntSlider(new GUIContent("Change Difficulty After Spawn", "Change Difficulty Step After Spawn {X} Platforms"), settingsManager.settings.changeStepsAfterSpawn, 1, 30);
        }
        else
        {
            settingsManager.settings.platformSpawnTimer = EditorGUILayout.Slider(new GUIContent("All Platforms Spawn Time", "All Platforms Spawn Time In Seconds"), settingsManager.settings.platformSpawnTimer, 0.1f, 3f);
        }
    }
}