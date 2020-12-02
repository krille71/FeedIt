using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOstrich : Player
{
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
    }
}
