using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPCameraController : MonoBehaviour
{

    private const float MIN_Y_ANGLE = -75.0f;
    private const float MAX_Y_ANGLE = 75.0f;
    [Tooltip("Object the camera will follow")]
    public Transform follow;
    private float inpX;
    private float inpY;

    private ShmupCameraController swapCam;
     void Start()
    {
        swapCam = GetComponent<ShmupCameraController>();
    }

    void OnEnable() 
    {
       transform.eulerAngles = new Vector3(0,0,0);
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Return)) {
            swapCam.enabled = true;
            this.enabled = false;
        }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        inpX = Input.GetAxis("Mouse X_");
        inpY = Input.GetAxis("Mouse Y_");
        inpY = Mathf.Clamp(inpY, MIN_Y_ANGLE, MAX_Y_ANGLE);

    }

    //Want to run after player movement
    void LateUpdate()
    {
        transform.position = follow.position + new Vector3(0, 1, 0);
        Vector3 inpRotation = new Vector3(-inpY, inpX, 0);
        transform.eulerAngles += inpRotation * 100f * Time.deltaTime;
    }
}