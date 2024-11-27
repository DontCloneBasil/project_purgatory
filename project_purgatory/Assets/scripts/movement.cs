using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;

public class movement : MonoBehaviour
{
    [Header("references")]
    private Rigidbody2D rb;
    public float moveSpeed;
    public float jumpForce;

    [Header("variables")]
    private Vector2 moveInput;
    private Vector2 moveDirection;

    private float fallforce;
    private float fallSpeedMax = 5f;

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
        //applies the movement in the direction you want
        rb.AddForce(moveDirection * moveSpeed * 10f, ForceMode2D.Force);
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
        if(grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
