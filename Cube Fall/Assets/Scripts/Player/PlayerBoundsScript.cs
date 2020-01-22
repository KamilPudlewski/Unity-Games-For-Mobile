using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoundsScript : MonoBehaviour
{
    private SettingsManager settingsManager;

    public float minX = -2.6f;
    public float maxX = 2.6f;
    public float minY = -5.6f;
    private bool outOfBounds;

    void Start()
    {
        settingsManager = ScriptableObject.CreateInstance("SettingsManager") as SettingsManager;
    }

    void Update()
    {
        if (settingsManager.settings.swapSideOnEdgeEnable)
        {
            CheckBoundsSwapSide();
        }
        else
        {
            CheckBoundsBlockSide();
        }
        CheckFallDown();
    }

    private void CheckBoundsBlockSide()
    {
        Vector2 temp = transform.position;

        if (temp.x > maxX)
            temp.x = maxX;

        if (temp.x < minX)
            temp.x = minX;

        transform.position = temp;
    }

    private void CheckBoundsSwapSide()
    {
        Vector2 temp = transform.position;

        if (temp.x > maxX)
            temp.x = minX;

        if (temp.x < minX)
            temp.x = maxX;

        transform.position = temp;
    }

    private void CheckFallDown()
    {
        Vector2 temp = transform.position;

        if (temp.y <= minY)
        {
            if (!outOfBounds)
            {
                outOfBounds = true;

                SoundManager.instance.DeathSound();
                GameManager.instance.RestartGame();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "TopSpikes")
        {
            transform.position = new Vector2(1000f, 1000f);
            SoundManager.instance.DeathSound();
            GameManager.instance.RestartGame();
        }
    }
}
