using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raccoon : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private Animator anim;

        [SerializeField] private float jumpVelocity = 10f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = transform.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
                if (Input.GetKeyDown(KeyCode.T))
                    rigidbody.velocity = Vector2.up * jumpVelocity;
    }
}
