using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dish : MonoBehaviour
{
    [SerializeField] private float BEAST_EATING_MOVEMENT = 0.5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Beast"))
        {
            other.gameObject.GetComponent<Beast>().Move(BEAST_EATING_MOVEMENT);
            Destroy(gameObject);
        }
    }
}
