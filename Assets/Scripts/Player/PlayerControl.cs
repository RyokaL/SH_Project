using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Tooltip("Speed of the player's movement")]
    public float moveSpeed;
    [Tooltip("How high the player jumps")]
    public float jumpHeight;
    [Tooltip("Camera following this object so the player moves intuitively with the camera")]
    public Transform playerCam;
    private CharacterController _controller;
    private Vector3 velocity;
    
    private int doubleJump = 1;
    private float inputH;
    private float inputV;
    private bool jump = false;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        this.velocity = new Vector3(0,0,0);
    }

    void Update() {
        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");
        if(!_controller.isGrounded && doubleJump < 1) {
            jump = false;
        }
        if(Input.GetButtonDown("Jump")) {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        velocity.x = 0;
        velocity.z = 0;
        float preY = velocity.y;

        //This section means the player should move in the correct direction regardless of camera type 1st/3rd
        // //Get the forward and right facing vectors for the camera
        Vector3 forwardCam = Vector3.ProjectOnPlane(playerCam.forward, Vector3.up).normalized;
        Vector3 rightCam = Vector3.ProjectOnPlane(playerCam.right, Vector3.up).normalized;

        if(forwardCam.Equals(Vector3.zero)) {
            velocity.x = inputH * moveSpeed;
            velocity.z = inputV * moveSpeed;
        }
        else {
            //Multiply with input to get the angle at which to move
            velocity += (rightCam * inputH + forwardCam * inputV);
            velocity = velocity * moveSpeed;
        }


        Vector3 face = new Vector3(velocity.x, 0, velocity.z);
        if(face != Vector3.zero) {
            transform.forward = face;
        }
        
        //resets any change to y velocity from directional inp
        velocity.y = preY;
        if(_controller.isGrounded) {
            velocity.y = -5;
            doubleJump = 1;

            if(jump) {
                jump = false;
                velocity.y = 0;
                velocity.y += Mathf.Sqrt(jumpHeight * -2 * Physics.gravity.y);
            }
        }
        else {
            velocity.y += Physics.gravity.y * Time.deltaTime;
            if(jump && doubleJump > 0) {
                jump = false;
                velocity.y = 0;
                doubleJump -= 1;
                velocity.y += Mathf.Sqrt(jumpHeight * -2 * Physics.gravity.y);
            }
        }

        _controller.Move(velocity * Time.deltaTime);
    }
}

