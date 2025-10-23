using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private float speed = 5f; //float for speed
    private float jumpSpeed = 5f; //float for how fast player jumps
    private float gravity = -20f;

    private float horizontalInput; //horizontal axis (forward/backwards - z axis)
    private float verticalInput; //vertical axis (left/right - x axis)


    Vector3 moveVelocity; //foward velocity

    public CharacterController characterController; //reference character controller


    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();

    }

    private void PlayerInput()
    {
        horizontalInput = Input.GetAxis("Horizontal"); // when pressing a & d

        verticalInput = Input.GetAxis("Vertical"); //when pressing w & s

        if (characterController.isGrounded)
        {
            moveVelocity = transform.forward * speed * verticalInput + transform.right * speed * horizontalInput;
            

            if (Input.GetKey(KeyCode.Space))
            {
                moveVelocity.y = jumpSpeed;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        moveVelocity.y += gravity * Time.deltaTime;

        characterController.Move(moveVelocity * Time.deltaTime);
 
    }
   


}
