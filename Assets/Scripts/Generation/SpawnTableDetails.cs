using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Generation", menuName = "Generation/SpawnTableEntry")]
public class SpawnTableDetails : ScriptableObject {
    public GameObject enemyType;
    public float spawnWeighting;
    public EnemyMaxStats maxStats;
}