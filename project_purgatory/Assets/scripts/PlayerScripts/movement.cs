using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;

public class movement : MonoBehaviour
{
    [Header("references")]
    private Rigidbody2D rb;
    public float moveSpeed;
    public float jumpForce;
    public float jumpCharges;
    private float jumpChargesLeft;

    [Header("variables")]
    private Vector2 moveInput;
    private Vector2 moveDirection;

    public float fallforce;
    public float fallSpeedMax;

    [Header("GroundCheck")]
    public float playerheight;
    public LayerMask whatIsGround;
    public bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.Raycast(transform.position, Vector2.down, playerheight * 0.5f + 0.2f, whatIsGround);

        if(!grounded)
        {
            if (fallforce <= fallSpeedMax) fallforce += Time.deltaTime * 10;   
            rb.AddForce(transform.up * fallforce * -1f, ForceMode2D.Force);
        }
        else if (grounded && fallforce != 0f)
        {
            if (jumpChargesLeft != jumpCharges) jumpChargesLeft = jumpCharges;
            fallforce = 0f;
        }
    }

    //fixed update is called once every physics calculation
    void FixedUpdate()
    {
        MovePlayer();
    }
    //this function is user to use the player direction to actually move the player
    private void MovePlayer()
    {
        //makes a new vector2 value from moveInput's x value (which is the only value we'll user for now)
        moveDirection = new Vector2(moveInput.x, 0f);
        if(grounded)
        {
           //applies the movement in the direction you want
            rb.AddForce(moveDirection * moveSpeed * 10f, ForceMode2D.Force);
        }
        else 
        {
            //applies the movement in the direction you want
            rb.AddForce(moveDirection * moveSpeed * 10f * 1.5f, ForceMode2D.Force); //makes air movement 50% faster
        }

    }
    //this value is called whenever the move input has changed and changes the direction the player will move too
    private void OnMove(InputValue inputValue)
    {
        // stores the input value into a vector 2
        moveInput = inputValue.Get<Vector2>();
        
    }
    // jump funcation
    private void OnJump()
    {
        if(jumpChargesLeft > 0)
        {
            jumpChargesLeft--;
            fallforce = 0;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
