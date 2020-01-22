using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void RestartGame()
    {
        ScoreManager.instance.StopCountScore();
        Invoke("RestartAfterTimer", 2f);
    }

    void RestartAfterTimer()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        ScoreManager.instance.ResetScore();
        ScoreManager.instance.StartCountScore();
    }
}
