using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dish : MonoBehaviour
{
    [SerializeField] private float BEAST_EATING_MOVEMENT = 0.5f;
    [SerializeField] private float scrollSpeed = 10f;
    [SerializeField] private float destroyPoistionX = -45f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Beast"))
        {
            other.gameObject.GetComponent<Beast>().Move(BEAST_EATING_MOVEMENT);
            Destroy(gameObject);
        }
    }

    protected virtual void FixedUpdate()
    {
        if(transform.position.x < destroyPoistionX)
        {
            Destroy(this.gameObject);
        }
         transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
    }
}
