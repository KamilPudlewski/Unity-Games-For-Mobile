using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public GameObject additionalPointsPrefab;

    private int score = 0;
    private bool countScore = true;

    private Text scoreText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        scoreText = GetComponent<Text>();
    }

    void Update()
    {
        if (countScore)
        {
            score += 1;
        }
        scoreText.text = score.ToString();
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = score.ToString();
    }

    public void StartCountScore()
    {
        countScore = true;
    }

    public void StopCountScore()
    {
        countScore = false;
    }

    public void AddScore(int score)
    {
        this.score += score;
    }

    public void AddScore(int score, Vector3 transform)
    {
        this.score += score;
        transform.x += 240;
        transform.y += 400;

        GameObject bonusScoreText = Instantiate(additionalPointsPrefab, transform, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
        bonusScoreText.GetComponent<AdditionalPointsScript>().SetPoints(score);
    }
}
