using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    private SettingsManager settingsManager;
    private float moveSpeed = 2f;
    public float boundY = 6f;

    public bool isPlatform;
    public bool isMovingPlatformLeft;
    public bool isMovingPlatformRight;
    public bool isBreakable;
    public bool isSpike;
    public bool isBounce;

    private Animator animator;
    private Rigidbody2D rigidbody2d;

    void Awake()
    {
        settingsManager = ScriptableObject.CreateInstance("SettingsManager") as SettingsManager;

        if (isBreakable)
        {
            animator = GetComponent<Animator>();
        }

        if (isBounce)
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
            rigidbody2d.sharedMaterial.bounciness = settingsManager.settings.bouncePlatformBounciness;
        }

        SetMoveSpeed();
    }

    void Update()
    {
        AdaptiveDifficultyAllPlatformsSpeedLevel();
        Move();
    }

    private void Move()
    {
        Vector2 temp = transform.position;
        temp.y += moveSpeed * Time.deltaTime;
        transform.position = temp;

        if (temp.y >= boundY)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void BreakableDeactivate()
    {
        Invoke("DeactivateGameObject", 0.25f);
    }

    private void DeactivateGameObject()
    {
        SoundManager.instance.IceBreakSound();
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Player")
        {
            if (isSpike)
            {
                target.transform.position = new Vector2(1000f, 1000f);
                SoundManager.instance.GameOverSound();
                GameManager.instance.RestartGame();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == "Player")
        {
            if (isPlatform)
            {
                SoundManager.instance.LandSound();
            }

            if (isBounce)
            {
                SoundManager.instance.LandSound();
                ScoreManager.instance.AddScore(settingsManager.settings.bouncePlatformBonusPoints, target.transform.position);
            }

            if (isBreakable)
            {
                SoundManager.instance.LandSound();
                animator.Play("Break");
            }
        }
    }

    private void OnCollisionStay2D(Collision2D target)
    {
        if (target.gameObject.tag == "Player")
        {
            if (isMovingPlatformLeft)
            {
                target.gameObject.GetComponent<PlayerMovementScript>().PlatformMove(-settingsManager.settings.speedPlatformLeftPowerActionSpeed);
            }

            if (isMovingPlatformRight)
            {
                target.gameObject.GetComponent<PlayerMovementScript>().PlatformMove(settingsManager.settings.speedPlatformRightPowerActionSpeed);
            }
        }
    }

    private void AdaptiveDifficultyAllPlatformsSpeedLevel()
    {
        if (settingsManager.settings.adaptiveDifficultyAllPlatformsSpeedLevel)
        {
            settingsManager.LoadData();
            moveSpeed = settingsManager.settings.currentPlatformSpawnTimer;
        }
    }

    private void SetMoveSpeed()
    {
        if (settingsManager.settings.allPlatformsSpeedEnable)
        {
            moveSpeed = settingsManager.settings.allPlatformsSpeed;
        }
        else if (settingsManager.settings.adaptiveDifficultyAllPlatformsSpeedLevel)
        {
            moveSpeed = settingsManager.settings.startAllPlatforsmSpeedLevel;
        }
        else
        {
            if (isPlatform)
            {
                moveSpeed = settingsManager.settings.standardPlatformSpeed;
            }
            if (isMovingPlatformLeft)
            {
                moveSpeed = settingsManager.settings.speedPlatformLeftSpeed;
            }
            if (isMovingPlatformRight)
            {
                moveSpeed = settingsManager.settings.speedPlatformRightSpeed;
            }
            if (isBreakable)
            {
                moveSpeed = settingsManager.settings.breakablePlatformSpeed;
            }
            if (isSpike)
            {
                moveSpeed = settingsManager.settings.spikePlatformSpeed;
            }
            if (isBounce)
            {
                moveSpeed = settingsManager.settings.bouncePlatformSpeed;
            }
        }
    }
}
