using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public PlayerMovement movementJoystick;
    public float playerSpeed;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(movementJoystick.joystickVec.y != 0)
        {
            rb.velocity = new Vector3(movementJoystick.joystickVec.x * playerSpeed, rb.velocity.y, movementJoystick.joystickVec.y * playerSpeed);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
}
