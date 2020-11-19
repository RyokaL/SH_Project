using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSpawn : MonoBehaviour {
    public GameObject treasureChest;
    public float spawnChance = 10;

    private GameObject spawnedChest;
    void Update() {
        if(transform.parent != null) {
            if(Random.Range(0, 100) < spawnChance) {
                spawnedChest = Instantiate(treasureChest, transform.position, transform.rotation) as GameObject;
                spawnedChest.transform.parent = transform;
            }
            else {
                Destroy(gameObject);
            }
        }
    }
}