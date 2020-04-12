using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbCollect : MonoBehaviour
{
    bool start = false;
    // Start is called before the first frame update
    void Start()
    {
        start = true;
    }

    void OnTriggerEnter(Collider other) {
        if(start && other.gameObject.tag.Equals("Player")) {
            SpawnHandler diffIncrease = GameObject.Find("DungeonCreator").GetComponent<SpawnHandler>();
            diffIncrease.incDiff();
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
