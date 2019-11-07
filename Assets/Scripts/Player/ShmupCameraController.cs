using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShmupCameraController : MonoBehaviour
{
    public GameObject follow;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable() 
    {
        follow.GetComponent<PlayerControl>().toggleShmupMode(true);
        transform.eulerAngles = new Vector3(90,0,0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        transform.position = follow.transform.position + new Vector3(0, 10, 0);
    }
}
