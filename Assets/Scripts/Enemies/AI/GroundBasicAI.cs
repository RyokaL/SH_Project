using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class GroundBasicAI : AI {

    public Vector3 lastKnownPos;
    private bool wasPlayerInRange = false;
    private EnemyStats stats;

    public override void nextUpdate(GameObject avatar, EnemyStats stats) {
        this.stats = stats;
        wasPlayerInRange = false;
        Transform avTransform = avatar.transform;
        Vector3 velocity = Vector3.zero;
        CharacterController avControl = GetComponent<CharacterController>();
        Collider[] playerInRange = Physics.OverlapSphere(avTransform.position, stats.sightRange);
        foreach(Collider c in playerInRange) {
            if(c.gameObject.tag == "Player") {
                wasPlayerInRange = true;
                if(Vector3.Angle(avTransform.position, c.transform.position) < stats.sightAngle) {
                    lastKnownPos = c.transform.position;
                    //avRigid.velocity = ((c.transform.position - avTransform.position).normalized * 3);
                    velocity = (c.transform.position - avTransform.position).normalized;
                }
            }
        }
        if(!wasPlayerInRange) {
            if((avTransform.position - lastKnownPos).magnitude <= 1) {
                velocity = Vector3.zero;
            }
            else {
                //avRigid.velocity = ((lastKnownPos - avTransform.position).normalized * 3);
                velocity = (lastKnownPos - avTransform.position).normalized;
            }
        }
        if(avControl.isGrounded) {
            velocity.y = -2.5f;
        }
        else {
            velocity.y = -9.81f;
        }
        transform.LookAt(avTransform.position + new Vector3(velocity.x, 0, velocity.z));
        avControl.Move(velocity * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<HealthControl>().takeDamage(stats.modifiers.damage);
        }
    }

    public override void onDeath(GameObject root) {
        Destroy(root);
    }
}