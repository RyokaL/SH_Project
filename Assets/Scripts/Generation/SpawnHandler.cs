using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum LevelStage {
    Starter,
    Easy,
    Medium,
    Hard,
    VeryHard,
    Nightmare
}

//Responsible for updating registered spawners and spawning world enemies
public class SpawnHandler : MonoBehaviour
{

    public float timeElapsed;
    public float timePoints;

    public SpawnTable worldSpawns;
    private List<SpawnTableDetails> completeSpawnTable;
    public LevelStage stage = LevelStage.Starter;

    public List<SpawnManager> spawnPoints = null;

    public GameObject player;

    public Text timerText;

    private int maxEnemies = 20;
    private int maxEnemiesMod = 20;

    private List<GameObject> enemiesSpawned;

    private Vector3 minBound;
    private Vector3 maxBound;

    void Start() {
        enemiesSpawned = new List<GameObject>();
        completeSpawnTable = new List<SpawnTableDetails>();
        updateSpawnTable();
    }

    public void registerSpawnPoints(List<SpawnManager> toAdd) {
        if(toAdd == null) {
            return;
        }
        if(spawnPoints == null) {
            spawnPoints = new List<SpawnManager>();
        }
        spawnPoints.AddRange(toAdd);
    }

    public void incDiff() {
        timeElapsed += 90;
    }

    public void registerBounds(Vector3 min, Vector3 max) {
        this.minBound = min;
        this.maxBound = max;
    }

    private void updateSpawnTable() {
        if(worldSpawns != null && worldSpawns.StarterTable != null) {
            completeSpawnTable.AddRange(worldSpawns.getTable(stage));
        }
    }

    void Update() {
        timeElapsed += Time.deltaTime;
        timePoints += Time.deltaTime;

        System.TimeSpan timeElapsedSpan = System.TimeSpan.FromSeconds(timeElapsed);

        string timeElapsedString = string.Format("{0:D2}:{1:D2}:{2:D2}", timeElapsedSpan.Hours, timeElapsedSpan.Minutes, timeElapsedSpan.Seconds);

        timerText.text = timeElapsedString;

        float timeMins = timeElapsed / 60;

        if(stage == LevelStage.Starter && timeMins >= 1) {
            stage = LevelStage.Easy;
            updateSpawnTable();
        }

        if(stage == LevelStage.Easy && timeMins >= 3) {
            stage = LevelStage.Medium;
            updateSpawnTable();
        }

        if(stage == LevelStage.Medium && timeMins >= 5) {
            stage = LevelStage.Hard;
            updateSpawnTable();
        }

        if(stage == LevelStage.Hard && timeMins >= 7) {
            stage = LevelStage.VeryHard;
            updateSpawnTable();
        }

        if(stage == LevelStage.VeryHard && timeMins >= 10) {
            stage = LevelStage.Nightmare;
            updateSpawnTable();
        }
        
        cleanEnemyList();
        spawnEnemiesNearPlayer();
    }

    private void cleanEnemyList() {
        enemiesSpawned.RemoveAll(x => x == null);

    }

    private void trySpawnFlying() {
        //Vector3 spawnPos = new Vector3(Random.Range(minBound.x, maxBound.x), Random.Range(minBound.y, maxBound.y), Random.Range(minBound.z, maxBound.z));
        Vector3 spawnPos = Random.insideUnitSphere * 100;
        if(spawnPos.y < 0) {
            spawnPos.y = -spawnPos.y;
        }
        spawnPos += player.transform.position;
        
        Collider[] test = Physics.OverlapSphere(spawnPos, 2.25f);
        if(test.Length == 0) {
            if(completeSpawnTable.Count > 0) {
                int randIndex = Random.Range(0, completeSpawnTable.Count);
                if(stage == LevelStage.Medium) {
                    randIndex = Random.Range(randIndex, completeSpawnTable.Count);
                }
                if(stage == LevelStage.VeryHard) {
                    randIndex = Random.Range(randIndex, completeSpawnTable.Count);
                }
                
                SpawnTableDetails entry = completeSpawnTable[randIndex];
                GameObject newEnemy = Instantiate(entry.enemyType, spawnPos, entry.enemyType.transform.rotation);
                newEnemy.GetComponent<Enemy>().stats = SpawnManager.calcStats(entry.maxStats, stage, timeElapsed);
                enemiesSpawned.Add(newEnemy);

                if(newEnemy.GetComponent<FlyingSwarmLeaderAI>()) {
                    newEnemy.GetComponent<FlyingSwarmLeaderAI>().spawnSwarm();
                }
            }
        }
    }

    private void spawnEnemiesNearPlayer() {
        spawnPoints.Sort((x, y) => Comparer<float>.Default.Compare((player.transform.position - x.gameObject.transform.position).magnitude, ((player.transform.position - y.gameObject.transform.position).magnitude)));

        foreach(SpawnManager s in spawnPoints) {
            if(enemiesSpawned.Count > maxEnemies) {
                break;
            }

            if(Random.Range(0, 100) < (10 * (int)stage)) {
                trySpawnFlying();
                continue;
            }

            GameObject newSpawn = s.spawnNewEnemy(stage, timeElapsed);
            if(newSpawn != null) {
                enemiesSpawned.Add(newSpawn);
                Debug.Log("Spawned");
            }
        }
    }

}