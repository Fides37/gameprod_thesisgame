using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class MouseCam : MonoBehaviour
{

    private float yRotation; // float for y rotation 
    private float xRotation;
    public Transform orientation; 
    public float sensX, sensY;

    public Transform playerBody;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // locks cursor in the center
        Cursor.visible = false; // makes cursor invisible

    }

    // Update is called once per frame
    void Update()
    {

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX; //when the mouse 
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;

        //xRotation = Mathf.Clamp(xRotation, -90f, 90f); //restricts cam rotation to only 180 degrees

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //rotate cam and orientation

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);


        playerBody.Rotate(Vector3.up * mouseX); 

    }

  


}
