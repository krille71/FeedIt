using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dish : MonoBehaviour
{
    [SerializeField] private float BEAST_EATING_MOVEMENT = 0.5f;
    private Rigidbody2D rigidbody;
    private bool onGround = false;
    [SerializeField] private float scrollSpeed = 10f;
    [SerializeField] private float destroyPoistionX = -45f;
    void start()
    {
        rigidbody = transform.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Beast"))
        {
            other.gameObject.GetComponent<Beast>().Move(BEAST_EATING_MOVEMENT);
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }

    protected virtual void FixedUpdate()
    {
        if(transform.position.x < destroyPoistionX)
        {
            Destroy(this.gameObject);
        }
         transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
        // Gravity
        //if (!onGround)
            //rigidbody.position += -Vector2.up * 0.01f;
            //rigidbody.velocity += Vector2.up * Physics2D.gravity.y * Time.deltaTime;
    }
}
