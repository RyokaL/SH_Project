using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject[] enemiesToSpawn;

    public float spawnChance = 0;
    public int maxEnemies = 1;

    public float spawnCooldown = 20;
    private int enemiesSpawned = 0;
    private int totalSpawned = 0;
    private float cooldownCount = 0;
    public bool disableAfterSpawned = false;
    public int spawnBeforeDisable = 1;

    private GameObject[] spawnedEnemies;
    // Start is called before the first frame update
    void Start()
    {
        spawnedEnemies = new GameObject[maxEnemies];
    }

    // Update is called once per frame
    void Update()
    {
        if(totalSpawned >= spawnBeforeDisable && disableAfterSpawned) {
            Destroy(gameObject);
        }
        bool arrayChanged = false;
        int firstIndex = maxEnemies;
        for(int i = 0; i < enemiesSpawned; i++) {
            if(spawnedEnemies[i] == null) {
                enemiesSpawned -= 1;
                arrayChanged = true;
                if(firstIndex < i) {
                    firstIndex = i;
                }
            }
        }

        if(arrayChanged) {
            for(int i = firstIndex; i < maxEnemies - 1; i++) {
                spawnedEnemies[i] = spawnedEnemies[i + 1];
            }
        }

        if(cooldownCount < spawnCooldown) {
            cooldownCount += Time.deltaTime;
        }
        else
        {
            cooldownCount -= spawnCooldown;
            if(enemiesSpawned < maxEnemies && spawnChance < (int)Random.Range(0, 100)) {
                spawnedEnemies[enemiesSpawned] = Instantiate(enemiesToSpawn[(int)Random.Range(0, enemiesToSpawn.Length)], transform.position, transform.rotation);
                enemiesSpawned += 1;
                totalSpawned += 1;
            }
        }
    }
}
