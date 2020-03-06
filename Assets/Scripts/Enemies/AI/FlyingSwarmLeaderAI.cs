using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSwarmLeaderAI : AI
{
    public List<FlyingSwarmAI> boids;
    private int nBoids;
    private Vector3 sumOfAllBoids;
    public override void nextUpdate(GameObject avatar, EnemyStats stats) {
        Rigidbody leader = avatar.GetComponent<Rigidbody>();
        leader.velocity = new Vector3(5, Random.Range(-2, 2), 0);

        boids.RemoveAll(f => f == null);

        Vector3 v1, v2, v3;
        nBoids = boids.Count;
        sumOfAllBoids = Vector3.zero;

        foreach(FlyingSwarmAI b in boids) {
            Rigidbody bR = b.GetComponent<Rigidbody>();
            sumOfAllBoids += bR.position;
        }

        foreach(FlyingSwarmAI b in boids) {
            Rigidbody boidRigid = b.GetComponent<Rigidbody>();
            GameObject boid = b.gameObject;

            //Rule 1: Boids always move towards the centre of mass of the other boids
            Vector3 centerOfOtherBoids = (sumOfAllBoids - boidRigid.position) / (nBoids - 1);
            v1 = (centerOfOtherBoids - b.transform.position) / 100;
            
            Vector3 r2 = Vector3.zero;
            Vector3 r3 = Vector3.zero;
            foreach(FlyingSwarmAI d in boids) {
                GameObject otherB = d.gameObject;
                Rigidbody otherBR = otherB.GetComponent<Rigidbody>();
                if(otherB != b) {
                    //Rule 2: Boids keep away from other boids and objects
                    if((otherBR.position - boidRigid.position).magnitude < 10) {
                        r2 = r2 - (otherBR.position - boidRigid.position);
                    }
                    //Rule 3: Match velocity of nearby boids
                    r3 = r3 + otherBR.velocity;
                }
            }

            Collider[] otherObjects = Physics.OverlapSphere(b.transform.position, 5);
            foreach(Collider c in otherObjects) {
                if(c.GetComponent<FlyingSwarmAI>()) {
                    continue;
                }
                GameObject otherB = c.gameObject;
                r2 = r2 - (otherB.transform.position - b.transform.position);
            }

            v2 = r2;
            v3 = (r3 / (nBoids - 1) - boidRigid.velocity) / 8;
            Vector3 v4 = (leader.position - boidRigid.position) / 100;

            boidRigid.velocity = boidRigid.velocity + (v1 + v2 + v3);
            if(boidRigid.velocity.magnitude > stats.speed) {
                boidRigid.velocity = boidRigid.velocity.normalized * stats.speed;
            }
            boid.transform.LookAt(boidRigid.position + boidRigid.velocity);
        }
    }
}
