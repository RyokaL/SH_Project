using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAICircle : AI {

    public float cooldown = 0;
    
    public override bool nextUpdate(GameObject avatar, EnemyStats stats) {
        bool action = false;
        Transform avTransform = avatar.transform;
        Collider[] playerInRange = Physics.OverlapSphere(avTransform.position, stats.sightRange);
        foreach(Collider c in playerInRange) {
            if(c.gameObject.tag == "Player") {
                action = true;
                cooldown += Time.deltaTime;
                if(cooldown >= (1 / stats.modifiers.fireRate)) {
                    Vector3 firePos = avTransform.position - avTransform.up.normalized;
                    stats.attack.fire(stats.modifiers, firePos, firePos + avTransform.right * 10);
                    stats.attack.fire(stats.modifiers, firePos, firePos - avTransform.right * 10);
                    stats.attack.fire(stats.modifiers, firePos, firePos + avTransform.forward * 10);
                    stats.attack.fire(stats.modifiers, firePos, firePos - avTransform.forward * 10);
                    cooldown -= (1 / stats.modifiers.fireRate);
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