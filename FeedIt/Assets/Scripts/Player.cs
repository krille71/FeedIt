using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator anim;

    [SerializeField] private LayerMask platformsLayerMask;
    private Rigidbody2D rigidbody;
    private BoxCollider2D boxCollider;

    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2.5f;
    [SerializeField] private float jumpVelocity = 10f;

    private bool holdingJumpKey = false;
    private bool holdingDownKey = false;
    [SerializeField] private float COYOTE_TIME = 0.1f;
    [SerializeField] private float JUMP_BUFFER_TIME = 0.1f;
    private float mayJump;
    private float jumpBuffer;

    // Sound effects
    public AudioSource jumpSound;
    public AudioSource landingSound;
    public AudioSource runningSound;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = transform.GetComponent<Rigidbody2D>();
        boxCollider = transform.GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        holdingJumpKey = false;
        holdingDownKey = false;
        UpdatePlayerAnimation(rigidbody.velocity.y);
        

        if (isGrounded())
            mayJump = COYOTE_TIME;

        // Pressed keys
        if (Input.GetKeyDown(KeyCode.UpArrow))
            jumpBuffer = JUMP_BUFFER_TIME;

        // Held keys
        if (Input.GetKey(KeyCode.UpArrow))
            holdingJumpKey = true;
        if (Input.GetKey(KeyCode.DownArrow))
            holdingDownKey = true;

        // Initial jump
        if (mayJump > 0 && jumpBuffer > 0 && holdingJumpKey)
        {
            rigidbody.velocity = Vector2.up * jumpVelocity;
            mayJump = 0;
            jumpBuffer = 0;
            jumpSound.Play();
        }

        // Jump through semisolid by deactivating their hitbox
        RaycastHit2D[] casts = boxCastAll(0.001f, 5f);
        foreach(RaycastHit2D cast in casts)
        {
            if(isGrounded() && cast.collider != null && cast.transform.gameObject.tag == "SemiSolid" && holdingDownKey)
                cast.transform.gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    private void FixedUpdate()
    {

        // Gravity based on where you are in the jump
        if (rigidbody.velocity.y < 0)
            rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        else if (rigidbody.velocity.y > 0 && !holdingJumpKey)

            rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;


        // Decrease cooldowns
        mayJump -= Time.deltaTime;
        jumpBuffer -= Time.deltaTime;
    }

    // Toggles between running, jumping and falling depending on character velocity on Y-axis
    private void UpdatePlayerAnimation(float _velocity){
        if( _velocity > 0){
            runningSound.Stop();
            anim.SetBool("Jumping", true);
            anim.SetBool("Falling", false);
        }else if(_velocity < 0){
            runningSound.Stop();
            anim.SetBool("Jumping", false);
            anim.SetBool("Falling", true);
        }else{
            if(anim.GetBool("Falling")){
                landingSound.Play();
            }
            if(!runningSound.isPlaying){
                runningSound.Play();
            }    
            anim.SetBool("Jumping", false);
            anim.SetBool("Falling", false);
        }
    }

    // Boxcast a tiny box from the bottom of the player and returns all hits
    private RaycastHit2D[] boxCastAll(float height, float widthMult)
    {
        Vector3 s = boxCollider.bounds.size;
        s.y = height;
        s.x *= widthMult;
        RaycastHit2D[] cast = Physics2D.BoxCastAll(
            boxCollider.bounds.center + Vector3.down * boxCollider.bounds.size.y / 2f,
            s,
            0f, Vector2.down, 0.1f, platformsLayerMask);
        return cast;
    }

    // Boxcast a tiny box from the bottom of the player
    private RaycastHit2D boxCast()
    {
        Vector3 s = boxCollider.bounds.size;
        s.y = 0.0005f;
        RaycastHit2D cast = Physics2D.BoxCast(
            boxCollider.bounds.center + Vector3.down * boxCollider.bounds.size.y / 2f,
            s,
            0f, Vector2.down, 0.1f, platformsLayerMask);
        return cast;
    }

    private bool isGrounded()
    {
        RaycastHit2D cast = boxCast();

        // If hit and not currently inside that object and not jumping return true
        return cast.collider != null 
            && !boxCollider.bounds.Intersects(cast.transform.GetComponent<BoxCollider2D>().bounds) 
            && rigidbody.velocity.y <= 0;
    }
}
