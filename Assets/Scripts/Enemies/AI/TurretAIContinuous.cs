using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAIContinuous : AI {

    public float cooldown = 0;
    
    public override bool nextUpdate(GameObject avatar, EnemyStats stats) {
        Transform avTransform = avatar.transform;
        cooldown += Time.deltaTime;
        if(cooldown >= (1 / stats.modifiers.fireRate)) {
            Vector3 firePos = avTransform.position - avTransform.up.normalized;
            stats.attack.fire(stats.modifiers, firePos, firePos - avTransform.up * 10);
            cooldown -= (1 / stats.modifiers.fireRate);
        }
        return true;
    }

    public override void onDeath(GameObject root) {
        Destroy(root);
    }
}