using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRacoon : Player
{

    [SerializeField] private PlayerOstrich ostrich;
    [SerializeField] private float DETACH_TIME = 0.3f;
    [SerializeField] private float RETACH_TIME = 0.3f;
    private float detachTimer = 0;
    private float retachTimer = 0;

    protected override void KeyInput()
    {
        // Pressed keys
        if (Input.GetKeyDown(KeyCode.W))
            jumpBuffer = JUMP_BUFFER_TIME;

        // Held keys
        if (Input.GetKey(KeyCode.W))
            holdingJumpKey = true;
        if (Input.GetKey(KeyCode.S))
            holdingDownKey = true;
    }

    protected override void HandleInput()
    {
        if (transform.parent != null && detachTimer < 0)
        {
            if (holdingJumpKey || holdingDownKey)
            {
                transform.parent = null;
                retachTimer = RETACH_TIME;
                rigidbody.isKinematic = false;
            }
        }

        // Jumping on ostrich
        /*if (transform.parent == null && retachTimer < 0)
        {
            transform.parent = ostrich;
            retachTimer = RETACH_TIME;
        }*/
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        // Decrease cooldowns
        retachTimer -= Time.deltaTime;
        detachTimer -= Time.deltaTime;
    }
}
