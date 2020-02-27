using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPCameraController : MonoBehaviour
{

    private const float MIN_Y_ANGLE = -60.0f;
    private const float MAX_Y_ANGLE = 75.0f;
    [Tooltip("Object the camera will follow")]
    public GameObject follow;
    private float inpX;
    private float inpY;

    private Vector2 mouseInp = new Vector2(0,0);

    
     void Start()
    {
    }

    void OnEnable() 
    {
        follow.GetComponent<PlayerControl>().toggleShmupMode(false);
       //transform.eulerAngles = new Vector3(0,0,0);
       transform.eulerAngles = follow.transform.forward;
       mouseInp.x = follow.transform.forward.x;
       mouseInp.y = follow.transform.forward.y;
    }

    void Update() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        inpX = Input.GetAxis("Mouse X_");
        inpY = Input.GetAxis("Mouse Y_");
        mouseInp += new Vector2(inpX, inpY);
        mouseInp.y = Mathf.Clamp(mouseInp.y, MIN_Y_ANGLE, MAX_Y_ANGLE);
    }

    //Want to run after player movement
    void LateUpdate()
    {
        transform.position = follow.transform.position + new Vector3(0, 0.75f, 0);

        transform.localRotation = Quaternion.Euler(-mouseInp.y, mouseInp.x, 0);
    }
}