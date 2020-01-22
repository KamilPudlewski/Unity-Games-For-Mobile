using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScoringMenu
{
    private SettingsManager settingsManager;

    private static Texture2D bouncePlatformTexture = null;

    public ScoringMenu(SettingsManager settingsManager)
    {
        this.settingsManager = settingsManager;

        bouncePlatformTexture = (Texture2D)Resources.Load("Platform/Bounce Platform", typeof(Texture2D));
    }

    public void View()
    {
        GUILayout.Label("Score Points Settings", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label(bouncePlatformTexture, GUILayout.Width(100), GUILayout.Height(30));
        settingsManager.settings.bouncePlatformBonusPoints = EditorGUILayout.IntSlider(new GUIContent("Bounce Platform Bonus Points", "Add Additional Points After Touch Bounce Platform"), settingsManager.settings.bouncePlatformBonusPoints, 10, 1000);
        EditorGUILayout.EndHorizontal();
    }
}