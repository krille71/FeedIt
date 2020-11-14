using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask platformsLayerMask;
    private Rigidbody2D rigidbody;
    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = transform.GetComponent<Rigidbody2D>();
        boxCollider = transform.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isGrounded() && Input.GetKeyDown(KeyCode.UpArrow))
        {
            // Hasty jump code, should be changed
            rigidbody.velocity = Vector2.up * 10f;
        }
    }

    private bool isGrounded()
    {

        // Boxcast a tiny box from the bottom
        Vector3 s = boxCollider.bounds.size;
        s.y = 0.001f;
        RaycastHit2D cast = Physics2D.BoxCast(
            boxCollider.bounds.center + Vector3.down * boxCollider.bounds.size.y/2f,
            s,
            0f, Vector2.down, 0.1f, platformsLayerMask);

        // If hit and not currently inside that object return true
        return cast.collider != null && !boxCollider.bounds.Intersects(cast.transform.GetComponent<BoxCollider2D>().bounds);
    }
}
