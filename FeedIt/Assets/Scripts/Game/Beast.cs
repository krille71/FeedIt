using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beast : MonoBehaviour
{
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

    void Start()
    {
        eatingIncreaseTimer = EATING_INCREASE_TIMER;
    }

    public void Move(float length, FoodType type)
    {
        moveRemainder += length;
        switch (type)
        {
            case FoodType.Ingredient: moveRemainder += increasedEatingMovement; break;
            case FoodType.NormalDish: moveRemainder += 2.5f * increasedEatingMovement; break;
            case FoodType.TranquilizedDish: break;
        }
        frameMovement = moveRemainder / MOVE_TIME;
    }

    private void FixedUpdate()
    {
        // Movement
        if (moveRemainder != 0) {
            transform.position += Vector3.right * frameMovement * Time.deltaTime;
            float prev = Mathf.Sign(moveRemainder);
            moveRemainder -= frameMovement * Time.deltaTime;
            float post = Mathf.Sign(moveRemainder);

            if (prev != post)
                moveRemainder = 0;
        }

        // Handle progressice difficulty 
        if (eatingIncreaseTimer <= 0)
        {
            increasedEatingMovement += EATING_MOVEMENT_INCREASE;
            eatingIncreaseTimer = EATING_INCREASE_TIMER;
        }

        eatingIncreaseTimer -= Time.deltaTime;
    }
}
