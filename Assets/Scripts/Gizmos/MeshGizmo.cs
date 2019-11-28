using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGizmo : MonoBehaviour
{

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
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
