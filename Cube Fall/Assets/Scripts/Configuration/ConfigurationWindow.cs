using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ConfigurationWindow : EditorWindow
{
    private SettingsManager settingsManager;
    private BalansMenu balansMenu;
    private ScoringMenu scoringMenu;
    private AdvencedMenu advencedMenu;

    private Vector2 scrollPosition = Vector2.zero;
    private string configuration = "Balance";
    private bool loadData = true;

    [MenuItem("Window/Configuration")]
    public static void ShowWindow()
    {
        GetWindow<ConfigurationWindow>(false, "Configuration", true);

        //ConfigurationWindow window = (ConfigurationWindow)EditorWindow.GetWindow(typeof(ConfigurationWindow), true, "My Empty Window");
        //window.position = new Rect(0, 0, 500, 500);
        //window.Show();
    }

    void OnGUI()
    {
        Initialization();

        GUILayout.Label("Configuration: " + configuration, EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Balance"))
        {
            configuration = "Balance";
        }
        if (GUILayout.Button("Scoring"))
        {
            configuration = "Scoring";
        }
        if (GUILayout.Button("Advenced"))
        {
            configuration = "Advenced";
        }
        EditorGUILayout.EndHorizontal();

        scrollPosition = GUILayout.BeginScrollView(scrollPosition, true, true, GUILayout.Width(position.width), GUILayout.Height(position.height - 40));

        if (configuration == "Balance")
        {
            balansMenu.View();
        }
        else if (configuration == "Scoring")
        {
            scoringMenu.View();
        }
        else if (configuration == "Advenced")
        {
            advencedMenu.View();
        }

        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Reset"))
        {
            ResetData();
        }
        if (GUILayout.Button("Close"))
        {
            this.Close();
        }

        GUILayout.EndScrollView();
        settingsManager.SaveData();
    }

    private void Initialization()
    {
        if (loadData)
        {
            settingsManager = ScriptableObject.CreateInstance("SettingsManager") as SettingsManager;
            balansMenu = new BalansMenu(settingsManager);
            scoringMenu = new ScoringMenu(settingsManager);
            advencedMenu = new AdvencedMenu(settingsManager);
            loadData = false;
        }
    }

    private void ResetData()
    {
        if (configuration == "Balance")
        {
            settingsManager.ResetBalance();
        }
        else if (configuration == "Scoring")
        {
            settingsManager.ResetScoring();
        }
        else
        {
            settingsManager.ResetData();
        }
    }
}