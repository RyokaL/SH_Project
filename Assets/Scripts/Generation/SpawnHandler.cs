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
    //

    void Update() {
        timeElapsed += Time.deltaTime;
        timePoints += Time.deltaTime;

        float timeMins = timeElapsed / 60;

        if(stage == LevelStage.Starter && timeMins >= 5) {
            stage = LevelStage.Easy;
            //Update spawners;
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
    }

}