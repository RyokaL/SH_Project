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

    public LevelStage stage = LevelStage.Starter;

    public List<SpawnManager> spawnPoints = null;

    public GameObject player;

    public Text timerText;

    private int maxEnemies = 20;
    private int maxEnemiesMod = 20;

    private List<GameObject> enemiesSpawned;

    void Start() {
        enemiesSpawned = new List<GameObject>();
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

    void Update() {
        timeElapsed += Time.deltaTime;
        timePoints += Time.deltaTime;

        System.TimeSpan timeElapsedSpan = System.TimeSpan.FromSeconds(timeElapsed);

        string timeElapsedString = string.Format("{0:D2}:{1:D2}:{2:D2}", timeElapsedSpan.Hours, timeElapsedSpan.Minutes, timeElapsedSpan.Seconds);

        timerText.text = timeElapsedString;

        float timeMins = timeElapsed / 60;

        if(stage == LevelStage.Starter && timeMins >= 5) {
            stage = LevelStage.Easy;
        }

        if(stage == LevelStage.Easy && timeMins >= 15) {
            stage = LevelStage.Medium;
        }

        if(stage == LevelStage.Medium && timeMins >= 25) {
            stage = LevelStage.Hard;
        }

        if(stage == LevelStage.Hard && timeMins >= 35) {
            stage = LevelStage.VeryHard;
        }

        if(stage == LevelStage.VeryHard && timeMins >= 45) {
            stage = LevelStage.Nightmare;
        }
        
        cleanEnemyList();
        spawnEnemiesNearPlayer();
    }

    private void cleanEnemyList() {
        enemiesSpawned.RemoveAll(x => x == null);

    }

    private void spawnEnemiesNearPlayer() {
        //TODO: Implement this for Tuesday 07/04/2020
        spawnPoints.Sort((x, y) => Comparer<float>.Default.Compare((player.transform.position - x.gameObject.transform.position).magnitude, ((player.transform.position - y.gameObject.transform.position).magnitude)));

        foreach(SpawnManager s in spawnPoints) {
            if(enemiesSpawned.Count > maxEnemies) {
                break;
            }
            GameObject newSpawn = s.spawnNewEnemy(stage, timeElapsed);
            if(newSpawn != null) {
                enemiesSpawned.Add(newSpawn);
                Debug.Log("Spawned");
            }
        }
    }

}