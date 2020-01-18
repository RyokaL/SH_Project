using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    void OnCollisionEnter(Collision collision) {
        Destroy(gameObject);
        //Generate explosion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
