﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beast : MonoBehaviour
{
    // Enum for the food type the beast is fed
    public enum FoodType
    {
        Ingredient,
        NormalDish,
        TranquilizedDish
    }

    // Movement
    private float moveRemainder = 0f;
    private float frameMovement = 0f;
    [SerializeField] private float MOVE_TIME = 0.5f;

    // Eating values
    [SerializeField] private float EATING_MOVEMENT_INCREASE = 0.05f;
    [SerializeField] private float EATING_INCREASE_TIMER = 60.0f;
    private float increasedEatingMovement = 0;
    private float eatingIncreaseTimer;

    // Animation values
    [SerializeField] private float AboveIsAngryX = -9.0f;
    [SerializeField] private float BelowIsSleepyX = -11.0f;

    // Level end value
    [SerializeField] private float BelowIsSleepingX = -12.0f;
    public bool isSleeping = false;
    private Animator anim;

    [SerializeField] private float MaxX = -6.0f;

    private bool angry = false;
    private bool shoutedWeDidIt = false;

    // Celebration shouting
    private float playWoohooCountdown = 1.0f;
    private bool playedWoohoo = false;

    void Start()
    {
        eatingIncreaseTimer = EATING_INCREASE_TIMER;
        anim = GetComponent<Animator>();
    }

    public void Move(float length, FoodType type)
    {
        if (!isSleeping)
        {
            anim.SetTrigger("Eat");
            moveRemainder += length;
            switch (type)
            {
                case FoodType.Ingredient: moveRemainder += increasedEatingMovement; break;
                case FoodType.NormalDish: moveRemainder += 2.5f * increasedEatingMovement; break;
                case FoodType.TranquilizedDish: break;
            }
            frameMovement = moveRemainder / MOVE_TIME;

            //eating sound
            FindObjectOfType<AudioManager>().Play("beast_eating_sound");
        }

    }

    private void Update()
    {
        // Beast starts to sleep if outside of the screen, should initialize zone change
        if (transform.position.x < BelowIsSleepingX)
        {
            isSleeping = true;
            if (!shoutedWeDidIt)
            {
                FindObjectOfType<AudioManager>().Play("raccon_say_we_did_it");
                shoutedWeDidIt = true;
            }
        }

        // Animations
        else if (transform.position.x > AboveIsAngryX)
        {
            anim.SetBool("Angry", true);
            if (!angry)
            {
                if(Random.Range(0, 2) <= 0)
                {
                    FindObjectOfType<AudioManager>().Play("raccon_say_the_mushrooms_will_make_him_sleepy");
                }
                else
                {
                    FindObjectOfType<AudioManager>().Play("raccon_say_get_the_mushroom");
                }

                angry = true;
            }
        }
        else if(transform.position.x < BelowIsSleepyX)
        {
            anim.SetBool("Sleepy", true);
            angry = false;
        }
        else
        {
            anim.SetBool("Sleepy", false);
            anim.SetBool("Angry", false);
            angry = false;
        }

        if(!playedWoohoo && playWoohooCountdown < 0)
        {
            FindObjectOfType<AudioManager>().Play("raccon_say_woohoo");
            playedWoohoo = true;
        }
    }

    private void FixedUpdate()
    {
        if (!isSleeping)
        {
            // Movement
            if (moveRemainder != 0 && transform.position.x < MaxX) {
                transform.position += Vector3.right * frameMovement * Time.deltaTime;
                float prev = Mathf.Sign(moveRemainder);
                moveRemainder -= frameMovement * Time.deltaTime;
                float post = Mathf.Sign(moveRemainder);

                if (prev != post)
                    moveRemainder = 0;

                // Set max position
                if (transform.position.x > MaxX)
                {
                    transform.position -= Vector3.right * transform.position.x;
                    transform.position += Vector3.right * MaxX;
                }
            }
        }

        // Handle progressice difficulty
        if (eatingIncreaseTimer <= 0)
        {
            increasedEatingMovement += EATING_MOVEMENT_INCREASE;
            eatingIncreaseTimer = EATING_INCREASE_TIMER;
        }

        // Decrease timers
        eatingIncreaseTimer -= Time.deltaTime;
        if (isSleeping)
            playWoohooCountdown -= Time.deltaTime;
    }
}
