using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{ 

    // Destroy the object when outside of the screen
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
