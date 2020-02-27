using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FlyingAI : AI {

    private float cooldown = 0;

    private float attackCooldown = 0;
    private float ATK_COOL = 2;
    private int atkCount = 0;
    private int MAX_ATK = 3;
    
    public override void nextUpdate(GameObject avatar, EnemyStats stats) {
        Transform avTransform = avatar.transform;
        Collider[] playerInRange = Physics.OverlapSphere(avTransform.position, stats.sightRange);
        foreach(Collider c in playerInRange) {
            if(c.gameObject.tag == "Player") {
                if(Vector3.Angle(avTransform.position, c.transform.position) < stats.sightAngle) {
                    //Cast a ray to see if player is visible
                    RaycastHit hit;
                    //We want to ignore other enemy colliders
                    LayerMask mask = ~(1 << 10);
                    Physics.Raycast(avTransform.position, (c.transform.position - avTransform.position), out hit, stats.sightRange, mask);
                    Debug.DrawRay(avTransform.position, (c.transform.position - avTransform.position));
                    //If the collider we hit is the player, we have a direct line
                    if(hit.collider.tag == "Player") {
                        avTransform.LookAt(c.transform);
                        Vector3 dir = (c.transform.position - avTransform.position);

                        if(avTransform.position.y <= c.transform.position.y + 5) {
                            avTransform.position = new Vector3(avTransform.position.x, c.transform.position.y + 5, avTransform.position.z);
                        }

                        if(dir.magnitude > 10) {
                            avTransform.position = avTransform.position + (dir.normalized * stats.speed * Time.fixedDeltaTime);
                        }
                    }

                    if(attackCooldown >= ATK_COOL) {
                        cooldown += Time.deltaTime;
                        if(cooldown >= (1 / stats.modifiers.fireRate) ) {
                            stats.attack.fire(stats.modifiers, avTransform, c.transform);
                            atkCount += 1;
                            cooldown -= (1 / stats.modifiers.fireRate);
                        }
                        if(atkCount >= MAX_ATK) {
                            atkCount = 0;
                            attackCooldown -= ATK_COOL;
                        }
                    }
                    else {
                        attackCooldown += Time.deltaTime;
                    }
                    break;
                } 
            }
            //If no player in range, patrol
        }
    }
}