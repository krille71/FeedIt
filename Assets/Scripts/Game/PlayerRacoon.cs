using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRacoon : Player
{
    [SerializeField] private PlayerOstrich ostrich;
    [SerializeField] private float DETACH_TIME = 0.0f;
    [SerializeField] private float RETACH_TIME = 0.3f;
    [SerializeField] private Vector3 localPosition = new Vector3(-0.31f, 0.44f, -0.1f);

    private float detachTimer = 0;
    private float retachTimer = 0;

    bool overOstrich = false;
    bool prevOverOstrich = false;

    bool pressedJumpKey = false;
    bool pressedDownKey = false;

    private CookingHandling cooking;

    protected override void Start()
    {
        base.Start();
        cooking = GameObject.FindGameObjectWithTag("Collection").GetComponent<CookingHandling>();
    }

    protected override void KeyInput()
    {
        // Pressed keys
        if (Input.GetKeyDown(KeyCode.W))
        {
            jumpBuffer = JUMP_BUFFER_TIME;
            pressedJumpKey = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
            pressedDownKey = true;

        // Held keys
        if (Input.GetKey(KeyCode.W))
            holdingJumpKey = true;
        if (Input.GetKey(KeyCode.S))
            holdingDownKey = true;
    }

    protected override void Update()
    {
        pressedJumpKey = false;
        pressedDownKey = false;
        base.Update();
    }


    protected override void HandleInput()
    {
        // Detach
        if (transform.parent != null && detachTimer < 0 && !cooking.isCooking)
        {
            if (pressedJumpKey || pressedDownKey)
            {
                transform.parent = null;
                retachTimer = RETACH_TIME;
                rigidbody.isKinematic = false;

                // Fix jumping velocity for the racoon when the ostrich is jumping
                // if(ostrich.transform.GetComponent<Rigidbody2D>().velocity.y < 0 && holdingDownKey)
                rigidbody.velocity = Vector3.zero;
            }
        }

        // Retach
        if (transform.parent == null && retachTimer < 0)
        {
            if(prevOverOstrich && !overOstrich)
            {
                detachTimer = DETACH_TIME;
                transform.parent = ostrich.transform;
                transform.localPosition = localPosition;
                rigidbody.isKinematic = true;
            }
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        // racoons positin in relation to the ostrich
        prevOverOstrich = overOstrich;
        var localPos = ostrich.transform.InverseTransformPoint(transform.position);
        overOstrich = localPos.y > localPosition.y;

        // Animations
        UpdateRaccoonAnimation(rigidbody.velocity.y);

        // Decrease cooldowns
        retachTimer -= Time.deltaTime;
        detachTimer -= Time.deltaTime;
    }



    private void UpdateRaccoonAnimation(float _velocity){
        if(transform.parent == null){
            if (_velocity > 0)
            {
                anim.SetBool("Jumping", true);
                anim.SetBool("Falling", false);
                anim.SetBool("Running", false);
                anim.SetBool("Cooking", false);
            }
            else if (_velocity < 0)
            {
                anim.SetBool("Jumping", false);
                anim.SetBool("Falling", true);
                anim.SetBool("Running", false);
                anim.SetBool("Cooking", false);
            }
            else
            {
                anim.SetBool("Jumping", false);
                anim.SetBool("Falling", false);
                anim.SetBool("Running", true);
                anim.SetBool("Cooking", false);
            }
        }else{
            if(cooking.isCooking){
                anim.SetBool("Jumping", false);
                anim.SetBool("Falling", false);
                anim.SetBool("Running", false);
                anim.SetBool("Cooking", true);
            }else{
                anim.SetBool("Jumping", false);
                anim.SetBool("Falling", false);
                anim.SetBool("Running", false);
                anim.SetBool("Cooking", false);
            }
        }
    }

    // play racoon Jumpsound
    protected override void playJumpSound(){
        if(transform.parent == null)
        {
            FindObjectOfType<AudioManager>().Play("racoon_jump");
        }
    }
}
