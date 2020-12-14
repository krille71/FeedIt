using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    protected Animator anim;

    [SerializeField] private GameObject InGameMenu;
    private InGameMenu inGameMenu;
    [SerializeField] private LayerMask platformsLayerMask;
    protected Rigidbody2D rigidbody;
    protected BoxCollider2D boxCollider;

    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2.5f;
    [SerializeField] private float jumpVelocity = 10f;

    protected bool holdingJumpKey = false;
    protected bool holdingDownKey = false;
    [SerializeField] private float COYOTE_TIME = 0.1f;
    [SerializeField] protected float JUMP_BUFFER_TIME = 0.1f;
    private float mayJump;
    protected float jumpBuffer;



    // Start is called before the first frame update
    protected virtual void Start()
    {
        rigidbody = transform.GetComponent<Rigidbody2D>();
        boxCollider = transform.GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        inGameMenu = InGameMenu.transform.GetComponent<InGameMenu>();
    }

    protected virtual void KeyInput()
    {

    }

    protected virtual void HandleInput()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!inGameMenu.GameIsPaused)
        {
            holdingJumpKey = false;
            holdingDownKey = false;
            UpdatePlayerAnimation(rigidbody.velocity.y);

            if (isGrounded())
                mayJump = COYOTE_TIME;

            KeyInput();
            HandleInput();

            // Initial jump
            if (mayJump > 0 && jumpBuffer > 0 && holdingJumpKey)
            {
                rigidbody.velocity = Vector2.up * jumpVelocity;
                mayJump = 0;
                jumpBuffer = 0;
                this.playJumpSound();

            }

            // Jump through semisolid by deactivating their hitbox
            if (holdingDownKey && isGrounded())
            {
                RaycastHit2D[] casts = boxCastAll(0.001f, 5f);
                foreach (RaycastHit2D cast in casts)
                {
                    if (cast.collider != null && cast.transform.gameObject.tag == "SemiSolid")
                        cast.transform.gameObject.GetComponent<Collider2D>().enabled = false;
                }
            }
        }
    }

    protected virtual void FixedUpdate()
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

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Beast"))
        {
            inGameMenu.GameOver();
            //"Players screaming when getting eaten"
            FindObjectOfType<AudioManager>().Play("beast_eating_player_sound");
            FindObjectOfType<AudioManager>().Play("bird_dying");
        }
    }

    // Toggles between running, jumping and falling depending on character velocity on Y-axis
    protected virtual void UpdatePlayerAnimation(float _velocity){

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
        // Always seen as grounded if kinematic
        if (rigidbody.isKinematic == true)
            return true;

        RaycastHit2D cast = boxCast();

        // If hit and not currently inside that object and not jumping return true
        return cast.collider != null
            && !boxCollider.bounds.Intersects(cast.transform.GetComponent<BoxCollider2D>().bounds)
            && rigidbody.velocity.y <= 0;
    }

    //should play jumpsound for induvidaul character ////jumpSound.Play();
    protected virtual void playJumpSound(){

    }
}
