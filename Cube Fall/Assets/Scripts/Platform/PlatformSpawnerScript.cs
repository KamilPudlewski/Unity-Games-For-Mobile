using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawnerScript : MonoBehaviour
{
    private SettingsManager settingsManager;

    public GameObject platformPrefab;
    public GameObject spikePlatformPrefab;
    public GameObject movingPlatformLeft;
    public GameObject movingPlatformRight;
    public GameObject breakablePlatformPrefab;
    public GameObject bouncePlatform;

    private float platformSpawnTimer = 1.6f;
    private float currentPlatformSpawnTimer;
    private float tmpPlatformSpawnTimer;

    private float startPlatformSpawnTimer = 2f;
    private float minimumPlatformSpawnTimer = 0.5f;
    private int changeSteps = 15;
    private int changeStepsAfterSpawn = 4;
    private int spawnCount = 0;

    private float startAllPlatforsmSpeedLevel = 1.8f;
    private float maximumAllPlatformsSpeedLevel = 4.0f;
    private int changeStepsAllPlatformsSpeedLevel = 15;
    private int changeStepsAllPlatformsSpeedLeveAfterSpawn = 10;
    private int speedSpawnCount = 0;


    private int platformSpawnCount;
    public int minX = -2;
    public int maxX = 2;

    private int changeGroup1 = 0;
    private int changeGroup2 = 0;
    private int changeGroup3 = 0;
    private List<GameObject> group1platforms = new List<GameObject>();
    private List<GameObject> group2platforms = new List<GameObject>();
    private List<GameObject> group3platforms = new List<GameObject>();

    void Start()
    {
        settingsManager = ScriptableObject.CreateInstance("SettingsManager") as SettingsManager;
        startPlatformSpawnTimer = settingsManager.settings.startPlatformSpawnTimer;
        minimumPlatformSpawnTimer = settingsManager.settings.maximumPlatformSpawnTimer;
        changeSteps = settingsManager.settings.changeSteps;
        changeStepsAfterSpawn = settingsManager.settings.changeStepsAfterSpawn;
        platformSpawnTimer = settingsManager.settings.platformSpawnTimer;

        startAllPlatforsmSpeedLevel = settingsManager.settings.startAllPlatforsmSpeedLevel;
        maximumAllPlatformsSpeedLevel = settingsManager.settings.maximumAllPlatformsSpeedLevel;
        changeStepsAllPlatformsSpeedLevel = settingsManager.settings.changeStepsAllPlatformsSpeedLevel;
        changeStepsAllPlatformsSpeedLeveAfterSpawn = settingsManager.settings.changeStepsAllPlatformsSpeedLeveAfterSpawn;

        if (settingsManager.settings.adaptiveDifficultyAllPlatformsSpeedLevel)
        {
            settingsManager.settings.currentPlatformSpawnTimer = settingsManager.settings.startAllPlatforsmSpeedLevel;
            settingsManager.SaveData();
        }

        if (settingsManager.settings.adaptiveDifficultySpawnLevel)
        {
            tmpPlatformSpawnTimer = startPlatformSpawnTimer;
            currentPlatformSpawnTimer = tmpPlatformSpawnTimer;
        }
        else
        {
            tmpPlatformSpawnTimer = platformSpawnTimer;
            currentPlatformSpawnTimer = tmpPlatformSpawnTimer;
        } 
        CalculateChangeSpawnGroup();
        PreparePlatformsGroups();
    }

    void Update()
    {
        SpawnPlatforms();
    }
    private void SpawnPlatforms()
    {
        currentPlatformSpawnTimer += Time.deltaTime;

        if (currentPlatformSpawnTimer >= tmpPlatformSpawnTimer)
        {
            platformSpawnCount++;
            spawnCount++;
            speedSpawnCount++;
            AdaptiveDifficultySpawnLevel();
            AdaptiveDifficultyAllPlatformsSpeedLevel();

            Vector3 temp = transform.position;
            temp.x = Random.Range(minX, maxX);

            GameObject newPlatform = null;

            if ((platformSpawnCount <= changeGroup1) && settingsManager.settings.groupPlatforms1Enable)
            {
                if (group1platforms.Count != 0)
                    newPlatform = Instantiate(group1platforms[Random.Range(0, group1platforms.Count)], temp, Quaternion.identity);
            }
            else if ((platformSpawnCount > changeGroup1 && platformSpawnCount <= changeGroup2) && settingsManager.settings.groupPlatforms2Enable)
            {
                if (group2platforms.Count != 0)
                    newPlatform = Instantiate(group2platforms[Random.Range(0, group2platforms.Count)], temp, Quaternion.identity);
            }
            else if ((platformSpawnCount > changeGroup2 && platformSpawnCount <= changeGroup3) && settingsManager.settings.groupPlatforms3Enable)
            {
                if (group3platforms.Count != 0)
                    newPlatform = Instantiate(group3platforms[Random.Range(0, group3platforms.Count)], temp, Quaternion.identity);
            }

            if (newPlatform)
            {
                newPlatform.transform.parent = transform;
            }
            currentPlatformSpawnTimer = 0f;
            ResetPlatformSpawnCount();
        }
    }

    private void ResetPlatformSpawnCount()
    {
        if (settingsManager.settings.groupPlatforms3Enable)
        {
            if (platformSpawnCount == changeGroup3)
            {
                platformSpawnCount = 0;
            }
        }
        else if (settingsManager.settings.groupPlatforms2Enable)
        {
            if (platformSpawnCount == changeGroup3 - 1)
            {
                platformSpawnCount = 0;
            }
        }
        else if (settingsManager.settings.groupPlatforms1Enable)
        {
            if (platformSpawnCount == changeGroup3 - 2)
            {
                platformSpawnCount = 0;
            }
        }
    }

    private void AdaptiveDifficultySpawnLevel()
    {
        if (settingsManager.settings.adaptiveDifficultySpawnLevel)
        {
            if (spawnCount == settingsManager.settings.changeStepsAfterSpawn)
            {
                spawnCount = 0;

                tmpPlatformSpawnTimer = tmpPlatformSpawnTimer + ((settingsManager.settings.maximumPlatformSpawnTimer - settingsManager.settings.startPlatformSpawnTimer) / settingsManager.settings.changeSteps);
                
                if (tmpPlatformSpawnTimer <= settingsManager.settings.maximumPlatformSpawnTimer)
                {
                    currentPlatformSpawnTimer = tmpPlatformSpawnTimer;
                }
            }
        }
    }

    private void AdaptiveDifficultyAllPlatformsSpeedLevel()
    {
        if (settingsManager.settings.adaptiveDifficultyAllPlatformsSpeedLevel)
        {
            if (speedSpawnCount == settingsManager.settings.changeStepsAllPlatformsSpeedLeveAfterSpawn)
            {
                speedSpawnCount = 0;

                float tmpPlatformSpeedTimer = settingsManager.settings.currentPlatformSpawnTimer + ((settingsManager.settings.maximumAllPlatformsSpeedLevel - settingsManager.settings.startAllPlatforsmSpeedLevel) / settingsManager.settings.changeStepsAllPlatformsSpeedLevel);

                if (tmpPlatformSpeedTimer <= settingsManager.settings.maximumAllPlatformsSpeedLevel)
                {
                    settingsManager.settings.currentPlatformSpawnTimer = tmpPlatformSpeedTimer;
                    settingsManager.SaveData();
                }
            }
        }
    }

    private void CalculateChangeSpawnGroup()
    {
        changeGroup1 = settingsManager.settings.changeGroupPlatforms1After;
        changeGroup2 = changeGroup1 + settingsManager.settings.changeGroupPlatforms2After;
        changeGroup3 = changeGroup2 + settingsManager.settings.changeGroupPlatforms3After;
    }

    private void PreparePlatformsGroups()
    {
        // Group 1 Platforms
        if (settingsManager.settings.groupPlatforms1StandardPlatformEnable && settingsManager.settings.standardPlatformEnable)
        {
            group1platforms.Add(platformPrefab);
        }
        if (settingsManager.settings.groupPlatforms1BreakablePlatformPlatformEnable && settingsManager.settings.breakablePlatformEnable)
        {
            group1platforms.Add(breakablePlatformPrefab);
        }
        if (settingsManager.settings.groupPlatforms1SpeedPlatformRightPlatformEnable && settingsManager.settings.speedPlatformRightEnable)
        {
            group1platforms.Add(movingPlatformRight);
        }
        if (settingsManager.settings.groupPlatforms1SpeedPlatformLeftPlatformEnable && settingsManager.settings.speedPlatformLeftEnable)
        {
            group1platforms.Add(movingPlatformLeft);
        }
        if (settingsManager.settings.groupPlatforms1SpikePlatformEnable && settingsManager.settings.spikePlatformEnable)
        {
            group1platforms.Add(spikePlatformPrefab);
        }
        if (settingsManager.settings.groupPlatforms1BouncePlatformEnable && settingsManager.settings.bouncePlatformEnable)
        {
            group1platforms.Add(bouncePlatform);
        }

        // Group 2 Platforms
        if (settingsManager.settings.groupPlatforms2StandardPlatformEnable && settingsManager.settings.standardPlatformEnable)
        {
            group2platforms.Add(platformPrefab);
        }
        if (settingsManager.settings.groupPlatforms2BreakablePlatformPlatformEnable && settingsManager.settings.breakablePlatformEnable)
        {
            group2platforms.Add(breakablePlatformPrefab);
        }
        if (settingsManager.settings.groupPlatforms2SpeedPlatformRightPlatformEnable && settingsManager.settings.speedPlatformRightEnable)
        {
            group2platforms.Add(movingPlatformRight);
        }
        if (settingsManager.settings.groupPlatforms2SpeedPlatformLeftPlatformEnable && settingsManager.settings.speedPlatformLeftEnable)
        {
            group2platforms.Add(movingPlatformLeft);
        }
        if (settingsManager.settings.groupPlatforms2SpikePlatformEnable && settingsManager.settings.spikePlatformEnable)
        {
            group2platforms.Add(spikePlatformPrefab);
        }
        if (settingsManager.settings.groupPlatforms2BouncePlatformEnable && settingsManager.settings.bouncePlatformEnable)
        {
            group2platforms.Add(bouncePlatform);
        }

        // Group 3 Platforms
        if (settingsManager.settings.groupPlatforms3StandardPlatformEnable && settingsManager.settings.standardPlatformEnable)
        {
            group3platforms.Add(platformPrefab);
        }
        if (settingsManager.settings.groupPlatforms3BreakablePlatformPlatformEnable && settingsManager.settings.breakablePlatformEnable)
        {
            group3platforms.Add(breakablePlatformPrefab);
        }
        if (settingsManager.settings.groupPlatforms3SpeedPlatformLeftPlatformEnable && settingsManager.settings.speedPlatformLeftEnable)
        {
            group3platforms.Add(movingPlatformRight);
        }
        if (settingsManager.settings.groupPlatforms3SpeedPlatformRightPlatformEnable && settingsManager.settings.speedPlatformRightEnable)
        {
            group3platforms.Add(movingPlatformLeft);
        }
        if (settingsManager.settings.groupPlatforms3SpikePlatformEnable && settingsManager.settings.spikePlatformEnable)
        {
            group3platforms.Add(spikePlatformPrefab);
        }
        if (settingsManager.settings.groupPlatforms3BouncePlatformEnable && settingsManager.settings.bouncePlatformEnable)
        {
            group3platforms.Add(bouncePlatform);
        }
    }
}