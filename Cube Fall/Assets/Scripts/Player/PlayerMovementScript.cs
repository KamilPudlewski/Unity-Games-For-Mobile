using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    private Rigidbody2D playerBody;

    private SettingsManager settingsManager;
    private float moveSpeed = 2f;

    void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        settingsManager = ScriptableObject.CreateInstance("SettingsManager") as SettingsManager;
        moveSpeed = settingsManager.settings.playerMoveSpeed;
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            playerBody.velocity = new Vector2(moveSpeed, playerBody.velocity.y);
        }
        if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            playerBody.velocity = new Vector2(-moveSpeed, playerBody.velocity.y);
        }
    }

    public void PlatformMove(float x)
    {
        playerBody.velocity = new Vector2(x, playerBody.velocity.y);
    }
}
