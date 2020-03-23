using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(gameObject.transform.position, gameObject.transform.localScale);
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag.Equals("Player")) {
            other.gameObject.GetComponent<CharacterController>().enabled = false;
            other.gameObject.transform.position = new Vector3(0, 1, 0);
            other.gameObject.GetComponent<CharacterController>().enabled = true;
        }
        else {
            AI checkAI = other.gameObject.GetComponent<AI>();
            if(checkAI && checkAI.enabled) {
                checkAI.onDeath(other.GetComponent<Enemy>().rootTransform.gameObject);
            } 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
