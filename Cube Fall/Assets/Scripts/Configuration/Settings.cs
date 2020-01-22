using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Settings
{
    // Game Settings
    public bool swapSideOnEdgeEnable;

    // Player Settings
    public float playerMoveSpeed;

    // Platforms Spawn Settings
    public float platformSpawnTimer;
    public bool adaptiveDifficultySpawnLevel;
    public float startPlatformSpawnTimer;
    public float maximumPlatformSpawnTimer;
    public int changeSteps;
    public int changeStepsAfterSpawn;

    // Platforms Group Settings
    public bool groupPlatforms1Enable;
    public bool groupPlatforms2Enable;
    public bool groupPlatforms3Enable;

    public bool groupPlatforms1StandardPlatformEnable;
    public bool groupPlatforms1BreakablePlatformPlatformEnable;
    public bool groupPlatforms1SpeedPlatformRightPlatformEnable;
    public bool groupPlatforms1SpeedPlatformLeftPlatformEnable;
    public bool groupPlatforms1SpikePlatformEnable;
    public bool groupPlatforms1BouncePlatformEnable;

    public bool groupPlatforms2StandardPlatformEnable;
    public bool groupPlatforms2BreakablePlatformPlatformEnable;
    public bool groupPlatforms2SpeedPlatformRightPlatformEnable;
    public bool groupPlatforms2SpeedPlatformLeftPlatformEnable;
    public bool groupPlatforms2SpikePlatformEnable;
    public bool groupPlatforms2BouncePlatformEnable;

    public bool groupPlatforms3StandardPlatformEnable;
    public bool groupPlatforms3BreakablePlatformPlatformEnable;
    public bool groupPlatforms3SpeedPlatformRightPlatformEnable;
    public bool groupPlatforms3SpeedPlatformLeftPlatformEnable;
    public bool groupPlatforms3SpikePlatformEnable;
    public bool groupPlatforms3BouncePlatformEnable;

    public int changeGroupPlatforms1After;
    public int changeGroupPlatforms2After;
    public int changeGroupPlatforms3After;

    // All Platforms Speed Settings
    public bool allPlatformsSpeedEnable;
    public float allPlatformsSpeed;

    public bool adaptiveDifficultyAllPlatformsSpeedLevel;
    public float startAllPlatforsmSpeedLevel;
    public float maximumAllPlatformsSpeedLevel;
    public int changeStepsAllPlatformsSpeedLevel;
    public int changeStepsAllPlatformsSpeedLeveAfterSpawn;
    public float currentPlatformSpawnTimer;

    // Platforms Settings
    public bool standardPlatformEnable;
    public float standardPlatformSpeed;

    public bool breakablePlatformEnable;
    public float breakablePlatformSpeed;

    public bool speedPlatformRightEnable;
    public float speedPlatformRightSpeed;
    public float speedPlatformRightPowerActionSpeed;

    public bool speedPlatformLeftEnable;
    public float speedPlatformLeftSpeed;
    public float speedPlatformLeftPowerActionSpeed;

    public bool spikePlatformEnable;
    public float spikePlatformSpeed;

    public bool bouncePlatformEnable;
    public float bouncePlatformSpeed;
    public float bouncePlatformBounciness;

    // Scoring Settings
    public int bouncePlatformBonusPoints;
}
