using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{

    [SerializeField] private float scrollSpeed = 10f;
    [SerializeField] private float destroyPoistionX = -45f;

    void Update()
    {
        // Destroy chunk when exiting the screen
        if(transform.position.x < destroyPoistionX)
        {
            Destroy(this.gameObject);
        }
    }

    // Move the object
    void FixedUpdate()
    {
        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
    }
}
