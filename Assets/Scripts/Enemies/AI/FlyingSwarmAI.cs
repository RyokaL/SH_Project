using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSwarmAI : AI
{   
    public Material colourChange;
    public GameObject matChange;
    public override bool nextUpdate(GameObject avatar, EnemyStats stats) {
        //Listen to swarm leader until death
        return true;
    }

    public override void onDeath(GameObject root) {
        Destroy(root);
    }
        
    public void makeLeader(List<FlyingSwarmAI> minions, EnemyStats stats) {
        FlyingSwarmLeaderAI leaderAI = GetComponent<FlyingSwarmLeaderAI>();
        Enemy enemyScript = GetComponent<Enemy>();
        enemyScript.stats = stats;
        matChange.GetComponent<MeshRenderer>().material = colourChange;
        GetComponent<MeshCollider>().isTrigger = true;
        leaderAI.setBoids(minions);
        enemyScript.ai = leaderAI;
    }
}
