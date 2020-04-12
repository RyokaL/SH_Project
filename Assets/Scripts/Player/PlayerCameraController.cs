using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    private const float MIN_Y_ANGLE = -30.0f;
    private const float MAX_Y_ANGLE = 75.0f;

    [Tooltip("Object the camera will follow")]
    public Transform follow;
    private float maxDistance = -5.0f;
    private float inpX;
    private float inpY;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnEnable()
    {
        transform.eulerAngles = new Vector3(25,0,0);
    }

    void Update() {
        inpX += Input.GetAxis("Mouse X");
        inpY += Input.GetAxis("Mouse Y");
        inpY = Mathf.Clamp(inpY, MIN_Y_ANGLE, MAX_Y_ANGLE);

    }

    //Want to run after player movement
    void LateUpdate()
    {
        Vector3 followDir = new Vector3(0,0,-maxDistance);
        Quaternion inpRotation = Quaternion.Euler(inpY, inpX, 0);

        //Needs changing for collisions as it will always be at max distance
        transform.position = (follow.position + inpRotation * followDir);

        transform.LookAt(follow);
    }
}