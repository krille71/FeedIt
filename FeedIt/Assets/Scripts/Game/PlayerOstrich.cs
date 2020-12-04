using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOstrich : Player
{

    private bool holdingEatKey = false;
    [SerializeField] private float EATING_TIME = 15;
    private float eatingTimer = 0;

    protected override void KeyInput()
    {
        // Pressed keys
        if (Input.GetKeyDown(KeyCode.UpArrow))
            jumpBuffer = JUMP_BUFFER_TIME;

        // Held keys
        if (Input.GetKey(KeyCode.UpArrow))
            holdingJumpKey = true;
        if (Input.GetKey(KeyCode.DownArrow))
            holdingDownKey = true;
        if(Input.GetKey(KeyCode.RightArrow))
            holdingEatKey = true;
    }

    protected override void Update()
    {
        holdingEatKey = false;
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        eatingTimer -= Time.deltaTime;
    }

    // Toggles between running, jumping and falling depending on character velocity on Y-axis
    protected override void UpdatePlayerAnimation(float _velocity)
    {
        if (_velocity > 0)
        {
            runningSound.Stop();
            anim.SetBool("Jumping", true);
            anim.SetBool("Falling", false);
        }
        else if (_velocity < 0)
        {
            runningSound.Stop();
            anim.SetBool("Jumping", false);
            anim.SetBool("Falling", true);
        }
        else
        {
            if (anim.GetBool("Falling") && !landingSound.isPlaying)
            {
                landingSound.Play();
            }
            if (!runningSound.isPlaying)
            {
                runningSound.Play();
            }
            anim.SetBool("Jumping", false);
            anim.SetBool("Falling", false);
        }

        // TODO ostrich eating animation based on eatingTimer being [0, EATING_TIME]
    }

    public bool eat()
    {
        if(holdingEatKey && eatingTimer <= 0)
        {
            eatingTimer = EATING_TIME;
            // TODO play ostrich eating sound
            return true;
        }
        return false;
    }
}
