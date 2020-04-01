using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAIContinuous : AI {

    public float cooldown = 0;
    
    public override void nextUpdate(GameObject avatar, EnemyStats stats) {
        Transform avTransform = avatar.transform;
        cooldown += Time.deltaTime;
        if(cooldown >= (1 / stats.modifiers.fireRate)) {
            stats.attack.fire(stats.modifiers, avTransform.position, avTransform.position + avTransform.up * 10);
            cooldown -= (1 / stats.modifiers.fireRate);
        }
    }

    public override void onDeath(GameObject root) {
        Destroy(root);
    }
}