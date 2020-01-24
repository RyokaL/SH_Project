using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject[] enemiesToSpawn;

    public float spawnChance = 0;
    public int maxEnemies = 1;

    public int spawnCooldown = 20;
    private int enemiesSpawned = 0;

    private GameObject[] spawnedEnemies;
    // Start is called before the first frame update
    void Start()
    {
        spawnedEnemies = new GameObject[maxEnemies];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
