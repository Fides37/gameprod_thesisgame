using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed; //float for how fast player can move
    private float jumpForce = 5f; //how high player can jump
    float horizontalInput; //horizontal input; a & d keys, left and right arrows
    float verticalInput; //vertical input; w & s keys, up and down arrows
    private float jumpCoolDown = 0.25f; //cooldown on jumping, 0.25 secs
    private float airMultiplier = 0.4f; // 
    private float groundDrag = 5f; 
    private float playerHeight = 2; //how tall player is
    private float crouchYScale = 1f;
    private float startYScale;


    private bool canJump;
    private bool isGrounded;

    [Header("public references + physics")]
    Vector3 moveDirection; //the direction the player moves to
    Rigidbody rb; //reference to player's rigidbody
    public LayerMask whatIsGrounded; //references the ground layermask
    public Transform orientation; //player's transform, their position and rotation

    public enum MovementStates
    {
        walking,
        sprinting,
        crouching,
        air
    }

    public MovementStates movementStates;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        startYScale = transform.localScale.y;
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGrounded);

        MyInput();
        SpeedControl();

        if (isGrounded)
        {
            rb.drag = groundDrag;
            canJump = true;
        }
        else
        {
            rb.drag = 0f;
            canJump = false;
        }

        Debug.Log(movementStates);

    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && canJump && isGrounded)
        {
            canJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCoolDown);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            movementStates = MovementStates.crouching;
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);

            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
            movementStates = MovementStates.walking;
        }

    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
        if (isGrounded)
        {
            movementStates = MovementStates.walking;
            rb.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force);
        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * speed * 10f * airMultiplier, ForceMode.Force);
        }

    }

    private void SpeedControl()
    {
        Vector3 flatvel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limit velocity if needed
        if(flatvel.magnitude > speed)
        {
            Vector3 limitedVel = flatvel.normalized * speed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        //reset y velocity

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        canJump = true;
    }

    private void StateManager()
    {
        switch (movementStates)
        {
            case MovementStates.walking:
                speed = 5f;
                break;
            case MovementStates.sprinting:
                speed = 10f;
                break;
            case MovementStates.crouching:
                speed = 2f;
                break;
            case MovementStates.air:
                speed = 0f;
                break;

        }
    }


}
