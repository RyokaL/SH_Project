using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera cam;
    private bool FP = true;
    private FPCameraController fpCam;
    private ShmupCameraController shmupCam;

    // Start is called before the first frame update
    void Start()
    {
        shmupCam = cam.GetComponent<ShmupCameraController>();  
        fpCam = cam.GetComponent<FPCameraController>();
    }

    void OnTriggerEnter(Collider collider) {
        if(!collider.CompareTag("Player")) {
            return;
        }
        if(FP == true) {
            FP = false;
            fpCam.enabled = false;
            shmupCam.enabled = true;
        } 
        else {
            FP = true;
            fpCam.enabled = true;
            shmupCam.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
