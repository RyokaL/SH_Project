using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShmupCameraController : MonoBehaviour
{
    public Transform follow;
    private FPCameraController swapCam;
    // Start is called before the first frame update
    void Start()
    {
        swapCam = GetComponent<FPCameraController>();
    }

    void OnEnable() 
    {
        transform.eulerAngles = new Vector3(90,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)) {
            swapCam.enabled = true;
            this.enabled = false;
        }
    }

    void LateUpdate()
    {
        transform.position = follow.transform.position + new Vector3(0, 10, 0);
    }
}
