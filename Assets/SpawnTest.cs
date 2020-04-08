using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTest : MonoBehaviour
{

    bool safe = true;
    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("Hi!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isSafe() {
        return safe;
    }

    void OnTriggerEnter(Collider other) {
        safe = false;
    }
}
