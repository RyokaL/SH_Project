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

    private Vector3 preForwardCam;
    private bool preMoving = false;
    private bool shmupMode = false;
    private Vector3 shmupDir = new Vector3(0,0,0);
    
    private int doubleJump = 1;
    private float inputH;
    private float inputV;
    private bool jump = false;
    private bool sprint = false;

    public int dashCharges = 2;
    private float dashCool = 0;
    private float DASH_COOLDOWN = 1;
    private bool dashing = false;
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        this.velocity = new Vector3(0,0,0);
    }

    void Update() {
        //To mitigate snap when switching camera
        Vector3 forwardCam = Vector3.ProjectOnPlane(playerCam.forward, Vector3.up).normalized;
        if(shmupMode) {
            if(!preForwardCam.Equals(Vector3.zero) && velocity.x != 0 && velocity.z != 0) {
                preMoving = true;
            }
            else {
                preMoving = false;
            }
        }
        else {
            preMoving = false;
        }

        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");
        if(!_controller.isGrounded && doubleJump < 1) {
            jump = false;
        }
        if(Input.GetButtonDown("Jump")) {
            jump = true;
        }
        if(Input.GetButtonDown("Sprint")) {
            // if(!sprint) {
            //     moveSpeed *= 2;
            //     sprint = true;
            // }
            // else {
            //     sprint = false;
            //     moveSpeed /= 2;
            // }
            if(dashCharges > 0) {
                Vector3 movDir;
                if(_controller.velocity.magnitude > 0) {
                    movDir = _controller.velocity.normalized;
                }
                else {
                    movDir = playerCam.forward.normalized;
                }
                movDir.y = 0;
                _controller.Move(movDir * 5);
                dashCharges -= 1;
                dashing = true;
            }
        }

        if(_controller.isGrounded) {
            dashing = false;
        }

        if(dashCharges < 2 && !dashing) {
            dashCool += Time.deltaTime;
            if(dashCharges == 0) {
                if(dashCool >= DASH_COOLDOWN * 2) {
                    dashCool -= DASH_COOLDOWN * 2;
                    dashCharges += 2;
                }
            }
            else if(dashCool >= DASH_COOLDOWN) {
                dashCool -= DASH_COOLDOWN;
                dashCharges += 1;
            }
        }
        shmupDir.x = Input.GetAxis("Mouse X_");
        shmupDir.z = Input.GetAxis("Mouse Y_");
    }

    void FixedUpdate()
    {
        transform.forward = playerCam.transform.forward;

        velocity.x = 0;
        velocity.z = 0;
        float preY = velocity.y;

        //This section means the player should move in the correct direction regardless of camera type 1st/3rd
        // //Get the forward and right facing vectors for the camera
        Vector3 forwardCam = Vector3.ProjectOnPlane(playerCam.forward, Vector3.up).normalized;
        Vector3 rightCam = Vector3.ProjectOnPlane(playerCam.right, Vector3.up).normalized;

        //To mitigate snap when switching camera
        if(shmupMode) {
            if(preMoving) {
                forwardCam = preForwardCam;
            }
            else {
                velocity.x = inputH * moveSpeed;
                velocity.z = inputV * moveSpeed;
            }
        }

        if(!forwardCam.Equals(Vector3.zero)) {
            //Multiply with input to get the angle at which to move
            velocity += (rightCam * inputH + forwardCam * inputV);
            velocity = velocity * moveSpeed;
        }
    
        preForwardCam = forwardCam;

        Vector3 face = new Vector3(velocity.x, 0, velocity.z);
        if(face != Vector3.zero && !shmupMode) {
            //transform.forward = face;
        }
        else {
            if(shmupMode && !shmupDir.Equals(Vector3.zero)) {
                transform.forward = shmupDir;
            }
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

    public void toggleShmupMode(bool toggle) {
        shmupMode = toggle;
    }

    public bool isShmup() {
        return shmupMode;
    }
}

