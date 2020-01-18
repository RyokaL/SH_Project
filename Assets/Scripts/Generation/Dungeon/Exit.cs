using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public bool closed = false;
    public GameObject exitCover = null;
    public int voxelWidth;

    public void setClosed() {
        closed = true;
    }

    public bool isClosed() {
        return closed;
    }

    public GameObject getExitCover() {
        return exitCover;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
