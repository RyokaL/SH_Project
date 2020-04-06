using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public SpawnTable spawnTable;
    public int maxEnemies = 1;

    public float spawnCooldown = 0;
    
    public float cooldownCounter = 0;

    private int enemiesSpawned = 0;
    private int totalSpawned = 0;
    public bool disableAfterSpawned = false;
    public int spawnBeforeDisable = 1;

    private LevelStage lastSpawnStage = LevelStage.Starter;
    private List<SpawnTableDetails> completeSpawnTable;

    private GameObject[] spawnedEnemies;
    // Start is called before the first frame update
    void Start()
    {
        spawnedEnemies = new GameObject[maxEnemies];
        completeSpawnTable.AddRange(spawnTable.StarterTable);
    }

    // Update is called once per frame

    public GameObject spawnNewEnemy(LevelStage diff) {
        if(diff != lastSpawnStage) {
            lastSpawnStage = diff;
            completeSpawnTable.AddRange(spawnTable.getTable(diff));
        }

        if(cooldownCounter >= spawnCooldown && completeSpawnTable.Count > 0) {
            cooldownCounter = 0;

            int randIndex = Random.Range(0, completeSpawnTable.Count);
            //Need to properly assign enemy stats here:
            return Instantiate(completeSpawnTable[randIndex].enemyType, transform.position, completeSpawnTable[randIndex].enemyType.transform.rotation);
        }
        else {
            return null;
        }
    }

    void Update()
    {
       cooldownCounter += Time.deltaTime;
    }
}
