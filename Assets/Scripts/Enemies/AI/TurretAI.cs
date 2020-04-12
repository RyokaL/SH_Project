using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretAI : AI {

    public float cooldown = 0;
    
    public override bool nextUpdate(GameObject avatar, EnemyStats stats) {
        bool action = false;
        Transform avTransform = avatar.transform;
        Collider[] playerInRange = Physics.OverlapSphere(avTransform.position, stats.sightRange);
        foreach(Collider c in playerInRange) {
            if(c.gameObject.tag == "Player") {
                if(Vector3.Angle(avTransform.position, c.transform.position) < stats.sightAngle) {
                    action = true;
                    cooldown += Time.deltaTime;
                    if(cooldown >= (1 / stats.modifiers.fireRate)) {
                        stats.attack.fire(stats.modifiers, avTransform.position, c.transform.position);
                        cooldown -= (1 / stats.modifiers.fireRate);
                    }
                    break;
                } 
            }
        }
        return action;
    }

    public override void onDeath(GameObject root) {
        Destroy(root);
    }
}