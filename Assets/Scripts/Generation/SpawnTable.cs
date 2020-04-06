using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Generation", menuName = "Generation/SpawnTable")]

public class SpawnTable : ScriptableObject {
    public List<SpawnTableDetails> StarterTable;

    public List<SpawnTableDetails> EasyTable;

    public List<SpawnTableDetails> MediumTable;

    public List<SpawnTableDetails> HardTable;

    public List<SpawnTableDetails> VeryHardTable;

    public List<SpawnTableDetails> NightmareTable;

    public List<SpawnTableDetails> getTable(LevelStage diff) {
        switch(diff) {
            case LevelStage.Starter:
                return StarterTable;
            case LevelStage.Easy:
                return EasyTable;
            case LevelStage.Medium:
                return MediumTable;
            case LevelStage.Hard:
                return HardTable;
            case LevelStage.VeryHard:
                return VeryHardTable;
            case LevelStage.Nightmare:
                return NightmareTable;
            default:
                return null;
        }
    }
}