using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class AI : MonoBehaviour {

    float timeSinceAction = 0;
    public void timeCheck(GameObject avatar, EnemyStats stats) {
        timeSinceAction += Time.deltaTime;
        if(nextUpdate(avatar, stats)) {
            timeSinceAction = 0;
        }

        if(timeSinceAction > 10) {
            onDeath(avatar);
        }
    }
    public abstract bool nextUpdate(GameObject avatar, EnemyStats stats);
    public abstract void onDeath(GameObject root);
}
