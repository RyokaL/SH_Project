using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Enemy AI", menuName = "Enemy AI/Turret")]
public class TurretAI : AI {

    private float cooldown = 0;
    
    public override void nextUpdate(GameObject avatar, EnemyStats stats) {
        Transform avTransform = avatar.transform;
        Collider[] playerInRange = Physics.OverlapSphere(avTransform.position, stats.sightRange);
        foreach(Collider c in playerInRange) {
            if(c.gameObject.tag == "Player") {
                if(Vector3.Angle(avTransform.position, c.transform.position) < stats.sightAngle) {
                    cooldown += Time.deltaTime;
                    if(cooldown >= (1 / stats.modifiers.fireRate)) {
                        stats.attack.fire(stats.modifiers, avTransform, c.transform);
                        cooldown -= (1 / stats.modifiers.fireRate);
                    }
                    break;
                } 
            }
        }
    }
}