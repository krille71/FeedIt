using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{

    [SerializeField] private float scrollSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Move the object
    void FixedUpdate()
    {
        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
    }

    // Destroy the object when outside of the screen
    void OnBecameInvisible()
    {
        Destroy(gameObject);
        //Debug.Log("Destroy!");
    }
}
