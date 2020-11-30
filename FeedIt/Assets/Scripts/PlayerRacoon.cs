using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRacoon : Player
{
    protected Animator raccoonAnim;

    [SerializeField] private PlayerOstrich ostrich;
    [SerializeField] private float DETACH_TIME = 0.3f;
    [SerializeField] private float RETACH_TIME = 0.3f;
    [SerializeField] private Vector3 localPosition = new Vector3(-0.51f, 0.44f, 1.0f);

    private float detachTimer = 0;
    private float retachTimer = 0;

    bool overOstrich = false;
    bool prevOverOstrich = false;

    private void Awake(){
        raccoonAnim = GetComponent<Animator>();

    }


    protected override void KeyInput()
    {
        // Pressed keys
        if (Input.GetKeyDown(KeyCode.W))
            jumpBuffer = JUMP_BUFFER_TIME;

        // Held keys
        if (Input.GetKey(KeyCode.W))
            holdingJumpKey = true;
        if (Input.GetKey(KeyCode.S))
            holdingDownKey = true;
    }

    protected override void HandleInput()
    {
        // Detach
        if (transform.parent != null && detachTimer < 0)
        {
            if (holdingJumpKey || holdingDownKey)
            {
                transform.parent = null;
                retachTimer = RETACH_TIME;
                rigidbody.isKinematic = false;

                // Fix jumping velocity for the racoon when the ostrich is jumping
                //if(ostrich.transform.GetComponent<Rigidbody2D>().velocity.y < 0 && holdingDownKey)
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
        UpdateRaccoonAnimation(rigidbody.velocity.y);
        // Decrease cooldowns
        retachTimer -= Time.deltaTime;
        detachTimer -= Time.deltaTime;
    }



    private void UpdateRaccoonAnimation(float _velocity){
        Debug.Log("UpdateRaccoonAnimation: " + _velocity);
        if(transform.parent == null){
            if (_velocity > 0)
            {
                raccoonAnim.SetBool("Jumping", true);
                raccoonAnim.SetBool("Falling", false);
                raccoonAnim.SetBool("Running", false);
            }
            else if (_velocity < 0)
            {
                raccoonAnim.SetBool("Jumping", false);
                raccoonAnim.SetBool("Falling", true);
                raccoonAnim.SetBool("Running", false);
            }
            else
            {
                raccoonAnim.SetBool("Jumping", false);
                raccoonAnim.SetBool("Falling", false);
                raccoonAnim.SetBool("Running", true);
                }
        }else{
            raccoonAnim.SetBool("Jumping", false);
            raccoonAnim.SetBool("Falling", false);
            raccoonAnim.SetBool("Running", false);}
        
        }
    
}
