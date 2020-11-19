using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSwarmAI : AI
{   
    public Material colourChange;
    public GameObject matChange;

    private float cooldown;
    public override bool nextUpdate(GameObject avatar, EnemyStats stats) {
        Transform avTransform = avatar.transform;
        Collider[] playerInRange = Physics.OverlapSphere(avTransform.position, stats.sightRange);
        foreach(Collider c in playerInRange) {
            if(c.gameObject.tag == "Player") {
                 cooldown += Time.deltaTime;
                    if(cooldown >= (1 / stats.modifiers.fireRate)) {
                        stats.attack.fire(stats.modifiers, avTransform.position, c.transform.position);
                        cooldown -= (1 / stats.modifiers.fireRate);
                    }
            }
        }
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
