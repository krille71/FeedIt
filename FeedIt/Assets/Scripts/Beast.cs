using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beast : MonoBehaviour
{
    private float moveRemainder = 0f;
    private float frameMovement = 0f;
    [SerializeField] private float MOVE_TIME = 0.5f;

    public void Move(float length)
    {
        moveRemainder += length;
        frameMovement = moveRemainder / MOVE_TIME;
    }

    private void FixedUpdate()
    {
        if (moveRemainder != 0) {
            transform.position += Vector3.right * frameMovement * Time.deltaTime;
            float prev = Mathf.Sign(moveRemainder);
            moveRemainder -= frameMovement * Time.deltaTime;
            float post = Mathf.Sign(moveRemainder);

            if (prev != post)
                moveRemainder = 0;
        }
    }
}
